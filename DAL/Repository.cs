using ComplaintTracker.ExternalAPI;
using ComplaintTracker.Handler;
using ComplaintTracker.Models;
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

namespace ComplaintTracker.DAL
{
    public static class Repository
    {
        static readonly Serilog.ILogger log = EventLogger._log;
        public static List<COMPLAINT> SearchComplaint()
        {
            List<COMPLAINT> lstComplaint = new List<COMPLAINT>();


            //DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode");

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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode_CREATE", param);

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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCodeCircle", param);

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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode_CREATE", param);

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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintType", param);
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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetSubComplaintByComplaintType", param);
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

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetPreviousComplaint", param);
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
        public static List<ModelComplaintDashboard> GetComplaintTypeSummary(int OfficeCode, int ComplaintType)
        {
            List<ModelComplaintDashboard> obj = new List<ModelComplaintDashboard>();
            SqlParameter[] param ={
                    new SqlParameter("@OfficeCode",OfficeCode),
                    new SqlParameter("@ComplaintType",ComplaintType),
                   };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintTypeSummary", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintDashboard
                    {
                        SDOName = Convert.ToString(dr["SDOName"]),
                        PreviousDayPending = Convert.ToString(dr["PreviousDayPending"]),
                        TodayRegister = Convert.ToString(dr["TodayRegister"]),
                        TotalComplaint = Convert.ToString(dr["TotalComplaint"]),
                        PreviousResolvedComplaint = Convert.ToString(dr["PreviousResolvedComplaint"]),
                        TodayResolved = Convert.ToString(dr["TodayResolved"]),
                        TotalResolved = Convert.ToString(dr["TotalResolved"]),
                        TotalPending = Convert.ToString(dr["TotalPending"]),
                    }
                    );
            }
            return (obj);
        }

