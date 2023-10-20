using Newtonsoft.Json;

namespace Commons;
public class JsonFileReader
{

    #region Login Portal

    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public const string jsonFileName = "JData.json";
    public string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);

    public LoginParameter ReadJsonFileSuccesLogin()
    {
        try
        {

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                var retVal = JsonConvert.DeserializeObject<LoginParameter>(jsonContent);

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

    //public virtual DataSectorEmpty ReadJsonFileCreateSectorFailed()
    //{
    //    try
    //    {

    //        if (File.Exists(jsonFilePath))
    //        {
    //            var jsonContent = File.ReadAllText(jsonFilePath);

    //            DataSectorEmpty retVal = JsonConvert.DeserializeObject<DataSectorEmpty>(jsonContent);

    //            return retVal;
    //        }


    //        return new DataSectorEmpty();
    //    }
    //    catch (Exception ex)
    //    {
    //        var message = ex.Message;

    //        return new DataSectorEmpty();
    //    }
    //}

    public virtual CheckBoxCount ReadJsonFileSelectCheckBoxes()
    {
        try
        {
            string jsonFileNamex = "Request.json";
            string jsonFilePathx = Path.Combine(desktopPath, jsonFileNamex);

            if (File.Exists(jsonFilePathx))
            {
                var jsonContent = File.ReadAllText(jsonFilePathx);
                var retVal = JsonConvert.DeserializeObject<CheckBoxCount>(jsonContent);
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
    public virtual CatalogueContainer ReadJsonFileForSelectOptionCatalogSelector()
    {
        try
        {
              string jsonFileNamex = "Category.json";
              string jsonFilePathx = Path.Combine(desktopPath, jsonFileNamex);
            if (File.Exists(jsonFilePathx))
            {
                var jsonContent = File.ReadAllText(jsonFilePathx);
                var retVal = JsonConvert.DeserializeObject<CatalogueContainer>(jsonContent);
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
    public TableDataSelectorContainer ReadJsonFileForTableDataSector()
    {
        try
        {
              string jsonFileNameTbl = "Tables.json";
              string jsonFilePath = Path.Combine(desktopPath, jsonFileNameTbl);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableDataSelectorContainer>(jsonContent);
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
    public IndicatorDataSelectorContainer ReadJsonFileDataIndicator()
    {
        try
        {
              string jsonIndiFileName = "Indicator.json";
              string jsonFilePath = Path.Combine(desktopPath, jsonIndiFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<IndicatorDataSelectorContainer>(jsonContent);
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
    public WorkFlowSelectionContaner ReadJsonFileWorkFlowSelection()
    {
        try
        {
            string jsonIndiFileName = "WorkFlow.json";
            string jsonFilePath = Path.Combine(desktopPath, jsonIndiFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<WorkFlowSelectionContaner>(jsonContent);
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
            string jsonIndiFileName = "WorkFlow.json";
            string jsonFilePath = Path.Combine(desktopPath, jsonIndiFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<ReviewSelectionContaner>(jsonContent);
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
    public int DataSectorIndexToSelect { get; set; }
    public int DataCategoryIndexToSelect { get; set; }
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
public class RequestData
{
    public string Title { get; set; }
    public string Reason { get; set; }
}

public class IndicatorRequestDataContainer
{
    public RequestData RequestData { get; set; }
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