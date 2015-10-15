using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leap;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;

namespace calibration_of_leap_motion
{
    public partial class Main : Form, ILeapEventDelegate
    {
        private Controller controller;
        private LeapEventListener listener;
        private Bitmap bitmapLeft, bitmapRight;
        private Rectangle lockArea;
        private Mat dx_L, dy_L, dx_R, dy_R, mat;
        private Leap.Image imageR;

        public Main()
        {
            InitializeComponent();
            this.controller = new Controller();
            this.listener = new LeapEventListener(this);
            controller.AddListener(listener);
            bitmapLeft = new Bitmap(640, 240, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            bitmapRight = new Bitmap(640, 240, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            InitializeBitmap(bitmapLeft);
            InitializeBitmap(bitmapRight);
        }

        private void InitializeBitmap(Bitmap bitmap)
        {           
            //set palette
            ColorPalette grayscale = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
            }
            bitmap.Palette = grayscale;
            lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        }

        //this allows any class implementing this interface to act as a delegate for the Leap Motion events.
        delegate void LeapEventDelegate(string EventName);
        public void LeapEventNotification(string EventName)
        {
            if (!this.InvokeRequired)
            {
                switch (EventName)
                {
                    case "onInit":
                        break;
                    case "onConnect":
                        this.connectHandler();
                        break;
                    case "onFrame":
                        if(!this.Disposing) 
                            this.newFrameHandler(this.controller.Frame());
                        break;
                }
            }
            else
            {
                BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }

        private void initDistortionMat(Leap.Image image, Mat dx, Mat dy)
        {
            //Draw the undistorted image using the warp() function
            //Rectangle lockBounds = new Rectangle(0, 0, image.Width, image.Height);
            //Bitmap targetBitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //Iterate over target image pixels, converting xy to ray slope
            for (float y = 0; y < image.Height; y++)
            {
                for (float x = 0; x < image.Width; x++)
                {
                    //Normalize from pixel xy to range [0..1]
                    Vector input = new Vector(x / image.Width, y / image.Height, 0);

                    //Convert from normalized [0..1] to slope [-4..4]
                    input.x = ((input.x - image.RayOffsetX) / image.RayScaleX);
                    input.y = ((input.y - image.RayOffsetY) / image.RayScaleY);

                    //Use slope to get coordinates of point in image.Data containing the brightness for this target pixel
                    Vector pixel = image.Warp(input);
                    if (pixel.x >= 0 && pixel.x < image.Width && pixel.y >= 0 && pixel.y < image.Height)
                    {
                        //int dataIndex = (int)(Math.Floor(pixel.y) * image.Width + Math.Floor(pixel.x)); //xy to buffer index
                        //byte brightness = image.Data[dataIndex];
                        //targetBitmap.SetPixel((int)x, (int)y, Color.FromArgb(brightness, brightness, brightness));

                        MatExtension.SetDoubleValue(dx, (int)y, (int)x, pixel.x);
                        MatExtension.SetDoubleValue(dy, (int)y, (int)x, pixel.y);
                    }
                    else
                    {
                        //targetBitmap.SetPixel((int)x, (int)y, Color.Red); //Display invalid pixels as red
                        MatExtension.SetDoubleValue(dx, (int)y, (int)x, -1);
                        MatExtension.SetDoubleValue(dy, (int)y, (int)x, -1);

                    }
                    ////Lock the bitmap's bits.  
                    // Rectangle rect = new Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height);
                    // BitmapData bmpData = targetBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, targetBitmap.PixelFormat);

                    // //data = scan0 is a pointer to our memory block.
                    // IntPtr data = bmpData.Scan0;
                    // //step = stride = amount of bytes for a single line of the image
                    // int step = bmpData.Stride;
                    // // So you can try to get you Mat instance like this:
                    // distortionMat = new Mat(targetBitmap.Height, targetBitmap.Width, DepthType.Cv32F, 1, data, step);
                    // //Unlock the bits.
                    // targetBitmap.UnlockBits(bmpData);
                }
            }
        }

        private void connectHandler()
        {
            //If using gestures, enable them:
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.Config.SetFloat("Gesture.Circle.MinRadius", 40.0f);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            //if using IR camera image, enablr this:
            this.controller.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
        }

        int cnt = 0;
        //Boolean initialize = true;
        private void newFrameHandler(Frame frame)
        {
            //The image at index 0 is the left camera; the image at index 1 is the right camera
            Leap.Image imageLeft = frame.Images[0];
            Leap.Image imageRight = frame.Images[1];
            if (imageLeft.IsValid)
            {
                if (cnt < 5)
                {
                    dx_L = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv32F, 1);
                    dy_L = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv32F, 1);
                    //mat = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv32F, 1);
                    this.initDistortionMat(imageLeft, dx_L, dy_L);
                    cnt++;
                }
                else
                {
                    //將LeapMotion影像資料轉成OpenCV的影像格式
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /*mat(rows, columns, type, channels)
                     **type is of the type CvEnum.DepthType, which is the depth of the image, you can pass CvEnum.DepthType.Cv32F which stands for 32bit depth images, 
                       other possible values are of the form CvEnum.DepthType.Cv{x}{t}, where {x} is any value of the set {8,16,32,64} and {t} can be Sfor Single or F for Float.
                     **channels depend on the type of image 
                     **Depthtype :
                       Member name	Value	Description
                       Default	      -1	default
                       Cv8U   	       0	Byte
                       Cv8S	           1	SByte
                       Cv16U	       2	UInt16
                       Cv16S  	       3	Int16
                       Cv32S	       4	Int32
                       Cv32F  	       5	float
                       Cv64F	       6	double
                     */
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Mat opencvImg = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    opencvImg.SetTo(imageLeft.Data);

                    //remap to undistorted image
                    Mat undistortedImg = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    //Mat emptymap = mat = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv32F, 1);
                    Mat dx = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    Mat dy = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    CvInvoke.ConvertMaps(dx_L, dy_L, dx, dy, DepthType.Cv8U, 1);
                    //CvInvoke.ConvertMaps(mat, emptymap, dx, dy, DepthType.Cv8U, 1);
                    CvInvoke.Remap(opencvImg, undistortedImg, dx, dy, Inter.Linear);

                    picboxLeapLeft.Image = undistortedImg.ToImage<Gray, Byte>().ToBitmap();
                }
                
            }
            if (imageRight.IsValid)
            {
                imageR = frame.Images[1];
                //將 Bitmap 鎖定在系統記憶體內，BitmapData 指定 Bitmap 的屬性，例如大小、像素格式、像素資料在記憶體中的起始位址，以及每條掃描線 (分散) 的長度。
                BitmapData bitmapData = bitmapRight.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = imageRight.Data;
                //Copy the image values back to the bitmap. bitmapData.Scan0 is the address of the first line. 
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, imageRight.Width * imageRight.Height);
                //從系統記憶體解除鎖定這個 Bitmap
                bitmapRight.UnlockBits(bitmapData);
                //show image in the picturebox
                picboxLeapRight.Image = bitmapRight;
            }
            //get leap motion camera current frame per second
            float fps = frame.CurrentFramesPerSecond;
            txtLeapFPS.Text = fps.ToString();         
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }
                    this.controller.RemoveListener(this.listener);
                    this.controller.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            Bitmap b = UndistortImage(imageR);
            picboxUndistorted.Image = b;

        }

        private Bitmap UndistortImage(Leap.Image image)
        {
            //Draw the undistorted image using the warp() function
            Rectangle lockBounds = new Rectangle(0, 0, image.Width, image.Height);
            Bitmap targetBitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //Iterate over target image pixels, converting xy to ray slope
            for (float y = 0; y < image.Height; y++)
            {
                for (float x = 0; x < image.Width; x++)
                {
                    //Normalize from pixel xy to range [0..1]
                    Vector input = new Vector(x / image.Width, y / image.Height, 0);

                    //Convert from normalized [0..1] to slope [-4..4]
                    input.x = (input.x - image.RayOffsetX) / image.RayScaleX;
                    input.y = (input.y - image.RayOffsetY) / image.RayScaleY;

                    //Use slope to get coordinates of point in image.Data containing the brightness for this target pixel
                    Vector pixel = image.Warp(input);

                    if (pixel.x >= 0 && pixel.x < image.Width && pixel.y >= 0 && pixel.y < image.Height)
                    {
                        int dataIndex = (int)(Math.Floor(pixel.y) * image.Width + Math.Floor(pixel.x)); //xy to buffer index
                        byte brightness = image.Data[dataIndex];
                        targetBitmap.SetPixel((int)x, (int)y, Color.FromArgb(brightness, brightness, brightness));
                    }
                    else
                    {
                        targetBitmap.SetPixel((int)x, (int)y, Color.Red); //Display invalid pixels as red
                    }
                }
            }
            return targetBitmap;
        }
    }

}