        public static async Task<string> SaveComplaint(COMPLAINT modelComplaint)
        {
            string retStatus = "";
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
            parmretComplaint_no.DbType = DbType.String;
            parmretComplaint_no.Size = 8;
            parmretComplaint_no.Direction = ParameterDirection.Output;




            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_CODE",modelComplaint.OFFICE_CODE_ID),
                    new SqlParameter("@Complaint_Center",modelComplaint.JE_AREA),
                    new SqlParameter("@CustomerName",modelComplaint.NAME),
                    new SqlParameter("@Complaint_no",modelComplaint.COMPLAINT_NO),

                    new SqlParameter("@MobileNo",modelComplaint.MOBILE_NO),
                    new SqlParameter("@Consumer_No",modelComplaint.ACCOUNT_NO),
                    new SqlParameter("@Address",modelComplaint.ADDRESS1),
                    new SqlParameter("@Complaint_comment",modelComplaint.REMARKS),
                     new SqlParameter("@USER_ID",modelComplaint.UserId),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_REGISTER", param);



                if (param[11].Value != DBNull.Value)// status
                    retStatus = Convert.ToString(param[11].Value);

                log.Information(retStatus.ToString());

            }
            catch (Exception ex)
            {
                retStatus = "0";
            }
            return retStatus;

        }


        public static async Task<Int64> SaveComplaintNCMS(COMPLAINT modelComplaint)
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
                    new SqlParameter("@COMPLAINT_TYPE",14),
                    new SqlParameter("@COMPLAINT_SOURCE_ID",modelComplaint.ComplaintSource),//modelComplaint.com),
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
                    new SqlParameter("@SUB_COMPLAINT_TYPE",47),//modelComplaint.SUB_COMPLAINT_TYPE_ID),
                    new SqlParameter("@REMARKS",modelComplaint.REMARKS),
                    new SqlParameter("@IS_ACTIVE",true),
                    new SqlParameter("@IS_DELETED",false),
                    new SqlParameter("@TIME_STAMP",DateTime.Now),
                    new SqlParameter("@IS_UPDATED",false),

                    new SqlParameter("@UPDATED_TIME_STAMP",DateTime.Now),
                    new SqlParameter("@PROCESS","I"),
                    new SqlParameter("@REMARK",modelComplaint.REMARKS),
                    new SqlParameter("@REMARK_DATE_TIME",DateTime.Now),
                     new SqlParameter("@USER_ID",modelComplaint.UserId),
                     new SqlParameter("@AssigneeId",1),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_REGISTER", param);



                if (param[37].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt64(param[37].Value);

                log.Information(retStatus.ToString());

                if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
                {
                    log.Information(modelComplaint.MOBILE_NO.ToString());
                    ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
                    TextSmsAPI textSmsAPI = new TextSmsAPI();

                    modelSmsAPI.To = "91" + modelComplaint.MOBILE_NO.ToString();
                    modelSmsAPI.Smstext = String.Format(modelSmsAPI.NCMS_ConsumerSMS, retStatus);
                    string response = await textSmsAPI.RegisterComplaintSMS(modelSmsAPI);
                    modelComplaint.SMS = modelSmsAPI.Smstext;
                    log.Information(response.ToString());

                    //if (response.Contains("avvnlalt"))
                    //{
                    PUSH_SMS_DETAIL_Consumer(modelComplaint, response);
                    //}
                    string retStatus1;
                    string ccMobileNo = GET_CC_MOBILE_NO(modelComplaint.SDO_CODE);
                    //string Name = GET_COMPLAINT_DETAIL(retStatus);
                    string name = string.Empty;
                    string Address = string.Empty;
                    string Landmark = string.Empty;
                    string MobileNo = string.Empty;
                    DataSet ds = GET_COMPLAINT_DETAIL(retStatus);

                    if (!string.IsNullOrEmpty(ccMobileNo))
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            //Consumer Info
                            name = Convert.ToString(dr["NAME"]);

                            Address = Convert.ToString(dr["Address"]);
                            Landmark = Convert.ToString(dr["LANDMARK"]);
                            MobileNo = Convert.ToString(dr["MOBILE_NO"]);
                            
                        }
                        ModelSmsAPI modelSmsCCAPI = new ModelSmsAPI();
                        modelSmsCCAPI.To = "91" + ccMobileNo;
                        modelSmsCCAPI.Smstext = String.Format(modelSmsCCAPI.NCMS_CCSMS, retStatus,name,Address,Landmark,MobileNo);
                        string responseCC = await textSmsAPI.RegisterComplaintSMS(modelSmsCCAPI);

                        PUSH_SMS_DETAIL_Consumer1(ccMobileNo, modelSmsCCAPI.Smstext, responseCC);
                    }



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

        public static async Task<Int64> SaveComplaintRegistration(COMPLAINT modelComplaint,string iMageName)
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
                     new SqlParameter("@USER_ID",modelComplaint.UserId),
                     new SqlParameter("@AssigneeId",1),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_REGISTER", param);



                if (param[37].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt64(param[37].Value);


                if (retStatus > 0)
                {
                    log.Information("In Image Save");
                    SqlParameter[] paramImg ={
                    new SqlParameter("@Complaint_NO",retStatus),
                    new SqlParameter("@Image",iMageName) 
                    };

                    SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Image_Save", paramImg);
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



        public static async Task<Int64> EditComplaint(COMPLAINT modelComplaint)
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
            parmretComplaint_no.DbType = DbType.Int32;
            parmretComplaint_no.Size = 8;
            parmretComplaint_no.Direction = ParameterDirection.Output;




            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",modelComplaint.COMPLAINT_NO),
                    new SqlParameter("@NAME",modelComplaint.NAME),
                    new SqlParameter("@FATHER_NAME",modelComplaint.FATHER_NAME),
                    new SqlParameter("@KNO",modelComplaint.KNO),
                    new SqlParameter("@LANDLINE_NO",modelComplaint.LANDLINE_NO),
                    new SqlParameter("@MOBILE_NO",modelComplaint.MOBILE_NO),
                    new SqlParameter("@ALTERNATE_MOBILE_NO",modelComplaint.ALTERNATE_MOBILE_NO),
                    new SqlParameter("@EMAIL",modelComplaint.EMAIL),
                    new SqlParameter("@AreaCode",modelComplaint.AREA_CODE),
                    new SqlParameter("@ADDRESS1",modelComplaint.ADDRESS1),
                    new SqlParameter("@ADDRESS2",modelComplaint.ADDRESS2),
                    new SqlParameter("@ADDRESS3",modelComplaint.ADDRESS3),
                    new SqlParameter("@LANDMARK",modelComplaint.LANDMARK),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_EDIT", param);

                if (param[15].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[15].Value);
                //if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
                //{
                //    ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
                //    TextSmsAPI textSmsAPI = new TextSmsAPI();

                //    modelSmsAPI.To = "91" + modelComplaint.MOBILE_NO.ToString();
                //    modelSmsAPI.Smstext = "Dear Consumer,Your Complaint has been registered with complaint No. " + retStatus + " on Date:" + DateTime.Now.ToString("dd-MMM-yyyy") + " AVVNL";
                //    string response = await textSmsAPI.RegisterComplaintSMS(modelSmsAPI);
                //    if (response.Contains("success"))
                //    {
                //        PUSH_SMS_DETAIL_Consumer(modelComplaint, response);
                //    }
                //}
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

            object requestCount = SqlHelper.ExecuteScalar(HelperClass.Connection, CommandType.StoredProcedure, "IsDuplicateRequest", param);

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

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintSource");

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

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintSource_register");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<ModelSearchComplaint> GetComplaintDetails(ModelSearchComplaint dataObject)
        {
            int TotalRec = 0;

            List<ModelSearchComplaint> lstComplaintSource = new List<ModelSearchComplaint>();
            ModelSearchComplaint objBlank = new ModelSearchComplaint();
            SqlParameter[] param ={
                new SqlParameter("@User_ID",dataObject.UserID),
                    new SqlParameter("@STARTROWINDEX",dataObject.start),
                    new SqlParameter("@MAXIMUMROWS",dataObject.length)};

            log.Debug(" GetComplaintDetails IP " + HelperClass.GetIPHelper() + " Proc Start Time :  " + DateTime.Now.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetails", param);
            log.Debug(" GetComplaintDetails IP " + HelperClass.GetIPHelper() + " Proc End Time :  " + DateTime.Now.ToString());


            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                    TotalRec = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                else
                    TotalRec = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objBlank = new ModelSearchComplaint();
                    objBlank.NAME = dr.ItemArray[1].ToString();
                    objBlank.COMPLAINT_DATE = dr.ItemArray[2].ToString();
                    objBlank.COMPLAINT_NO = dr.ItemArray[0].ToString();
                    objBlank.ADDRESS = dr.ItemArray[3].ToString();
                    objBlank.MOBILE_NO = dr.ItemArray[4].ToString();
                    objBlank.LineMan_Name = dr.ItemArray[5].ToString();
                    objBlank.Consumer_no = dr.ItemArray[8].ToString();
                    objBlank.LineMan_No = dr.ItemArray[6].ToString();
                    objBlank.SDO = dr.ItemArray[7].ToString();
                    objBlank.COMPLAINT_NO1 = Convert.ToInt64(dr.ItemArray[10].ToString());
                    objBlank.Total = TotalRec;
                    lstComplaintSource.Add(objBlank);
                }

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


        public static List<ModelUser> GetUserList()
        {
            List<ModelUser> obj = new List<ModelUser>();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUsers");
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelUser
                    {
                        User_id = Convert.ToInt32(dr["USER_ID"]),
                        User_Name = Convert.ToString(dr["USER_NAME"]),
                        Name = Convert.ToString(dr["NAME"]),
                        Role = Convert.ToString(dr["ROLE_NAME"]),
                        Mobile_NO = Convert.ToInt64(dr["MOBILE_NO"]),
                        Email = Convert.ToString(dr["EMAIL_ID"]),
                        Address = Convert.ToString(dr["ADDRESS"]),
                        Is_Active = Convert.ToBoolean(dr["IS_ACTIVE"]),
                        Is_deleted = Convert.ToBoolean(dr["IS_DELETED"]),
                    }
                    );
            }
            return (obj);
        }
        public static int SaveUser(ModelUser modelUser, string s)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            ModelUser obj = new ModelUser();
            obj = modelUser;

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

            string msg = String.Empty;


            if (s == "I")
            {
                msg = Utility.EncryptText(modelUser.Password);
            }
            else
            {
                msg = GetOldPassword(modelUser.User_Name);
            }
            //string msg = modelUser.Password;
            SqlParameter[] param ={
                new SqlParameter("@USER_ID",modelUser.User_id),
                new SqlParameter("@USER_NAME",modelUser.User_Name),
                new SqlParameter("@PASSWORD",msg),
                new SqlParameter("@NAME",modelUser.Name),
                new SqlParameter("@ROLE_ID",modelUser.RoleID),
                new SqlParameter("@ADDRESS",modelUser.Address),
                new SqlParameter("@MOBILE_NO",modelUser.Mobile_NO),
                new SqlParameter("@EMAIL_ID",modelUser.Email),
                new SqlParameter("@Phone_Login",modelUser.PhoneLogin),
                new SqlParameter("@Phone_Pass",modelUser.PhonePassword),
                new SqlParameter("@action",s),
                new SqlParameter("@OFFICE_ID",modelUser.Office_Id),
                new SqlParameter("@DIALER_STATUS",modelUser.DIALER_STATUS),
                parmretStatus,parmretMsg};
            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "INSERTUSER", param);

                if (param[3].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[3].Value);
                else
                    retStatus = 0;
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }



            return retStatus;

        }
        public static string GetOldPassword(string userid)
        {
            SqlParameter[] param ={
                    new SqlParameter("@User_Name",userid)
                                       };
            string old_password = string.Empty;
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUsersPassword", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                old_password = Convert.ToString(dr["PASSWORD"]);

            }
            return (old_password);
        }

        public static ModelUser EditUser(int id)
        {
            ModelUser obj = new ModelUser();
            SqlParameter[] param ={
                    new SqlParameter("@User_ID",id)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUsers", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.User_Name = Convert.ToString(dr["USER_NAME"]);
                obj.Name = Convert.ToString(dr["NAME"]);
                obj.Role = Convert.ToString(dr["ROLE_NAME"]);
                obj.Mobile_NO = Convert.ToInt64(dr["MOBILE_NO"]);
                obj.Email = Convert.ToString(dr["EMAIL_ID"]);
                obj.Address = Convert.ToString(dr["ADDRESS"]);
                obj.Is_Active = Convert.ToBoolean(dr["IS_ACTIVE"]);
                obj.Is_deleted = Convert.ToBoolean(dr["IS_DELETED"]);

            }
            return (obj);
        }


        public static List<ModelRoles> GetRolesList()
        {
            List<ModelRoles> lstRoles = new List<ModelRoles>();
            ModelRoles objBlank = new ModelRoles();
            objBlank.RoleID = 0;
            objBlank.RoleName = "Select Roles";
            lstRoles.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetRoles");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelRoles();
                objBlank.RoleName = dr.ItemArray[0].ToString();
                objBlank.RoleID = Convert.ToInt32(dr.ItemArray[1].ToString());
                lstRoles.Add(objBlank);
            }
            return lstRoles;
        }



        public static List<SelectListItem> UserList(int RoleId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                    new SqlParameter("@RoleId",RoleId)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUserList", param);

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
        public static Response SaveRemark(COMPLAINT modelRemark)
        {
            Response response = new Response();
            string retStatus = "-1";
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
                new SqlParameter("@COMPLAINT_NO",modelRemark.COMPLAINT_NO),
                    new SqlParameter("@REMARK",modelRemark.REMARKS),
                    new SqlParameter("@USER_ID",modelRemark.UserId),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "SAVE_REMARK", param);

                if(parmretStatus.Value !=DBNull.Value)
                {
                    retStatus = parmretStatus.Value.ToString();
                }
                if (parmretMsg.Value != DBNull.Value)
                {
                    retMsg = parmretMsg.Value.ToString();
                }

                response.status= retStatus;
                response.message= retMsg;
            }
            catch (Exception ex)
            {
                log.Information("SaveRemark :  " + ex.Message);
                return response;
            }



            return response;

        }

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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "CHANGE_COMPLAINT_TYPE", param);
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

        public static Response ChangeComplaintType_Save(COMPLAINT modelRemark, int UserID)
        {
            Response response = new Response();
            string retStatus = "-1";
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
                new SqlParameter("@COMPLAINT_NO",modelRemark.COMPLAINT_NO),
                    new SqlParameter("@COMPLAINT_TYPE",modelRemark.ComplaintTypeId),
                    new SqlParameter("@USER_ID",UserID),
                    new SqlParameter("@SUB_COMPLAINT_TYPE",modelRemark.SUB_COMPLAINT_TYPE_ID),
                    new SqlParameter("@PROCESS","I"),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "CHANGE_COMPLAINT_TYPE", param);

                if (parmretStatus.Value != DBNull.Value)
                {
                    retStatus = parmretStatus.Value.ToString();
                }
                if (parmretMsg.Value != DBNull.Value)
                {
                    retMsg = parmretMsg.Value.ToString();
                }

                response.status = retStatus;
                response.message = retMsg;
            }
            catch (Exception ex)
            {
                log.Information("ChangeComplaintType_Save :  " + ex.Message);
                return response;
            }



             return response; ;

        }
        public static List<ModelEsclatedCOmplaints> GetExclatedComplaintSummary(int OfficeCode, int ComplaintType, int SLAType)
        {
            List<ModelEsclatedCOmplaints> obj = new List<ModelEsclatedCOmplaints>();
            SqlParameter[] param ={
                    new SqlParameter("@OfficeCode",OfficeCode),
                    new SqlParameter("@ComplaintType",ComplaintType),
                    new SqlParameter("@SLAType",SLAType),
                   };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetEsclatedComplaints_new", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelEsclatedCOmplaints
                    {
                        OFFICE_CODE = Convert.ToString(dr["OFFICE_CODE"]),
                        SDOName = Convert.ToString(dr["SDOName"]),
                        TotalComplaintOpen = Convert.ToString(dr["TotalComplaintOpen"]),
                        WithInSLA = Convert.ToString(dr["WithInSLA"]),
                        OUTOfSLA = Convert.ToString(dr["OUTOfSLA"]),
                        AEN = Convert.ToString(dr["AEN"]),
                        XEN = Convert.ToString(dr["XEN"]),
                        SE = Convert.ToString(dr["SE"]),
                        CORPORATE = Convert.ToString(dr["CE"]),
                        DT = Convert.ToString(dr["DT"]),
                        backcolor = Convert.ToString(dr["TEXT_COLOR"]),
                    }
                    );
            }
            return (obj);
        }
        public static COMPLAINT GetMobileEmail(Int64 id)
        {
            COMPLAINT obj = new COMPLAINT();
            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",id),
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetail", param);
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
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintAssignee");
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
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Save_Complaint_Assignee", param);

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
                    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetail", param1);
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
        //public static List<ModelSearchComplaint> GetReopenComplaintDetails(ModelSearchComplaint dataObject)
        //{
        //    List<ModelSearchComplaint> lstComplaintSource = new List<ModelSearchComplaint>();
        //    ModelSearchComplaint objBlank = new ModelSearchComplaint();
        //    //objBlank.Value = "0";
        //    //objBlank.Text = "Select Source";
        //    //lstComplaintSource.Insert(0, objBlank);
        //    SqlParameter[] param ={
        //        new SqlParameter("@complaint_no",dataObject.COMPLAINT_NO),
        //            new SqlParameter("@kno",dataObject.KNO),
        //            new SqlParameter("@mobile_no",dataObject.MOBILE_NO),

        //            };

        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetReopenComplaintDetails", param);

        //    // lstComplaintSource = DataTableToList<ModelSearchComplaint>(ds.Tables[0]).ToList();

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        objBlank = new ModelSearchComplaint();
        //        objBlank.KNO = Convert.ToInt64(dr.ItemArray[0].ToString());
        //        objBlank.NAME = dr.ItemArray[1].ToString();
        //        objBlank.COMPLAINT_DATE = dr.ItemArray[2].ToString().Substring(0, 10);
        //        objBlank.COMPLAINT_NO = Convert.ToInt64(dr.ItemArray[3].ToString());
        //        objBlank.OFFICE_NAME = dr.ItemArray[4].ToString();
        //        objBlank.ADDRESS = dr.ItemArray[5].ToString();
        //        objBlank.COMPLAINT_TYPE = dr.ItemArray[6].ToString();
        //        objBlank.SOURCE_NAME = dr.ItemArray[7].ToString();
        //        objBlank.COMPLAINT_status = dr.ItemArray[8].ToString();
        //        objBlank.ASSIGNED_TO = dr.ItemArray[9].ToString();
        //        objBlank.MOBILE_NO = dr.ItemArray[10].ToString();
        //        objBlank.LANDLINE_NO = dr.ItemArray[11].ToString();
        //        objBlank.REJECTION_CAUSE = dr.ItemArray[12].ToString();
        //        lstComplaintSource.Add(objBlank);
        //    }
        //    return lstComplaintSource;
        //}
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
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Repoen", param);

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
        public static string ChangePassword(ModelUser modelUser)
        {

            string retStatus = "0";
            string retMsg = String.Empty; ;
            ModelUser obj = new ModelUser();
            obj = modelUser;

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

            string msg = Utility.EncryptText(modelUser.Password);
            //string msg = modelUser.Password;
            SqlParameter[] param ={
                new SqlParameter("@USER_ID",modelUser.User_id),
                    new SqlParameter("@PASSWORD",msg),
                    parmretStatus,parmretMsg};
            if (modelUser.Password.ToUpper() == modelUser.Confirm_Password.ToUpper())
            {

                try
                {
                    SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "ChangePassword", param);

                    if (param[3].Value != DBNull.Value)// status
                        retStatus = "Password Changed Successfully";
                    else
                        retStatus = "Password Not Changed";
                }
                catch (Exception ex)
                {
                    retStatus = "Password Not Changed";
                }
            }
            else
            {
                retStatus = "New And Confirm Password not match";
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
                //SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "ChangePassword", param);

                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "ShowComplaint_Detail", param);

            }
            catch (Exception ex)
            {
                ds = new DataSet();
            }
            return ds;
        }
        public static async Task<int> CloseComplaint_Save(ModelCloseComplaint modelRemark, int UserID)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            ModelCloseComplaint obj = new ModelCloseComplaint();
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
            string abnoramility = "";

            SqlParameter[] param ={
                new SqlParameter("@complaint_no",modelRemark.ComplaintNo),
                    new SqlParameter("@Complaint_CategoryID",modelRemark.OutageTypeCollectionId1),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Save_Complaint_Close", param);

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
        #endregion

        #region Close Complaint
        public static List<SelectListItem> GetOutageType()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Category");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> GetOutageTypeSub(int OutageTypeId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                new SqlParameter("@OUTAGE_TYPE_ID",OutageTypeId)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetSubOutageType", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> GetOutageCause(int OutageTypeId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                new SqlParameter("@OUTAGE_TYPE_ID",OutageTypeId)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOutageCause", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        #endregion

        public static List<COMPLAINT> ShowEsclatedComplaint(string OfficeId, string ComplaintStatus, string ComplainttypeId)
        {
            List<COMPLAINT> obj = new List<COMPLAINT>();
            DataSet ds = new DataSet();
            SqlParameter[] param ={
                new SqlParameter("@OFFICE_CODE",OfficeId),
                new SqlParameter("@SLA_TYPE",ComplaintStatus),
                new SqlParameter("@COMPLAINT_TYPE",ComplainttypeId),
            };
            try
            {

                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "ESCLATE_COMPLAINT_RAW_NEW", param);



                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj.Add(

                        new COMPLAINT
                        {
                            OfficeName = Convert.ToString(dr["SDOName"]),
                            COMPLAINT_NO = Convert.ToString(dr["COMPLAINT_NO"]),
                            KNO = Convert.ToString(dr["KNO"]),
                            NAME = Convert.ToString(dr["NAME"]),
                            MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]),
                            ALTERNATE_MOBILE_NO = Convert.ToString(dr["ALTERNATE_MOBILE_NO"]),
                            Complaintdate = Convert.ToString(dr["COMPLAINT_DATE"]),
                            ComplaintTypeName = Convert.ToString(dr["COMPLAINT_TYPE"]),
                            ComplaintSourceName = Convert.ToString(dr["SOURCE_NAME"]),
                            ASSIGNEEName = Convert.ToString(dr["ASSIGNED_AT"]),
                            Esclation_time = Convert.ToString(dr["ESCLATION_TIME"]),
                            Esclated_BY = Convert.ToString(dr["ESCLATED_BY"]),

                            excel_OFFICE_CODE = OfficeId,
                            excel_ComplaintStatus = ComplaintStatus,
                            excel_ComplainttypeId = ComplainttypeId,
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

        //#region reports
        //public static List<ModelComplaintAnalysisReport> ReportComplaintAnalysisData(ModelReport dataObject)
        //{
        //    List<ModelComplaintAnalysisReport> lstReportdata = new List<ModelComplaintAnalysisReport>();
        //    ModelComplaintAnalysisReport objData = new ModelComplaintAnalysisReport();
        //    SqlParameter[] param ={
        //            new SqlParameter("@START_TIME",dataObject.fromdate),
        //             new SqlParameter("@END_TIME",dataObject.todate),
        //                      new SqlParameter("@OFFICE_ID",dataObject.OfficeCode)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINT_ANALYSIS_RAW", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelComplaintAnalysisReport();
        //            objData.SDOCode = Convert.ToString(dr.ItemArray[0]);
        //            objData.KNO = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.RegistrationofComplaint = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.ComplaintNumber = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.Complaintclosedate = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.ResolutioninHHMI = Convert.ToString(dr.ItemArray[5].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<EsclationComplaint> ReportEscalatedComplaint(ModelReport dataObject)
        //{
        //    List<EsclationComplaint> lstReportdata = new List<EsclationComplaint>();
        //    EsclationComplaint objData = new EsclationComplaint();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROMDATE",dataObject.fromdate),
        //             new SqlParameter("@TODATE",dataObject.todate),
        //                      new SqlParameter("@OFFICE_ID",dataObject.OfficeCode)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Total_Escalate_Complaint", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new EsclationComplaint();
        //            objData.TotalEsclatedComplaint = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<PerformanceTrackerModel> ReportFRTPerformance(ModelReport dataObject)
        //{
        //    List<PerformanceTrackerModel> lstReportdata = new List<PerformanceTrackerModel>();
        //    PerformanceTrackerModel objData = new PerformanceTrackerModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //                      new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //                      new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "FRT_Performance", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new PerformanceTrackerModel();
        //            objData.SdoCode = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalComplaintsReceived = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.AverageFollowUpsPerComplaint = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.TotalFollowUps = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.AvgFollowUpTime = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.AverageResolutionHours = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.ComplaintsAcceptedByFRT = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.ComplaintClosedByFRT = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.RepeatComplaints = Convert.ToString(dr.ItemArray[8].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<HelpDeskPerformanceTrackerModel> ReportHelpdeskPerformance(ModelReport dataObject)
        //{
        //    List<HelpDeskPerformanceTrackerModel> lstReportdata = new List<HelpDeskPerformanceTrackerModel>();
        //    HelpDeskPerformanceTrackerModel objData = new HelpDeskPerformanceTrackerModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //                      new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //                      new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "HELPDESK_PERFORMANCE", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new HelpDeskPerformanceTrackerModel();
        //            objData.SdoCode = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalComplaintsReceived = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.AverageFollowUpsPerComplaint = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.TotalFollowUps = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.AvgFollowUpTime = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.AverageResolutionHours = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.TotalResolutionHours = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.RepeatComplaints = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.TotalNumberOfShutDowns = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.AvgShutDownTime = Convert.ToString(dr.ItemArray[9].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<OutBoundPerformanceTrackerModel> ReportOutboundPerformance(ModelReport dataObject)
        //{
        //    List<OutBoundPerformanceTrackerModel> lstReportdata = new List<OutBoundPerformanceTrackerModel>();
        //    OutBoundPerformanceTrackerModel objData = new OutBoundPerformanceTrackerModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //                      new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //                      new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_PERFORMANCE", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new OutBoundPerformanceTrackerModel();
        //            objData.SdoCode = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalComplaintReceived = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.AverageFollowUpsPerComplaint = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.TotalFollowUps = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.AvgFollowUpTime = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.AverageResolutionHours = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.TotalShutDowns = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.AvgShutDown = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.RepeatComplaints = Convert.ToString(dr.ItemArray[8].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<FRTPanelty> ReportFRTPanalty(ModelReport dataObject)
        //{
        //    List<FRTPanelty> lstReportdata = new List<FRTPanelty>();
        //    FRTPanelty objData = new FRTPanelty();
        //    SqlParameter[] param1 ={
        //            new SqlParameter("@FROMDATE",dataObject.fromdate),
        //             new SqlParameter("@TODATE",dataObject.todate),
        //                      new SqlParameter("@OFFICE_ID",dataObject.OfficeCode)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Total_FRT_Panaty", param1);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new FRTPanelty();
        //            objData.Type = Convert.ToString(dr.ItemArray[0]);
        //            objData.NoOfDefaultsUpTo2Slots = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.NoOfDefaultsMoreThan2Slots = Convert.ToString(dr.ItemArray[2].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<CallQueueWaitingModel> ReportCallQueueWaiting(ModelReport dataObject)
        //{
        //    List<CallQueueWaitingModel> lstReportdata = new List<CallQueueWaitingModel>();
        //    CallQueueWaitingModel objData = new CallQueueWaitingModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Call_Queue_Waiting", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new CallQueueWaitingModel();
        //            objData.TotalCallAttended = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalCallAttendedIn60Sec = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.TotalCallAttendedAfter60Sec = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.PercentageCallAttendedWithIn60Sec = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.PercentageCallAttendedAfter60Sec = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[5].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<InBoundPerformanceTrackerModel> ReportInBoundPerformance(ModelReport dataObject)
        //{
        //    List<InBoundPerformanceTrackerModel> lstReportdata = new List<InBoundPerformanceTrackerModel>();
        //    InBoundPerformanceTrackerModel objData = new InBoundPerformanceTrackerModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "InBound_Performance", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new InBoundPerformanceTrackerModel();
        //            objData.TotalCallOffered = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalCallAnswered = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<NonITPanalty> ReportNonITPenalty(ModelReport dataObject)
        //{
        //    List<NonITPanalty> lstReportdata = new List<NonITPanalty>();
        //    NonITPanalty objData = new NonITPanalty();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROMDATE",dataObject.fromdate),
        //             new SqlParameter("@TODATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Non_IT_Penalty", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new NonITPanalty();
        //            objData.Type = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<UniformPanalty> ReportUniformPenalty(ModelReport dataObject)
        //{
        //    List<UniformPanalty> lstReportdata = new List<UniformPanalty>();
        //    UniformPanalty objData = new UniformPanalty();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROMDATE",dataObject.fromdate),
        //             new SqlParameter("@TODATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Uniform_Penalty", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new UniformPanalty();
        //            objData.TotalAgents = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<UniformPanalty> ReportMissingAgentsPenalty(ModelReport dataObject)
        //{
        //    List<UniformPanalty> lstReportdata = new List<UniformPanalty>();
        //    UniformPanalty objData = new UniformPanalty();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROMDATE",dataObject.fromdate),
        //             new SqlParameter("@TODATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Missing_Agent_Penalty", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new UniformPanalty();
        //            objData.TotalAgents = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<SystemAvailability> ReportSystemAvailability(ModelReport dataObject)
        //{
        //    List<SystemAvailability> lstReportdata = new List<SystemAvailability>();
        //    SystemAvailability objData = new SystemAvailability();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "System_Availability_Penalty_Report", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new SystemAvailability();
        //            objData.TotalInstance = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<FalseClouse> ReportFalseClose(ModelReport dataObject)
        //{
        //    List<FalseClouse> lstReportdata = new List<FalseClouse>();
        //    FalseClouse objData = new FalseClouse();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROMDATE",dataObject.fromdate),
        //             new SqlParameter("@TODATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "False_Close", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new FalseClouse();
        //            objData.FalseCount = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[1].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<CallQueueAbandonmentModel> ReportCallAbandonment(ModelReport dataObject)
        //{
        //    List<CallQueueAbandonmentModel> lstReportdata = new List<CallQueueAbandonmentModel>();
        //    CallQueueAbandonmentModel objData = new CallQueueAbandonmentModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Call_Queue_Abandonment", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new CallQueueAbandonmentModel();
        //            objData.TotalCall = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalCallAbandon = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.PercentageCallAbandon = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.TotalPaneltyAmount = Convert.ToString(dr.ItemArray[3].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}
        //public static List<ModelComplaintHourlyReport> ReportComplaintHourlyData(ModelReport dataObject)
        //{

        //    List<ModelComplaintHourlyReport> lstReportdata = new List<ModelComplaintHourlyReport>();
        //    ModelComplaintHourlyReport objData = new ModelComplaintHourlyReport();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //             new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //             new SqlParameter("@COMPLAINT_STATUS",dataObject.ComplaintStatus),
        //             new SqlParameter("@COMPLAINT_SOURCE",dataObject.ComplaintSource),
        //             new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType)
        //               };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINT_HOURLY_REPORT", param);

        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelComplaintHourlyReport();
        //            objData.OFFICE_CODE = Convert.ToString(dr.ItemArray[0]);
        //            objData.OFFICE_NAME = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.ONE_HOUR = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.TWO_HOUR = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.THREE_HOUR = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.FOUR_HOUR = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.FIVE_HOUR = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.MOTE_THEN_FIVE_HOUR = Convert.ToString(dr.ItemArray[7].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<COMPLAINT> ReportComplaintHourlyRawData(ModelReport dataObject)
        //{

        //    List<COMPLAINT> lstReportdata = new List<COMPLAINT>();
        //    COMPLAINT objData = new COMPLAINT();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //             new SqlParameter("@OFFICE_CODE",dataObject.OfficeCode),
        //             new SqlParameter("@COMPLAINT_STATUS",dataObject.ComplaintStatus),
        //             new SqlParameter("@COMPLAINT_SOURCE",dataObject.ComplaintSource),
        //             new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType),
        //              new SqlParameter("@SLA_TYPE",dataObject.SlaType),

        //               };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "HOURLY_REPORT_COMPLAINT_RAW", param);

        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new COMPLAINT();
        //            objData.KNO = Convert.ToString(dr.ItemArray[0].ToString());
        //            objData.NAME = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.Complaintdate = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[3]);
        //            objData.ACCOUNT_NO = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.ADDRESS1 = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.ComplaintTypeName = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.ComplaintSourceName = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.COMPLAINT_status = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.ASSIGNEEName = Convert.ToString(dr.ItemArray[10].ToString());

        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}
        public static List<ModelRawComplaintReport> ReportRawComplaintData(ModelReport dataObject)
        {
            SqlParameter parmretTotalRecords = new SqlParameter();
            parmretTotalRecords.ParameterName = "@TOTALRECORDS";
            parmretTotalRecords.DbType = DbType.Int32;
            parmretTotalRecords.Size = 8;
            parmretTotalRecords.Direction = ParameterDirection.Output;

            int TotalRec = 0;

            List<ModelRawComplaintReport> lstReportdata = new List<ModelRawComplaintReport>();
            ModelRawComplaintReport objData = new ModelRawComplaintReport();
            SqlParameter[] param ={
                    new SqlParameter("@Month",dataObject.Bill_Month),
                    new SqlParameter("@UserID",dataObject.UserID),
                    new SqlParameter("@STARTROWINDEX",dataObject.start),
                    new SqlParameter("@MAXIMUMROWS",dataObject.length),parmretTotalRecords};

            log.Debug(" RAW_COMPLAINT IP " + HelperClass.GetIPHelper() + " Proc Start Time :  " + DateTime.Now.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Resolved_Complaints", param);
            log.Debug(" RAW_COMPLAINT IP " + HelperClass.GetIPHelper() + " Proc End Time :  " + DateTime.Now.ToString());
            if (param[3].Value != DBNull.Value)// status
                TotalRec = Convert.ToInt32(param[3].Value);
            else
                TotalRec = 0;

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData = new ModelRawComplaintReport();
                    objData.ComplaintNo = Convert.ToString(dr.ItemArray[0].ToString());
                    objData.Customer_Name = Convert.ToString(dr.ItemArray[1].ToString());
                    objData.Mobile_no = Convert.ToInt64(dr.ItemArray[2].ToString());
                    objData.Category_Name = Convert.ToString(dr.ItemArray[3].ToString());
                    objData.LineMan_Name = Convert.ToString(dr.ItemArray[4].ToString());
                    objData.Complaint_date = Convert.ToString(dr.ItemArray[5].ToString());
                    objData.Resolved_Date = Convert.ToString(dr.ItemArray[6].ToString());
                    objData.Response_Time = Convert.ToString(dr.ItemArray[7].ToString());
                    objData.Total = TotalRec;
                    lstReportdata.Add(objData);
                }
            }
            return lstReportdata;
        }

        //public static List<ModelRawComplaintReportNewConnection> ReportRawComplaintNewConnectionData(ModelReport dataObject)
        //{
        //    SqlParameter parmretTotalRecords = new SqlParameter();
        //    parmretTotalRecords.ParameterName = "@TOTALRECORDS";
        //    parmretTotalRecords.DbType = DbType.Int32;
        //    parmretTotalRecords.Size = 8;
        //    parmretTotalRecords.Direction = ParameterDirection.Output;

        //    int TotalRec = 0;

        //    List<ModelRawComplaintReportNewConnection> lstReportdata = new List<ModelRawComplaintReportNewConnection>();
        //    ModelRawComplaintReportNewConnection objData = new ModelRawComplaintReportNewConnection();
        //    SqlParameter[] param ={
        //            new SqlParameter("@START_DATE",dataObject.fromdate),
        //            new SqlParameter("@END_DATE",dataObject.todate),
        //            new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //            new SqlParameter("@STARTROWINDEX",dataObject.start),
        //            new SqlParameter("@MAXIMUMROWS",dataObject.length),parmretTotalRecords};

        //    log.Debug(" RAW_COMPLAINT IP " + HelperClass.GetIPHelper() + " Proc Start Time :  " + DateTime.Now.ToString());
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "RAW_COMPLAINT_NEW_CONNECTION", param);
        //    log.Debug(" RAW_COMPLAINT IP " + HelperClass.GetIPHelper() + " Proc End Time :  " + DateTime.Now.ToString());
        //    if (param[5].Value != DBNull.Value)// status
        //        TotalRec = Convert.ToInt32(param[5].Value);
        //    else
        //        TotalRec = 0;

        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelRawComplaintReportNewConnection();
        //            objData.SDO_CODE = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.SDO_NAME = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[0].ToString());
        //            objData.KNO = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.NAME = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.FATHER_NAME = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.ADDRESS = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.ALTERNATE_MOBILE_NO = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.MOBILE_NO = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[9].ToString());
        //            objData.OUTAGE_TYPE = Convert.ToString(dr.ItemArray[11].ToString());
        //            objData.SUB_COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[10].ToString());
        //            objData.SUB_OUTAGE_TYPE = Convert.ToString(dr.ItemArray[12].ToString());
        //            objData.DS_NDS = Convert.ToString(dr.ItemArray[13].ToString());
        //            objData.COMPLAINT_DATE_TIME = Convert.ToString(dr.ItemArray[14].ToString());
        //            objData.CLOSED_DATE_TIME = Convert.ToString(dr.ItemArray[15].ToString());
        //            objData.DURATION = Convert.ToString(dr.ItemArray[16].ToString());
        //            objData.COMPLAINT_STATUS = Convert.ToString(dr.ItemArray[17].ToString());
        //            objData.CURRENT_STATUS = Convert.ToString(dr.ItemArray[18].ToString());
        //            objData.CREATED_BY_USER = Convert.ToString(dr.ItemArray[19].ToString());
        //            objData.CLOSED_BY_USER = Convert.ToString(dr.ItemArray[20].ToString());
        //            objData.Total = TotalRec;
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<ModelRawComplaintReport> ReportRawComplaintData()
        //{
        //    ModelReport dataObject = new ModelReport();
        //    List<ModelRawComplaintReport> lstReportdata = new List<ModelRawComplaintReport>();
        //    ModelRawComplaintReport objData = new ModelRawComplaintReport();
        //    SqlParameter[] param ={
        //            new SqlParameter("@START_DATE",dataObject.fromdate),
        //            new SqlParameter("@END_DATE",dataObject.todate),
        //            new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //            new SqlParameter("@Complaint_source",dataObject.ComplaintSource),
        //            new SqlParameter("@Complaint_Type",dataObject.ComplaintType)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "RAW_COMPLAINT", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelRawComplaintReport();
        //            objData.SDO_CODE = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.SubDivisionName = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[0].ToString());
        //            objData.AreaCode = Convert.ToString(dr.ItemArray[14].ToString());
        //            objData.Name = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.FatherName = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.Address = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.AlternateNo = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.MobileNo = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.KNO = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.ComplaintType = Convert.ToString(dr.ItemArray[9].ToString());
        //            objData.ComplaintDate = Convert.ToString(dr.ItemArray[11].ToString());
        //            objData.SubComplaintType = Convert.ToString(dr.ItemArray[10].ToString());
        //            objData.ClosedDate = Convert.ToString(dr.ItemArray[12].ToString());
        //            objData.Duration = Convert.ToString(dr.ItemArray[13].ToString());
        //            objData.CurrentStatus = Convert.ToString(dr.ItemArray[15].ToString());
        //            objData.SOURCE_NAME = Convert.ToString(dr.ItemArray[16].ToString());
        //            objData.CreatedUserID = Convert.ToString(dr.ItemArray[17].ToString());
        //            objData.ClosedUserID = Convert.ToString(dr.ItemArray[18].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<ModelRedressal> ReportRedressalData(ModelReport dataObject)
        //{
        //    List<ModelRedressal> lstReportdata = new List<ModelRedressal>();
        //    ModelRedressal objData = new ModelRedressal();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //                      new SqlParameter("@OFFICE_CODE",dataObject.OfficeCode),
        //                      new SqlParameter("@AREA",dataObject.SlaType)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "REDRESSAL_REPORT_NEW", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelRedressal();
        //            objData.OFFICE_NAME = Convert.ToString(dr.ItemArray[0]);
        //            objData.COMPLAINTS_PENDING_TILL_FROM_DATE = Convert.ToString(dr.ItemArray[1]);
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_NO_CURRENT_COMPLAINT = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_TRANSFORMER_FAILURE = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_ENERGY_THEFT = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_SAFETY_RELATED = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_HARASSMENT_BY_OFFICIALS = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_BILL_RELATED = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_METER_RELATED = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_OTHER_TRCHNICAL_COMPLAINTS = Convert.ToString(dr.ItemArray[9].ToString());
        //            objData.COMPLAINS_RECEIEVED_UPTO_MONTH_TOTAL = Convert.ToString(dr.ItemArray[10].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_NO_CURRENT_COMPLAINT = Convert.ToString(dr.ItemArray[11].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_TRANSFORMER_FAILURE = Convert.ToString(dr.ItemArray[12].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_ENERGY_THEFT = Convert.ToString(dr.ItemArray[13].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_SAFETY_RELATED = Convert.ToString(dr.ItemArray[14].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_HARASSMENT_BY_OFFICIALS = Convert.ToString(dr.ItemArray[15].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_BILL_RELATED = Convert.ToString(dr.ItemArray[16].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_METER_RELATED = Convert.ToString(dr.ItemArray[17].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_OTHER_TRCHNICAL_COMPLAINTS = Convert.ToString(dr.ItemArray[18].ToString());
        //            objData.COMPLAINS_REDRESSED_UPTO_MONTH_TOTAL = Convert.ToString(dr.ItemArray[19].ToString());
        //            objData.BALANCE_NO_CURRENT_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[20].ToString());
        //            objData.BALANCE_TRANSFORMER_FAILURE_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[21].ToString());
        //            objData.BALANCE_ENERGY_THEFT_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[22].ToString());
        //            objData.BALANCE_SAFETY_RELATED_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[23].ToString());
        //            objData.BALANCE_HARASSMENT_BY_OFFICIALS_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[24].ToString());
        //            objData.BALANCE_BILL_RELATED_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[25].ToString());
        //            objData.BALANCE_METER_RELATED_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[26].ToString());
        //            objData.BALANCE_OTHER_TECHNICAL_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[27].ToString());
        //            objData.TOTAL_BALANCE_COMPLAINTS_TILL_TO_DATE = Convert.ToString(dr.ItemArray[28].ToString());
        //            objData.NO_CURRENT_AVEG = Convert.ToString(dr.ItemArray[29].ToString());
        //            objData.TRANSFORMER_FAILURE_AVEG = Convert.ToString(dr.ItemArray[30].ToString());
        //            objData.ENERGY_THEFT = Convert.ToString(dr.ItemArray[31].ToString());
        //            objData.SAFETY_RELATED_AVEG = Convert.ToString(dr.ItemArray[32].ToString());
        //            objData.HARASSMENT_BY_OFFICIALS_AVEG = Convert.ToString(dr.ItemArray[33].ToString());

        //            objData.BILL_RELATED_AVEG = Convert.ToString(dr.ItemArray[34].ToString());
        //            objData.METER_RELATED_AVEG = Convert.ToString(dr.ItemArray[35].ToString());
        //            objData.OTHER_TECHNICAL_COMPLAINTS_AVEG = Convert.ToString(dr.ItemArray[36].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<ModelPendingComplaint> ReporttPendingComplaint(ModelReport dataObject)
        //{
        //    List<ModelPendingComplaint> lstReportdata = new List<ModelPendingComplaint>();
        //    ModelPendingComplaint objData = new ModelPendingComplaint();
        //    SqlParameter[] param ={
        //            new SqlParameter("@OFFICE_FILTER",dataObject.SlaType),
        //             new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //                      new SqlParameter("@COMPLAINT_SOURCE",dataObject.ComplaintSource),
        //                      new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType)

        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINT_STATUS", param);
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelPendingComplaint();
        //            objData.OfficeName = Convert.ToString(dr.ItemArray[0]);
        //            objData.TotalComplaintReceived = Convert.ToString(dr.ItemArray[1]);
        //            objData.TodayPending = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.PreviousDayPending = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.TodayResolved = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.OverAllPending = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.OverAllResolved = Convert.ToString(dr.ItemArray[6].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}


        //public static List<ModelOutageReport> ReportOutageData(ModelReport dataObject)
        //{
        //    List<ModelOutageReport> lstReportdata = new List<ModelOutageReport>();
        //    ModelOutageReport objData = new ModelOutageReport();
        //    SqlParameter[] param ={
        //                       new SqlParameter("@ID",dataObject.ID),
        //            new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
        //            new SqlParameter("@START_TIME",dataObject.fromdate),
        //             new SqlParameter("@END_TIME",dataObject.todate),
        //             new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType),
        //             new SqlParameter("@COLONIES",dataObject.COLONIES),
        //              new SqlParameter("@INFORMATION_SOURCE",dataObject.INFORMATION_SOURCE),
        //             new SqlParameter("@PROCESS_TYPE",dataObject.PROCESS_TYPE),
        //             new SqlParameter("@SHUTDOWN_INFORMATION",dataObject.SHUTDOWN_INFORMATION)

        //   };
        //    if (dataObject.PROCESS_TYPE == "I" || dataObject.PROCESS_TYPE == "U")
        //    {
        //        DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "POWER_OUTAGE", param);
        //    }
        //    else if (dataObject.PROCESS_TYPE == "S")
        //    {
        //        DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "POWER_OUTAGE", param);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelOutageReport();
        //            objData.ID = Convert.ToString(dr.ItemArray[0].ToString());
        //            objData.OFFICE_CODE = Convert.ToString(dr.ItemArray[1]);
        //            objData.StartTime = Convert.ToString(dr.ItemArray[2]);


        //            objData.EndTime = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.COLONIES = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.SHUT_DOWN_INFORMATION = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.INFORMATION_SOURCE = Convert.ToString(dr.ItemArray[7].ToString());

        //            lstReportdata.Add(objData);
        //        }

        //    }
        //    else if (dataObject.PROCESS_TYPE == "SA")
        //    {
        //        DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "POWER_OUTAGE", param);
        //        if (ds.Tables.Count > 0)
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                objData = new ModelOutageReport();
        //                objData.ID = Convert.ToString(dr.ItemArray[0].ToString());
        //                objData.OFFICE_CODE = Convert.ToString(dr.ItemArray[1]);
        //                objData.StartTime = Convert.ToString(dr.ItemArray[2]);


        //                objData.EndTime = Convert.ToString(dr.ItemArray[3].ToString());
        //                objData.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[4].ToString());
        //                objData.COLONIES = Convert.ToString(dr.ItemArray[5].ToString());
        //                objData.SHUT_DOWN_INFORMATION = Convert.ToString(dr.ItemArray[6].ToString());
        //                objData.INFORMATION_SOURCE = Convert.ToString(dr.ItemArray[7].ToString());

        //                lstReportdata.Add(objData);
        //            }
        //        }

        //    }
        //    return lstReportdata;
        //}





        //public static List<ModelRandomizerReport> ReportRandomizerData(ModelReport dataObject)
        //{


        //    List<ModelRandomizerReport> lstReportdata = new List<ModelRandomizerReport>();
        //    ModelRandomizerReport objData = new ModelRandomizerReport();

        //    SqlParameter parmretTotalRecords = new SqlParameter();
        //    parmretTotalRecords.ParameterName = "@TOTALRECORDS";
        //    parmretTotalRecords.DbType = DbType.Int32;
        //    parmretTotalRecords.Size = 8;
        //    parmretTotalRecords.Direction = ParameterDirection.Output;

        //    int TotalRec = 0;

        //    SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",dataObject.fromdate),
        //             new SqlParameter("@TO_DATE",dataObject.todate),
        //             new SqlParameter("@COUNT",string.IsNullOrEmpty( dataObject.count) ? "0": dataObject.count),
        //             new SqlParameter("@STARTROWINDEX",dataObject.start),
        //            new SqlParameter("@MAXIMUMROWS",dataObject.length),parmretTotalRecords};


        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "RANDOMIZER", param);
        //    if (param[5].Value != DBNull.Value)// status
        //        TotalRec = Convert.ToInt32(param[5].Value);
        //    else
        //        TotalRec = 0;
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelRandomizerReport();
        //            objData.KNO = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.OFFICE_CODE = Convert.ToString(dr.ItemArray[2]);
        //            objData.SDOCode = Convert.ToString(dr.ItemArray[3]);


        //            objData.CONSUMER_NAME = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.MOBILE_NO = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.TIME_STAMP = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.CLOSE_DATE = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.DURATION = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[9].ToString());
        //            objData.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[10].ToString());
        //            objData.SUB_COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[11].ToString());
        //            objData.COMPLAINT_SOURCE = Convert.ToString(dr.ItemArray[12].ToString());
        //            objData.REMARKS = Convert.ToString(dr.ItemArray[13].ToString());
        //            objData.Total = TotalRec;
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}
        //#endregion

        #region Dashboard
        public static ModelDashboard GetDashBoardData(String OFFICE_ID)
        {
            ModelDashboard dashboard = new ModelDashboard();
            List<ComplaintSummaryGraph> lstComplaintSummary = new List<ComplaintSummaryGraph>();
            List<ComplaintSummaryGraph> lstComplaintSummaryTotal = new List<ComplaintSummaryGraph>();

            List<CircleWiseComplaintSummary> lstComplaintSummary1 = new List<CircleWiseComplaintSummary>();
            ComplaintSummaryGraph objBlank = null;
            ComplaintSummaryGraph objBlanksummary = null;
            CircleWiseComplaintSummary objBlank1 = null;

            SqlParameter[] param = { new SqlParameter("@Month","2023-11"),
            new SqlParameter("@div", 1),
            new SqlParameter("@Subdiv", 0)};




            //DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "DASHBOARD_new", param);
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Dashboard_NewAjay");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ComplaintSummaryGraph();
                objBlank.Month = dr.ItemArray[0].ToString();
                objBlank.TotalComplaint = dr.ItemArray[1].ToString();
                objBlank.ResolveComplaint = dr.ItemArray[2].ToString();
                lstComplaintSummary.Add(objBlank);
            }
            dashboard.ComplaintSummaries = lstComplaintSummary;

            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                objBlanksummary = new ComplaintSummaryGraph();
                objBlanksummary.Month = dr.ItemArray[0].ToString();
                objBlanksummary.TotalComplaint = dr.ItemArray[1].ToString();

                lstComplaintSummaryTotal.Add(objBlanksummary);
            }
            dashboard.ComplaintSummaries1 = lstComplaintSummaryTotal;



            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                objBlank1 = new CircleWiseComplaintSummary();
                objBlank1.TotalComplaint = dr.ItemArray[0].ToString();
                objBlank1.TotalReopenComplaint = dr.ItemArray[1].ToString();
                objBlank1.TotalPendingComplaints = dr.ItemArray[2].ToString();


                lstComplaintSummary1.Add(objBlank1);
            }
            dashboard.CircleWiseComplaintSummaryData = lstComplaintSummary1;


            return dashboard;
        }
        #endregion


        #region saveApiCalls

        public static void SaveLogin(ModelUser modelUser)
        {
            SqlParameter LOGIN_TIME = new SqlParameter();
            LOGIN_TIME.ParameterName = "@LOGIN_TIME";
            LOGIN_TIME.DbType = DbType.DateTime;
            LOGIN_TIME.Size = 8;
            LOGIN_TIME.Direction = ParameterDirection.Input;
            LOGIN_TIME.Value = DateTime.Now;

            SqlParameter LOGOUT_TIME = new SqlParameter();
            LOGOUT_TIME.ParameterName = "@LOGOUT_TIME";
            LOGOUT_TIME.DbType = DbType.DateTime;
            LOGOUT_TIME.Size = 8;
            LOGOUT_TIME.Direction = ParameterDirection.Input;
            LOGOUT_TIME.Value = DateTime.Now;

            SqlParameter BREAK_TIME = new SqlParameter();
            BREAK_TIME.ParameterName = "@BREAK_TIME";
            BREAK_TIME.DbType = DbType.DateTime;
            BREAK_TIME.Size = 8;
            BREAK_TIME.Direction = ParameterDirection.Input;
            BREAK_TIME.Value = DateTime.Now;

            SqlParameter BREAK_END_TIME = new SqlParameter();
            BREAK_END_TIME.ParameterName = "@BREAK_END_TIME";
            BREAK_END_TIME.DbType = DbType.DateTime;
            BREAK_END_TIME.Size = 8;
            BREAK_END_TIME.Direction = ParameterDirection.Input;
            BREAK_END_TIME.Value = DateTime.Now;






            SqlParameter[] param ={
                  new SqlParameter("@AGENT_ID",modelUser.User_Name),
                    new SqlParameter("@AGENT_TYPE",modelUser.agent_type),LOGIN_TIME,LOGOUT_TIME,BREAK_TIME,BREAK_END_TIME,

                  new SqlParameter("@PROCESS_TYPE","IN"),

                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "AGENT_LOGIN", param);


        }

        #endregion

        #region Vaibhav
        public static int SaveCall(string agent_id, string phone, string agent_type, DateTime callStart, string callType, string callStatus, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",callStart),
                new SqlParameter("@CALL_END_TIME",DateTime.Now),
                new SqlParameter("@CALL_HOLD_TIME",DateTime.Now),
                new SqlParameter("@CALL_PICKUP_TIME",DateTime.Now),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS",callStatus),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }
        public static int HoldCall(string agent_id, string phone, string agent_type, DateTime callHold, string callType, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",DateTime.Now),
                new SqlParameter("@CALL_END_TIME",DateTime.Now),
                new SqlParameter("@CALL_HOLD_TIME",callHold),
                new SqlParameter("@CALL_PICKUP_TIME",DateTime.Now),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS","Hold"),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }
        public static int PickCall(string agent_id, string phone, string agent_type, DateTime callpick, string callType, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",DateTime.Now),
                new SqlParameter("@CALL_END_TIME",DateTime.Now),
                new SqlParameter("@CALL_HOLD_TIME",DateTime.Now),
                new SqlParameter("@CALL_PICKUP_TIME",callpick),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS","Hold"),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }
        public static int HangCall(string agent_id, string phone, string agent_type, DateTime callHang, string callType, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",DateTime.Now),
                new SqlParameter("@CALL_END_TIME",callHang),
                new SqlParameter("@CALL_HOLD_TIME",DateTime.Now),
                new SqlParameter("@CALL_PICKUP_TIME",DateTime.Now),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS","Hold"),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }

        public static void AgentLogin(string agent_id, string agent_type, DateTime LoginTime, DateTime logOutTime, DateTime BreakINTime, DateTime BreakOutTime, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@LOGIN_TIME",LoginTime),
                new SqlParameter("@LOGOUT_TIME",logOutTime),
                new SqlParameter("@BREAK_TIME",BreakINTime),
                new SqlParameter("@BREAK_END_TIME",BreakOutTime),
                new SqlParameter("@PROCESS_TYPE",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "AGENT_LOGIN", param);
        }
        #endregion

        //#region ExceptionsLog
        //public static List<ModelExceptionLogger> GetExceptions(ModelReport dataObject)
        //{
        //    List<ModelExceptionLogger> lstExceptions = new List<ModelExceptionLogger>();
        //    ModelExceptionLogger objBlank = new ModelExceptionLogger();
        //    SqlParameter[] param ={
        //            new SqlParameter("@FromDate",dataObject.fromdate),
        //             new SqlParameter("@ToDate",dataObject.todate)};
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetExceptions", param);

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        objBlank = new ModelExceptionLogger();
        //        objBlank.ExceptionId = Convert.ToInt32(dr.ItemArray[0]);
        //        objBlank.ExceptionMessage = Convert.ToString(dr.ItemArray[1].ToString());
        //        objBlank.ControllerName = Convert.ToString(dr.ItemArray[2].ToString());
        //        objBlank.ActionName = Convert.ToString(dr.ItemArray[3].ToString());
        //        objBlank.ExceptionStackTrack = Convert.ToString(dr.ItemArray[4].ToString());
        //        objBlank.ExceptionLogTime = Convert.ToString(dr.ItemArray[5].ToString());
        //        lstExceptions.Add(objBlank);
        //    }
        //    return lstExceptions;
        //}

        //public static void SaveUnhandeledExceptions(ModelExceptionLogger dataObject)
        //{

        //    SqlParameter[] param ={
        //            new SqlParameter("@ExceptionMessage",dataObject.ExceptionMessage),
        //             new SqlParameter("@ControllerName",dataObject.ControllerName),
        //             new SqlParameter("@ActionName",dataObject.ActionName),
        //             new SqlParameter("@ExceptionStackTrack",dataObject.ExceptionStackTrack)
        //    };


        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "SaveUnhandeledExceptions", param);


        //}
        //#endregion


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

        public static int PUSH_SMS_DETAIL_Consumer1(string mobile_no,string smstext, string response)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();



            SqlParameter[] param ={

                new SqlParameter("@PHONE_NO",mobile_no),
                    new SqlParameter("@TEXT_MEESAGE",smstext),
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

        public static int PUSH_SMS_DETAIL_FRT(COMPLAINT modelRemark, string response)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;



            SqlParameter[] param ={

                new SqlParameter("@PHONE_NO",modelRemark.Assign_FRTMobile),
                    new SqlParameter("@TEXT_MEESAGE",modelRemark.SMS_FRT),
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
        //public static List<ModelComplaintWiseDetailReport> ReportComplaintWiseDetail(ModelReport dataObject)
        //{

        //    List<ModelComplaintWiseDetailReport> lstReportdata = new List<ModelComplaintWiseDetailReport>();
        //    ModelComplaintWiseDetailReport objData = new ModelComplaintWiseDetailReport();
        //    SqlParameter[] param ={
        //            new SqlParameter("@BILL_MONTH",dataObject.BILL_MONTH),
        //             new SqlParameter("@BILL_YEAR",dataObject.BILL_YEAR),
        //             new SqlParameter("@CIRCLE",dataObject.OfficeCode),
        //             new SqlParameter("@COMPLAINT_TYPE",dataObject.ComplaintType)
        //               };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINT_WISE_DETAIL_REPORT", param);

        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new ModelComplaintWiseDetailReport();
        //            objData.CIRCLE = Convert.ToString(dr.ItemArray[0]);
        //            objData.TOTAL_RECEIEVED_IN_SELECTED_MONTH = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.TOTAL_REDERSSAL_COMPLAINTS_WITHIN_TIME_IN_SELECTED_MONTH = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.BALANCE_COMPLAINTS_IN_SELECTED_MONTH = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.AVERAGE_REDRESSAL_TIME_IN_SELECTED_MONTH = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.TOTAL_RECEIEVED_BEFORE_SELECTED_MONTH = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.TOTAL_REDERSSAL_COMPLAINTS_WITHIN_TIME_BEFORE_SELECTED_MONTH = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.BALANCE_COMPLAINTS_BEFORE_SELECTED_MONTH = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.AVERAGE_REDRESSAL_TIME_BEFORE_SELECTED_MONTH = Convert.ToString(dr.ItemArray[8].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<DTModel> ReportDT(ModelReport dataObject)
        //{

        //    List<DTModel> lstReportdata = new List<DTModel>();
        //    DTModel objData = new DTModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@BILL_MONTH",dataObject.BILL_MONTH),
        //             new SqlParameter("@BILL_YEAR",dataObject.BILL_YEAR),
        //             new SqlParameter("@CIRCLE",dataObject.OfficeCode)
        //               };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "DT_REPORT", param);

        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new DTModel();
        //            objData.CIRCLE = Convert.ToString(dr.ItemArray[0]);
        //            objData.DT_FAILED_FOR_SELECTED_MONTH = Convert.ToString(dr.ItemArray[1].ToString());
        //            objData.DT_REPLACE_FOR_SELECTED_MONTH = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.BALANCE_FOR_SELECTED_MONTH = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.AVERAGE_REDRESSAL_TIME_IN_SELECTED_MONTH = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.DT_FAILED_UPTO_SELECTED_MONTH = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.DT_REPLACE_UPTO_SELECTED_MONTH = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.BALANCE_FOR_UPTO_SELECTED_MONTH = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.AVERAGE_REDRESSAL_TIME_UPTO_SELECTED_MONTH = Convert.ToString(dr.ItemArray[8].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        //public static List<NewConnectionModel> ReportNewConnection(ModelReport dataObject)
        //{

        //    List<NewConnectionModel> lstReportdata = new List<NewConnectionModel>();
        //    NewConnectionModel objData = new NewConnectionModel();
        //    SqlParameter[] param ={
        //            new SqlParameter("@OFFICE_ID",dataObject.OfficeCode)
        //               };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_REPORT",param);
            
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            objData = new NewConnectionModel();
        //            objData.ZONE = Convert.ToString(dr.ItemArray[0]);
        //            objData.CIRCLE = Convert.ToString(dr.ItemArray[1]);
        //            objData.DIVISION = Convert.ToString(dr.ItemArray[2].ToString());
        //            objData.SUB_DIVISION = Convert.ToString(dr.ItemArray[3].ToString());
        //            objData.DS_COMPLAINT_RECEIVED = Convert.ToString(dr.ItemArray[4].ToString());
        //            objData.NDS_COMPLAINT_RECEIVED = Convert.ToString(dr.ItemArray[5].ToString());
        //            objData.TOTAL_REQUEST_RECEIVED = Convert.ToString(dr.ItemArray[6].ToString());
        //            objData.DS_COMPLAINT_RESOLVED = Convert.ToString(dr.ItemArray[7].ToString());
        //            objData.NDS_COMPLAINT_RESOLVED = Convert.ToString(dr.ItemArray[8].ToString());
        //            objData.TOTAL_REQUEST_RESOLVED = Convert.ToString(dr.ItemArray[9].ToString());
        //            objData.REQUEST_WITHDRAW_BY_CONSUMER = Convert.ToString(dr.ItemArray[10].ToString());
        //            objData.FIle_Pending_Request_Pending = Convert.ToString(dr.ItemArray[11].ToString());
        //            objData.FIle_Deposited_Request_Pending = Convert.ToString(dr.ItemArray[12].ToString());
        //            objData.Demand_Issued_Request_Pending = Convert.ToString(dr.ItemArray[13].ToString());
        //            objData.Demand_Deposited_Request_Pending = Convert.ToString(dr.ItemArray[14].ToString());
        //            objData.SJO_Complated_Request_Pending = Convert.ToString(dr.ItemArray[15].ToString());
        //            objData.SCO_Complated = Convert.ToString(dr.ItemArray[16].ToString());
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}
        //public static List<ModelComplaintSourceWiseDetailReport> ReportComplaintSourceWiseDetail(ModelReport dataObject)
        //{

        //    List<ModelComplaintSourceWiseDetailReport> lstReportdata = new List<ModelComplaintSourceWiseDetailReport>();
        //    ModelComplaintSourceWiseDetailReport objData = new ModelComplaintSourceWiseDetailReport();
        //    SqlParameter[] param ={
        //            new SqlParameter("@BILL_MONTH",dataObject.BILL_MONTH),
        //             new SqlParameter("@BILL_YEAR",dataObject.BILL_YEAR),
        //             new SqlParameter("@OFFICE_CODE",dataObject.OfficeCode),
        //             new SqlParameter("@COMPLAINT_SOURCE",dataObject.ComplaintSource)
        //               };
        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "SOURCE_WISE_COMPLAINT_COUNT", param);

        //    if (ds.Tables.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            objData = new ModelComplaintSourceWiseDetailReport();
        //            objData.OfficeCode = Convert.ToString(ds.Tables[0].Rows[i]["OFFICE_CODE"].ToString());

        //            for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
        //            {
        //                if (ds.Tables[0].Columns[j].ColumnName == "1")
        //                {
        //                    objData.One = Convert.ToString(ds.Tables[0].Rows[i]["1"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "2")
        //                {
        //                    objData.Two = Convert.ToString(ds.Tables[0].Rows[i]["2"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "3")
        //                {
        //                    objData.Three = Convert.ToString(ds.Tables[0].Rows[i]["3"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "4")
        //                {
        //                    objData.Four = Convert.ToString(ds.Tables[0].Rows[i]["4"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "5")
        //                {
        //                    objData.Five = Convert.ToString(ds.Tables[0].Rows[i]["5"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "6")
        //                {
        //                    objData.Six = Convert.ToString(ds.Tables[0].Rows[i]["16"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "7")
        //                {
        //                    objData.Seven = Convert.ToString(ds.Tables[0].Rows[i]["7"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "8")
        //                {
        //                    objData.Eight = Convert.ToString(ds.Tables[0].Rows[i]["8"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "9")
        //                {
        //                    objData.Nine = Convert.ToString(ds.Tables[0].Rows[i]["9"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "10")
        //                {
        //                    objData.Ten = Convert.ToString(ds.Tables[0].Rows[i]["10"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "11")
        //                {
        //                    objData.Eleven = Convert.ToString(ds.Tables[0].Rows[i]["11"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "12")
        //                {
        //                    objData.Twelve = Convert.ToString(ds.Tables[0].Rows[i]["12"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "13")
        //                {
        //                    objData.Thirteen = Convert.ToString(ds.Tables[0].Rows[i]["13"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "14")
        //                {
        //                    objData.Fourteen = Convert.ToString(ds.Tables[0].Rows[i]["14"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "15")
        //                {
        //                    objData.Fifteen = Convert.ToString(ds.Tables[0].Rows[i]["15"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "16")
        //                {
        //                    objData.Sixteen = Convert.ToString(ds.Tables[0].Rows[i]["16"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "17")
        //                {
        //                    objData.Seventeen = Convert.ToString(ds.Tables[0].Rows[i]["17"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "18")
        //                {
        //                    objData.Eighteen = Convert.ToString(ds.Tables[0].Rows[i]["18"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "19")
        //                {
        //                    objData.Nineteen = Convert.ToString(ds.Tables[0].Rows[i]["19"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "20")
        //                {
        //                    objData.Twenty = Convert.ToString(ds.Tables[0].Rows[i]["20"].ToString());
        //                }

        //                if (ds.Tables[0].Columns[j].ColumnName == "21")
        //                {
        //                    objData.TwentyOne = Convert.ToString(ds.Tables[0].Rows[i]["21"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "22")
        //                {
        //                    objData.TwentyTwo = Convert.ToString(ds.Tables[0].Rows[i]["22"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "23")
        //                {
        //                    objData.TwentyThree = Convert.ToString(ds.Tables[0].Rows[i]["23"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "24")
        //                {
        //                    objData.TwentyFour = Convert.ToString(ds.Tables[0].Rows[i]["24"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "25")
        //                {
        //                    objData.TwentyFive = Convert.ToString(ds.Tables[0].Rows[i]["25"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "26")
        //                {
        //                    objData.TwentySix = Convert.ToString(ds.Tables[0].Rows[i]["26"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "27")
        //                {
        //                    objData.TwentySeven = Convert.ToString(ds.Tables[0].Rows[i]["27"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "28")
        //                {
        //                    objData.TwentyEight = Convert.ToString(ds.Tables[0].Rows[i]["28"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "29")
        //                {
        //                    objData.TwentyNine = Convert.ToString(ds.Tables[0].Rows[i]["29"].ToString());
        //                }
        //                if (ds.Tables[0].Columns[j].ColumnName == "30")
        //                {
        //                    objData.Thirty = Convert.ToString(ds.Tables[0].Rows[i]["30"].ToString());
        //                }

        //            }
        //            lstReportdata.Add(objData);
        //        }
        //    }
        //    return lstReportdata;
        //}

        public static List<ModelComplaintSourceWiseDetailReport> ComplaintSourceWiseDetailPopUp(string sDate, string ComplaintSource, string OfficeCode)
        {

            List<ModelComplaintSourceWiseDetailReport> lstReportdata = new List<ModelComplaintSourceWiseDetailReport>
            {
                //ModelComplaintSourceWiseDetailReport objData = new ModelComplaintSourceWiseDetailReport();
                //SqlParameter[] param ={
                //        new SqlParameter("@BILL_MONTH",dataObject.BILL_MONTH),
                //         new SqlParameter("@BILL_YEAR",dataObject.BILL_YEAR),
                //         new SqlParameter("@OFFICE_CODE",dataObject.OfficeCode),
                //         new SqlParameter("@COMPLAINT_SOURCE",dataObject.ComplaintSource)
                //           };
                //DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "SOURCE_WISE_COMPLAINT_COUNT", param);

                new ModelComplaintSourceWiseDetailReport() { One = "aBC", Two = "asg" },
                new ModelComplaintSourceWiseDetailReport() { One = "aBC", Two = "asg" },
                new ModelComplaintSourceWiseDetailReport() { One = "aBC", Two = "asg" },
                new ModelComplaintSourceWiseDetailReport() { One = "aBC", Two = "asg" },
                new ModelComplaintSourceWiseDetailReport() { One = "aBC", Two = "asg" },
                new ModelComplaintSourceWiseDetailReport() { One = "aBC", Two = "asg" }

            };

            return lstReportdata;
        }

        //public static DataTable ComplaintRepeat(ModelReport modelReport)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlParameter[] param ={
        //            new SqlParameter("@FROM_DATE",Convert.ToDateTime(modelReport.fromdate)),
        //             new SqlParameter("@TO_DATE",Convert.ToDateTime(modelReport.todate)),
        //             new SqlParameter("@OFFICE_CODE",modelReport.OfficeCode),
        //             new SqlParameter("@COMPLAINT_TYPE",modelReport.ComplaintType)
        //               };

        //        dt = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetRepeatedComplaints", param).Tables[0];
        //        return dt;
        //    }
        //    catch
        //    {
        //        return dt;
        //    }
        //}

        public static List<SystemAvailabilityIssue> GETSystemAvailabilityIssue()
        {
            List<SystemAvailabilityIssue> systemAvailabilityIssues = new List<SystemAvailabilityIssue>();
            SystemAvailabilityIssue objBlank = new SystemAvailabilityIssue();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GETSystemAvailabilityIssue");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SystemAvailabilityIssue();
                objBlank.ID = Convert.ToString(dr.ItemArray[0]);
                objBlank.IssueType = Convert.ToString(dr.ItemArray[1].ToString());
                systemAvailabilityIssues.Add(objBlank);
            }
            return systemAvailabilityIssues;
        }

        public static List<NoNITIssue> GETNonITIssue()
        {
            List<NoNITIssue> systemAvailabilityIssues = new List<NoNITIssue>();
            NoNITIssue objBlank = new NoNITIssue();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GETNonITTypes");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new NoNITIssue();
                objBlank.ID = Convert.ToString(dr.ItemArray[0]);
                objBlank.IssueType = Convert.ToString(dr.ItemArray[1].ToString());
                systemAvailabilityIssues.Add(objBlank);
            }
            return systemAvailabilityIssues;
        }


        public static int SaveSystemAvailabilityIssue(ModelSystemAvaialablity dataObject)
        {
            SqlParameter[] param ={new SqlParameter("@FromDate",dataObject.From_Date),
                new SqlParameter("@ToDate",dataObject.To_Date),
                new SqlParameter("@IssueID",dataObject.SystemAvailabilityIssue_ID),
                new SqlParameter("@UserID",dataObject.EnterByUserID)


            };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "System_Availability_Penalty_SP", param);
            return 1;
        }

        public static int SaveNonITIssue(ModelNonIT dataObject)
        {
            SqlParameter[] param ={new SqlParameter("@FromDate",dataObject.From_Date),
                new SqlParameter("@IssueID",dataObject.NonITIssue_ID),
                new SqlParameter("@UserID",dataObject.EnterByUserID),
                new SqlParameter("@number",dataObject.number)


            };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Non_IT_Penalty_SP", param);
            return 1;
        }

        public static int SaveCCCAgent(CCCAgent dataObject)
        {
            SqlParameter[] param ={new SqlParameter("@FromDate",dataObject.From_Date),
                new SqlParameter("@UserID",dataObject.EnterByUserID),
                new SqlParameter("@number",dataObject.number)


            };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "CCC_AGENT_Penalty_SP", param);
            return 1;
        }
        public static int SaveUniformMissing(CCCAgent dataObject)
        {
            SqlParameter[] param ={new SqlParameter("@FromDate",dataObject.From_Date),
                new SqlParameter("@UserID",dataObject.EnterByUserID),
                new SqlParameter("@number",dataObject.number),
                new SqlParameter("@UniformType",dataObject.UniformType)
              

            };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Uniform_Missing_Penalty_SP", param);
            return 1;
        }


        #region FalseComplaint



        public static List<ModelFalseComplaintGrid> SEARCH_COMPLAINT_FOR_FALSE_CLOSURE(ModelReport dataObject)
        {
            SqlParameter parmretTotalRecords = new SqlParameter();
            parmretTotalRecords.ParameterName = "@TOTALRECORDS";
            parmretTotalRecords.DbType = DbType.Int32;
            parmretTotalRecords.Size = 8;
            parmretTotalRecords.Direction = ParameterDirection.Output;

            int TotalRec = 0;

            List<ModelFalseComplaintGrid> lstReportdata = new List<ModelFalseComplaintGrid>();
            ModelFalseComplaintGrid objData = new ModelFalseComplaintGrid();
            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",dataObject.ComplaintNo),
                    new SqlParameter("@STARTROWINDEX",dataObject.start),
                    new SqlParameter("@MAXIMUMROWS",dataObject.length),parmretTotalRecords};

            log.Debug(" SEARCH_COMPLAINT_FOR_FALSE_CLOSURE IP " + HelperClass.GetIPHelper() + " Proc Start Time :  " + DateTime.Now.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "SEARCH_COMPLAINT_FOR_FALSE_CLOSURE", param);
            log.Debug(" SEARCH_COMPLAINT_FOR_FALSE_CLOSURE IP " + HelperClass.GetIPHelper() + " Proc End Time :  " + DateTime.Now.ToString());
            if (param[2].Value != DBNull.Value)// status
                TotalRec = Convert.ToInt32(param[2].Value);
            else
                TotalRec = 0;

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData = new ModelFalseComplaintGrid();
                    objData.KNO = Convert.ToString(dr.ItemArray[0].ToString());
                    objData.NAME = Convert.ToString(dr.ItemArray[1].ToString());
                    objData.MOBILE_NO = Convert.ToString(dr.ItemArray[2].ToString());
                    objData.COMPLAINT_DATE = Convert.ToString(dr.ItemArray[3].ToString());
                    objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[4].ToString());
                    objData.OFFICE_NAME = Convert.ToString(dr.ItemArray[5].ToString());
                    objData.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[6].ToString());

                    objData.Total = TotalRec;
                    lstReportdata.Add(objData);
                }
            }
            return lstReportdata;
        }
        #endregion





        public static int SaveFalseCompalint(ModelFalseComplaint dataObject)
        {
            try
            {
                SqlParameter[] param ={new SqlParameter("@COMPLAINT_NO",dataObject.ComplaintNo),
                new SqlParameter("@UserID",dataObject.EnterByUserID),
                new SqlParameter("@REMARK",dataObject.Remarks)};
                DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "INSERT_COMPLAINT_FOR_FALSE_CLOSURE", param);
                return 1;
            }
            catch
            {

                return -1;
            }
        }

        #region ShowHaresmentComplaintDetails
        public static List<ModelDashboardHaresment> ShowHaresmentComplaintDetails(string compstatus, string cmonth)
        {
            List<ModelDashboardHaresment>  modelDashboardHaresments  = new List<ModelDashboardHaresment>();
            ModelDashboardHaresment objBlank = new ModelDashboardHaresment();
            SqlParameter[] param ={
                new SqlParameter("@COMPLAINT_MONTH",cmonth),
                new SqlParameter("@COMPLAINT_STATUS",compstatus)
            };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "EXPORT_HARASSMENT", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelDashboardHaresment();
                objBlank.COMPLAINT_NO = Convert.ToString(dr.ItemArray[0].ToString());
                objBlank.KNO = Convert.ToString(dr.ItemArray[1].ToString());
                objBlank.COMPLAINT_DATE = Convert.ToString(dr.ItemArray[2].ToString());
                objBlank.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[3].ToString());
                objBlank.NAME = Convert.ToString(dr.ItemArray[4].ToString());
                objBlank.ADDRESS = Convert.ToString(dr.ItemArray[5]);
                objBlank.MOBILE_NO = Convert.ToString(dr.ItemArray[6].ToString());
                objBlank.STATUS = Convert.ToString(dr.ItemArray[7].ToString());
                modelDashboardHaresments.Add(objBlank);
            }
            return modelDashboardHaresments;
        }
        #endregion

        //#region GetRepeatedComplaints_RAW
        //public static List<ModelDashboardHaresment> GetRepeatedComplaints_RAW(ModelReport modelReport)
        //{
        //    List<ModelDashboardHaresment> modelDashboardHaresments = new List<ModelDashboardHaresment>();
        //    ModelDashboardHaresment objBlank = new ModelDashboardHaresment();
        //    SqlParameter[] param ={
        //        new SqlParameter("@COMPLAINT_TYPE",modelReport.ComplaintType),
        //        new SqlParameter("@OFFICE_CODE",modelReport.OfficeCode),
        //        new SqlParameter("@FROM_DATE",modelReport.fromdate),
        //        new SqlParameter("@TO_DATE",modelReport.todate),
        //        new SqlParameter("@REPEATE_COUNT",modelReport.count)
        //    };

        //    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetRepeatedComplaints_RAW", param);

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        objBlank = new ModelDashboardHaresment();
        //        objBlank.KNO = Convert.ToString(dr.ItemArray[0].ToString());
        //        objBlank.COMPLAINT_DATE = Convert.ToString(dr.ItemArray[1].ToString());
        //        objBlank.NAME = Convert.ToString(dr.ItemArray[2].ToString());
        //        objBlank.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[3].ToString());
        //        objBlank.MOBILE_NO = Convert.ToString(dr.ItemArray[4].ToString());
        //        objBlank.ADDRESS = Convert.ToString(dr.ItemArray[5]);

        //        modelDashboardHaresments.Add(objBlank);
        //    }
        //    return modelDashboardHaresments;
        //}
        //#endregion

        #region GET_CC_MOBILE_NO
        public static string GET_CC_MOBILE_NO(string sdoCode)
        {
            string retMsg = String.Empty; ;
            SqlParameter[] param ={new SqlParameter("@SDO_CODE", sdoCode)};
            try
            {
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_CC_MOBILE_NO", param);
                if(ds.Tables.Count > 0) 
                {
                    retMsg = ds.Tables[0].Rows[0][0].ToString(); 
                }
            }
            catch (Exception ex)
            {
                
            }

            return retMsg;

        }
        #endregion

        //#region GET_COMPLAINT_DETAIL
        //public static string GET_COMPLAINT_DETAIL(Int64 Complaint_NO)
        //{
        //    string retMsg = String.Empty; ;
        //    SqlParameter[] param = { new SqlParameter("@Complaint_NO", Complaint_NO) };
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_COMPLAINT_DETAIL_BY_COMPLAINT_NO", param);
        //        if (ds.Tables.Count > 0)
        //        {
        //            retMsg = ds.Tables[0].Rows[0][0].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return retMsg;

        //}
        //#endregion
        #region GET_COMPLAINT_DETAIL
        public static DataSet GET_COMPLAINT_DETAIL(Int64 Complaint_NO)
        {
            string retMsg = String.Empty;

            DataSet ds = new DataSet();
            SqlParameter[] param = { new SqlParameter("@Complaint_NO", Complaint_NO) };
            try
            {
                //DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_COMPLAINT_DETAIL_BY_COMPLAINT_NO", param);
                //if (ds.Tables.Count > 0)
                //{
                //    retMsg = ds.Tables[0].Rows[0][0].ToString();
                //}
            }
            catch (Exception ex)
            {

            }

            return ds;

        }
        #endregion

        #region New Connection
        public static List<SelectListItem> GetNewConnectionSteps()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_STEP");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        public static List<ModelComplaintStepsGrid> GetNewConnectionStepsGrid(Int64 ComplaintNo)
        {
            SqlParameter[] param = {new SqlParameter("@COMPLAINT_NO", ComplaintNo) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_STEP_DETAIL", param);
            var newConnList = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new ModelComplaintStepsGrid
                {
                         COMPLAINT_NO = dataRow.Field<Int64>("COMPLAINT_NO"),
                         STEP_NAME = dataRow.Field<string>("STEP_NAME"),
                         DS_NDS = dataRow.Field<string>("DS_NDS")
                }).ToList();

            return newConnList;
        }

        public  static async Task<int> SaveNewConnection(ModelNewConnection dataObject)
        {
            try
            {
                SqlParameter[] param ={new SqlParameter("@COMPLAINT_NO",dataObject.ComplaintNo),
                new SqlParameter("@STEP_ID",dataObject.NewConnectionStepId),
                new SqlParameter("@USER_ID",dataObject.userId),
                new SqlParameter("@DS_NDS",dataObject.rdoDsOrNds)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_STEP_CHANGE", param);
                return 1;
            }
            catch
            {
                return -1;
            }
        }


        #endregion

        public static List<SelectListItem> FillBillYear()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Month_Year");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> FillGetDivision()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetDivision");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        public static List<SelectListItem> FillGetSubDivision(string Division_ID)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={new SqlParameter("@Division_ID",Division_ID)



            };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetSubDivision", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

    }
}
