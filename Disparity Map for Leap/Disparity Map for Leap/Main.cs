using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;

namespace Disparity_Map_for_Leap
{
    public partial class MainForm : Form
    {
        private Mat imageLeft, imageRight;
        private Mat disparity, disp8;
        private StereoSGBM sgbm;
        //private StereoBM sbm;
        private int minDisparity, numDisparities, SADWindowSize, P1, P2, disp12MaxDiff, preFilterCap, uniquenessRatio, speckleWindowSize, speckleRange;
        private Image<Gray, Byte> IRblendImage;

        public MainForm()
        {
            InitializeComponent();
            ReadImage();
            SetSGBMParameter();
            picboxLeft.Image = imageLeft.Bitmap;
            picbocRight.Image = imageRight.Bitmap;
            picboxDisp.Image = disp8.Bitmap;
        }

        private void ReadImage()
        {
            imageLeft = CvInvoke.Imread("D:/Research/Image/disparity map/dm_LeapLeft1_rect.bmp", Emgu.CV.CvEnum.LoadImageType.Grayscale);
            imageRight = CvInvoke.Imread("D:/Research/Image/disparity map/dm_LeapRight1_rect.bmp", Emgu.CV.CvEnum.LoadImageType.Grayscale);
            disparity = new Mat();
            disp8 = new Mat();
        }

        private void SetSGBMParameter()
        {
            minDisparity = TBminDisparity.Value;        //Minimum possible disparity value. Normally, it is zero but sometimes rectification algorithms can shift images, so this parameter needs to be adjusted accordingly.
                                                        //Default is zero, should be set to a negative value, if negative disparities are possible (depends on the angle between the cameras views and the distance of the measured object to the cameras).
            numDisparities = TBnumDisparities.Value;    //Maximum disparity minus minimum disparity. The value is always greater than zero. In the current implementation, this parameter must be divisible by 16.
            SADWindowSize = TBSADWindowSize.Value;      //(= blockSize) Matched block size. It must be an odd number >=1 . Normally, it should be somewhere in the 3..11 range. Use 0 for default.
            P1 = TBp1.Value;                            //The first parameter controlling the disparity smoothness. It is the penalty on the disparity change by plus or minus 1 between neighbor pixels. 
                                                        //Reasonably good value is 8*number_of_image_channels*SADWindowSize*SADWindowSize. Use 0 for default
            P2 = TBp2.Value;                            //The second parameter controlling the disparity smoothness. It is the penalty on the disparity change by more than 1 between neighbor pixels. 
                                                        //The algorithm requires p2 > p1. Reasonably good value is 32*number_of_image_channels*SADWindowSize*SADWindowSize. Use 0 for default
            disp12MaxDiff = TBdisp12MaxDiff.Value;      //Maximum allowed difference (in integer pixel units) in the left-right disparity check. Set it to a non-positive value to disable the check.
            preFilterCap = TBpreFilterCap.Value;        //Truncation value for the prefiltered image pixels. The algorithm first computes x-derivative at each pixel and clips its value by [-preFilterCap, preFilterCap] interval. 
                                                        //The result values are passed to the Birchfield-Tomasi pixel cost function.
            uniquenessRatio = TBuniquenessRatio.Value;  //Margin in percentage by which the best (minimum) computed cost function value should “win” the second best value to consider the found match correct. 
                                                        //Normally, a value within the 5-15 range is good enough.
            speckleWindowSize = TBspeckleWindowSize.Value;   //Maximum size of smooth disparity regions to consider their noise speckles and invalidate. Set it to 0 to disable speckle filtering. Otherwise, set it somewhere in the 50-200 range
            speckleRange = TBspeckleRange.Value;        //Maximum disparity variation within each connected component. If you do speckle filtering, set the parameter to a positive value, 
                                                        //it will be implicitly multiplied by 16. Normally, 1 or 2 is good enough.
            StereoSGBM.Mode mode = StereoSGBM.Mode.SGBM;                           
                                                        //mode (Optional) : Set it to HH to run the full-scale two-pass dynamic programming algorithm. It will consume O(W*H*numDisparities) bytes, 
                                                        //which is large for 640x480 stereo and huge for HD-size pictures. By default, it is set to false.

            sgbm = new StereoSGBM(minDisparity, numDisparities, SADWindowSize, P1, P2, disp12MaxDiff, preFilterCap, uniquenessRatio, speckleWindowSize, speckleRange, mode);

            //Computes disparity map for the specified stereo pair
            sgbm.Compute(imageLeft, imageRight, disparity);
            //Since Disparity will be either CV_16S or CV_32F, it needs to be compressed and normalized to CV_8U
            CvInvoke.Normalize(disparity, disp8, 0, 255, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv8U);
        }

/*        private void SetSBMParameter()
        {
            sbm = new StereoBM();
            Emgu.CV.Structure.MCvStereoBMState state = new Emgu.CV.Structure.MCvStereoBMState();
            state.SADWindowSize = 9;
            state.preFilterType = 1;
            state.preFilterSize = 5;
            state.preFilterCap = 39;
            state.minDisparity = 0;
            state.numberOfDisparities = 112;
            state.textureThreshold = 607;
            state.uniquenessRatio = 8;
            state.speckleRange = 8;
            state.speckleWindowSize = 0;

        }*/

