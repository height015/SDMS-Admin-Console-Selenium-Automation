using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SuccessLogin;


public class JsonFileReader
{

    #region Login Portal

    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public const string jsonFileName = "JData.json";
    public string jsonFilePath = Path.Combine(desktopPath, jsonFileName);


    public LoginParameter ReadJsonFileSuccesLogin()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                LoginParameter retVal = JsonConvert.DeserializeObject<LoginParameter>(jsonContent);

                return retVal;
            }


            return new LoginParameter();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new LoginParameter();
        }
    }

    public LoginFailedParameter ReadJsonFileWrongLoginCredential()
    {
        try
        {


            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                LoginFailedParameter retVal = JsonConvert.DeserializeObject<LoginFailedParameter>(jsonContent);

                return retVal;
            }

            return new LoginFailedParameter();


        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new LoginFailedParameter();
        }
    }

    public LoginEmptyUserNameParameter ReadJsonFileEmptyUserNameLoginCredential()
    {
        try
        {

            var jsonContent = File.ReadAllText(jsonFilePath);

            LoginEmptyUserNameParameter retVal = JsonConvert.DeserializeObject<LoginEmptyUserNameParameter>(jsonContent);

            return retVal;

        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new LoginEmptyUserNameParameter();
        }
    }

    #endregion

    public virtual DataSector ReadJsonFileCreateSector()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                DataSector retVal = JsonConvert.DeserializeObject<DataSector>(jsonContent);

                return retVal;
            }


            return new DataSector();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataSector();
        }
    }

    public virtual DataSectorEmpty ReadJsonFileCreateSectorFailed()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                DataSectorEmpty retVal = JsonConvert.DeserializeObject<DataSectorEmpty>(jsonContent);

                return retVal;
            }


            return new DataSectorEmpty();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataSectorEmpty();
        }
    }

    public virtual CheckBoxCount ReadJsonFileSelectCheckBoxes()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                CheckBoxCount retVal = JsonConvert.DeserializeObject<CheckBoxCount>(jsonContent);

                return retVal;
            }


            return new CheckBoxCount();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new CheckBoxCount();
        }
    }

    public virtual Request ReadJsonFileForSelectCheckBoxesProcessNewRequest()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                Request retVal = JsonConvert.DeserializeObject<Request>(jsonContent);

                return retVal;
            }


            return new Request();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new Request();
        }
    }

    public virtual CatRequest ReadJsonFileForSelectCheckBoxesProcessCatNewRequest()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                CatRequest retVal = JsonConvert.DeserializeObject<CatRequest>(jsonContent);

                return retVal;
            }


            return new CatRequest();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new CatRequest();
        }
    }

    public virtual CatalogueContainer ReadJsonFileForSelectOptionCatalogSelector()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                CatalogueContainer retVal = JsonConvert.DeserializeObject<CatalogueContainer>(jsonContent);

                return retVal;
            }


            return new CatalogueContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new CatalogueContainer();
        }
    }

    public virtual DataCategoryContainer ReadJsonFileForEnterNewDataCategory()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                DataCategoryContainer retVal = JsonConvert.DeserializeObject<DataCategoryContainer>(jsonContent);

                return retVal;
            }


            return new DataCategoryContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataCategoryContainer();
        }
    }
    public TableDataSelectorContainer ReadJsonFileForTableDataSector()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                TableDataSelectorContainer retVal = JsonConvert.DeserializeObject<TableDataSelectorContainer>(jsonContent);

                return retVal;
            }


            return new TableDataSelectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableDataSelectorContainer();
        }
    }

    public TableFrequencyContainer ReadJsonFileForTableFrequency()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                TableFrequencyContainer retVal = JsonConvert.DeserializeObject<TableFrequencyContainer>(jsonContent);

                return retVal;
            }


            return new TableFrequencyContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableFrequencyContainer();
        }
    }

    public TableUnitContainer ReadJsonFileForTableUnit()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                TableUnitContainer retVal = JsonConvert.DeserializeObject<TableUnitContainer>(jsonContent);

                return retVal;
            }


            return new TableUnitContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableUnitContainer();
        }
    }


    public TableNewDataContainer ReadJsonFileForNewDataTable()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                TableNewDataContainer retVal = JsonConvert.DeserializeObject<TableNewDataContainer>(jsonContent);
                return retVal;
            }

            return new TableNewDataContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableNewDataContainer();
        }
    }

    public IndicatorRequestDataContainer ReadJsonFileForNewRequestIndicator()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                IndicatorRequestDataContainer retVal = JsonConvert.DeserializeObject<IndicatorRequestDataContainer>(jsonContent);
                return retVal;
            }

            return new IndicatorRequestDataContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new IndicatorRequestDataContainer();
        }
    }

    public TableRequestDataContainer ReadJsonFileForNewRequestTable()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                TableRequestDataContainer retVal = JsonConvert.DeserializeObject<TableRequestDataContainer>(jsonContent);
                return retVal;
            }

            return new TableRequestDataContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableRequestDataContainer();
        }
    }

    public DataTableSettingContainer ReadJsonFileNewDataTableSettings()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataTableSettingContainer retVal = JsonConvert.DeserializeObject<DataTableSettingContainer>(jsonContent);
                return retVal;
            }

            return new DataTableSettingContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataTableSettingContainer();
        }
    }

    public DataTableTxtValContainer ReadJsonFileDataTableTxt()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataTableTxtValContainer retVal = JsonConvert.DeserializeObject<DataTableTxtValContainer>(jsonContent);
                return retVal;
            }

            return new DataTableTxtValContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataTableTxtValContainer();
        }
    }

    public DataFrequencyContainer ReadJsonFileDataFrequency()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataFrequencyContainer retVal = JsonConvert.DeserializeObject<DataFrequencyContainer>(jsonContent);
                return retVal;
            }

            return new DataFrequencyContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataFrequencyContainer();
        }
    }

    public DataUnitContainer ReadJsonFileDataUnit()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataUnitContainer retVal = JsonConvert.DeserializeObject<DataUnitContainer>(jsonContent);
                return retVal;
            }

            return new DataUnitContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataUnitContainer();
        }
    }
    public DataEntitiesContainer ReadJsonFileDataEntity()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataEntitiesContainer retVal = JsonConvert.DeserializeObject<DataEntitiesContainer>(jsonContent);
                return retVal;
            }

            return new DataEntitiesContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataEntitiesContainer();
        }
    }
    public DataSourceContainer ReadJsonFileDataSource()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataSourceContainer retVal = JsonConvert.DeserializeObject<DataSourceContainer>(jsonContent);
                return retVal;
            }

            return new DataSourceContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataSourceContainer();
        }
    }
    public IndicatorDataSelectorContainer ReadJsonFileDataIndicator()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                IndicatorDataSelectorContainer retVal = JsonConvert.DeserializeObject<IndicatorDataSelectorContainer>(jsonContent);
                return retVal;
            }

            return new IndicatorDataSelectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new IndicatorDataSelectorContainer();
        }
    }

    public NewDataIndicatorContainer ReadJsonFileNewDataIndicator()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                NewDataIndicatorContainer retVal = JsonConvert.DeserializeObject<NewDataIndicatorContainer>(jsonContent);
                return retVal;
            }

            return new NewDataIndicatorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new NewDataIndicatorContainer();
        }
    }


    public WorkFlowSelectionContaner ReadJsonFileWorkFlowSelection()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                WorkFlowSelectionContaner retVal = JsonConvert.DeserializeObject<WorkFlowSelectionContaner>(jsonContent);
                return retVal;
            }

            return new WorkFlowSelectionContaner();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new WorkFlowSelectionContaner();
        }
    }
    public ReviewSelectionContaner ReadJsonFileWorkFlowReview()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                ReviewSelectionContaner retVal = JsonConvert.DeserializeObject<ReviewSelectionContaner>(jsonContent);
                return retVal;
            }

            return new ReviewSelectionContaner();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new ReviewSelectionContaner();
        }
    }

    public AnalyticsDataSectorContainer ReadJsonCMSAnalytis()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                AnalyticsDataSectorContainer retVal = JsonConvert.DeserializeObject<AnalyticsDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new AnalyticsDataSectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new AnalyticsDataSectorContainer();
        }

    }


    public FlashCardDataSectorContainer ReadJsonCMSFlashCard()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                FlashCardDataSectorContainer retVal = JsonConvert.DeserializeObject<FlashCardDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new FlashCardDataSectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new FlashCardDataSectorContainer();
        }

    }

    public FeaturedContentDataSectorContainer ReadJsonCMSFeaturedContent()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                FeaturedContentDataSectorContainer retVal = JsonConvert.DeserializeObject<FeaturedContentDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new FeaturedContentDataSectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new FeaturedContentDataSectorContainer();
        }

    }

    public QFlashDataSectorContainer ReadJsonCMSQFlash()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                QFlashDataSectorContainer retVal = JsonConvert.DeserializeObject<QFlashDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new QFlashDataSectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new QFlashDataSectorContainer();
        }

    }


    public BulkTableNewDataContainer ReadJsonBulkTabe()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<BulkTableNewDataContainer>(jsonContent);
                return retVal;
            }
            return new BulkTableNewDataContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new BulkTableNewDataContainer();
        }

    }



    public BulkIndicatorNewDataContainer ReadJsonBulkIndicator()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<BulkIndicatorNewDataContainer>(jsonContent);
                return retVal;
            }
            return new BulkIndicatorNewDataContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new BulkIndicatorNewDataContainer();
        }

    }

}


