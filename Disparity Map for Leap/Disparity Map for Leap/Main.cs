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
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;


namespace Disparity_Map_for_Leap
{
    public partial class MainForm : Form
    {
        private Mat disparity, disp8;        
        private int minDisparity, numDisparities, SADWindowSize, P1, P2, disp12MaxDiff, preFilterCap, uniquenessRatio, speckleWindowSize, speckleRange;
        private Image<Gray, byte> irLeftImage, irRightImage, imageLeft, imageRight, IRblendImage, imageZ;
        private Image<Bgr, byte> undistortedRGB, grayBGRImage;        

        private Matrix<double> IRleftCameraMatrix = new Matrix<double>(3, 3);
        private Matrix<double> IRrightCameraMatrix = new Matrix<double>(3, 3);
        private Matrix<double> RGBcameraMatrix = new Matrix<double>(3, 3);
        private Matrix<double> IRleftDistortionM, IRrightDistortionM;
        
        private MCvPoint3D32f[] depthPoints;

        public MainForm()
        {
            InitializeComponent();
            ReadImageAndCameraMatrix();
           
            picboxRGB.Image = undistortedRGB.Bitmap;
            undistortedRGB.Save("D:/Research/Image/disparity map/undistortRGB3.bmp");
            
            get3DspacePoint();
            picboxLeft.Image = imageLeft.Bitmap;
            picbocRight.Image = imageRight.Bitmap;
            picboxDisp.Image = disp8.Bitmap;
            imageLeft.Save("D:/Research/Image/disparity map/rectLeapLeft3.bmp");
            imageRight.Save("D:/Research/Image/disparity map/rectLeapRight3.bmp");
            getDepth();
        }

        private void ReadImageAndCameraMatrix()
        {
            Mat imageL = CvInvoke.Imread("D:/Research/Image/disparity map/LeapLeft3.bmp", Emgu.CV.CvEnum.LoadImageType.Grayscale);
            Mat imageR = CvInvoke.Imread("D:/Research/Image/disparity map/LeapRight3.bmp", Emgu.CV.CvEnum.LoadImageType.Grayscale);
            irLeftImage = imageL.ToImage<Gray, byte>();
            irRightImage = imageR.ToImage<Gray, byte>();
            Mat rgb = CvInvoke.Imread("D:/Research/Image/disparity map/rgb3.bmp", Emgu.CV.CvEnum.LoadImageType.Color);
            Image<Bgr, byte> rgbImage = rgb.ToImage<Bgr, byte>();    //1920 x 1080

            //new undistored image            
            undistortedRGB = new Image<Bgr, byte>(rgbImage.Size);

            //get camera matrix of ir cameras and rgb camera
            // K is known as the camera intrsic matrix, and defined as follows: 
            //     [ fc(1)  alpha_c*fc(1)  cc(1) ]
            // K = [   0        fc(2)      cc(2) ]
            //     [   0          0          1   ]
            double[] Kirleft = new double[9] { 134.150, 0.318, 318.021, 0, 67.096, 119.986, 0, 0, 1 };
            double[] Kirright = new double[9] { 133.929, 0.335, 318.059, 0, 67.052, 119.872, 0, 0, 1 };
            double[] Krgb = new double[9] { 706.138, 0.840, 948.851, 0, 705.323, 519.707, 0, 0, 1 };
            //KC is the camera distortion matrix
            double[] KCirleft = new double[5] { 0.18099, -0.11245, 0, 0, 0.02025 };
            double[] KCirright = new double[5] { 0.18082, -0.11238, 0, 0, 0.02046 };
            double[] KCrgb = new double[5] { -0.00516, -0.02390, -0.00112, 0.00058, 0.00328 };

            IRleftDistortionM = new Matrix<double>(KCirleft);
            IRrightDistortionM = new Matrix<double>(KCirright);

            for (int i = 0; i < 9; i++)
            {
                if (i < 3)
                {
                    IRleftCameraMatrix[0, i] = Kirleft[i];
                    IRrightCameraMatrix[0, i] = Kirright[i];
                    RGBcameraMatrix[0, i] = Krgb[i];
                }
                else if (i < 6)
                {
                    IRleftCameraMatrix[1, i - 3] = Kirleft[i];
                    IRrightCameraMatrix[1, i - 3] = Kirright[i];
                    RGBcameraMatrix[1, i - 3] = Krgb[i];
                }
                else
                {
                    IRleftCameraMatrix[2, i - 6] = Kirleft[i];
                    IRrightCameraMatrix[2, i - 6] = Kirright[i];
                    RGBcameraMatrix[2, i - 6] = Krgb[i];
                }
            }
            //remove distortion
            CvInvoke.Undistort(rgbImage, undistortedRGB, RGBcameraMatrix, new Matrix<double>(KCrgb));

            disparity = new Mat();
            disp8 = new Mat();
        }

