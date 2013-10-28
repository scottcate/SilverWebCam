using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SilverlightWebCam
{
    public partial class MainPage
    {
        private CaptureSource _cs;

        public MainPage()
        {
            InitializeComponent();
        }

        private void startVideo_Click(object sender, RoutedEventArgs e)
        {
            bool ok = CaptureDeviceConfiguration.AllowedDeviceAccess;

            if (!ok)
            {
                ok = CaptureDeviceConfiguration.RequestDeviceAccess();
            }

            if (ok)
            {
                _cs = new CaptureSource
                {
                    VideoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice()
                };

                var vidBrush = new VideoBrush();
                vidBrush.SetSource(_cs);
                Video.Fill = vidBrush;
                Video.Stretch = Stretch.UniformToFill;

                StartVideo.Visibility =
                    ResizeAnchor.Visibility =
                    MoveAnchor.Visibility =
                    Visibility.Collapsed;
                _cs.Start();

            }
        }

        private void StopVideo(object sender, MouseButtonEventArgs e)
        {
            _cs.Stop();
            StartVideo.Visibility =
                ResizeAnchor.Visibility =
                MoveAnchor.Visibility =
                Visibility.Visible;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser &&
                Application.Current.HasElevatedPermissions)
            {
                Application.Current.MainWindow.DragMove();
            }
        }

        private void DragBottomRight(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser &&
                Application.Current.HasElevatedPermissions)
            {
                Application.Current.MainWindow.DragResize
                           (WindowResizeEdge.BottomRight);
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser &&
                Application.Current.HasElevatedPermissions)
            {
                ResizeAnchor.Visibility =
                    MoveAnchor.Visibility = Visibility.Visible;
            }
            else
            {
                ResizeAnchor.Visibility =
                    MoveAnchor.Visibility = Visibility.Collapsed;
            }
        }

    }
}
