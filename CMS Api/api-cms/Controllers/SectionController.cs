using api_cms.Object;
using CMS_API.ConfigurationDB;
using CMS_API.Library;
using CMS_API.Object;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static api_cms.Object.Section;
using static api_cms.Object.User;
using static CMS_API.Library.GeneralLib;
using static CMS_API.Object.Log;

namespace api_cms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        [HttpGet]
        public GeneralResponseData<List<ListSections>> GetAll()
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListSections>> ObjResponseListSections = new GeneralResponseData<List<ListSections>>();
            List<ListSections> ObjListSections = new List<ListSections>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region FlowControl
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.SELECT_ALL_SECTION;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ObjListSections.Add(new ListSections
                                {
                                    section_id = reader.GetInt32(reader.GetOrdinal("section_id")),
                                    menu_id = reader.GetInt32(reader.GetOrdinal("menu_id")),
                                    section_name = reader.GetString(reader.GetOrdinal("section_name")),
                                    section_number = reader.GetInt32(reader.GetOrdinal("section_number")),
                                    section_approve = reader.GetInt32(reader.GetOrdinal("section_approve"))
                                });
                            }
                        }
                    }
                }

                ObjResponseListSections = new GeneralResponseData<List<ListSections>>
                {
                    Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS,
                    Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS,
                    Data = ObjListSections
                };

            }
            catch (Exception ex)
            {
                ObjResponseListSections.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListSections.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;

                throw;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListSections;
            writeFileLogResult.param = null;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListSections.Code, strMethod);

            #endregion

            return ObjResponseListSections;
        }

        [HttpGet("{section_id}")]
        public GeneralResponseData<List<ListSections>> GetById(int section_id)
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListSections>> ObjResponseListSections = new GeneralResponseData<List<ListSections>>();
            List<ListSections> ObjListSections = new List<ListSections>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            bool isValid = true;
            #endregion

            #region Validation
            if (section_id == null || section_id == 0)
            {
                ObjResponseListSections.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListSections.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                isValid = false;
            }

            #endregion

            #region FlowControl
            if (isValid)
            {
                try
                {
                    string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                    string query = HabakuDB.Query.SELECT_SECTION;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@section_id", section_id);

                            connection.Open();
                            command.ExecuteNonQuery();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ObjListSections.Add(new ListSections
                                    {
                                        section_id = reader.GetInt32(reader.GetOrdinal("section_id")),
                                        menu_id = reader.GetInt32(reader.GetOrdinal("menu_id")),
                                        section_name = reader.GetString(reader.GetOrdinal("section_name")),
                                        section_number = reader.GetInt32(reader.GetOrdinal("section_number")),
                                        section_approve = reader.GetInt32(reader.GetOrdinal("section_approve"))
                                    });
                                }
                            }
                        }
                    }

                    ObjResponseListSections = new GeneralResponseData<List<ListSections>>
                    {
                        Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS,
                        Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS,
                        Data = ObjListSections
                    };

                }
                catch (Exception ex)
                {
                    ObjResponseListSections.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                    ObjResponseListSections.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;

                    throw;
                }
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListSections;
            writeFileLogResult.param = section_id;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListSections.Code, strMethod);

            #endregion

            return ObjResponseListSections;
        }

        [HttpPost]
        public GeneralResponseData<List<ListSections>> Create(ParamCreateSection collection)
        {
            #region Intansiasi Object
            var ObjParamRequestSection = new ParamCreateSection();
            var ObjResponse = new GeneralResponse();
            GeneralResponseData<List<ListSections>> ObjResponseListSection = new GeneralResponseData<List<ListSections>>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            bool isValid = true;
            #endregion

            #region Validation
            if (collection.menu_id == 0 || collection.menu_id == null || string.IsNullOrEmpty(collection.section_name) || collection.section_number == null)
            {
                ObjResponseListSection.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListSection.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                isValid = false;
            }
            #endregion

            #region FlowControl
            if (isValid)
            {
                try
                {
                    string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                    string query = HabakuDB.Query.INSERT_SECTION;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@menu_id", collection.menu_id);
                            command.Parameters.AddWithValue("@section_name", collection.section_name);
                            command.Parameters.AddWithValue("@section_number", collection.section_number);
                            command.Parameters.AddWithValue("@section_approve", collection.section_approve);
                            command.Parameters.AddWithValue("@created_at", DateTime.Now);
                            command.Parameters.AddWithValue("@created_by", collection.created_by);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    ObjResponseListSection.Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS;
                    ObjResponseListSection.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS;
                }
                catch (Exception ex)
                {
                    ObjResponseListSection.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                    ObjResponseListSection.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
                }
            }

            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListSection;
            writeFileLogResult.param = collection;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListSection.Code, strMethod);

            #endregion

            return ObjResponseListSection;
        }

        [HttpPut]
        public GeneralResponseData<List<ListSections>> Update(ParamUpdateSection collection)
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListSections>> ObjResponseListSection = new GeneralResponseData<List<ListSections>>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            bool isValid = true;
            #endregion

            #region Validation
            if (collection.menu_id == 0 || collection.menu_id == null || string.IsNullOrEmpty(collection.section_name) || collection.section_number == null)
            {
                ObjResponseListSection.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListSection.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                isValid = false;
            }
            #endregion

            #region FlowControl
            if (isValid)
            {
                try
                {
                    string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                    string query = HabakuDB.Query.UPDATE_SECTION;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@section_id", collection.section_id);
                            command.Parameters.AddWithValue("@menu_id", collection.menu_id);
                            command.Parameters.AddWithValue("@section_name", collection.section_name);
                            command.Parameters.AddWithValue("@section_number", collection.section_number);
                            command.Parameters.AddWithValue("@section_approve", collection.section_approve);
                            command.Parameters.AddWithValue("@modified_at", DateTime.Now);
                            command.Parameters.AddWithValue("@modified_by", collection.modified_by);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    ObjResponseListSection.Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS;
                    ObjResponseListSection.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS;
                }
                catch (Exception ex)
                {
                    ObjResponseListSection.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                    ObjResponseListSection.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
                }
            }

            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListSection;
            writeFileLogResult.param = collection;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListSection.Code, strMethod);

            #endregion

            return ObjResponseListSection;
        }
    }
}
