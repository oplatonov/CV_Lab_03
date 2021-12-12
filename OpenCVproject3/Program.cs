using OpenCvSharp;

namespace Lab_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Dilate();
            Erosion();
            Close();
            Open();
            Skeleton();
        }

        static void Erosion()
        {
            Mat image = Cv2.ImRead("pic.jpg");

            var binary = Extention.GrayOtsu(image);
            Extention.ShowImage(binary, "gray to binary");

            Cv2.BitwiseNot(binary, binary);
            Cv2.Erode(binary, binary, new Mat());
            Cv2.BitwiseNot(binary, binary);
            Extention.ShowImage(binary, "Erode");
        }

        static void Dilate()
        {
            Mat image = Cv2.ImRead("pic.jpg");

            Mat binary = Extention.GrayOtsu(image);
            Extention.ShowImage(binary, "gray to binary");

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            Cv2.Dilate(binary, binary, kernel);
            Extention.ShowImage(binary, "Dilate");
        }

        static void Close()
        {
            Mat image = Cv2.ImRead("pic.jpg");

            Mat binary = Extention.GrayOtsu(image);
            Extention.ShowImage(binary, "gray to binary");

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            Cv2.MorphologyEx(binary, binary, MorphTypes.Close, kernel);
            Extention.ShowImage(binary, "Closing");
        }

        static void Open()
        {
            Mat image = Cv2.ImRead("pic.jpg");

            Mat binary = Extention.GrayOtsu(image);
            Extention.ShowImage(binary, "gray to binary");

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            Cv2.MorphologyEx(binary, binary, MorphTypes.Open, kernel);
            Extention.ShowImage(binary, "Opening");
        }

        static void Skeleton()
        {
            Mat image = Cv2.ImRead("pic.jpg");

            Mat binary = Extention.GrayOtsu(image);
            Extention.ShowImage(binary, "gray to binary");

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Cross, new Size(3, 3));
            Mat eroded = new();
            Mat temp = new();
            Mat skel = new(binary.Rows, binary.Cols, MatType.CV_8UC1, new Scalar(0));

            int size = image.Rows * image.Cols;
            bool done = false;
            while (!done)
            {
                Cv2.MorphologyEx(binary, eroded, MorphTypes.Erode, kernel);
                Cv2.MorphologyEx(eroded, temp, MorphTypes.Dilate, kernel);
                Cv2.Subtract(binary, temp, temp);
                Cv2.BitwiseOr(skel, temp, skel);
                eroded.CopyTo(binary);

                int zeros = size - Cv2.CountNonZero(binary);
                if (zeros == size)
                    done = true;
            }

            Extention.ShowImage(skel, "Skeleton");
        }
    }
}
