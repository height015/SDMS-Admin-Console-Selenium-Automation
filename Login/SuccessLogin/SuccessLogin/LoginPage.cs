using OpenQA.Selenium;

namespace SuccessLogin;


public class LoginPage
{
    private readonly IWebDriver _webDriver;

    public LoginPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;

    }

    public IWebElement txtEmail => _webDriver.FindElement(By.Id("txtUsername"));

    public IWebElement txtPassword => _webDriver.FindElement(By.Id("txtPassword"));


    public IWebElement btnLogin => _webDriver.FindElement(By.XPath("//input[@type='submit']"));

    public IWebElement divElementText => _webDriver.FindElement(By.ClassName("alert-danger"));


    public void EnterUserNameAndPassword(string userName, string password)
    {
        txtEmail.SendKeys(userName);
        txtPassword.SendKeys(password);
    }

    public void ClickLogin()
    {
        btnLogin.Clicks();
    }

}