        private void get3DspacePoint()
        {
            imageLeft = new Image<Gray, byte>(irRightImage.Size);
            imageRight = new Image<Gray, byte>(irRightImage.Size);
            //Computes rectification transforms for each head of a calibrated stereo camera.
            //R is the rotational matrix of ir cameras
            double[] R = new double[9] { 1, -0.0006, -0.0063, 0.0006, 1, 0.0004, 0.0063, -0.0004, 1 };
            //T is the translation matrix of ir cameras
            double[] T = new double[3] { -39.6423, 0.1201, 0.1893 };
            Matrix<double> Tmatrix = new Matrix<double>(T);
            Matrix<double> Rmatrix = new Matrix<double>(3, 3);
            for (int i = 0; i < 9; i++)
            {
                if (i < 3) Rmatrix[0, i] = R[i];
                else if (i < 6) Rmatrix[1, i - 3] = R[i];
                else Rmatrix[2, i - 6] = R[i];
            }
            System.Drawing.Rectangle Rec1 = new System.Drawing.Rectangle(); //Rectangle Calibrated in camera 1
            System.Drawing.Rectangle Rec2 = new System.Drawing.Rectangle(); //Rectangle Caliubrated in camera 2
            Matrix<double> Q = new Matrix<double>(4, 4); //This is what were interested in the disparity-to-depth mapping matrix
            Matrix<double> r1 = new Matrix<double>(3, 3); //rectification transforms (rotation matrices) for Camera 1.
            Matrix<double> r2 = new Matrix<double>(3, 3); //rectification transforms (rotation matrices) for Camera 1.
            Matrix<double> p1 = new Matrix<double>(3, 4); //projection matrices in the new (rectified) coordinate systems for Camera 1.
            Matrix<double> p2 = new Matrix<double>(3, 4); //projection matrices in the new (rectified) coordinate systems for Camera 2.
            CvInvoke.StereoRectify(IRleftCameraMatrix, IRleftDistortionM, IRrightCameraMatrix, IRrightDistortionM, irLeftImage.Size, Rmatrix, Tmatrix, r1, r2, p1, p2, Q, Emgu.CV.CvEnum.StereoRectifyType.Default, -1, irRightImage.Size, ref Rec1, ref Rec2);
            //rectify images
            Mat map1 = new Mat();
            Mat map2 = new Mat();
            Mat map3 = new Mat();
            Mat map4 = new Mat();
            CvInvoke.InitUndistortRectifyMap(IRleftCameraMatrix, IRleftDistortionM, r1, p1, imageLeft.Size, Emgu.CV.CvEnum.DepthType.Cv32F, map1, map2);
            CvInvoke.Remap(irLeftImage, imageLeft, map1, map2, Emgu.CV.CvEnum.Inter.Linear);
            CvInvoke.InitUndistortRectifyMap(IRrightCameraMatrix, IRrightDistortionM, r2, p2, imageRight.Size, Emgu.CV.CvEnum.DepthType.Cv32F, map3, map4);
            CvInvoke.Remap(irRightImage, imageRight, map3, map4, Emgu.CV.CvEnum.Inter.Linear);

            //get disparity map
            GetDisparityMap();

            //Reprojects disparity image to 3D space.
            depthPoints = PointCollection.ReprojectImageTo3D(disparity, Q);
        }

