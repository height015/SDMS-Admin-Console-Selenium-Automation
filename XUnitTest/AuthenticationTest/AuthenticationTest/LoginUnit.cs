using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SuccessLogin;
using  LoginFailed;


namespace AuthenticationTest;


public class LoginUnit : IDisposable
{
    private readonly IWebDriver driver;

    private readonly SuccessLogin.Program loginObj;

    private readonly LoginFailed.Program loginFailedObj;

    private static readonly string _URL = "http://197.255.51.104:9035";


    public LoginUnit()
    {
        driver = new ChromeDriver();
        loginObj = new SuccessLogin.Program();
        loginFailedObj = new LoginFailed.Program();
    }


    [Fact]
    public void LoginSuccess_ValidCredentials_ReturnsTrue()
    {
        bool loginSuccess = loginObj.LoginSuccess(driver);
        Assert.True(loginSuccess.Equals(true));

    }

    [Fact]
    public void LoginFailed_Should_Return_String_On_Successful_Test()
    {
        string loginFailed = loginFailedObj.LoginFailled(driver);
        Assert.True(loginFailed.Equals("No Member Login Information Found!"));
    }

    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }
}
