using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Commons;
using SuccessLogin;

namespace LoginFailed;

public class Program
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var customMethod = new Program();
            string login2 = customMethod.LoginFailled(driver);
        }
    }
    public string LoginFailled(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileWrongLoginCredential();
            string loginUrl = _URL + "/account/sign-in";
            driver.Navigate().GoToUrl(loginUrl);
            var loginPage = new LoginPage(driver);
            loginPage.EnterUserNameAndPassword(loginVal.LoginFailedTest.Username, loginVal.LoginFailedTest.Password);
            loginPage.ClickLogin();
            var teaxVal = loginPage.divElementText;
            Sleep(3000);
            if (teaxVal != null)
            {
                string divText = teaxVal.Text;
                return divText;
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }
    private void Sleep(int timeVal)
    {
        Thread.Sleep(timeVal);
    }
}