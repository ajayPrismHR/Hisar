using ComplaintTracker;
using DirectComplaintRegister.ExternalAPI;
using ComplaintTracker.Handler;
using DirectComplaintRegister.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml.Linq;

namespace DirectComplaintRegister.DAL
{
    public static class RepositoryArea
    {
        static readonly Serilog.ILogger log = EventLogger._log;
        public static List<COMPLAINT> SearchComplaint()
        {
            List<COMPLAINT> lstComplaint = new List<COMPLAINT>();


            //DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode");

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    objBlank = new ModelOfficeCode();
            //    objBlank.OfficeCode = dr.ItemArray[0].ToString();
            //    objBlank.OfficeId = dr.ItemArray[1].ToString();
            //    lstOfficeCode.Add(objBlank);
            //}
            return lstComplaint;
        }
        public static List<ModelOfficeCode> GetOfficeList(String OFFICE_ID)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            objBlank.OfficeId = "0";
            objBlank.OfficeCode = "Select Office Code";
            lstOfficeCode.Insert(0, objBlank);
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return lstOfficeCode;
        }

        public static List<ModelOfficeCode> GetOfficeListCircle(String OFFICE_ID)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            objBlank.OfficeId = "0";
            objBlank.OfficeCode = "Select Office Code";
            lstOfficeCode.Insert(0, objBlank);
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCodeCircle", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return lstOfficeCode;
        }
        public static List<ModelOfficeCode> GetOfficeList_Create(String OFFICE_ID)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            //objBlank.OfficeId = "0";
            //objBlank.OfficeCode = "Select Office Code";
            //lstOfficeCode.Insert(0, objBlank);
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode_CREATE", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return lstOfficeCode;
        }
        public static List<ModelComplaintType> GetComplaintTypeList(string OFFICE_ID)
        {
            List<ModelComplaintType> obj = new List<ModelComplaintType>();
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintType", param);
            //ds.Tables[0].Rows.RemoveAt(0);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintType
                    {
                        ComplaintTypeId = Convert.ToInt32(dr["Id"]),
                        ComplaintType = Convert.ToString(dr["Complaint_Type"]),
                        ComplaintTileColor = Convert.ToString(dr["TileColor"]),
                        Status = Convert.ToBoolean(dr["IS_ACTIVE"]),
                        COMPLAINT_COUNT = Convert.ToString(dr["COMPLAINT_COUNT"]),
                    }
                    );
            }
            return (obj);
        }
        public static List<ModelComplaintType> GetSubComplaintTypeList(int ComplaintTypeId)
        {
            List<ModelComplaintType> obj = new List<ModelComplaintType>();
            SqlParameter[] param ={
                    new SqlParameter("@ComplaintTypeId",ComplaintTypeId)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetSubComplaintByComplaintType", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintType
                    {
                        SubComplaintTypeId = Convert.ToInt32(dr["Id"]),
                        SubComplaintType = Convert.ToString(dr["SUB_COMPLAINT_TYPE"]),
                        Status = Convert.ToBoolean(dr["IS_ACTIVE"]),
                    }
                    );
            }
            return (obj);
        }
        public static List<COMPLAINT> GetPreviousComplaint(int OfficeCode, int ConsumerType, string searchparm)
        {
            List<COMPLAINT> obj = new List<COMPLAINT>();
            SqlParameter[] param ={
                    new SqlParameter("@OfficeCode",OfficeCode),
                    new SqlParameter("@ConsumerType",ConsumerType),
                    new SqlParameter("@Searchparm",searchparm) };

            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetPreviousComplaint", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new COMPLAINT
                    {
                        //Consumer Info
                        SDO_CODE = Convert.ToString(dr["SDO_CODE"]),

                        ADDRESS1 = Convert.ToString(dr["ADDRESS1"]),
                        ADDRESS2 = Convert.ToString(dr["ADDRESS2"]),
                        ADDRESS3 = Convert.ToString(dr["ADDRESS3"]),
                        NAME = Convert.ToString(dr["NAME"]),
                        FATHER_NAME = Convert.ToString(dr["FATHER_NAME"]),
                        KNO = Convert.ToString(dr["KNO"]),
                        LANDMARK = Convert.ToString(dr["LANDMARK"]),
                        LANDLINE_NO = Convert.ToString(dr["LANDLINE_NO"]),
                        MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]),
                        ALTERNATE_MOBILE_NO = Convert.ToString(dr["ALTERNATE_MOBILE_NO"]),
                        CONSUMER_STATUS = Convert.ToString(dr["CONSUMER_STATUS"]),
                        EMAIL = Convert.ToString(dr["EMAIL"]),
                        FEEDER_NAME = Convert.ToString(dr["FEEDER_NAME"]),
                        ACCOUNT_NO = Convert.ToString(dr["ACCOUNT_NO"]),
                        AREA_CODE = Convert.ToString(dr["AREA_CODE"]),

                    }
                    );
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                obj.Add(

                    new COMPLAINT
                    {
                        //Consumer Previous Complaint
                        Complaintdate = Convert.ToString(dr["Complaintdate"]),
                        COMPLAINT_NO = Convert.ToString(dr["COMPLAINT_NO"]),
                        COMPLAINT_TYPE = Convert.ToString(dr["COMPLAINT_TYPE"]),
                        REMARKS = Convert.ToString(dr["REMARKS"]),
                        COMPLAINT_status = Convert.ToString(dr["COMPLAINT_status"]),
                    }
                    );
            }
            return (obj);
        }
       


        public static async Task<Int64> SaveComplaintRegistration(COMPLAINT modelComplaint)
        {
            Int64 retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelComplaint;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;


            SqlParameter parmretComplaint_no = new SqlParameter();
            parmretComplaint_no.ParameterName = "@retComplaint_no";
            parmretComplaint_no.DbType = DbType.Int64;
            parmretComplaint_no.Size = 8;
            parmretComplaint_no.Direction = ParameterDirection.Output;




            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_CODE",modelComplaint.OFFICE_CODE_ID),
                    new SqlParameter("@CONSUMER_TYPE",modelComplaint.CONSUMER_TYPE),
                    new SqlParameter("@COMPLAINT_TYPE",modelComplaint.ComplaintTypeId),
                    new SqlParameter("@COMPLAINT_SOURCE_ID",15),
                    new SqlParameter("@COMPLAINT_CURRENT_STATUS_ID",3),//modelComplaint.COMPLAINT_status_ID),
                    new SqlParameter("@JE_AREA_ID",modelComplaint.JE_AREA),
                    new SqlParameter("@COMPLAINT_status",1),//modelComplaint.COMPLAINT_status_ID),
                    new SqlParameter("@SDO_CODE",modelComplaint.SDO_CODE),
                    new SqlParameter("@NAME",modelComplaint.NAME),

                    new SqlParameter("@FATHER_NAME",modelComplaint.FATHER_NAME),
                    new SqlParameter("@KNO",modelComplaint.KNO),
                    new SqlParameter("@LANDLINE_NO",modelComplaint.LANDLINE_NO),
                    new SqlParameter("@MOBILE_NO",modelComplaint.MOBILE_NO),
                    new SqlParameter("@ALTERNATE_MOBILE_NO",modelComplaint.ALTERNATE_MOBILE_NO),
                    new SqlParameter("@EMAIL",modelComplaint.EMAIL),
                    new SqlParameter("@ACCOUNT_NO",modelComplaint.ACCOUNT_NO),
                    new SqlParameter("@ADDRESS1",modelComplaint.ADDRESS1),
                    new SqlParameter("@ADDRESS2",modelComplaint.ADDRESS2),
                    new SqlParameter("@ADDRESS3",modelComplaint.ADDRESS3),

                    new SqlParameter("@LANDMARK",modelComplaint.LANDMARK),
                    new SqlParameter("@CONSUMER_STATUS",modelComplaint.CONSUMER_STATUS),
                    new SqlParameter("@FEEDER_NAME",modelComplaint.FEEDER_NAME),
                    new SqlParameter("@AREA_CODE",modelComplaint.AREA_CODE),
                    new SqlParameter("@SUB_COMPLAINT_TYPE",modelComplaint.SUB_COMPLAINT_TYPE_ID),//modelComplaint.SUB_COMPLAINT_TYPE_ID),
                    new SqlParameter("@REMARKS",modelComplaint.REMARKS),
                    new SqlParameter("@IS_ACTIVE",true),
                    new SqlParameter("@IS_DELETED",false),
                    new SqlParameter("@TIME_STAMP",DateTime.Now),
                    new SqlParameter("@IS_UPDATED",false),

                    new SqlParameter("@UPDATED_TIME_STAMP",DateTime.Now),
                    new SqlParameter("@PROCESS","I"),
                    new SqlParameter("@REMARK",modelComplaint.REMARKS),
                    new SqlParameter("@REMARK_DATE_TIME",DateTime.Now),
                     new SqlParameter("@USER_ID",1),
                     new SqlParameter("@AssigneeId",1),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_REGISTER", param);



                if (param[37].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt64(param[37].Value);


                if (retStatus > 0)
                {
                    log.Information("In Image Save");
                    SqlParameter[] paramImg ={
                    new SqlParameter("@Complaint_NO",retStatus),
                    new SqlParameter("@Image",retStatus + "-"  + modelComplaint.ImageData) 
                    };

                    SqlHelper.ExecuteNonQuery(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Image_Save", paramImg);
                    log.Information("Image Saveed");
                }


                log.Information(retStatus.ToString());

                if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
                {
                    log.Information(modelComplaint.MOBILE_NO.ToString());
                    ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
                    TextSmsAPI textSmsAPI = new TextSmsAPI();

                    modelSmsAPI.To = "91" + modelComplaint.MOBILE_NO.ToString();
                    modelSmsAPI.Smstext = "Dear Consumer,Your Complaint has been registered with complaint No. " + retStatus + " on Date: " + DateTime.Now.ToString("dd-MMM-yyyy") + " AVVNL";
                    string response = await textSmsAPI.RegisterComplaintSMS(modelSmsAPI);
                    modelComplaint.SMS = modelSmsAPI.Smstext;
                    log.Information(response.ToString());

                    //if (response.Contains("avvnlalt"))
                    //{
                    PUSH_SMS_DETAIL_Consumer(modelComplaint, response);
                    //}
                }
                else
                    retStatus = 0;
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }
            return retStatus;

        }




        public static bool IsDuplicateComplaint(string kNO)
        {

            SqlParameter[] param = { new SqlParameter("@KNO", kNO) };

            object requestCount = SqlHelper.ExecuteScalar(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "IsDuplicateRequest", param);

            if (Convert.ToInt16(requestCount) == 0)
            {
                return false;
            }

            return true;

        }
        public static List<SelectListItem> ComplaintSourceJson()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintSource");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> ComplaintSourceJson_Register()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintSource_register");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        



        private static readonly IDictionary<Type, ICollection<PropertyInfo>> _Properties =
      new Dictionary<Type, ICollection<PropertyInfo>>();

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static IEnumerable<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var objType = typeof(T);
                ICollection<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable().Skip(1))
                {
                    var obj = new T();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

                            prop.SetValue(obj, safeValue, null);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }


       
   
      

       


       


        public static List<SelectListItem> UserList(int RoleId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                    new SqlParameter("@RoleId",RoleId)};
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetUserList", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        #region Sourabh
        

        public static COMPLAINT ChangeComplaintType(Int64 id, string s, int user_id)
        {
            COMPLAINT obj = new COMPLAINT();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;
            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",id),
                    new SqlParameter("@PROCESS",s),
                    new SqlParameter("@USER_ID",user_id),
                    parmretStatus,parmretMsg
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "CHANGE_COMPLAINT_TYPE", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.COMPLAINT_NO = Convert.ToString(dr["COMPLAINT_NO"]);
                obj.NAME = Convert.ToString(dr["NAME"]);
                obj.FATHER_NAME = Convert.ToString(dr["FATHER_NAME"]);
                obj.KNO = Convert.ToString(dr["KNO"]);
                obj.MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                obj.ALTERNATE_MOBILE_NO = Convert.ToString(dr["ALTERNATE_MOBILE_NO"]);
                obj.EMAIL = Convert.ToString(dr["EMAIL"]);
                obj.COMPLAINT_TYPE = Convert.ToString(dr["COMPLAINT_TYPE"]);
                obj.ADDRESS1 = Convert.ToString(dr["ADDRESS1"]);
                obj.ADDRESS2 = Convert.ToString(dr["ADDRESS2"]);
                obj.ADDRESS3 = Convert.ToString(dr["ADDRESS3"]);
                obj.LANDMARK = Convert.ToString(dr["LANDMARK"]);
                obj.AREA_CODE = Convert.ToString(dr["AREA_CODE"]);
                obj.CONSUMER_STATUS = Convert.ToString(dr["CONSUMER_STATUS"]);
                obj.FEEDER_NAME = Convert.ToString(dr["FEEDER_NAME"]);
                obj.SUB_COMPLAINT_TYPE_ID = Convert.ToInt32(dr["SUB_COMPLAINT_TYPE_ID"]);
                obj.ComplaintTypeId = Convert.ToInt32(dr["COMPLAINT_TYPE_ID"]);

            }
            return (obj);
        }

     
      
        public static COMPLAINT GetMobileEmail(Int64 id)
        {
            COMPLAINT obj = new COMPLAINT();
            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",id),
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetail", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                obj.EMAIL = Convert.ToString(dr["EMAIL"]);
                obj.Complaintdate = Convert.ToString(dr["Complaint_date"]);
                obj.OFFICE_CODE_ID = Convert.ToInt64(dr["OFFICE_CODE"]);
                obj.SDO_CODE = dr["SDO_CODE"].ToString();
                obj.KNO = dr["KNO"].ToString();
                obj.NAME = dr["NAME"].ToString();
                obj.FATHER_NAME = dr["FATHER_NAME"].ToString();
                obj.ADDRESS1 = dr["ADDRESS1"].ToString();
                obj.ADDRESS2 = dr["ADDRESS2"].ToString();
                obj.ADDRESS3 = dr["ADDRESS3"].ToString();
                obj.LANDMARK = dr["LANDMARK"].ToString();
                obj.LANDLINE_NO = dr["LANDLINE_NO"].ToString();
                obj.MOBILE_NO = dr["MOBILE_NO"].ToString();
                obj.ALTERNATE_MOBILE_NO = dr["ALTERNATE_MOBILE_NO"].ToString();
                obj.CONSUMER_STATUS = dr["CONSUMER_STATUS"].ToString();
                obj.EMAIL = dr["EMAIL"].ToString();
                obj.FEEDER_NAME = dr["FEEDER_NAME"].ToString();
                obj.ACCOUNT_NO = dr["ACCOUNT_NO"].ToString();
                obj.AREA_CODE = dr["AREA_CODE"].ToString();
                obj.COMPLAINT_NO = id.ToString();


            }
            return (obj);
        }
        public static List<ModelComplaintAssign> GetAssigneeList()
        {
            List<ModelComplaintAssign> obj = new List<ModelComplaintAssign>();
            DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintAssignee");
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintAssign
                    {
                        AssignId = Convert.ToInt32(dr["ID"]),
                        AssigneeName = Convert.ToString(dr["ASSIGNEE"]),
                    }
                    );
            }
            return (obj);
        }
        public static async Task<int> ChangeAssignee_Save(COMPLAINT modelRemark, int UserID)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                new SqlParameter("@complaint_no",modelRemark.COMPLAINT_NO),
                    new SqlParameter("@ASSIGNEE",modelRemark.ASSIGNEEId),
                    new SqlParameter("@MOBILE_NO",modelRemark.MOBILE_NO),
                    new SqlParameter("@EMAIL",modelRemark.EMAIL),
                    new SqlParameter("@SMS",modelRemark.SMS),
                    new SqlParameter("@EMAIL_SENT",modelRemark.EMAIL_SEND),
                    new SqlParameter("@USER_ID",UserID),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "Save_Complaint_Assignee", param);

                if (param[7].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[7].Value);
                else
                    retStatus = 0;
                log.Information(modelRemark.MOBILE_NO.ToString());
                ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
                TextSmsAPI textSmsAPI = new TextSmsAPI();
                modelSmsAPI.To = "91" + modelRemark.MOBILE_NO.ToString();
                modelSmsAPI.Smstext = modelRemark.SMS;
                string response = await textSmsAPI.RegisterComplaintSMS(modelSmsAPI);
                log.Information(response.ToString());
                PUSH_SMS_DETAIL_Consumer(modelRemark, response);
                if (modelRemark.Assign_FRTMobile.Length == 10)
                {
                    SqlParameter[] param1 ={
                    new SqlParameter("@COMPLAINT_NO",modelRemark.COMPLAINT_NO),
                                       };
                    DataSet ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetail", param1);
                    //Bind Complaint generic list using dataRow     
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        modelRemark.Complaintdate = Convert.ToString(dr["Complaint_date"]);
                        modelRemark.NAME = dr["NAME"].ToString();
                        modelRemark.FATHER_NAME = dr["FATHER_NAME"].ToString();
                        modelRemark.ADDRESS1 = dr["ADDRESS1"].ToString();
                        modelRemark.ADDRESS2 = dr["ADDRESS2"].ToString();
                        modelRemark.ADDRESS3 = dr["ADDRESS3"].ToString();
                        modelRemark.LANDMARK = dr["LANDMARK"].ToString();
                        modelRemark.COMPLAINT_TYPE = dr["COMPLAINT_TYPE"].ToString();
                        modelRemark.ALTERNATE_MOBILE_NO = dr["ALTERNATE_MOBILE_NO"].ToString();

                    }
                    log.Information(modelRemark.MOBILE_NO.ToString());
                    ModelSmsAPI modelSmsAPI_FRT = new ModelSmsAPI();
                    TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                    modelSmsAPI_FRT.To = "91" + modelRemark.Assign_FRTMobile.ToString();
                    modelSmsAPI_FRT.Smstext = "Dear FRT Complaint has been assigned to you with the details below  COMPLAINT TYPE :" + modelRemark.COMPLAINT_TYPE + " ,COMPLAINT NO: " + modelRemark.COMPLAINT_NO + " ,NAME OF CONSUMER: " + modelRemark.NAME + " ,ADDRESS OF CONSUMER: " + modelRemark.ADDRESS1 + "," + modelRemark.ADDRESS2 + "," + modelRemark.ADDRESS3 + ", Mobile No. " + modelRemark.MOBILE_NO + ", Alternate Mobile No. " +modelRemark.ALTERNATE_MOBILE_NO + " AVVNL";
                    string response1 = await textSmsAPI1.RegisterComplaintSMS(modelSmsAPI_FRT);
                    modelRemark.SMS = modelSmsAPI_FRT.Smstext;
                    modelRemark.MOBILE_NO = modelSmsAPI_FRT.To;
                    log.Information(response1.ToString());
                    PUSH_SMS_DETAIL_Consumer(modelRemark, response1);
                }
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }



            return retStatus;

        }

        public static int ReopenComplaint(Int64 s, int userID)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;
            SqlParameter[] param ={
                new SqlParameter("@COMPLAINT_NO",s),
                new SqlParameter("@UserID",userID),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Repoen", param);

                if (param[2].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[2].Value);
                else
                    retStatus = 0;
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }



            return retStatus;

        }

        public static DataSet ShowComplaintDetails(string id)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param ={
                new SqlParameter("@Complaint_no",id)};
            try
            {
                //SqlHelper.ExecuteNonQuery(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "ChangePassword", param);

                ds = SqlHelper.ExecuteDataset(ComplaintTracker.HelperClass.Connection, CommandType.StoredProcedure, "ShowComplaint_Detail", param);

            }
            catch (Exception ex)
            {
                ds = new DataSet();
            }
            return ds;
        }

        #endregion



        public static List<COMPLAINT> GetConsumerDetails(string searchParm)
        {
            List<COMPLAINT> obj = new List<COMPLAINT>();
            DataSet ds = new DataSet();
            SqlParameter[] param = { new SqlParameter("@searchParm", searchParm) };
            try
            {
                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetConsumerDetails", param);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj.Add(
                        new COMPLAINT
                        {

                            NAME = Convert.ToString(dr["NAME"]),
                            ADDRESS1 = Convert.ToString(dr["ADDRESS1"]),
                            ADDRESS2 = Convert.ToString(dr["ADDRESS2"]),
                            ADDRESS3 = Convert.ToString(dr["ADDRESS3"]),
                            KNO = Convert.ToString(dr["KNO"]),
                            MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]),

                        }
                        );
                }

            }
            catch (Exception ex)
            {
                return obj;
            }
            return obj;
        }

        public static int PUSH_SMS_DETAIL_Consumer(COMPLAINT modelRemark, string response)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;



            SqlParameter[] param ={

                new SqlParameter("@PHONE_NO",modelRemark.MOBILE_NO),
                    new SqlParameter("@TEXT_MEESAGE",modelRemark.SMS),
                    new SqlParameter("@DELIVERY_RESPONSE",response),
                    new SqlParameter("@REMARK","SMS SENT")};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PUSH_SMS_DETAIL", param);
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }

            return retStatus;

        }

    }
}
