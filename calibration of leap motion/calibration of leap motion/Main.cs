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
            InitializeBitmap(bitmapLeft);
            InitializeBitmap(bitmapRight);        
        }

        private void InitializeBitmap(Bitmap bitmap)
        {
            bitmap = new Bitmap(640, 240, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            //set palette
            ColorPalette grayscale = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
            }
            bitmap.Palette = grayscale;
            lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        }

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

        void connectHandler()
        {
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.Config.SetFloat("Gesture.Circle.MinRadius", 40.0f);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        }

        void newFrameHandler(Frame frame)
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
            }
            if (imageRight.IsValid)
            {
                BitmapData bitmapData = bitmapRight.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = imageRight.Data;
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, imageRight.Width * imageRight.Height);
                bitmapRight.UnlockBits(bitmapData);
            }
           
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
