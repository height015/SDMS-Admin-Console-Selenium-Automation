
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Moq;
using SuccessLogin;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace DataSetTest;

public class DataSetTest : IDisposable
{
    private readonly IWebDriver driver;

    private readonly SuccessLogin.Program loginObj;

    private readonly AddNewSector.Program data;
    private readonly NewSectorRequest.Program newRequestSector;

    private readonly CreateNewCategory.Program createNewCategory;
    private readonly RequestCategory.Program requestCategory;
   
    private readonly NewTable.Program newTable;
    private readonly NewRequestTable.Program newTableRequest;


    private readonly BulkIndicator.Program bulkIndicator;
    private readonly NewIndicator.Program newIndicator;
    private readonly NewRequestIndicator.Program newIndicatorRequest;

    private static readonly string _URL = "http://197.255.51.104:9035";
    public DataSetTest()
    {
        driver = new ChromeDriver();
        loginObj = new SuccessLogin.Program();
        data = new AddNewSector.Program();
        newRequestSector = new  NewSectorRequest.Program();
        newIndicator = new NewIndicator.Program();
        createNewCategory = new CreateNewCategory.Program();
        requestCategory = new RequestCategory.Program();

    }

    [Fact]
    public void ClickDataSet_ShouldReturnTrueOnSuccess()
    {
        bool loginSuccess = loginObj.LoginSuccess(driver);
        Assert.True(loginSuccess.Equals(true));

        var clickDataSet = new AddNewSector.Program();

        var check = clickDataSet.ClickDataSet(driver);

        Assert.True(check.Equals(true));

    }

    [Fact]
    public void ClickSector_ShouldReturnTrueOnSuccess()
    {
        bool loginSuccess = loginObj.LoginSuccess(driver);
        Assert.True(loginSuccess.Equals(true));
        var clickDataSet = new AddNewSector.Program();
        clickDataSet.ClickDataSet(driver);
        var check = clickDataSet.ClickSector(driver);
        Assert.True(check.Equals(true));
    }