//LoginFailedTest
#region Portal Login
#region Success Login Object

public class LoginParameters
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginParameter
{
    public LoginParameters LoginParameters { get; set; }

}
#endregion

#region Wrong Credentails Login Object

public class LoginFailedTest
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginFailedParameter
{
    public LoginFailedTest LoginFailedTest { get; set; }
}
#endregion

#region Wrong Credentails Empty UserName Fields

public class LoginEmptyUserName
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginEmptyUserNameParameter
{
    public LoginEmptyUserName LoginEmptyUserName { get; set; }
}
#endregion

#endregion


#region New Sector Data Creation

public class SectorField
{
    public string Name { get; set; }
    public string Title { get; set; }

}

public class DataSector
{
    public SectorField SectorField { get; set; }

}

public class SectorFieldEmpty
{
    public string Name { get; set; }
    public string Title { get; set; }

}

public class DataSectorEmpty
{
    public SectorFieldEmpty SectorFieldEmpty { get; set; }

}

#endregion


#region New Request

public class CheckBoxNumbers
{
    public string Index { get; set; }
    public int RequestType { get; set; }


    public int[] GetIndexArray()
    {
        return Index?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
    }
}

public class CheckBoxCount
{
    public CheckBoxNumbers CheckBoxNumbers { get; set; }

}

