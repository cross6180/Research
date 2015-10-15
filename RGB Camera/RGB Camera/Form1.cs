using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace RGB_Camera
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private Bitmap image;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FilterInfoCollection Captures the video devices connected to the machine
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            foreach (FilterInfo device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
            }

            videoSource = new VideoCaptureDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else 
            {
                //VideoCaptureDevice is used to Capture Stream from a FilterInfoCollection object or a cam specified to be exact.
                videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                //set resolution width x height : [0-7] 640 x 480, 160 x 120, 176 x 144, 320 x 240, 352 x 288, 800 x 600, 1280 x 720, 1920 x 1080
                videoSource.VideoResolution = videoSource.VideoCapabilities[7];
           
                //set new frame event handler
                videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                videoSource.Start();
            }
        }

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //if don't dispose the oldimage the ram memory will increases and after that the programm will crash 
            Image oldImage = image;
            image = (Bitmap)eventArgs.Frame.Clone();
            //image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // create filter - rotate for 90 degrees keeping original image size
            RotateBilinear filter = new RotateBilinear(90, true);
            // apply the filter
            //Bitmap newImage = new Bitmap(image.Height, image.Width);
            Bitmap newImage = filter.Apply(image);
            pictureBox1.Image = newImage;

            if (oldImage != null)
            {
                oldImage.Dispose();
                //newImage.Dispose();
            }          
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap img = (Bitmap) pictureBox1.Image.Clone();
            img.Save(@"D:\Research\Image\Test4.bmp", ImageFormat.Bmp);
            img.Dispose();
            img = null;
        }
    }
}
