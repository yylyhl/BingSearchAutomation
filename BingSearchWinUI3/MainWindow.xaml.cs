using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;
using System.Threading;
using Windows.System;
using Microsoft.UI.Xaml.Controls;
using System.Runtime.InteropServices;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BingSearchWinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            new Thread(delegate () { BingSearchCommon.RandomSearchTerm.GenerateUniqueQuestions(100); }) { IsBackground = true }.Start();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            await OpenEdge();
        }

        #region 将窗口置顶
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        public const uint SWP_NOMOVE = 0x2;
        public const uint SWP_NOSIZE = 0x1;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private void SetWindowToTopMost(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        } 
        #endregion
        private async Task OpenEdge()
        {
            _ = int.TryParse(doTimes.Text, out var times);
            times = Math.Max(times, 1);
            times = Math.Min(times, 99);
            var rdm = new Random();
            for (int i = 0; i < times; i++)
            {
                try
                {
                    string query = BingSearchCommon.RandomSearchTerm.GenerateRandomSearchTerm() + i;
                    query = BingSearchCommon.RandomSearchTerm.UniqueQuestions[i];
                    var searchUrl = $"https://cn.bing.com/search?q={query}";
                    await Launcher.LaunchUriAsync(new Uri(searchUrl));//使用默认浏览器
                    var edgeDriverPath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe";
                    Process.Start(edgeDriverPath, searchUrl);//指定认浏览器
                }
                catch (Exception ex)
                {
                    await new ContentDialog
                    {
                        Title = "error",
                        Content = ex.Message + "\n" + ex.StackTrace,
                        CloseButtonText = "知道了",
                        XamlRoot = this.Content.XamlRoot
                    }.ShowAsync();
                    Environment.Exit(0);
                }
                Thread.Sleep(rdm.Next(1500, 10000));
            }
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            SetWindowToTopMost(hwnd.ToInt32()); // 调用方法将窗口置顶
            //this.TopMost = true;
            //new Thread(delegate () { Thread.Sleep(3000); Environment.Exit(0); }) { IsBackground = true }.Start();
            var dialog = new ContentDialog
            {
                Title = "tip",
                Content = "执行结束",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            //// 设置 XAML 根元素，解决可能出现的“Value does not fall within the expected range”错误
            //if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            //{
            //    dialog.XamlRoot = this.Content.XamlRoot;
            //}
            await dialog.ShowAsync();
            Environment.Exit(0);
        }
    }
}