public class RequestInformation
{
    public string Title { get; set; }
    public string Reason { get; set; }

}

public class Request
{
    public RequestInformation RequestInformation { get; set; }

}


public class CatRequestInformation
{
    public string Title { get; set; }
    public string Reason { get; set; }

}

public class CatRequest
{
    public CatRequestInformation CatRequestInformation { get; set; }

}


#endregion

#region Cataloguo

public class CatalogueSelector
{
    public int OptionToSelect { get; set; }

}
public class CatalogueContainer
{
    public CatalogueSelector CatalogueSelector { get; set; }
}

public class CatalogueSelectorValue
{
    public int value { get; set; }
}

public class CatalogueSelectorValueContainer
{
    public CatalogueSelectorValue CatalogueSelectorValue { get; set; }
}

#endregion

#region Category

public class DataCategory
{
    public string Name { get; set; }
    public string Title { get; set; }

}

public class DataCategoryContainer
{
    public DataCategory DataCategory { get; set; }

}

#endregion
/// <summary>
/// Table
/// </summary>

#region Table
public class TableDataSelector
{
    public int OptionToSelect { get; set; }
}

public class TableDataSelectorContainer
{
    public TableDataSelector TableDataSelector { get; set; }
}

public class TableNewData
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

}

public class TableNewDataContainer
{
    public TableNewData TableNewData { get; set; }
}