    [Fact]
    public void Click_CreateNew_DataSector_Success()
    {
        var createSecMock = new Mock<AddNewSector.Sector>();
        createSecMock.Setup(cs => cs.ClickNew());
        createSecMock.Setup(cs => cs.EnterNameAndTitle(It.IsAny<string>(), It.IsAny<string>()));
        createSecMock.Setup(cs => cs.ClickSubmit());
        createSecMock.Setup(cs => cs.textMsgRes.Text).
            Returns("Data Sector information was saved successfully");
        var jsonFileReaderMock = new Mock<JsonFileReader>();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileCreateSector()).Returns(new DataSector
        {
            SectorField = new SectorField
            {
                Name = "XUnit Test",
                Title = "XUnit Title"
            }
        });
        var myClass = new AddNewSector.Program();
        var result = myClass.CreateNewDataSectorSuccess(driver);
        Assert.Equal("", result);
    }

    [Fact]
    public void ClickNewRequest_ShouldReturnTrueOnSuccess()
    {
        var driverMock = new Mock<IWebDriver>();
        var elementMock = new Mock<IWebElement>();
        driverMock
            .Setup(d => d.FindElement(It.IsAny<By>()))
            .Returns(elementMock.Object);

        bool result = NewSectorRequest.Program.ClickNewRequest(driverMock.Object);

        Assert.True(result);

        elementMock.Verify(e => e.Click(), Times.Once);
    }

    [Fact]
    public void ClickNewRequest_ShouldReturnFalseOnError()
    {
        var driverMock = new Mock<IWebDriver>();

        driverMock
            .Setup(d => d.FindElement(It.IsAny<By>()))
            .Throws(new Exception("Test Exception"));

        var result = NewSectorRequest.Program.ClickNewRequest(driverMock.Object);

        Assert.False(result);
    }

    [Fact]
    public void Test_ClickRequestType_Success()
    {
        var driverMock = new Mock<IWebDriver>();
        var newRequestMock = new Mock<NewSectorRequest.NewRequestObj>();

        var jsonFileReaderMock = new Mock<JsonFileReader>();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileSelectCheckBoxes()).Returns(
            new CheckBoxCount
            {
                CheckBoxNumbers = new CheckBoxNumbers
                {
                    RequestType = (int)RequestType.AuthorizationRequest
                }
            });
        var rows = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
        newRequestMock.Setup(nr => nr.table).Returns(driverMock.Object.FindElement(By.CssSelector("table.table-selector")));
        newRequestMock.Setup(nr => nr.rows).Returns(rows.ToList());
        var rowsList = rows.ToList();
        var result = NewSectorRequest.Program.ClickRequestType(driverMock.Object);
        Assert.Equal("0", result);
    }

    [Fact]
    public void Test_RequestInfBox_Success()
    {
        // Arrange
        var driverMock = new Mock<IWebDriver>();
        var jsonFileReaderMock = new Mock<JsonFileReader>();
        var createSecMock = new Mock<NewSectorRequest.NewRequestObj>();
        var loginVal = new CheckBoxCount();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileSelectCheckBoxes()).Returns(loginVal);
        var RequestInforVal = new Request();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForSelectCheckBoxesProcessNewRequest()).Returns(new Request
        {
            RequestInformation = new RequestInformation
            {
                Title = "Selenium Test",
                Reason = "Selenium Test",
            }
        });

        var datconsole = new NewSectorRequest.Program();
        var result = datconsole.RequestInfBox(driverMock.Object);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Test_CategoryRequestInfBox_Success()
    {
        var driverMock = new Mock<IWebDriver>();
        var jsonFileReaderMock = new Mock<JsonFileReader>();
        var createSecMock = new Mock<RequestCategory.NewRequest>();
        var loginVal = new CheckBoxCount();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileSelectCheckBoxes()).Returns(loginVal);
        var RequestInforVal = new CatRequest();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForSelectCheckBoxesProcessCatNewRequest()).Returns(RequestInforVal);
        var dataConsole = new RequestCategory.Program();
        var result = dataConsole.CategoryRequestInfBox(driverMock.Object);
        Assert.False(result);
    }

    //[Fact]
    //public void Test_CreateNew_DataSector_Failed()
    //{
    //    // Arrange
    //    var driverMock = new Mock<IWebDriver>();
    //    var createSecMock = new Mock<AddNewSector.Sector>();
    //    var textMsgResMock = new Mock<IWebElement>();
    //    textMsgResMock.SetupGet(element => element.Text).Returns("This is an error message");
    //    driverMock.Setup(driver => driver.FindElement(By.LinkText("Datasets"))).Returns((IWebElement)null);
    //    driverMock.Setup(driver => driver.FindElement(By.CssSelector("input[name='SectorName']"))).Returns(textMsgResMock.Object);
    //    driverMock.Setup(driver => driver.FindElement(By.CssSelector("input[name='SectorTitle']"))).Returns(textMsgResMock.Object);
    //    createSecMock.Setup(cs => cs.ClickNew());
    //    createSecMock.Setup(cs => cs.ClickSubmit());
    //    createSecMock.Setup(cs => cs.textMsgRes).Returns(textMsgResMock.Object);
    //    createSecMock.Setup(cs => cs.ClickOk());
    //    createSecMock.Setup(cs => cs.ClickClose());
    //    var jsonFileReaderMock = new Mock<JsonFileReader>();
    //    jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileCreateSectorFailed()).Returns(new DataSectorEmpty
    //    {
    //        SectorFieldEmpty = new SectorFieldEmpty
    //        {
    //            Name = null,
    //            Title = ""
    //        }
    //    });
    //    // Act
    //    var result = AddNewNSector.CreateNewDataSectorFailed(driverMock.Object);
    //    // Assert
    //    Assert.Equal("", result);
    //}

    [Fact]
    public void Test_ClickData_CatalogCard_Success()
    {
        var driverMock = new Mock<IWebDriver>();
        var cartCardMock = new Mock<IWebElement>();
        driverMock.Setup(driver => driver.FindElement(By.LinkText("Data Catalogues."))).Returns(cartCardMock.Object);
        cartCardMock.Setup(card => card.Click());
        CreateNewCategory.Program.ClickDataCatalogCard(driverMock.Object);
        cartCardMock.Verify(card => card.Click(), Times.Once);
    }

    [Fact]
    public void TestClick_CategoryCard_Success()
    {
        var driverMock = new Mock<IWebDriver>();
        var cartCardMock = new Mock<IWebElement>();
        var selectElementMock = new Mock<SelectElement>();
        driverMock.Setup(driver => driver.FindElement(By.LinkText("Categories"))).Returns(cartCardMock.Object);
        cartCardMock.Setup(card => card.Click());
        var jsonFileReaderMock = new Mock<JsonFileReader>();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForSelectOptionCatalogSelector()).Returns(new CatalogueContainer
        {
            CatalogueSelector = new CatalogueSelector
            {
                OptionToSelect = 1
            }
        });
        CreateNewCategory.Program.ClickCategoryCard(driverMock.Object);
        cartCardMock.Verify(card => card.Click(), Times.Once);
        //selectElementMock.Verify(dropdown => dropdown.SelectByIndex(0), Times.Once); 
    }

    [Fact]
    public void Test_ClickNew_DataCategoryButton_Success()
    {
        var driverMock = new Mock<IWebDriver>();
        var newDataCategoryButtonMock = new Mock<IWebElement>();
        var catSecMock = new Mock<CreateNewCategory.Category>();
        var jsonFileReaderMock = new Mock<JsonFileReader>();
        driverMock.Setup(driver => driver.FindElement(By.CssSelector("a.item-button[href*='/dataset/categories/add']"))).Returns(newDataCategoryButtonMock.Object);
        newDataCategoryButtonMock.Setup(button => button.Click());
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForEnterNewDataCategory())
            .Returns(new DataCategoryContainer
            {
                DataCategory = new DataCategory
                {
                    Name = "CategoryName",
                    Title = "CategoryTitle"
                }
            });
        var result = CreateNewCategory.Program.ClickNewDataCategoryButton(driverMock.Object);
        newDataCategoryButtonMock.Verify(button => button.Click(), Times.Once);
        Assert.False(result);
    }

    [Fact]
    public void Test_ClickTable_Card_Success()
    {
        var driverMock = new Mock<IWebDriver>();
        var elementMock = new Mock<IWebElement>();
        driverMock.Setup(driver => driver.FindElement(By.LinkText("Tables"))).Returns(elementMock.Object);
        var yourService = new NewIndicator.Program();
        NewTable.Program.ClickTableCard(driverMock.Object);
        driverMock.Verify(driver => driver.FindElement(By.LinkText("Tables")), Times.Once);
        elementMock.Verify(element => element.Click(), Times.Once);
    }

    [Fact]
    public void TestClickIndicators()
    {
        var driverMock = new Mock<IWebDriver>();
        driverMock.Setup(driver => driver.FindElement(By.LinkText("Indicators"))).Returns(Mock.Of<IWebElement>());
        NewIndicator.Program.ClickIndicators(driverMock.Object);
        driverMock.Verify(driver => driver.FindElement(By.LinkText("Indicators")), Times.Once);
    }

    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }
}

