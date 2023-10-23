namespace Commons;
public class Utils
{
    public static void Sleep(int timeVal)
    {
        Thread.Sleep(timeVal);
    }
    public static void LogE(string trace, string errSource, string msg)
    {
        try
        {
            string jsonFileNamex = "Errorfile.txt";
            string localDate = GetLocalDate();
            string localTime = GetLocalTime();
            string text = GetBaseLogErrorPath(jsonFileNamex);
            if (File.Exists(text))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(text);
                    if (fileInfo.Length >> 10 >= 10)
                    {
                        string text2 = text.Substring(0, text.Length - 4);
                        text2 = text2 + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + fileInfo.Extension;
                        fileInfo.CopyTo(text2);
                        File.WriteAllText(text, string.Empty);
                    }
                }
                catch (Exception)
                {
                }

                StreamWriter streamWriter = new StreamWriter(text, append: true);
                string text3 = "\r\n";
                text3 += "<New Error>\r\n";
                text3 = text3 + "[Time] : " + localDate + " -:- " + localTime + "\r\n";
                text3 = text3 + "[Error Message] : " + msg + "\r\n";
                text3 = text3 + "[Error Location] : " + trace + "\r\n";
                text3 = text3 + "[Instance] : " + errSource + "\r\n";
                text3 += "</New Error>";
                streamWriter.WriteLine(text3);
                streamWriter.Close();
            }
            else
            {
                new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite).Close();
                StreamWriter streamWriter2 = new StreamWriter(text, append: true);
                string text4 = "\r\n";
                text4 += "<New Error>\r\n";
                text4 = text4 + "[Time] : " + localDate + " -:- " + localTime + "\r\n";
                text4 = text4 + "[Error Message] : " + msg + "\r\n";
                text4 = text4 + "[Error Location] : " + trace + "\r\n";
                text4 = text4 + "[Instance] : " + errSource + "\r\n";
                text4 += "</New Error>";
                streamWriter2.WriteLine(text4);
                streamWriter2.Close();
            }
        }
        catch (Exception)
        {
        }
    }


    public static void LogSuccess(string operation, string module)
    {
        string successJsonFileNamex = "success.txt"; 
        try
        {
            string logMessage = $"- {operation} Operation Successful \n- Time {CurrentTimeStamp()}\n- from Module {module}\n";
            string filePath = GetBaseLogErrorPath(successJsonFileNamex);
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(logMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(logMessage);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }


    private static string GetBaseLogErrorPath(string jsonFileNamex)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string jsonFilePathx = Path.Combine(desktopPath,
            "SeleniumTest", jsonFileNamex);
            return jsonFilePathx;
    }

    public static string CurrentTimeStamp()
    {
        return DateTime.Now.ToUniversalTime().AddHours(1.0).ToString("yyyy/MM/dd - hh:mm:ss tt");
    }

    public static string GetLocalDate()
    {
        try
        {
            return DateTime.Now.ToUniversalTime().AddHours(1.0).ToString("yyyy/MM/dd");
        }
        catch (Exception)
        {
            return DateTime.Now.ToString("yyyy/MM/dd");
        }
    }

    public static string GetLocalTime()
    {
        try
        {
            return DateTime.Now.ToUniversalTime().AddHours(1.0).ToString("hh:mm:ss");
        }
        catch (Exception)
        {
            return DateTime.Now.ToString("hh:mm tt");
        }
    }


}