public class TableRequestData
{
    public string Title { get; set; }
    public string Reason { get; set; }

}
public class TableRequestDataContainer
{
    public TableRequestData TableRequestData { get; set; }
}
public class TableFrequency
{
    public int OptionToSelect { get; set; }
}

public class TableFrequencyContainer
{
    public TableFrequency TableFrequency { get; set; }
}

public class TableUnit
{
    public int OptionToSelect { get; set; }
}

public class TableUnitContainer
{
    public TableUnit TableUnit { get; set; }
}

public class DataTableSetting
{
    public bool Data1 { get; set; }
    public bool Data2 { get; set; }
    public bool Data3 { get; set; }
    public bool Data4 { get; set; }
    public bool Data5 { get; set; }

}

public class DataTableSettingContainer
{
    public DataTableSetting DataTableSetting { get; set; }

}

public class DataTableTxtVal
{
    public string Txt1 { get; set; }
    public string Txt2 { get; set; }
    public string Txt3 { get; set; }
    public string Txt4 { get; set; }
    public string Txt5 { get; set; }

}

public class DataTableTxtValContainer
{
    public DataTableTxtVal DataTableTxtVal { get; set; }
}

//public class BulkTableNewData
//{
//	public int FreqIndexToSelect { get; set; }
//	public int UnitIndexToSelect { get; set; }

//}

//public class BulkTableNewDataContainer
//{
//    public BulkTableNewData BulkTableNewData { get; set; }
//}



//public class BulkTableNewData
//{
//	public int FreqIndexToSelect { get; set; }
//	public int UnitIndexToSelect { get; set; }
//}

//public class BulkTableNewDataCon
//{
//	public string ApplyAll { get; set; }
//	public List<BulkTableNewData> BulkTableNewData { get; set; }
//}


public class BulkTableNewDataCon
{
    public bool ApplyAll { get; set; }
    public List<BulkTableNewData> BulkTableNewData { get; set; }
}

public class BulkTableNewData
{
    public int FreqIndexToSelect { get; set; }
    public int UnitIndexToSelect { get; set; }
}

public class BulkTableNewDataContainer
{
    public BulkTableNewDataCon BulkTableNewDataCon { get; set; }
}

#endregion

#region  Indicators
public class IndicatorRequestData
{
    public string Title { get; set; }
    public string Reason { get; set; }
}

public class IndicatorRequestDataContainer
{
    public IndicatorRequestData IndicatorRequestData { get; set; }
}




public class BulkIndicatorNewDataCon
{
    public bool Modify { get; set; }
    public int indexToSelect { get; set; }
    public List<BulkIndicatorNewData> BulkIndicatorNewData { get; set; }
}

public class BulkIndicatorNewData
{
    public int DisplayOrder { get; set; }
    public bool DisplayInChart { get; set; }
    public string GraphTitle { get; set; }

}

public class BulkIndicatorNewDataContainer
{
    public BulkIndicatorNewDataCon BulkIndicatorNewDataCon { get; set; }
}

#endregion

//Frequncy
#region Frequency
public class DataFrequency
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataFrequencyContainer
{
    public DataFrequency DataFrequency { get; set; }
}

#endregion

#region Units
public class DataUnit
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataUnitContainer
{
    public DataUnit DataUnit { get; set; }
}

#endregion

#region Data Entities
public class DataEntities
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataEntitiesContainer
{
    public DataEntities DataEntities { get; set; }
}
#endregion

