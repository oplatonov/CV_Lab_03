using OpenCvSharp;

namespace Lab_03
{
    public static class Extention
    {
        public static Mat GrayOtsu(Mat image)
        {
            Mat gray = new(image.Rows, image.Cols, MatType.CV_8UC1);
            Cv2.CvtColor(image, gray, ColorConversionCodes.RGB2GRAY);

            return gray.Threshold(0, 255, ThresholdTypes.Otsu);
        }

        public static void ShowImage(Mat image, string message)
        {
            Cv2.ImShow(message, image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}