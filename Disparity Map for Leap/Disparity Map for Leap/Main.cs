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

namespace Disparity_Map_for_Leap
{
    public partial class Main : Form
    {
        private Mat imageLeft, imageRight;
        private Mat disparity, disp8;
        private StereoSGBM sgbm;

        public Main()
        {
            InitializeComponent();
            ReadImage();
            SetSGBMParameter();
            CvInvoke.Imshow("left", imageLeft);
            CvInvoke.Imshow("right", imageRight);
            CvInvoke.Imshow("disp", disp8);
        }

        private void ReadImage()
        {
            imageLeft = CvInvoke.Imread("D:/Research/Image/disparity map/ULeapLeft1.bmp", Emgu.CV.CvEnum.LoadImageType.Grayscale);
            imageRight = CvInvoke.Imread("D:/Research/Image/disparity map/ULeapRight1.bmp", Emgu.CV.CvEnum.LoadImageType.Grayscale);
            disparity = new Mat();
            disp8 = new Mat();
        }

        private void SetSGBMParameter()
        {
            /*//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            StereoSGBM(int minDisparity, int numDisparities, int SADWindowSize, int P1, int P2, int disp12MaxDiff, int preFilterCap, 
            int uniquenessRatio, int speckleWindowSize, int speckleRange, bool fullDP);
            minDisparity : Minimum possible disparity value. Normally, it is zero but sometimes rectification algorithms can shift images, 
                           so this parameter needs to be adjusted accordingly.
            numDisparities : Maximum disparity minus minimum disparity. The value is always greater than zero. 
                             In the current implementation, this parameter must be divisible by 16.
            blockSize : Matched block size. It must be an odd number >=1 . Normally, it should be somewhere in the 3..11 range. Use 0 for default.
            p1 (Optional) : The first parameter controlling the disparity smoothness. It is the penalty on the disparity change 
                            by plus or minus 1 between neighbor pixels. Reasonably good value is 8*number_of_image_channels*SADWindowSize*SADWindowSize. Use 0 for default
            p2 (Optional) : The second parameter controlling the disparity smoothness. It is the penalty on the disparity change by more than 1 between neighbor pixels. 
                            The algorithm requires p2 > p1. Reasonably good value is 32*number_of_image_channels*SADWindowSize*SADWindowSize. Use 0 for default
            disp12MaxDiff (Optional) : Maximum allowed difference (in integer pixel units) in the left-right disparity check. Set it to a non-positive value to disable the check.
            preFilterCap (Optional) : Truncation value for the prefiltered image pixels. The algorithm first computes x-derivative at each pixel and clips 
                                      its value by [-preFilterCap, preFilterCap] interval. The result values are passed to the Birchfield-Tomasi pixel cost function.
            uniquenessRatio (Optional) : Margin in percentage by which the best (minimum) computed cost function value should “win” the second best value 
                                         to consider the found match correct. Normally, a value within the 5-15 range is good enough.
            speckleWindowSize (Optional) : Maximum size of smooth disparity regions to consider their noise speckles and invalidate. 
                                           Set it to 0 to disable speckle filtering. Otherwise, set it somewhere in the 50-200 range
            speckleRange (Optional) : Maximum disparity variation within each connected component. If you do speckle filtering, set the parameter to a positive value, 
                                      it will be implicitly multiplied by 16. Normally, 1 or 2 is good enough.
            mode (Optional) : Set it to HH to run the full-scale two-pass dynamic programming algorithm. It will consume O(W*H*numDisparities) bytes, 
                              which is large for 640x480 stereo and huge for HD-size pictures. By default, it is set to false.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
            sgbm = new StereoSGBM(0, 112, 9, 600, 2400, 10, 39, 8, 0, 8, StereoSGBM.Mode.SGBM);

            //Computes disparity map for the specified stereo pair
            sgbm.Compute(imageLeft, imageRight, disparity);
            //Since Disparity will be either CV_16S or CV_32F, it needs to be compressed and normalized to CV_8U
            CvInvoke.Normalize(disparity, disp8, 0, 255, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv8U);
        }
    }
}
