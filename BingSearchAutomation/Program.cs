using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace BingSearchAutomation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            new Thread(delegate () { BingSearchCommon.RandomSearchTerm.GenerateUniqueQuestions(100); }) { IsBackground = true }.Start();
            AutoSearchByPlaywright().Wait();
            //AutoSearch();
        }

        #region AutoSearchByPlaywright
        private static async Task AutoSearchByPlaywright()
        {
            var playwright = await Playwright.CreateAsync();
            //var browser = await playwright.Chromium.LaunchAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Channel = "msedge",
                Headless = false
            });
            await using var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                StorageStatePath = "D:\\Study\\My\\BingSearchAutomation\\BingSearchAutomation\\context.json"
            });//恢复BrowserContext的状态，保留登录状态、cookies和其他会话数据
               //var page = await browser.NewPageAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://www.bing.com");
            Thread.Sleep(10000);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    string query = BingSearchCommon.RandomSearchTerm.GenerateRandomSearchTerm() + i;
                    query = BingSearchCommon.RandomSearchTerm.UniqueQuestions[i];
                    // 找到搜索框并输入内容
                    await page.FillAsync("input[id='sb_form_q']", query);
                    // 可根据实际情况模拟点击搜索按钮或执行其他操作
                    //page.ClickAsync("input[id='searchButtonId']");
                    await page.Keyboard.PressAsync("Enter");

                    Thread.Sleep(new Random().Next(500, 5000));
                }
                finally
                {
                    //await page.CloseAsync();
                }
            }
            await context.StorageStateAsync(new BrowserContextStorageStateOptions
            {
                Path = "D:\\Study\\My\\BingSearchAutomation\\BingSearchAutomation\\context.json"
            });//用于存储和恢复BrowserContext的状态，这样就可以保留登录状态、cookies和其他会话数据
        }
        #endregion

        #region AutoSearchByDriver
        private static void AutoSearchByDriver()
        {
            var edgeDriverPath = "C:\\Users\\Administrator\\Downloads\\edgedriver_win64\\msedgedriver.exe";// Edge WebDriver的路径
            using (var driver = new EdgeDriver(edgeDriverPath))// 创建一个Edge浏览器实例
            {
                try
                {
                    driver.Navigate().GoToUrl("https://www.bing.com");// 打开Bing搜索页面
                    string searchUrl = $"https://www.bing.com/search?q=first&form=QBRE";// 包含form=QBRE参数的搜索URL
                    driver.Navigate().GoToUrl(searchUrl);// 打开Bing搜索页面并带上参数
                    Thread.Sleep(8000);
                    for (int i = 0; i < 3; i++)
                    {
                        string query = BingSearchCommon.RandomSearchTerm.GenerateRandomSearchTerm() + i;
                        query = BingSearchCommon.RandomSearchTerm.UniqueQuestions[i];
                        Console.WriteLine("搜索内容: " + query);
                        var searchBoxId = By.Id("sb_form_q"); // Bing搜索框的ID
                        var searchBox = driver.FindElement(searchBoxId); // 找到搜索框
                        searchBox.SendKeys(query); // 输入内容
                        Thread.Sleep(new Random().Next(50, 200));
                        //searchBox.Click();// 提交搜索（点击搜索按钮或按下回车键）
                        searchBox.SendKeys(Keys.Enter);// 提交搜索（点击搜索按钮或按下回车键）

                        // 等待搜索结果加载（可选，根据你的需要添加等待逻辑）
                        // ...

                        // 在这里可以添加其他操作，比如检查搜索结果等

                        // 示例：打印URL，确认我们确实在搜索结果页面上
                        Console.WriteLine("当前URL: " + driver.Url);
                        Thread.Sleep(new Random().Next(500, 5000));
                    }
                }
                finally
                {
                    driver.Quit();// 关闭浏览器
                }
            }
        }
        #endregion
    }
}
