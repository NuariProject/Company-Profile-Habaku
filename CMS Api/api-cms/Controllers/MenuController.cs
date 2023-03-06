﻿using CMS_API.ConfigurationDB;
using CMS_API.Library;
using CMS_API.Object;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static CMS_API.Library.GeneralLib;
using static CMS_API.Object.Log;

namespace CMS_API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MenuController : ControllerBase
    {
        // GET: MenuController
        [HttpGet]
        [Route("[controller]")]
        public GeneralResponseData<List<ListMenus>> GetAllMenus()
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListMenus>> ObjResponseListMenus = new GeneralResponseData<List<ListMenus>>();
            List<ListMenus> ObjListMenus = new List<ListMenus>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.SELECT_ALL_MENU;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ObjListMenus.Add(new ListMenus
                                {
                                    menu_id = reader.GetInt32(reader.GetOrdinal("menu_id")),
                                    menu_name = reader.GetString(reader.GetOrdinal("menu_name")),
                                    status = reader.GetBoolean(reader.GetOrdinal("status"))
                                });
                            }
                        }
                    }
                }

                ObjResponseListMenus = new GeneralResponseData<List<ListMenus>>
                {
                    Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS,
                    Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS,
                    Data = ObjListMenus
                };

            }
            catch (Exception ex)
            {
                ObjResponseListMenus.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListMenus.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListMenus;
            writeFileLogResult.param = null;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListMenus.Code, strMethod);

            #endregion

            return ObjResponseListMenus;
        }

        // GET : MenuController/5
        [HttpGet("[controller]/{menu_id}")]
        public GeneralResponseData<List<ListMenus>> GetMenuByName(string menu_id)
        {
            #region Intansiasi Object
            GeneralResponseData<List<ListMenus>> ObjResponseListMenus = new GeneralResponseData<List<ListMenus>>();
            List<ListMenus> ObjListMenus = new List<ListMenus>();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Validation
            if (string.IsNullOrEmpty(menu_id))
            {
                ObjResponseListMenus.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListMenus.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                return ObjResponseListMenus;
            }
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.SELECT_MENU;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@menu_id", menu_id);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ObjListMenus.Add(new ListMenus
                                {
                                    menu_id = reader.GetInt32(reader.GetOrdinal("menu_id")),
                                    menu_name = reader.GetString(reader.GetOrdinal("menu_name")),
                                    status = reader.GetBoolean(reader.GetOrdinal("status"))
                                });
                            }
                        }
                    }
                }

                ObjResponseListMenus = new GeneralResponseData<List<ListMenus>>
                {
                    Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS,
                    Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS,
                    Data = ObjListMenus
                };
            }
            catch (Exception ex)
            {
                ObjResponseListMenus.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponseListMenus.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR + ex.Message;
            }
            #endregion

            #region Create Log File

            //create log file
            writeFileLogResult.id = GenerateUniqueID();
            writeFileLogResult.result = ObjResponseListMenus;
            writeFileLogResult.param = menu_id;
            writeFileLogResult.trxDate = GetDateFormatDate();
            writeFileLogResult.trxTime = GetDateFormatTime();
            //writeFileLogResult.paramExt = strJSONObj;

            WriteFileLog.Write(JsonConvert.SerializeObject(writeFileLogResult), strControllerName, ObjResponseListMenus.Code, strMethod);

            #endregion

            return ObjResponseListMenus;
        }

        // POST: MenuController/Create
        [Route("[controller]")]
        [HttpPost]
        public GeneralResponse Create(Menu collection)
        {

            #region Intansiasi Object
            var ObjParamRequestMenu = new Menu();
            var ObjResponse = new GeneralResponse();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Validation
            if (string.IsNullOrEmpty(collection.menu_name))
            {
                ObjResponse.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponse.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_NULL;

                return ObjResponse;
            }
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.INSERT_MENU;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@menu_name", collection.menu_name);
                        command.Parameters.AddWithValue("@status", 0);
                        command.Parameters.AddWithValue("@created_at", DateTime.Now);
                        command.Parameters.AddWithValue("@created_by", collection.created_by);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ObjResponse.Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS;
                ObjResponse.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS;
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

            return ObjResponse;
        }

        // PUT: MenuController/5
        [HttpPut("[controller]/{menu_id}")]
        public GeneralResponse Update(UpdateMenu collection)
        {
            #region Intansiasi Object
            var ObjParamRequestMenu = new Menu();
            var ObjResponse = new GeneralResponse();

            WriteFileLogResult writeFileLogResult = new WriteFileLogResult();
            string strMethod = this.HttpContext.Request.Method;
            string strControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            #endregion

            #region Validation
            if (collection.menu_id == null || collection.menu_id == 0)
            {
                ObjResponse.Code = GeneralLib.Constan.CONST_RES_CD_ERROR;
                ObjResponse.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_ERROR_ID_NULL;

                return ObjResponse;
            }
            #endregion

            #region Flow Control
            try
            {
                string connectionString = HabakuDB.ConnectionStrings.HABAKU_CONNECTION;
                string query = HabakuDB.Query.UPDATE_MENU;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@menu_id", collection.menu_id);
                        command.Parameters.AddWithValue("@menu_name", collection.menu_name);
                        command.Parameters.AddWithValue("@status", collection.status);
                        command.Parameters.AddWithValue("@modified_at", DateTime.Now);
                        command.Parameters.AddWithValue("@modified_by", collection.modified_by);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ObjResponse.Code = GeneralLib.Constan.CONST_RES_CD_SUCCESS;
                ObjResponse.Messages = GeneralLib.Constan.CONST_RES_MESSAGE_SUCCESS;
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

            return ObjResponse;
        }
    }
}