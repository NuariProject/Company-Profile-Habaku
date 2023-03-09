using api_cms.Object;
using CMS_API.ConfigurationDB;
using CMS_API.Library;
using CMS_API.Object;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static api_cms.Object.User;
using static CMS_API.Library.GeneralLib;
using static CMS_API.Object.Log;

namespace api_cms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public GeneralResponseData<List<ListUsers>> GetAll()
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListUsers>> ObjResponseListUsers = new GeneralResponseData<List<ListUsers>>();
            List<ListUsers> ObjListUsers = new List<ListUsers>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region FlowControl
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.SELECT_ALL_USER;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ObjListUsers.Add(new ListUsers
                                {
                                    user_id = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    user_name = reader.GetString(reader.GetOrdinal("user_name")),
                                    pasasword = reader.GetString(reader.GetOrdinal("password")),
                                    role = reader.GetString(reader.GetOrdinal("role"))
                                });
                            }
                        }
                    }
                }

                ObjResponseListUsers = new GeneralResponseData<List<ListUsers>>
                {
                    Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS,
                    Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS,
                    Data = ObjListUsers
                };

            }
            catch (Exception ex)
            {
                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;

                throw;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListUsers;
            writeFileLogResult.param = null;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListUsers.Code, strMethod);

            #endregion

            return ObjResponseListUsers;
        }

        [HttpGet("{user_id}")]
        public GeneralResponseData<List<ListUsers>> GetById(string user_id)
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListUsers>> ObjResponseListUsers = new GeneralResponseData<List<ListUsers>>();
            List<ListUsers> ObjListUsers = new List<ListUsers>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Validation
            if (string.IsNullOrEmpty(user_id))
            {
                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                return ObjResponseListUsers;
            }
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.SELECT_USER;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ObjListUsers.Add(new ListUsers
                                {
                                    user_id = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    user_name = reader.GetString(reader.GetOrdinal("user_name")),
                                    pasasword = reader.GetString(reader.GetOrdinal("password")),
                                    role = reader.GetString(reader.GetOrdinal("role"))
                                });
                            }
                        }
                    }
                }

                ObjResponseListUsers = new GeneralResponseData<List<ListUsers>>
                {
                    Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS,
                    Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS,
                    Data = ObjListUsers
                };
            }
            catch (Exception ex)
            {
                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListUsers;
            writeFileLogResult.param = user_id;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListUsers.Code, strMethod);

            #endregion

            return ObjResponseListUsers;
        }

        [HttpPost]
        public GeneralResponseData<List<ListUsers>> Create(ParamCreateUser collection)
        {
            #region Intansiasi Object
            var ObjParamRequestUser = new ParamCreateUser();
            var ObjResponse = new GeneralResponse();
            GeneralResponseData<List<ListUsers>> ObjResponseListUsers = new GeneralResponseData<List<ListUsers>>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Validation
            if (string.IsNullOrEmpty(collection.user_name) || string.IsNullOrEmpty(collection.password) || string.IsNullOrEmpty(collection.role))
            {
                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                return ObjResponseListUsers;
            }
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.INSERT_USER;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_name", collection.user_name);
                        command.Parameters.AddWithValue("@password", collection.password);
                        command.Parameters.AddWithValue("@role", collection.role);
                        command.Parameters.AddWithValue("@created_at", DateTime.Now);
                        command.Parameters.AddWithValue("@created_by", collection.created_by);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS;
            }
            catch (Exception ex)
            {
                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponse;
            writeFileLogResult.param = collection;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponse.Code, strMethod);

            #endregion

            return ObjResponseListUsers;
        }

        [HttpPut("{user_id}")]
        public GeneralResponseData<List<ListUsers>> Update(ParamUpdateUser collection)
        {
            #region Intansiasi Object
            var ObjParamRequestUser = new User();
            var ObjResponse = new GeneralResponse();
            GeneralResponseData<List<ListUsers>> ObjResponseListUsers = new GeneralResponseData<List<ListUsers>>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Validation
            if (collection.user_id == null || collection.user_id == 0 || string.IsNullOrEmpty(collection.user_name) || string.IsNullOrEmpty(collection.password) || string.IsNullOrEmpty(collection.role))
            {
                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                return ObjResponseListUsers;
            }
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.UPDATE_USER;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", collection.user_id);
                        command.Parameters.AddWithValue("@user_name", collection.user_name);
                        command.Parameters.AddWithValue("@password", collection.password);
                        command.Parameters.AddWithValue("@role", collection.role);
                        command.Parameters.AddWithValue("@modified_at", DateTime.Now);
                        command.Parameters.AddWithValue("@modified_by", collection.modified_by);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ObjResponseListUsers.Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS;
                ObjResponseListUsers.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS;
            }
            catch (Exception ex)
            {
                ObjResponse.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponse.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponse;
            writeFileLogResult.param = collection;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponse.Code, strMethod);

            #endregion

            return ObjResponseListUsers;
        }

    }
}
