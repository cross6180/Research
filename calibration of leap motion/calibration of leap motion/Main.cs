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
                BitmapData bitmapData = bitmapLeft.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = imageLeft.Data;
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, imageLeft.Width * imageLeft.Height);
                bitmapLeft.UnlockBits(bitmapData);
                picboxLeapLeft.Image = bitmapLeft;
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
