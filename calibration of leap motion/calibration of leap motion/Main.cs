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

namespace calibration_of_leap_motion
{
    public partial class Main : Form, ILeapEventDelegate
    {
        private Controller controller;
        private LeapEventListener listener;
        private Bitmap bitmapLeft, bitmapRight;
        private Rectangle lockArea;

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

        private static Map<String,Mat> initDistortionMat(Leap.Image image) 
        {
            Mat distortionX, distortionY;
            distortionX = new Mat(image.Height,image.Width,Emgu.CV.CvEnum.DepthType.Cv32F, 1);
            distortionY = new Mat(image.Height,image.Width,Emgu.CV.CvEnum.DepthType.Cv32F, 1);
            for(int y = 0; y < image.Height; ++y) {
                for(int x = 0; x < image.Width;++x) 
                {
                    Vector input = new Vector((float)x/image.Width, (float)y/image.Height, 0);
                    //Normalize from pixel xy to range [0..1]
                    //Convert from normalized [0..1] to slope [-4..4]
                    input.x = ((input.x - image.RayOffsetX) / image.RayOffsetX);
                    input.y = ((input.y - image.RayOffsetY) / image.RayOffsetY);
                    //Use slope to get coordinates of point in image.Data containing the brightness for this target pixel
                    Vector pixel = image.Warp(input);
                    if(pixel.x >= 0 && pixel.x < image.Width && pixel.y >= 0 && pixel.y < image.Height)
                    {
                        int dataIndex = (int)(Math.Floor (pixel.y) * image.Width + Math.Floor (pixel.x)); //xy to buffer index
                        byte brightness = image.Data [dataIndex];
                        
                        distortionX[y,x,0] = pixel.x;
                        distortionX.put(y, x, pixel.getX());
                        distortionY.put(y, x, pixel.y);
                    } 
                    else {
                        distortionX.put(y, x, -1);
                        distortionY.put(y, x, -1);    
                    }
                }
            }
            
            Map<String, Mat> distortionMats = new HashMap<>();
            distortionMats.put("x", tempX);
            distortionMats.put("y", tempY);
            
            return distortionMats;
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

        private void newFrameHandler(Frame frame)
        {
            //The image at index 0 is the left camera; the image at index 1 is the right camera
            Leap.Image imageLeft = frame.Images[0];
            Leap.Image imageRight = frame.Images[1];
            if (imageLeft.IsValid)
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
                Mat opencvImg = new Mat(imageLeft.Height, imageLeft.Width, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                opencvImg.SetTo(imageLeft.Data);
                picboxLeapLeft.Image = opencvImg.ToImage<Gray, Byte>().ToBitmap();
                
            }
            if (imageRight.IsValid)
            {
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
    }

}
