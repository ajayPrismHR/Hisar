using System;
using System.Drawing;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Globalization;
using System.Resources;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Data;
using System.Threading;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Net.NetworkInformation;
using ComplaintTracker;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using System.Web;

namespace ComplaintTracker
{
    public class HelperClass
    {
     


        #region Global Variables for login access
        private static int _LoginClientId;

        private static int _loginUserId;
        private static string _loginUserName;

        private static int _loginCompanyId;
        private static int _loginRoleId;
        private static int _ShiftId;
        private static string _applicationversion;


        public static int LoginUserId
        {
            get
            {
                return _loginUserId;
            }
            set
            {
                _loginUserId = value;
            }
        }
        public static int LoginCompanyId
        {
            get
            {
                return _loginCompanyId;
            }
            set
            {
                _loginCompanyId = value;
            }
        }
        public static int LoginRoleId
        {
            get
            {
                return _loginRoleId;
            }
            set
            {
                _loginRoleId = value;
            }
        }
        public static string LoginUserName
        {
            get
            {
                return _loginUserName;
            }
            set
            {
                _loginUserName = value;
            }
        }
        public static int LoginClientId
        {
            get
            {
                return _LoginClientId;
            }
            set
            {
                _LoginClientId = value;
            }
        }

        public static int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                _ShiftId = value;
            }
        }

        public static int InvoiceDealId
        {
            get;
            set;
        }
        public static string Applicationversion
        {
            get
            {
                return _applicationversion;
            }
            set
            {
                _applicationversion = value;
            }
        }


        #endregion



        #region Connection
        public static string Connection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DbconAvvnl"].ConnectionString;
            }
        }

        #endregion




       

        static bool invalidEmail = false;
        public static bool IsValidEmail(string strIn)
        {

            if (String.IsNullOrEmpty(strIn))
                return false;

            if (invalidEmail)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

       

        public static bool IsValidNumber(string strIn)
        {
            double Num;
            bool isNum = double.TryParse(strIn, out Num);
            return isNum;
        }

        public static bool IsValidInteger(string strIn)
        {
            int Num;
            bool isNum = Int32.TryParse(strIn, out Num);
            return isNum;
        }

        public static XmlDocument GetEntityXml<T>(List<T> objList)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XPathNavigator nav = xmlDoc.CreateNavigator();
            using (XmlWriter writer = nav.AppendChild())
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<T>));
                ser.Serialize(writer, objList);
            }
            return xmlDoc;
        }




        #region User role Combo
        public static DataTable GetUserRole()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("UserRole");

            // 1 super admin, 2 > shop admin, 3 > pos user

            DataRow dRow = dt.NewRow();

            //dRow["Id"] = 1;
            //dRow["UserRole"] = "Super Admin";
            //dt.Rows.Add(dRow);

            dRow = dt.NewRow();
            dRow["Id"] = 2;
            dRow["UserRole"] = "Shop Admin";
            dt.Rows.Add(dRow);

            dRow = dt.NewRow();
            dRow["Id"] = 3;
            dRow["UserRole"] = "POS User";
            dt.Rows.Add(dRow);



            return dt;
        }
        #endregion

       

        #region ReportDataBaseCredentials
        public static string GetDatabaseServerName
        {
            get
            {


                //return @".\MYSQLVPSL";
                return @".";
                // return @"216.67.248.114";
                //return _ServerName;


            }
        }
        public static string GetDatabaseName
        {
            get
            {

                // return @"K";
                return @"S";


            }
        }
        public static string GetDatabaseUserName
        {
            get
            {
                // return @"newtestdb1user"; 
                return @"";

            }
        }
        public static string GetDatabasePassword
        {
            get
            {
                // return @"newtestdb"; 
                return @"";
            }
        }
        #endregion




        #region Folder Access
        public static void FolderACL(String folderPath)
        {
            String accountName = "Everyone";
            FileSystemRights Rights;

            //What rights are we setting?

            Rights = FileSystemRights.FullControl;
            bool modified;
            InheritanceFlags none = new InheritanceFlags();
            none = InheritanceFlags.None;

            //set on dir itself
            FileSystemAccessRule accessRule = new FileSystemAccessRule(accountName, Rights, none, PropagationFlags.NoPropagateInherit, AccessControlType.Allow);
            DirectoryInfo dInfo = new DirectoryInfo(folderPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.ModifyAccessRule(AccessControlModification.Set, accessRule, out modified);

            //Always allow objects to inherit on a directory 
            InheritanceFlags iFlags = new InheritanceFlags();
            iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

            //Add Access rule for the inheritance
            FileSystemAccessRule accessRule2 = new FileSystemAccessRule(accountName, Rights, iFlags, PropagationFlags.InheritOnly, AccessControlType.Allow);
            dSecurity.ModifyAccessRule(AccessControlModification.Add, accessRule2, out modified);

            dInfo.SetAccessControl(dSecurity);
        }
        #endregion

        #region GetMacAddress And Registry key
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        public static string GetKey()
        {
            string strKeyName = "AK_ACMEKEY";
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(strKeyName);
            if (key == null)
            {
                return "";
            }
            else
            {
                string keyVal = key.GetValue("AK_ACMEKEY").ToString();
                key.Close();
                return keyVal;
            }
        }
        #endregion

        public static void GetApplicationVersion()
        {
            System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            var version = fieVersionInfo.FileVersion;
            Applicationversion = "GST ver." + version;
        }


        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string GetIPHelper()
        {
            string ip = "0";
            try
            {
                ip = HttpContext.Current.Request.UserHostAddress;
                return ip;
            }
            catch (Exception ex) 
            {
                
            }
            return ip;
            
        }

    }

}
