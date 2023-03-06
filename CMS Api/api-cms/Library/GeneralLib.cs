namespace CMS_API.Library
{
    public class GeneralLib
    {
        public static class Constan
        {
            #region General
            public const string CONST_RES_CD_SUCCESS = "200";
            public const string CONST_RES_CD_SUCCESS_EXC = "201";
            public const string CONST_RES_CD_ERROR = "400";
            public const string CONST_RES_CD_ERROR_UPLOAD = "401";
            public const string CONST_RES_CD_ERROR_SEND_WA = "402";
            public const string CONST_RES_CD_CATCH = "500";
            public const string CONST_RES_CD_ERROR_GATEWAY = "504";
            public const string CONST_RES_CD_DB = "600";

            public const string CONST_RES_MESSAGE_SUCCESS = "Success !";
            public const string CONST_RES_MESSAGE_ERROR = "Error! ";
            public const string CONST_RES_MESSAGE_INVALIDINPUT = "Invalid Input ! ";
            public const string CONST_RES_MESSAGE_SUCCESS_UPLOAD = "Upload Success !";
            public const string CONST_RES_STATUS_SUCCESS_UPLOAD = "OK !";
            public const string CONST_RES_MESSAGE_ERROR_UPLOAD = "File Not Found!";
            public const string CONST_RES_STATUS_ERROR_UPLOAD = "Failed";
            public const string CONST_RES_MESSAGE_ERROR_UPLOAD_10 = "Error Upload or File More Than 10MB";
            public const string CONST_RES_MESSAGE_ERROR_UPLOAD_NOT = "File Name Not exist";
            public const string CONST_RES_MESSAGE_ERROR_UPLOAD_FILE = "File Name cannot Null";
            public const string CONST_RES_MESSAGE_ERROR_NULL = "Parameter Cannot Null Value";
            public const string CONST_RES_MESSAGE_ERROR_GATEWAY = "Gateway Timeout !";
            public const string CONST_RES_MESSAGE_ERROR_ID_NULL = "Id Cannot Null Value";

            #endregion
        }
        #region Log
        public static string GenerateUniqueID()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss") + Guid.NewGuid().ToString("N");
        }
        public static string GetDateFormatDate()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
        public static string GetDateFormatTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        public static class WriteFileLog
        {
            public static void Write(string pstrText, string pstrController, string pstrMetaDataCode, string pstrMethod)
            {
                try
                {

                    pstrText = pstrText + ";"; /*separator*/

                    string strFileName = DateTime.Now.ToString("yyyyMMdd");
                    string strPathBaseUrl = "D:\\Habaku Repository\\" + "\\Log";
                    string strPathController = strPathBaseUrl + "\\" + pstrController + "\\" + pstrMethod;
                    string strPathControllerMetaDataCode = strPathController + "\\" + pstrMetaDataCode;

                    //1.1.CREATE FOLDER CONTROLLER
                    if (!Directory.Exists(strPathController))
                    {
                        Directory.CreateDirectory(strPathController);
                    }

                    //1.2.CREATE FOLDER METADATA CODE
                    if (!Directory.Exists(strPathControllerMetaDataCode))
                    {
                        Directory.CreateDirectory(strPathControllerMetaDataCode);
                    }

                    string strPathFinal = strPathControllerMetaDataCode + "\\" + strFileName + ".txt";


                    if (!File.Exists(strPathFinal))
                    {
                        using (StreamWriter sw = File.CreateText(strPathFinal))
                        {

                            sw.WriteLine(pstrText);
                        }
                    }
                    else
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPathFinal, true))
                        {
                            file.WriteLine(pstrText);
                        }
                    }

                }
                catch
                {
                    // do Nothing
                }
            }
        }
        #endregion
    }
}
