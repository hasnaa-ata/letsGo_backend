using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Classes.Common;
using DataLayer.Common;
using LetsGo.BackEnd.Utilities;
using Newtonsoft.Json;
using PlusAction.BackEnd.Common;

namespace Classes.Utilities
{
    public class Utility
    {
        public static bool ValidateUploadedDocument(string contentType, string fileSize, ref string errorMessage)
        {
            try
            {
                if (string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(fileSize))
                {
                    errorMessage = "No file uploaaded";
                    return false;
                }
                var hasPDF = contentType.ToLower().Contains("pdf");
                var hasJPG = contentType.ToLower().Contains("jpg");
                var hasJPEG = contentType.ToLower().Contains("jpeg");
                var haspng = contentType.ToLower().Contains("png");
                var hassvg = contentType.ToLower().Contains("svg");
                if (hasPDF == false && hasJPG == false && hasJPEG == false && haspng == false && hassvg == false)
                {

                    errorMessage = "File with no extention";
                    return false;
                }

                if (double.Parse(fileSize) > double.Parse("2") * 1024 * 1024)
                {

                    errorMessage = "File size is more than 2 mb";
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
                return false;
            }
        }

        public static string saveFile(string filePattern, string filePath, UploadedDocument documentInformation)
        {
            string FilePath = filePath;
            try
            {
                string extention = string.Empty;
                if (documentInformation.ContentType.ToLower().Contains("pdf"))
                {
                    extention = ".pdf";
                }
                if (documentInformation.ContentType.ToLower().Contains("jpeg"))
                {
                    extention = ".jpeg";
                }
                if (documentInformation.ContentType.ToLower().Contains("jpg"))
                {
                    extention = ".jpg";
                }
                if (documentInformation.ContentType.ToLower().Contains("png"))
                {
                    extention = ".png";
                }
                if (documentInformation.ContentType.ToLower().Contains("svg"))
                {
                    extention = ".svg";
                }

                string expectedFileName = $"{filePattern}{extention}";

                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
                var bytes = Convert.FromBase64String(documentInformation.UploadedFile);
                FilePath = FilePath + @"/" + expectedFileName;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                System.IO.File.WriteAllBytes(FilePath, bytes);

            }
            catch (Exception networkError)
            {
                throw networkError;
            }
            return FilePath;
        }

        public static string UpdateFile(string filePattern, string filePath, UploadedDocument documentInformation, string oldFilePattern)
        {
            string FilePath = filePath;
            try
            {
                string extention = string.Empty;
                if (documentInformation.ContentType.ToLower().Contains("pdf"))
                {
                    extention = ".pdf";
                }
                if (documentInformation.ContentType.ToLower().Contains("jpeg"))
                {
                    extention = ".jpeg";
                }
                if (documentInformation.ContentType.ToLower().Contains("jpg"))
                {
                    extention = ".jpg";
                }
                if (documentInformation.ContentType.ToLower().Contains("png"))
                {
                    extention = ".png";
                }
                if (documentInformation.ContentType.ToLower().Contains("svg"))
                {
                    extention = ".svg";
                }

                string expectedFileName = $"{filePattern}{extention}";
                string expectedOldFileName = $"{oldFilePattern}{extention}";

                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
                var bytes = Convert.FromBase64String(documentInformation.UploadedFile);
                FilePath = FilePath + @"/" + expectedFileName;

                string oldFilePath = FilePath + @"/" + expectedOldFileName;

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                System.IO.File.WriteAllBytes(FilePath, bytes);

            }
            catch (Exception networkError)
            {
                throw networkError;
            }
            return FilePath;
        }

        public static NotificationResponse SendFirebaseNotification(NotificationRequestBody request)
        {
            request.priority = "high";
            request.notification.android_channel_id = "lets_go_notification_channel";
            NotificationResponse deserializedData = new NotificationResponse();
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Authorization] = "key=AAAAQnQkYdI:APA91bGePkdr_2XIyPu5P1g35WnwMuqeWAZ3lWl1e5w74cHwtdIGhuZy9-NF1U8CG_7NwVzXz-3BDS_RFXk9KMsABIUXHTcnxRGPgSZYuCxX4E2LWsIckQ0tLlP2Y0lgXB_r1HZ4fTd2";
                    string body = JsonConvert.SerializeObject(request);
                    string svcdata = client.UploadString("https://fcm.googleapis.com/fcm/send", "POST", body);
                    deserializedData = JsonConvert.DeserializeObject<NotificationResponse>(svcdata);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    deserializedData = JsonConvert.DeserializeObject<NotificationResponse>(response);
                }
                else
                {
                    Dictionary<string, dynamic> errorDictionary = new Dictionary<string, dynamic>();
                    errorDictionary.Add("error", "no response from firebase");
                    deserializedData.failure = 1;
                    deserializedData.success = 0;
                    deserializedData.results = new List<Dictionary<string, dynamic>>() { errorDictionary };
                }
            }
            return deserializedData;
        }

        public static object GetPropertyValue(object entity, string propertyName)
        {
            Type type = entity.GetType();

            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(entity);
            }
            return null;
        }

        public static object GetFieldValue(object entity, string fieldName)
        {
            Type type = entity.GetType();
            FieldInfo fieldInfo = type.GetFields().Where(x => x.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(entity);
            }
            return null;
        }

        //public static ActionResult AsJson(object transItems)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    serializer.MaxJsonLength = Int32.MaxValue;
        //    var result = new ContentResult
        //    {
        //        Content = serializer.Serialize(transItems),
        //        ContentType = "application/json"
        //    };
        //    return result;
        //}

        public static void SetPropertyValue(ref object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var objValue = value;
                if (value.GetType() != propertyInfo.PropertyType)
                {
                    objValue = ChangeType(value, propertyInfo.PropertyType);
                }
                propertyInfo.SetValue(obj, objValue);
            }

        }

        public static void SetPropertyValue<T>(ref T obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties().Where(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (propertyInfo != null)
            {
                var objValue = value;
                if (value.GetType() != propertyInfo.PropertyType)
                {
                    objValue = ChangeType(value, propertyInfo.PropertyType);
                }
                propertyInfo.SetValue(obj, objValue);
            }

        }

        public static bool HasMethod(object obj, string methodName)
        {
            var type = obj.GetType();
            return type.GetMethod(methodName) != null;
        }

        internal static void InvokeMethod<T>(ref T obj, string methodName)
        {
            Type objType = obj.GetType();
            MethodInfo objMethod = objType.GetMethod(methodName);
            objMethod.Invoke(obj, null);
        }

        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //get match properties from source and target
            var mathProperties = sourceType.GetProperties().Where(x => targetType.GetProperties().Any(y => y.Name == x.Name));
            //  Loop through the source properties
            foreach (PropertyInfo p in mathProperties)
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                if (p.CanWrite)
                {
                    //  Set the value in the destination
                    targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
                }
            }
        }
        
        public static void CopyObject<T>(object sourceObject, ref T destObject, List<PropertyInfo> ex)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            List<PropertyInfo> lst = sourceType.GetProperties().ToList();
            lst = sourceType.GetProperties().Where(x => !ex.Any(y => y.Name == x.Name)).ToList();
            //  Loop through the source properties
            foreach (PropertyInfo p in lst)
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                if (p.CanWrite)
                {
                    //  Set the value in the destination
                    targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
                }
            }
        }

        public static TEntity CopyEntity<TEntity>(TEntity source) where TEntity : class, new()
        {

            // Get properties from EF that are read/write and not marked witht he NotMappedAttribute
            var sourceProperties = typeof(TEntity)
                                    .GetProperties()
                                    .Where(p => p.CanRead && p.CanWrite &&
                                                p.GetCustomAttributes(
                                                    typeof(System.ComponentModel.DataAnnotations.Schema.
                                                        NotMappedAttribute), true).Length == 0);
            var notVirtualProperties = sourceProperties.Where(p => !p.GetGetMethod().IsVirtual);
            var newObj = new TEntity();

            foreach (var property in notVirtualProperties)
            {
                // Copy value
                property.SetValue(newObj, property.GetValue(source, null), null);

            }
            return newObj;
        }

        public static string GetDDLValue(params object[] pks)
        {
            return string.Join("_", pks);
        }

        public static string GetDDLText(string text, string altText)
        {
            return text;
        }

        public static string GetText(string text, string altText)
        {
            return text;
        }

        public static object ChangeType(object value, Type conversionType)
        {
            var targetType = conversionType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                targetType = Nullable.GetUnderlyingType(targetType);
            }
            return Convert.ChangeType(value, targetType);
        }

        //public static string DataImportHelper(HttpPostedFileBase file, string directoryPath, string folderPath, string fileName = null)
        //{
        //    string outFileName;
        //    return DataImportHelper(file, directoryPath, folderPath, out outFileName, fileName);
        //}

        //public static string DataImportHelper(HttpPostedFileBase file, string directoryPath, string folderPath , out string outFileName, string fileName = null)
        //{
        //    string fileName_datetimePart = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_");
        //    if (!Directory.Exists(directoryPath + folderPath))
        //    {
        //        Directory.CreateDirectory(directoryPath + folderPath);
        //    }

        //    outFileName = fileName_datetimePart + (fileName ?? file.FileName);
        //    string filePath = Path.Combine(directoryPath + folderPath, outFileName);
        //    var directoryInfo = (new FileInfo(filePath)).Directory;
        //    directoryInfo?.Create();
        //    file.SaveAs(filePath);
        //    return filePath;
        //}

        //public static string FileSave(HttpPostedFileBase file, string directoryPath, string folderName, out string fileName, string overridName = null)
        //{
        //    string fileName_datetimePart = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_");
        //    if (!Directory.Exists(directoryPath + folderName))
        //    {
        //        Directory.CreateDirectory(directoryPath + folderName);
        //    }
            
        //    if (string.IsNullOrEmpty(overridName))
        //    {
        //        fileName = fileName_datetimePart + file.FileName;
        //    }
        //    else
        //    {
        //        fileName = overridName;
        //    }
            
        //    string filePath = Path.Combine(directoryPath + folderName, fileName);
        //    var directoryInfo = (new FileInfo(filePath)).Directory;
        //    directoryInfo?.Create();
        //    file.SaveAs(filePath);
        //    return filePath;
        //}
        
        internal static DateTime? ParseDateTime(string datetimeStr)
        {
            DateTime datetime = new DateTime();
            Double seconds;
            //check if string is long
            if (double.TryParse(datetimeStr, out seconds))
            {
                DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                datetime = unixEpoch.AddSeconds(seconds).ToLocalTime();
                return datetime;
            }
            else if (DateTime.TryParse(datetimeStr,out datetime))
            {
                return datetime;
            }
            return null;
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString(Constant.DateFormat);
        }

        public static string FormatDate(DateTime? date, string dateFormat)
        {
            if (date.HasValue)
            {
                dateFormat = dateFormat ?? Constant.DateFormat;
                return date.Value.ToString(dateFormat);
            }
            else
            {
                return null;
            }
        }

        internal static string FormatDateTime(DateTime datetime)
        {
            return datetime.ToString(Constant.DateTimeFormat);
        }

        internal static string FormatDateTime(DateTime datetime, string format)
        {
            return datetime.ToString(format);
        }

        internal static bool? ParseBool(string boolStr)
        {
            bool flag = false;
            if (bool.TryParse(boolStr, out flag))
            {
                return flag;
            }
            else
            {
                switch (boolStr)
                {
                    case "0":
                        return false;
                    case "1":
                        return true;
                    default:
                        break;
                }
            }
            return null;
        }

        internal static string[] GetForeignKeyValue(string fk)
        {
            return fk.Split('_');
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static IEnumerable<T> ToObjects<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetObject<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetObject<T>(DataRow dr)
        {
            Type modelType = typeof(T);
            T obj = Activator.CreateInstance<T>();
            PropertyInfo property;

            foreach (DataColumn column in dr.Table.Columns)
            {
                var value = dr[column.ColumnName];
                property = modelType.GetProperty(column.ColumnName);
                if (value != DBNull.Value)
                    property.SetValue(obj, dr[column.ColumnName], null);
            }
            return obj;
        }

        internal static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        internal static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        //internal static string GetExtension(HttpPostedFileBase file)
        //{
        //    return Path.GetExtension(file.FileName);
        //}

        

        #region Validation
        public class Validation
        {
            internal static bool HasValue_String(string value)
            {
                return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
            }

            internal static bool IsValidIdentity(string identity)
            {
                string reqExp = @"^[1-2]\d{9}$";
                return Regex.IsMatch(identity, reqExp);
            }
            
            #region Mobile Number
            internal static bool MobileNumber_IsValid(string mobileNumber)
            {
                string reqExp = @"^\+9665\d{8}$";
                return Regex.IsMatch(mobileNumber, reqExp);
            }

            internal static bool MobileNumber_IsStartWithKey(string mobileNumber)
            {
                return mobileNumber.StartsWith("+966");
            }
            internal static string MobileNumber_SetAsDefault(string mobileNumber)
            {
                if(mobileNumber.StartsWith("966"))
                {
                    return "+" + mobileNumber;
                }
                else if(mobileNumber.StartsWith("05"))
                {
                    mobileNumber = mobileNumber.Remove(0, 1);
                    return "+966" + mobileNumber;
                }
                else if (mobileNumber.StartsWith("5"))
                {
                    return "+966" + mobileNumber;
                }
                else
                {
                    return mobileNumber;
                }
            }
            #endregion

            internal static bool IsHijriDate(DateTime date)
            {
                int currentYear = DateTime.Now.Year;
                if((currentYear - date.Year) > 200)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        
        #endregion
    }

    //public class AlertMessage
    //{
    //    public Enums.AlertMessageType MessageType { get; set; }
    //    private string _MessageContent;
    //    public string MessageContent
    //    {
    //        get
    //        {
    //            if(_MessageContent == null)
    //            {
    //                _MessageContent = GetAlertMessage();
    //            }

    //            return _MessageContent;
    //        }
    //        set
    //        {
    //            _MessageContent = value;
    //        }
    //    }
    //    public int? TransactionCount { get; set; }
    //    public Enums.Transactions Transaction { get; set; }

    //    internal string GetAlertMessage()
    //    {
    //        var message = "";
    //        switch (Transaction)
    //        {
    //            case Enums.Transactions.Create:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertAddSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertAddErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertAddWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertAddInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Edit:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertEditSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertEditErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertEditWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertEditInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Delete:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Import:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertImportSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertImportErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertImportWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertImportInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Export:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertExportSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertExportErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertExportWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertExportInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Deactive:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeactiveInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Active:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertActiveSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertActiveErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertActiveWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertActiveInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.Restore:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertRestoreSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertRestoreErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertRestoreWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertRestoreInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            case Enums.Transactions.DeleteForever:
    //                switch (MessageType)
    //                {
    //                    case Enums.AlertMessageType.Success:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteForeverSuccessMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Error:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteForeverErrorMessage;
    //                        break;
    //                    case Enums.AlertMessageType.Warning:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteForeverWarningMessage;
    //                        break;
    //                    case Enums.AlertMessageType.info:
    //                        message = this.TransactionCount + " " + Resources.Resource.AlertDeleteForeverInfoMessage;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                break;
    //            default:
    //                break;
    //        }
    //        return message;
    //    }
    //    internal static string GetAlertMessage(Enums.AlertMessageType alertType, int TransactionCount, string messageContent)
    //    {
    //        return TransactionCount + " " + messageContent;
    //    }
    //}

    //public class CompareObject
    //{
    //    public static bool Compare<T>(T e1, T e2)
    //    {
    //        var type = typeof(T);
    //        if (e1 == null || e2 == null)
    //            return false;

    //        foreach (var property in type.GetProperties())
    //        {
    //            if (property.Name == "ExtensionData") continue;
    //            var object1Value = string.Empty;
    //            var object2Value = string.Empty;
    //            if (type.GetProperty(property.Name)?.GetValue(e1, null) != null)
    //                object1Value = type.GetProperty(property.Name)?.GetValue(e1, null).ToString();
    //            if (type.GetProperty(property.Name)?.GetValue(e2, null) != null)
    //                object2Value = type.GetProperty(property.Name)?.GetValue(e2, null).ToString();
    //            if (object2Value != null && (object1Value != null && object1Value.Trim() != object2Value.Trim()))
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }
    //}

    //public class JsonUtility
    //{
    //    //public static T Deserialize<T>(string json)
    //    //{
    //    //    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
    //    //    json_serializer.MaxJsonLength = int.MaxValue;
    //    //    return (T)json_serializer.Deserialize(json, typeof(T));
    //    //}
    //}

    ////public class SecurityUtility
    ////{
    ////    public UserViewModel User = new UserViewModel();
    ////    public Guid ServiceId;
    ////    public IEnumerable<UserRoleServiceAccessViewModel> UserPermission { get; set; }

    ////    public SecurityUtility(UserViewModel user, Guid serviceId)
    ////    {
    ////        this.User = user;
    ////        this.ServiceId = serviceId;
    ////        this.UserPermission = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetSavedUserPermission();
    ////    }

    ////    public bool UserHasPermission(Guid accessTypeId)
    ////    {
    ////        if (UserPermission == null)
    ////        {
    ////            UserPermission = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetSavedUserPermission();
    ////        }

    ////        bool grantPermission = false;
    ////        if (User.UserTypeId == DBEnums.UserType.Super_System_Administrator)
    ////        {
    ////            grantPermission = true;
    ////        }
    ////        else
    ////        {
    ////            grantPermission = UserPermission.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == accessTypeId);
    ////        }
    ////        return grantPermission;
    ////    }

    ////    public bool UserHasPermission(Guid Service_Id, Guid accessTypeId)
    ////    {
    ////        if (UserPermission == null)
    ////        {
    ////            UserPermission = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetSavedUserPermission();
    ////        }

    ////        bool grantPermission = false;
    ////        if (User.UserTypeId == DBEnums.UserType.Super_System_Administrator)
    ////        {
    ////            grantPermission = true;
    ////        }
    ////        else
    ////        {
    ////            grantPermission = UserPermission.Any(x => x.ServiceId == Service_Id && x.AccessTypeId == accessTypeId);
    ////        }
    ////        return grantPermission;
    ////    }

    ////    public IEnumerable<GenericDataFormat.FilterItem> GetUserServiceAccessConditionAsFilter(Guid serviceId, Guid accessTypeId)
    ////    {
    ////        IEnumerable<UserRoleServiceAccessConditionViewModel> serviceAccessCondition_lst = new UserRoleServiceAccessConditionModel<UserRoleServiceAccessConditionViewModel>().GetData(UserId: User.UserId, ServiceId: serviceId, AccessTypeId: accessTypeId);
    ////        if (serviceAccessCondition_lst != null)
    ////        {
    ////            serviceAccessCondition_lst = serviceAccessCondition_lst.Where(x => !string.IsNullOrEmpty(x.ConditionColumnName) && !string.IsNullOrEmpty(x.ConditionComparisonOperator)).OrderBy(x => x.RoleServiceAccessConditionId);
    ////            if (serviceAccessCondition_lst.Any())
    ////            {
    ////                return serviceAccessCondition_lst.Select(x => Utility.GetFilterFromCondition(x));
    ////            }
    ////        }
    ////        return null;
    ////    }
    ////}

    public class CustomSelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
        public string Group { get; set; }
        public bool Disabled { get; set; }
    }

    public static class ViewModelExtension
    {
        public static void Get_Create_Modify_User(this ICommonViewEntity viewModel, bool isBlock, bool isDelete, Guid createUserId, Guid? modifyUserId)
        {
            viewModel.IsBlock_str = isBlock ? "true" : "false";
            viewModel.IsDeleted_str = isDelete ? "true" : "false";

            string CreateUser_FullName = null, CreateUser_FullAltName = null,
                ModifyUser_FullName = null, ModifyUser_FullAltName = null;
            new Mawid.BackEnd.Models.UserModel().Get_Create_Modify_User(createUserId, modifyUserId, ref CreateUser_FullName,
                ref CreateUser_FullAltName, ref ModifyUser_FullName, ref ModifyUser_FullAltName);

            viewModel.CreateUser_FullName = CreateUser_FullName;
            viewModel.CreateUser_FullAltName = CreateUser_FullAltName;
            viewModel.ModifyUser_FullName = ModifyUser_FullName;
            viewModel.ModifyUser_FullAltName = ModifyUser_FullAltName;
        }
    }

    


}