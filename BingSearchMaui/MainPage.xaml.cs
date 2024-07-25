namespace BingSearchMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            new Thread(delegate () { BingSearchCommon.RandomSearchTerm.GenerateUniqueQuestions(100); }) { IsBackground = true }.Start();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            new Thread(async delegate () { await OpenEdge(); }) { IsBackground = true }.Start();
            //await OpenEdge();
        }

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
                    var options = new BrowserLaunchOptions
                    {
                        LaunchMode = BrowserLaunchMode.External
                    };
                    await Browser.Default.OpenAsync(searchUrl, options);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("error", ex.Message + "\n" + ex.StackTrace, "知道了");
                    Environment.Exit(0);
                }
                await Task.Delay(rdm.Next(1500, 10000));
            }
            await DisplayAlert("tip","执行结束", "OK");
            Environment.Exit(0);
        }
    }

}