#region Data Source
public class DataSource
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataSourceContainer
{
    public DataSource DataSource { get; set; }
}


#endregion

#region Indicator PopUp
public class IndicatorDataSelector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }

}

public class IndicatorDataSelectorContainer
{
    public IndicatorDataSelector IndicatorDataSelector { get; set; }
}

#endregion

#region New Data Indicator PopUp
public class NewDataIndicator
{
    public int TopLevelIndi { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }

    public bool EmboldenIndicatorTitle { get; set; }

    public int DisplayOrder { get; set; }

    public bool DisplayInChart { get; set; }

    public string TitleToDisplay { get; set; }

}

public class NewDataIndicatorContainer
{
    public NewDataIndicator NewDataIndicator { get; set; }
}

#endregion


#region Authorization Workflow
public class WorkFlowSelection
{
    public int CategoryIndex { get; set; }
    public int SourceTypeIndex { get; set; }
    public int RoleIndex { get; set; }

}

public class WorkFlowSelectionContaner
{
    public WorkFlowSelection WorkFlowSelection { get; set; }

}

public class ReviewSelection
{
    public string Comment { get; set; }
    public bool Status { get; set; }
}

public class ReviewSelectionContaner
{
    public ReviewSelection ReviewSelection { get; set; }

}




#endregion

#region    CMS Analytics
public class AnalyticsDataSector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }
    public string StartDate { get; set; }
    public string StopDate { get; set; }
    public string IndicatorsIndexToSelect { get; set; }


    public string Name { get; set; }
    public string Title { get; set; }
    public string SeriesTitle { get; set; }

    public int ChartTypeIndex { get; set; }
    public int ContentSpotIndex { get; set; }

    public string Note { get; set; }

    public int[] GetIndexArray()
    {
        return IndicatorsIndexToSelect?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
    }

}

public class AnalyticsDataSectorContainer
{
    public AnalyticsDataSector AnalyticsDataSector { get; set; }
}
#endregion


#region FlashCard
public class FlashCardDataSector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }
    public string StartDate { get; set; }
    public string StopDate { get; set; }
    public string IndicatorsIndexToSelect { get; set; }


    public string Name { get; set; }
    public string Title { get; set; }
    public string SeriesTitle { get; set; }

    public int ChartTypeIndex { get; set; }
    public int ContentSpotIndex { get; set; }

    public string Note { get; set; }

    public int[] GetIndexArray()
    {
        return IndicatorsIndexToSelect?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
    }

}
public class FlashCardDataSectorContainer
{
    public FlashCardDataSector FlashCardDataSector { get; set; }
}
#endregion


#region FeatureContent 
public class FeaturedContentDataSector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }
    public string StartDate { get; set; }
    public string StopDate { get; set; }
    public string IndicatorsIndexToSelect { get; set; }


    public string Name { get; set; }
    public string Title { get; set; }
    public string SeriesTitle { get; set; }

    public int ChartTypeIndex { get; set; }
    public int ContentSpotIndex { get; set; }

    public string Note { get; set; }

    public int[] GetIndexArray()
    {
        return IndicatorsIndexToSelect?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
    }

}
public class FeaturedContentDataSectorContainer
{
    public FeaturedContentDataSector FeaturedContentDataSector { get; set; }
}
#endregion

#region QFlash 
public class QFlashDataSector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }
    public string StartDate { get; set; }
    public string StopDate { get; set; }
    public string IndicatorsIndexToSelect { get; set; }


    public string Name { get; set; }
    public string Title { get; set; }
    public int ArrowType { get; set; }
    public int ArrowDirection { get; set; }
    public int Value { get; set; }
    public string Note { get; set; }

    public int[] GetIndexArray()
    {
        return IndicatorsIndexToSelect?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
    }

}
public class QFlashDataSectorContainer
{
    public QFlashDataSector QFlashDataSector { get; set; }
}
#endregion