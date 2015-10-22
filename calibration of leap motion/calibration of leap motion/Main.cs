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
using AForge.Video;
using AForge.Video.DirectShow;

namespace calibration_of_leap_motion
{
    public partial class Main : Form, ILeapEventDelegate
    {
        private Controller controller;
        private LeapEventListener listener;
        private Bitmap bitmapLeft, bitmapRight;
        private Rectangle lockArea;
        //private Mat dx_L, dy_L, dx_R, dy_R, mat;
        private Leap.Image imageR, imageL;
        //for rgb camera
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private Bitmap imageRGB;

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

        /*private void initDistortionMat(Leap.Image image, Mat dx, Mat dy)
        {
            //Draw the undistorted image using the warp() function
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
                }
            }
        }*/


        private void connectHandler()
        {
            //If using gestures, enable them:
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.Config.SetFloat("Gesture.Circle.MinRadius", 40.0f);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            //if using IR camera image, enablr this:
            this.controller.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
        }

        //Boolean initialize = true;
        private void newFrameHandler(Frame frame)
        {
            //The image at index 0 is the left camera; the image at index 1 is the right camera
            Leap.Image imageLeft = frame.Images[0];
            Leap.Image imageRight = frame.Images[1];
            if (imageLeft.IsValid)
            {
                imageL = frame.Images[0];
                //將 Bitmap 鎖定在系統記憶體內，BitmapData 指定 Bitmap 的屬性，例如大小、像素格式、像素資料在記憶體中的起始位址，以及每條掃描線 (分散) 的長度。
                BitmapData bitmapData = bitmapLeft.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = imageLeft.Data;
                //byte[] destination = new byte[640 * 240];

                ////define needed variables outside the inner loop
                //double calibrationX, calibrationY;
                //double weightX, weightY;
                //double dX, dX1, dX2, dX3, dX4;
                //double dY, dY1, dY2, dY3, dY4;
                //int x1, x2, y1, y2;
                //int distortionWidth = imageLeft.DistortionWidth;

                //float[] distortion_buffer = imageLeft.Distortion;

                //for (int i = 0; i < 640; i++)
                //{
                //    for (int j = 0; j < 240; j++)
                //    {
                //        //Calculate the position in the calibration map (still with a fractional part)
                //        calibrationX = 63.0f * (double)i / 640;
                //        calibrationY = 62.0f * (1.0f - (double)j / 240); // The y origin is at the bottom
                //        weightX = calibrationX - (double)((int)calibrationX);
                //        weightY = calibrationY - (double)((int)calibrationY);
                //        //Get the x,y coordinates of the closest calibration map points to the target pixel
                //        x1 = (int)calibrationX; //Note truncation to int
                //        y1 = (int)calibrationY;
                //        x2 = x1 + 1;
                //        y2 = y1 + 1;

                //        //Look up the x and y values for the 4 calibration map points around the target
                //        dX1 = distortion_buffer[x1 * 2 + y1 * distortionWidth];
                //        dX2 = distortion_buffer[x2 * 2 + y1 * distortionWidth];
                //        dX3 = distortion_buffer[x1 * 2 + y2 * distortionWidth];
                //        dX4 = distortion_buffer[x2 * 2 + y2 * distortionWidth];
                //        dY1 = distortion_buffer[x1 * 2 + y1 * distortionWidth + 1];
                //        dY2 = distortion_buffer[x2 * 2 + y1 * distortionWidth + 1];
                //        dY3 = distortion_buffer[x1 * 2 + y2 * distortionWidth + 1];
                //        dY4 = distortion_buffer[x2 * 2 + y2 * distortionWidth + 1];

                //        //Bilinear interpolation of the looked-up values:
                //        // X value
                //        dX = dX1 * (1 - weightX) * (1 - weightY) +
                //                dX2 * weightX * (1 - weightY) +
                //                dX3 * (1 - weightX) * weightY +
                //                dX4 * weightX * weightY;

                //        // Y value
                //        dY = dY1 * (1 - weightX) * (1 - weightY) +
                //                dY2 * weightX * (1 - weightY) +
                //                dY3 * (1 - weightX) * weightY +
                //                dY4 * weightX * weightY;

                //        // Reject points outside the range [0..1]
                //        if ((dX >= 0) && (dX <= 1) && (dY >= 0) && (dY <= 1))
                //        {
                //            //look up the brightness value for the target pixel
                //            if ((rawImageData[(int)((dX * imageLeft.Width) + (int)(dY * imageLeft.Height) * imageLeft.Width)]) < 255)
                //                destination[i + (int)(640 * j)] = (byte)(rawImageData[(int)((dX * imageLeft.Width) + (int)(dY * imageLeft.Height) * imageLeft.Width)]);
                //            else
                //                destination[i + (int)(640 * j)] = 255;
                //        }
                //    }
                //}
                
                //Copy the image values back to the bitmap. bitmapData.Scan0 is the address of the first line. 
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, imageLeft.Width * imageLeft.Height);
                //從系統記憶體解除鎖定這個 Bitmap
                bitmapLeft.UnlockBits(bitmapData);
                //show image in the picturebox
                picboxLeapLeft.Image = bitmapLeft;
                /*
                if (initialize)
                {
                    dx_L = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv32F, 1);
                    dy_L = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv32F, 1);
                    this.initDistortionMat(imageLeft, dx_L, dy_L);
                    initialize = false;
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
                    /*Mat opencvImg = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    opencvImg.SetTo(imageLeft.Data);

                    //remap to undistorted image
                    Mat undistortedImg = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    Mat dx = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    Mat dy = new Mat(imageLeft.Height, imageLeft.Width, DepthType.Cv8U, 1);
                    CvInvoke.ConvertMaps(dx_L, dy_L, dx, dy, DepthType.Cv8U, 1);
                    CvInvoke.Remap(opencvImg, undistortedImg, dx, dy, Inter.Linear);
   
                    picboxLeapLeft.Image = undistortedImg.ToImage<Gray, Byte>().ToBitmap();
                  
                }*/
                
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
                        targetBitmap.SetPixel((int)x, (int)y, Color.Black); //Display invalid pixels as red
                    }
                }
            }
            return targetBitmap;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //FilterInfoCollection Captures the video devices connected to the machine
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice();
            //VideoCaptureDevice is used to Capture Stream from a FilterInfoCollection object or a cam specified to be exact.
            videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
            //set resolution width x height : [0-7] 640 x 480, 160 x 120, 176 x 144, 320 x 240, 352 x 288, 800 x 600, 1280 x 720, 1920 x 1080
            videoSource.VideoResolution = videoSource.VideoCapabilities[7];

            //set new frame event handler
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            videoSource.Start();
        }

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //if don't dispose the oldimage the ram memory will increases and after that the programm will crash 
            System.Drawing.Image oldImage = imageRGB;
            imageRGB = (Bitmap)eventArgs.Frame.Clone();

            //rotate for 90 degrees keeping original image size
            //imageRGB.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picboxRGB.Image = imageRGB;

            if (oldImage != null)
            {
                oldImage.Dispose();
            }
          
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }

        private int cnt = 1; 
        private void btnSavaImage_Click(object sender, EventArgs e)
        {
            Bitmap distortedL = (Bitmap) picboxLeapLeft.Image.Clone();
            Bitmap distortedR = (Bitmap) picboxLeapRight.Image.Clone();
            Bitmap bRight = UndistortImage(imageR);
            picboxUndistortedR.Image = (Bitmap) bRight.Clone();
            Bitmap bLeft = UndistortImage(imageL);
            picboxUndistortedL.Image = (Bitmap) bLeft.Clone();
            Bitmap imgRGB = (Bitmap)picboxRGB.Image.Clone();
            picboxRGBCapture.Image = (Bitmap) imgRGB.Clone();

            bRight.Save(@"D:\Research\Image\Undistorted Leap Right\leapRight" + cnt.ToString() + ".bmp", ImageFormat.Bmp);
            bLeft.Save(@"D:\Research\Image\Undistorted Leap Left\leapLeft" + cnt.ToString() + ".bmp", ImageFormat.Bmp);
            imgRGB.Save(@"D:\Research\Image\RGB\rgb" + cnt.ToString() + ".bmp", ImageFormat.Bmp);
            distortedL.Save(@"D:\Research\Image\LeapLeft\distortedLeapLeft" + cnt.ToString() + ".bmp", ImageFormat.Bmp);
            distortedR.Save(@"D:\Research\Image\LeapRight\distortedLeapRight" + cnt.ToString() + ".bmp", ImageFormat.Bmp);

            bRight.Dispose(); bLeft.Dispose(); imgRGB.Dispose(); distortedL.Dispose(); distortedR.Dispose();
            bRight = null; bLeft = null; imgRGB = null; distortedL = null; distortedR = null;

            cnt++;
        }
    }

}
