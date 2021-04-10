using OpenCvSharp;
using System;
using System.IO;



namespace ConsoleApp
{
    class ImageProcessing
    {
        public static string ProjectRoot = Directory.GetParent("./../..").FullName;
        public static string ImagesRoot = $"{ProjectRoot}\\Defect_images";


        public static void Test()
        {
            var files = Directory.GetFiles(ImagesRoot);
            

            var src = new Mat(files[0], ImreadModes.Grayscale);

            Mat kernel = new Mat(3, 3, MatType.CV_8UC1, new Scalar(0));
            Mat dst = new Mat(src.Size(), MatType.CV_8UC1);

            kernel.Set<int>(1, 1, 5);
            kernel.Set<int>(0, 1, -1);
            kernel.Set<int>(2, 1, -1);
            kernel.Set<int>(1, 0, -1);
            kernel.Set<int>(1, 2, -1);


            //Cv2.Sobel(src, dst, MatType.CV_16U, 1, 0, 3);
            
            Cv2.Filter2D(src, dst, MatType.CV_8UC1, kernel);

            //Cv2.Canny(src, dst, 50, 200);
            using (new Window("src image", src))
            using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }

        }
    }
}