        private void GetDisparityMap()
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

            StereoSGBM sgbm = new StereoSGBM(minDisparity, numDisparities, SADWindowSize, P1, P2, disp12MaxDiff, preFilterCap, uniquenessRatio, speckleWindowSize, speckleRange, mode);
            //Computes disparity map for the specified stereo pair
            sgbm.Compute(imageLeft, imageRight, disparity);
            //Since Disparity will be either CV_16S or CV_32F, it needs to be compressed and normalized to CV_8U
            CvInvoke.Normalize(disparity, disp8, 0, 255, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv8U);
        }

        private void getDepth()
        {
            // 設定儲存檔名，不用設定副檔名，系統自動判斷 excel 版本，產生 .xls 或 .xlsx 副檔名
            string pathFile = @"D:\Research\depth1";
            
            Microsoft.Office.Interop.Excel.Application excelApp;
            Microsoft.Office.Interop.Excel._Workbook wBook;
            Microsoft.Office.Interop.Excel._Worksheet wSheet;
            //Microsoft.Office.Interop.Excel.Range wRange;

            // 開啟一個新的應用程式
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            // 讓Excel文件可見
            excelApp.Visible = true;
            // 停用警告訊息
            excelApp.DisplayAlerts = false;
            // 加入新的活頁簿
            excelApp.Workbooks.Add(Type.Missing);
            // 引用第一個活頁簿
            wBook = excelApp.Workbooks[1];
            // 設定活頁簿焦點
            wBook.Activate();
            // 引用第一個工作表
            wSheet = (Microsoft.Office.Interop.Excel._Worksheet)wBook.Worksheets[1];
            // 命名工作表的名稱
            wSheet.Name = "深度資訊列表";
            // 設定工作表焦點
            wSheet.Activate();

            // 設定第1列資料
            excelApp.Cells[1, 1] = "pixel";
            excelApp.Cells[1, 2] = "X";
            excelApp.Cells[1, 3] = "Y";
            excelApp.Cells[1, 4] = "Z";

            disparity.Clone();
            int cnt = 2;
            for (int i = 0; i < depthPoints.Length; i++)
            {
                if (depthPoints[i].Z < 0 || depthPoints[i].Z > 1000) continue;
                excelApp.Cells[cnt, 1] = i;
                excelApp.Cells[cnt, 2] = depthPoints[i].X;
                excelApp.Cells[cnt, 3] = depthPoints[i].Y;
                excelApp.Cells[cnt, 4] = depthPoints[i].Z;
                //Z = fB/d, where
                //double f = 67; //f is the focal length (in pixels), you called it as eye base/translation between cameras
                //double B = 0.03964;//B is the stereo baseline (in meters)
                //double d = (double) disparity; //disparity[i % 640, i / 640]//d is disparity (in pixels) that measures the difference in retinal position between corresponding points
                //double Z = f * B / d;//Z is the distance along the camera Z axis
                //excelApp.Cells[cnt, 3] = Z;
                cnt++;
            }

            //另存活頁簿
            wBook.SaveAs(pathFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Console.WriteLine("儲存文件於 " + Environment.NewLine + pathFile);

            //關閉活頁簿
            wBook.Close(false, Type.Missing, Type.Missing);

            //關閉Excel
            excelApp.Quit();

            //釋放Excel資源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            wBook = null;
            wSheet = null;
            //wRange = null;
            excelApp = null;
            GC.Collect();
        
        }

        private void TBminDisparity_Scroll(object sender, EventArgs e)
        {
            labminDisparity.Text = TBminDisparity.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBnumDisparities_Scroll(object sender, EventArgs e)
        {
            labnumDisparities.Text = TBnumDisparities.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBSADWindowSize_Scroll(object sender, EventArgs e)
        {
            labSADWindowSize.Text = TBSADWindowSize.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBp1_Scroll(object sender, EventArgs e)
        {
            labp1.Text = TBp1.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBp2_Scroll(object sender, EventArgs e)
        {
            labp2.Text = TBp2.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBdisp12MaxDiff_Scroll(object sender, EventArgs e)
        {
            labdisp12MaxDiff.Text = TBdisp12MaxDiff.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBpreFilterCap_Scroll(object sender, EventArgs e)
        {
            labpreFilterCap.Text = TBpreFilterCap.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBuniquenessRatio_Scroll(object sender, EventArgs e)
        {
            labuniquenessRatio.Text = TBuniquenessRatio.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBspeckleWindowSize_Scroll(object sender, EventArgs e)
        {
            labspeckleWindowSize.Text = TBspeckleWindowSize.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        private void TBspeckleRange_Scroll(object sender, EventArgs e)
        {
            labspeckleRange.Text = TBspeckleRange.Value.ToString();
            GetDisparityMap();
            picboxDisp.Image = disp8.Bitmap;
        }

        //進行IR與深度影像的疊合，兩張影像必須有相同的類型和相同的大小
        private void IRImageBlending()
        {
            double alpha = 0.8; //第一個影像的權值
            double beta = 1.0 - alpha; //第二個影像的權值
            double gamma = 0.0; //添加的常數項
            //imageLeft：第一個影像，leap motion左邊相機影像
            //disparity：第二個影像，深度影像
            IRblendImage = imageLeft.AddWeighted(disparity.ToImage<Gray, Byte>(), alpha, beta, gamma);//輸出的影像
            CvInvoke.Imshow("IRblend", IRblendImage);
        }

        private void btnBlend_Click(object sender, EventArgs e)
        { 
            IRImageBlending();
            IRblendImage.Save(@"D:\Research\Image\disparity map\blend image\IRandDepth3.bmp");
            disp8.ToImage<Gray, byte>().Save(@"D:\Research\Image\disparity map\depth image\depth3.bmp");
        }

        private void RGBImageBlend()
        {
            double alpha = 0.7; //第一個影像的權值
            double beta = 1.0 - alpha; //第二個影像的權值
            double gamma = 0.0; //添加的常數項
            //Image<Gray, Byte> depthImage = disparity.ToImage<Gray, byte>();   //640 x 240
            //Image<Gray, Byte> zoomImage = depthImage.Resize(3.0, Emgu.CV.CvEnum.Inter.Linear);  //1920 x 720
            //將leap motion left影像大小轉成跟彩色影像(1920*1080)一樣
            //640 x 240
            Image<Gray, Byte> zoomImage = imageLeft.Resize(3.0, Emgu.CV.CvEnum.Inter.Linear);  //1920 x 720
            imageZ = new Image<Gray, Byte>(1920, 1080);
            for (int y = 0; y < 1080; y++)
            {
                //if (y < 720)
                //{
                //    for (int x = 0; x < 1920; x++)
                //    {
                //        imageZ.Data[y, x, 0] = zoomImage.Data[y, x, 0];
                //    }
                //}
                //else
                //{
                //    for (int x = 0; x < 1920; x++)
                //    {
                //        imageZ.Data[y, x, 0] = 0;//black
                //    }
                //}
                if (y < 180 || y >= 900)
                {
                    for (int x = 0; x < 1920; x++)
                    {
                        imageZ.Data[y, x, 0] = 0;//black
                    }
                }
                else
                {
                    for (int x = 0; x < 1920; x++)
                    {
                        imageZ.Data[y, x, 0] = zoomImage.Data[y - 180, x, 0];
                    }
                }
            }
            //converting the grayscale image back to color
            grayBGRImage = imageZ.Convert<Bgr, byte>();
            //blend leap left and rgb
            Image<Bgr, Byte> blendImage = undistortedRGB.AddWeighted(grayBGRImage, alpha, beta, gamma);
            //CvInvoke.Imshow("zoom", imageZ);
            //CvInvoke.Imshow("rgb", undistortedRGB);
            CvInvoke.Imshow("rgbBlend", blendImage);
            blendImage.Save("D:/Research/Image/disparity map/blend image/rgbBlend3.bmp");
        }

        private void getCalibIRImage()
        {
            Image<Gray, byte> imageCalib = new Image<Gray, byte>(1920, 1080, new Gray(255));
            double[] data = new double[640 * 240];
            double[] Krgb = new double[9] { 706.138, 0.840, 948.851, 0, 705.323, 519.707, 0, 0, 1 };
            double[] _Kirleft = new double[9] { 132.620, 0.195, 318.153, 0, 66.408, 120.117, 0, 0, 1 };
            double[] _KirleftInverse = new double[9] { 0.0025, 0, -2.3963, 0, 0.005, -1.8088, 0, 0, 1 };
            double[] _KCirleft = new double[5] { 0.16715, -0.10133, -0.00150, 0, 0.01748 };
            //R is the rotational matrix of rgb camera and ir camera
            double[] _R = new double[9] { 1, -0.0065, -0.0047, 0.0062, 0.9973, -0.0725, 0.0051, 0.0725, 0.9974 };
            //T is the translation matrix of rgb camera and ir camera
            double[] _T = new double[3] { -19.0855, -38.7696, -4.5285 };
            //z is the depth
            double z = 1;
            //double max = 0;
           
            for (int y = 0; y < 240; y++)
            {
                for (int x = 0; x < 640; x++)
                {
                    //int index = y * 640 + x;
                    //z = depthPoints[index].Z;
                    //z = imageLeft.Data[y, x, 0];
                    //z = grayBGRImage.Data[y, x, 0];
                    z = imageZ.Data[y, x, 0];
                    //if (z > max) max = z;
                    //乘以IR內部參數反矩陣，得到此點在IR相機座標中的座標
                    double Xir = (_KirleftInverse[0] * x + _KirleftInverse[1] * y + _KirleftInverse[2]) * z;
                    double Yir = (_KirleftInverse[3] * x + _KirleftInverse[4] * y + _KirleftInverse[5]) * z;
                    double Zir = z;
                    //透過平移與旋轉矩陣，將點轉換到彩色相機座標
                    double Xrgb = _R[0] * Xir + _R[1] * Yir + _R[2] * Zir + _T[0];
                    double Yrgb = _R[3] * Xir + _R[4] * Yir + _R[5] * Zir + _T[1];
                    double Zrgb = _R[6] * Xir + _R[7] * Yir + _R[8] * Zir + _T[2];
                    //乘以彩色相機內部參數矩陣，得到此點在彩色影像座標中的位置
                    //這個是做normailize，不除的話影像座標平面會是[x y z]，除z 之後變成 [x/z y/z 1]才符合一開始的假設
                    double x_ = Krgb[0] * Xrgb / Zrgb + Krgb[1] * Yrgb / Zrgb + Krgb[2];
                    double y_ = Krgb[3] * Xrgb / Zrgb + Krgb[4] * Yrgb / Zrgb + Krgb[5];

                    if (x_ < 0 || x_ >= 1920 || y_ < 0 || y_ >= 1080) continue;
                    imageCalib[(int)(y_ + .5), (int)(x_ + .5)] = imageZ[y, x];//補齊四捨五入的值，因為int會無條件捨棄
                    //int index_ = (int)y_ * 640 + (int)x_;
                    //data[index_] = depthPoints[index].Z / max * 255;
                }
            }
            
            //Image<Bgr, byte> image = new Image<Bgr, byte>(
            CvInvoke.Imshow("1", imageCalib);
            pictureBox1.Image = imageCalib.Bitmap;
            double alpha = 0.7; //第一個影像的權值
            double beta = 1.0 - alpha; //第二個影像的權值
            double gamma = 0.0; //添加的常數項
            Image<Bgr, Byte> blendImage = undistortedRGB.AddWeighted(imageCalib.Convert<Bgr, byte>(), alpha, beta, gamma);
            CvInvoke.Imshow("2", blendImage);
            imageCalib.Save("D:/Research/Image/disparity map/test3.bmp");
            //CvInvoke.cvCreateImage(CvInvoke.cvGetSize(data), Emgu.CV.CvEnum.IplDepth.IplDepth_8U, 3);
            
        }

        private void btnBlendRGB_Click(object sender, EventArgs e)
        {
            RGBImageBlend();
            getCalibIRImage();
        }
    }
}