        private void TBminDisparity_Scroll(object sender, EventArgs e)
        {
            labminDisparity.Text = TBminDisparity.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBnumDisparities_Scroll(object sender, EventArgs e)
        {
            labnumDisparities.Text = TBnumDisparities.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBSADWindowSize_Scroll(object sender, EventArgs e)
        {
            labSADWindowSize.Text = TBSADWindowSize.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBp1_Scroll(object sender, EventArgs e)
        {
            labp1.Text = TBp1.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBp2_Scroll(object sender, EventArgs e)
        {
            labp2.Text = TBp2.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBdisp12MaxDiff_Scroll(object sender, EventArgs e)
        {
            labdisp12MaxDiff.Text = TBdisp12MaxDiff.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBpreFilterCap_Scroll(object sender, EventArgs e)
        {
            labpreFilterCap.Text = TBpreFilterCap.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBuniquenessRatio_Scroll(object sender, EventArgs e)
        {
            labuniquenessRatio.Text = TBuniquenessRatio.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBspeckleWindowSize_Scroll(object sender, EventArgs e)
        {
            labspeckleWindowSize.Text = TBspeckleWindowSize.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBspeckleRange_Scroll(object sender, EventArgs e)
        {
            labspeckleRange.Text = TBspeckleRange.Value.ToString();
            SetSGBMParameter();
            picboxDisp.Image = disp8.Bitmap;
        }

        //進行IR與深度影像的疊合，兩張影像必須有相同的類型和相同的大小
        private void IRImageBlending()
        {
            double alpha = 0.8; //第一個影像的權值
            double beta = 1.0 - alpha; //第二個影像的權值
            double gamma = 0.0; //添加的常數項
            Image<Gray, Byte> imageL = imageLeft.ToImage<Gray, byte>();//imageL：第一個影像，leap motion左邊相機影像
            //disparity：第二個影像，深度影像
            IRblendImage = imageL.AddWeighted(disparity.ToImage<Gray, Byte>(), alpha, beta, gamma);//輸出的影像
            CvInvoke.Imshow("IRblend", IRblendImage);
        }

        private void btnBlend_Click(object sender, EventArgs e)
        { 
            IRImageBlending();
            IRblendImage.Save(@"D:\Research\Image\disparity map\blend image\IRandDepth1.bmp");
            disp8.ToImage<Gray, byte>().Save(@"D:\Research\Image\disparity map\depth image\depth1.bmp");
        }

        private void RGBImageBlend()
        {
            double alpha = 0.8; //第一個影像的權值
            double beta = 1.0 - alpha; //第二個影像的權值
            double gamma = 0.0; //添加的常數項
            Mat rgb = CvInvoke.Imread("D:/Research/Image/disparity map/rgb1.bmp", Emgu.CV.CvEnum.LoadImageType.Color);
            Image<Bgr, byte> rgbImage = rgb.ToImage<Bgr, byte>();    //1920 x 1080
            //Image<Gray, Byte> depthImage = disparity.ToImage<Gray, byte>();   //640 x 240
            //Image<Gray, Byte> zoomImage = depthImage.Resize(3.0, Emgu.CV.CvEnum.Inter.Linear);  //1920 x 720
            //將leap motion left影像大小轉成跟彩色影像一樣
            Image<Gray, Byte> imageL = imageLeft.ToImage<Gray, byte>();  //640 x 240
            Image<Gray, Byte> zoomImage = imageL.Resize(3.0, Emgu.CV.CvEnum.Inter.Linear);  //1920 x 720
            Image<Gray, Byte> image = new Image<Gray, Byte>(1920, 1080);
            for (int y = 0; y < 1080; y++)
            {
                if (y < 180 || y >= 900)
                {
                    for (int x = 0; x < 1920; x++)
                    {
                        image.Data[y, x, 0] = 0;//black
                    }
                }
                else
                {
                    for (int x = 0; x < 1920; x++)
                    {
                        image.Data[y, x, 0] = zoomImage.Data[y - 180, x, 0];
                    }
                }
            }
            //converting the grayscale image back to color
            Image<Bgr, byte> grayBGRImage = image.Convert<Bgr, byte>();
            //blend leap left and rgb
            Image<Bgr, Byte> blendImage = rgbImage.AddWeighted(grayBGRImage, alpha, beta, gamma);
            CvInvoke.Imshow("zoom", image);
            CvInvoke.Imshow("rgb", rgbImage);
            CvInvoke.Imshow("rgbBlend", blendImage);
            blendImage.Save("D:/Research/Image/disparity map/blend image/rgbBlend1.bmp");
        }

        private void btnBlendRGB_Click(object sender, EventArgs e)
        {
            RGBImageBlend();
            
        }
    }
}
