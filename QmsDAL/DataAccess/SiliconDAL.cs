using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using QmsDAL.Context;
using QmsDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QmsDAL.DataAccess
{
    public class SiliconDAL
    {
        QmsContext _repoQms = null;
        public SiliconDAL()
        { 
            _repoQms = new QmsContext();
        }
        #region GetPreSilicon [Code Owner : Chenthilkumaran (24-04-2023)]
        public object GetPreSilicon(ReqGetSilicon objData)
        {
            string _commandText = "[dbo].[USP_GetSilicon]";
            PreSilicon response = new PreSilicon();
            List<GetPlatformSku> _lstGetPlatformSku = new List<GetPlatformSku>();
            List<PreSiliconList> _lstPreSilicon = new List<PreSiliconList>();
            List<PlaformSkuList> _lstPlatformSku = new List<PlaformSkuList>();
            try
            {
                List<SqlDataRecord> resultTable = new List<SqlDataRecord>();
                List<SqlMetaData> sqlMetaData = new List<SqlMetaData>();
                sqlMetaData.Add(new SqlMetaData("PlatformName", SqlDbType.NVarChar, 500));
                sqlMetaData.Add(new SqlMetaData("SkuName", SqlDbType.NVarChar, 500));

                foreach (var item in objData.SiliconReq)
                {
                    SqlDataRecord row = new SqlDataRecord(sqlMetaData.ToArray());
                    row.SetValues(new object[] {
                        item.PlatformName,
                        item.SKUName
                    });
                    resultTable.Add(row);
                }
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                    SqlParameter[] parameters = null;
                    parameters = new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@ObjPlatformSku", SqlDbType = SqlDbType.Structured,TypeName="dbo.ObjPlatformSku", Direction = ParameterDirection.Input,Value = resultTable.Count>0? resultTable:null},
                        new SqlParameter() { ParameterName = "@Type", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = 1 },
                    };
                    using (DataSet ds = DatabaseContext.GetDataSetWithUserDefinedTableTypeParameter(cmd, _commandText, parameters))
                    {
                        if (ds != null)
                        {
                            _lstGetPlatformSku = (from DataRow dr in ds.Tables[0].Rows
                                                  select new GetPlatformSku
                                                  {
                                                      PlatformId = Convert.ToInt32(dr["PlatformId"]),
                                                      PlatformName = Convert.ToString(dr["PlatformName"]),
                                                      PlatformShortName = Convert.ToString(dr["PlatformShortName"]),
                                                      SkuId = Convert.ToInt32(dr["SKUId"]),
                                                      SkuName = Convert.ToString(dr["SKUName"])
                                                  }).ToList();

                            _lstPreSilicon = (from DataRow dr in ds.Tables[1].Rows
                                              select new PreSiliconList()
                                              {
                                                  PlatformName = Convert.ToString(dr["PlatformName"]),
                                                  PlatformShortName = Convert.ToString(dr["PlatformShortName"]),
                                                  SkuName = Convert.ToString(dr["SkuName"]),
                                                  fRNewPercentage = Convert.ToDouble(dr["fRNewPercentage"]),
                                                  fRLegacyPercentage = Convert.ToDouble(dr["fRLegacyPercentage"]),
                                                  fRNewCounts = Convert.ToInt32(dr["fRNewCounts"]),
                                                  fRLegacyCounts = Convert.ToInt32(dr["fRLegacyCounts"]),
                                                  CurrentNewPercentage = Convert.ToDouble(dr["CurrentNewPercentage"]),
                                                  CurrentLegacyPercentage = Convert.ToDouble(dr["CurrentLegacyPercentage"]),
                                                  CurrentNewCounts = Convert.ToInt32(dr["CurrentNewCounts"]),
                                                  CurrentLegacyCounts = Convert.ToInt32(dr["CurrentLegacyCounts"]),
                                                  ccBTotal = Convert.ToInt32(dr["ccBTotal"]),
                                                  ccBAdded = Convert.ToInt32(dr["ccBAdded"]),
                                                  ccBRemoved = Convert.ToInt32(dr["ccBRemoved"]),
                                                  FR_Simulation_Snapshot = Convert.ToDouble(dr["FR_Simulation_Snapshot"]),
                                                  FR_Emulation_Snapshot = Convert.ToDouble(dr["FR_Emulation_Snapshot"]),
                                                  N_1_Snapshot = Convert.ToDouble(dr["N_1_Snapshot"]),
                                                  EnabledIntegrated_Snapshot = Convert.ToDouble(dr["EnabledIntegrated_Snapshot"]),
                                                  PRQPV_PreSilicon_Snapshot = Convert.ToDouble(dr["PRQPV_PreSilicon_Snapshot"]),
                                                  PRQPV_PostSilicon_Snapshot = Convert.ToDouble(dr["PRQPV_PostSilicon_Snapshot"]),
                                                  FR_Simulation_Current = Convert.ToDouble(dr["FR_Simulation_Current"]),
                                                  FR_Emulation_Current = Convert.ToDouble(dr["FR_Emulation_Current"]),
                                                  N_1_Current = Convert.ToDouble(dr["N_1_Current"]),
                                                  EnabledIntegrated_Current = Convert.ToDouble(dr["EnabledIntegrated_Current"]),
                                                  PRQPV_PreSilicon_Current = Convert.ToDouble(dr["PRQPV_PreSilicon_Current"]),
                                                  PRQPV_PostSilicon_Current = Convert.ToDouble(dr["PRQPV_PostSilicon_Current"]),
                                                  
                                              }).ToList();
                        }
                        conn.Close();

                        foreach (var item in _lstGetPlatformSku)
                        {
                            PlaformSkuList _lstPS = new PlaformSkuList();
                            _lstPS.PlatformId = item.PlatformId;
                            _lstPS.PlatformName = item.PlatformName;
                            _lstPS.PlatformShortName = item.PlatformShortName;
                            _lstPS.Sku = new List<SkuList>();

                            var platforms = _lstGetPlatformSku.Where(x => x.PlatformId == item.PlatformId);
                            foreach (var sku in platforms)
                            {
                                SkuList _lstSku = new SkuList();
                                if (sku.SkuName != "")
                                {
                                    _lstSku.SkuName = sku.SkuName;
                                    _lstSku.SkuId = sku.SkuId;

                                    _lstPS.Sku.Add(_lstSku);
                                }
                            }
                            _lstPlatformSku.Add(_lstPS);
                        }
                        response.platformSku = _lstPlatformSku.GroupBy(g => g.PlatformId).Select(x => x.FirstOrDefault()).ToList();
                        response.preSilicon = _lstPreSilicon;
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        #endregion
        #region GetPostSiliconDefects [Code Owner : Chenthilkumaran (24-04-2023)]
        public object GetPostSiliconDefects(ReqGetSilicon objData)
        {
            string _commandText = "[dbo].[USP_GetSilicon]";
            PostSiliconDefects response = new PostSiliconDefects();
            List<Platform> _lstPlatform = new List<Platform>();
            List<PostSiliconDefectsList> _lstPostSiliconDefects = new List<PostSiliconDefectsList>();

            try
            {
                List<SqlDataRecord> resultTable = new List<SqlDataRecord>();
                List<SqlMetaData> sqlMetaData = new List<SqlMetaData>();
                sqlMetaData.Add(new SqlMetaData("PlatformName", SqlDbType.NVarChar, 500));
                sqlMetaData.Add(new SqlMetaData("SkuName", SqlDbType.NVarChar, 500));

                foreach (var item in objData.SiliconReq)
                {
                    SqlDataRecord row = new SqlDataRecord(sqlMetaData.ToArray());
                    row.SetValues(new object[] {
                        item.PlatformName,
                        ""
                    });
                    resultTable.Add(row);
                }
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                    SqlParameter[] parameters = null;
                    parameters = new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@ObjPlatformSku", SqlDbType = SqlDbType.Structured,TypeName="dbo.ObjPlatformSku", Direction = ParameterDirection.Input,Value = resultTable.Count>0? resultTable:null},
                        new SqlParameter() { ParameterName = "@Type", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = 2 },
                    };
                    using (DataSet ds = DatabaseContext.GetDataSetWithUserDefinedTableTypeParameter(cmd, _commandText, parameters))
                    {
                        if (ds != null)
                        {
                            _lstPlatform = (from DataRow dr in ds.Tables[0].Rows
                                                  select new Platform
                                                  {
                                                      PlatformId = Convert.ToInt32(dr["PlatformId"]),
                                                      PlatformName = Convert.ToString(dr["PlatformName"]),
                                                      PlatformShortName = Convert.ToString(dr["PlatformShortName"]),                                                      
                                                  }).ToList();

                            _lstPostSiliconDefects = (from DataRow dr in ds.Tables[1].Rows
                                                    select new PostSiliconDefectsList()
                                                    {
                                                        PlatformName = Convert.ToString(dr["PlatformName"]),
                                                        PlatformShortName = Convert.ToString(dr["PlatformShortName"]),                                                        
                                                        CurrentDefects = Convert.ToInt32(dr["CurrentDefects"]),
                                                        CurrentTPT = Convert.ToInt32(dr["CurrentTPT"]),
                                                        Defects = Convert.ToInt32(dr["DefectsAtPV"]),
                                                        TPT = Convert.ToInt32(dr["TPTAtPV"]),
                                                    }).ToList();
                        }
                        conn.Close();

                        response.platform = _lstPlatform; //_lstPlatformSku.Select(x => new GetPlatformSku {PlatformId = x.PlatformId, PlatformName=x.PlatformName,PlatformShortName=x.PlatformShortName }).ToList();
                        response.postSiliconDefects = _lstPostSiliconDefects;
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        #endregion
        #region GetPostSiliconDeviation [Code Owner : Chenthilkumaran (24-04-2023)]
        public object GetPostSiliconDeviation(ReqGetSilicon objData)
        {
            string _commandText = "[dbo].[USP_GetSilicon]";
            PostSiliconDeviation response = new PostSiliconDeviation();
            List<GetPlatformSku> _lstGetPlatformSku = new List<GetPlatformSku>();
            List<PostSiliconDeviationList> _lstPostSiliconDeviation = new List<PostSiliconDeviationList>();
            List<PlaformSkuList> _lstPlatformSku = new List<PlaformSkuList>();
            try
            {
                List<SqlDataRecord> resultTable = new List<SqlDataRecord>();
                List<SqlMetaData> sqlMetaData = new List<SqlMetaData>();
                sqlMetaData.Add(new SqlMetaData("PlatformName", SqlDbType.NVarChar, 500));
                sqlMetaData.Add(new SqlMetaData("SkuName", SqlDbType.NVarChar, 500));

                foreach (var item in objData.SiliconReq)
                {
                    SqlDataRecord row = new SqlDataRecord(sqlMetaData.ToArray());
                    row.SetValues(new object[] {
                        item.PlatformName,
                        item.SKUName
                    });
                    resultTable.Add(row);
                }
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                    SqlParameter[] parameters = null;
                    parameters = new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@ObjPlatformSku", SqlDbType = SqlDbType.Structured,TypeName="dbo.ObjPlatformSku", Direction = ParameterDirection.Input,Value = resultTable.Count>0? resultTable:null},
                        new SqlParameter() { ParameterName = "@Type", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = 4 },
                    };
                    using (DataSet ds = DatabaseContext.GetDataSetWithUserDefinedTableTypeParameter(cmd, _commandText, parameters))
                    {
                        if (ds != null)
                        {
                            _lstGetPlatformSku = (from DataRow dr in ds.Tables[0].Rows
                                                  select new GetPlatformSku
                                                  {
                                                      PlatformId = Convert.ToInt32(dr["PlatformId"]),
                                                      PlatformName = Convert.ToString(dr["PlatformName"]),
                                                      PlatformShortName = Convert.ToString(dr["PlatformShortName"]),
                                                      SkuId = Convert.ToInt32(dr["SKUId"]),
                                                      SkuName = Convert.ToString(dr["SKUName"])
                                                  }).ToList();

                            _lstPostSiliconDeviation = (from DataRow dr in ds.Tables[1].Rows
                                                          select new PostSiliconDeviationList()
                                                          {
                                                              PlatformName = Convert.ToString(dr["PlatformName"]),
                                                              PlatformShortName = Convert.ToString(dr["PlatformShortName"]),
                                                              SkuName = Convert.ToString(dr["SkuName"]),
                                                              DPMO = Convert.ToInt32(dr["DPMO"]),
                                                              DPMT = Convert.ToInt32(dr["DPMT"]),
                                                              KPIPower = Convert.ToString(dr["KPIPower"]),
                                                              KPIPerformance = Convert.ToString(dr["KPIPerformance"]),
                                                              BKCPercentage = Convert.ToInt32(dr["BKCPercentage"]),
                                                              unique_defects_current = Convert.ToString(dr["unique_defects_current"]),
                                                              unique_defects_presilicon = Convert.ToString(dr["unique_defects_presilicon"]),
                                                          }).ToList();
                        }
                        conn.Close();

                        foreach (var item in _lstGetPlatformSku)
                        {
                            PlaformSkuList _lstPS = new PlaformSkuList();
                            _lstPS.PlatformId = item.PlatformId;
                            _lstPS.PlatformName = item.PlatformName;
                            _lstPS.PlatformShortName = item.PlatformShortName;
                            _lstPS.Sku = new List<SkuList>();

                            var platforms = _lstGetPlatformSku.Where(x => x.PlatformId == item.PlatformId);
                            foreach (var sku in platforms)
                            {
                                SkuList _lstSku = new SkuList();
                                if (sku.SkuName != "")
                                {
                                    _lstSku.SkuName = sku.SkuName;
                                    _lstSku.SkuId = sku.SkuId;

                                    _lstPS.Sku.Add(_lstSku);
                                }
                            }
                            _lstPlatformSku.Add(_lstPS);
                        }
                        response.platformSku = _lstPlatformSku.GroupBy(g => g.PlatformId).Select(x => x.FirstOrDefault()).ToList();
                        response.postSiliconDeviation = _lstPostSiliconDeviation;
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        #endregion
        
        #region GetQMS [Code Owner : Chenthilkumaran (10-07-2023)]
        public object GetQMS(ReqGetQMS objData)
        {
            string _commandText = "[dbo].[USP_GetQMS]";
            QMSList response = new QMSList();   
            List<Platform> _lstPlatform = new List<Platform>();
            List<Get1QMS> _lstGetQMS = new List<Get1QMS>();

            try
            {
                List<SqlDataRecord> resultTable = new List<SqlDataRecord>();
                List<SqlMetaData> sqlMetaData = new List<SqlMetaData>();
                sqlMetaData.Add(new SqlMetaData("PlatformName", SqlDbType.NVarChar, 500));
                sqlMetaData.Add(new SqlMetaData("SkuName", SqlDbType.NVarChar, 500));

                foreach (var item in objData.QMSReq)
                {
                    SqlDataRecord row = new SqlDataRecord(sqlMetaData.ToArray());
                    row.SetValues(new object[] {
                        item.PlatformName,
                        ""
                    });
                    resultTable.Add(row);
                }
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                    SqlParameter[] parameters = null;
                    parameters = new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@ObjPlatformSku", SqlDbType = SqlDbType.Structured,TypeName="dbo.ObjPlatformSku", Direction = ParameterDirection.Input,Value = resultTable.Count>0? resultTable:null},
                    };
                    using (DataSet ds = DatabaseContext.GetDataSetWithUserDefinedTableTypeParameter(cmd, _commandText, parameters))
                    {
                        if (ds != null)
                        {
                            _lstPlatform = (from DataRow dr in ds.Tables[0].Rows
                                            select new Platform
                                            {
                                                PlatformId = Convert.ToInt32(dr["PlatformId"]),
                                                PlatformName = Convert.ToString(dr["PlatformName"]),
                                                PlatformShortName = Convert.ToString(dr["PlatformShortName"]),
                                            }).ToList();

                            _lstGetQMS = (from DataRow dr in ds.Tables[1].Rows
                                          select new Get1QMS()
                                          {
                                              PlatformName = Convert.ToString(dr["PlatformName"]),
                                              PlatformShortName = Convert.ToString(dr["PlatformShortName"]),
                                              QLEsCurrentAll = Convert.ToInt32(dr["QLEsCurrentAll"]),
                                              QLEsCurrentCompleteRejected = Convert.ToInt32(dr["QLEsCurrentCompleteRejected"]),
                                              CMFsCompleted = Convert.ToString(dr["CMFsCompleted"])+"%",
                                              PRQPOP = Convert.ToString(dr["PRQPOP"]).Contains("-") ? Convert.ToString(dr["PRQPOP"]) + " Wks" : "+" + Convert.ToString(dr["PRQPOP"]) + " Wks",
                                              PVCurrent = Convert.ToString(dr["PVCurrent"]).Contains("-") ? Convert.ToString(dr["PVCurrent"]) + " Wks" : "+" + Convert.ToString(dr["PVCurrent"]) + " Wks",
                                              QLEsCurrentAll_IsUpdated = Convert.ToString(dr["QLEsCurrentAll_IsUpdated"]),
                                              QLEsCurrentAll_UpdatedBy = Convert.ToString(dr["QLEsCurrentAll_UpdatedBy"]),
                                              QLEsCurrentCompleteRejected_IsUpdated = Convert.ToString(dr["QLEsCurrentCompleteRejected_IsUpdated"]),
                                              QLEsCurrentCompleteRejected_UpdatedBy = Convert.ToString(dr["QLEsCurrentCompleteRejected_UpdatedBy"]),
                                              CMFsCompleted_IsUpdated = Convert.ToString(dr["CMFsCompleted_IsUpdated"]),
                                              CMFsCompleted_UpdatedBy = Convert.ToString(dr["CMFsCompleted_UpdatedBy"]),
                                              PRQPOP_IsUpdated = Convert.ToString(dr["PRQPOP_IsUpdated"]),
                                              PRQPOP_UpdatedBy = Convert.ToString(dr["PRQPOP_UpdatedBy"]),
                                              PVCurrent_IsUpdated = Convert.ToString(dr["PVCurrent_IsUpdated"]),
                                              PVCurrent_UpdatedBy = Convert.ToString(dr["PVCurrent_UpdatedBy"]),
                                          }).ToList();
                        }
                        conn.Close();

                        response.platform = _lstPlatform; 
                        response.GetQMS = _lstGetQMS;
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region UpdateQMS [Code Owner : Chenthilkumaran (10-07-2023)]
        public object UpdateQMS(ReqUpdateQMS objData)
        {
            string _commandText = "[dbo].[USP_UpdateQMS]";
            
            try
            {
                List<SqlDataRecord> resultTable = new List<SqlDataRecord>();
                List<SqlMetaData> sqlMetaData = new List<SqlMetaData>();
                sqlMetaData.Add(new SqlMetaData("PlatformName", SqlDbType.NVarChar, 500));
                sqlMetaData.Add(new SqlMetaData("PlatformShortName", SqlDbType.NVarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsCurrentAll", SqlDbType.Int));
                sqlMetaData.Add(new SqlMetaData("QLEsCurrentCompleteRejected", SqlDbType.Int));
                sqlMetaData.Add(new SqlMetaData("CMFsCompleted", SqlDbType.Float));
                sqlMetaData.Add(new SqlMetaData("PRQPOP", SqlDbType.Int));
                sqlMetaData.Add(new SqlMetaData("PVCurrent", SqlDbType.Int));
                sqlMetaData.Add(new SqlMetaData("QLEsCurrentAll_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsCurrentAll_UpdatedBy", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsCurrentCompleteRejected_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsCurrentCompleteRejected_UpdatedBy", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("CMFsCompleted_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("CMFsCompleted_UpdatedBy", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("PVCurrent_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("PVCurrent_UpdatedBy", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("PRQPOP_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("PRQPOP_UpdatedBy", SqlDbType.VarChar, 50));
                foreach (var item in objData.Req)
                {
                    SqlDataRecord row = new SqlDataRecord(sqlMetaData.ToArray());
                    row.SetValues(new object[] {
                        item.PlatformName,
                        item.PlatformShortName,
                        item.QLEsCurrentAll,
                        item.QLEsCurrentCompleteRejected,
                        Convert.ToDouble(item.CMFsCompleted),
                        Convert.ToInt32(item.PRQPOP),
                        Convert.ToInt32(item.PVCurrent),
                        item.QLEsCurrentAll_IsUpdated,
                        item.QLEsCurrentAll_UpdatedBy,
                        item.QLEsCurrentCompleteRejected_IsUpdated,
                        item.QLEsCurrentCompleteRejected_UpdatedBy,
                        item.CMFsCompleted_IsUpdated,
                        item.CMFsCompleted_UpdatedBy,
                        item.PRQPOP_IsUpdated,
                        item.PRQPOP_UpdatedBy,
                        item.PVCurrent_IsUpdated,
                        item.PVCurrent_UpdatedBy,
                    });
                    resultTable.Add(row);
                }
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                    SqlParameter[] parameters = null;
                    parameters = new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@ObjQMS", SqlDbType = SqlDbType.Structured,TypeName="dbo.ObjQMS", Direction = ParameterDirection.Input,Value = resultTable.Count>0? resultTable:null},
                    };
                    using (DataSet ds = DatabaseContext.GetDataSetWithUserDefinedTableTypeParameter(cmd, _commandText, parameters))
                    {
                        if (ds != null)
                        {
                            string status = ds.Tables[0].Rows[0]["status_message"].ToString();

                            if (status == Status.ok)
                            {
                                return new MessageStatus { Message = "Updated Successfully", Status = true };
                            }
                            else
                            {
                                return new MessageStatus { Message = status, Status = false };
                            }
                            conn.Close();
                        }
                        else
                        {
                            return null;
                        }                        
                    }
                }               
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        #endregion
        
        #region Get2QMS [Code Owner : Chenthilkumaran (23-07-2023)]
        public object GetSecondQMS()
        {
            string _commandText = "[dbo].[USP_Get2QMS]";
            try 
            { 
                List<Get2QMS> _lstGet2QMS = new List<Get2QMS>();
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();

                    List<SqlParameter> sppm = new List<SqlParameter>();

                    using (DataSet ds = DatabaseContext.GetDataSet(cmd, _commandText, sppm))
                    {
                        _lstGet2QMS = (from DataRow dr in ds.Tables[0].Rows
                                       select new Get2QMS()
                                       {
                                           Year = Convert.ToInt32(dr["Year"]),
                                           QLEsSWFW = Convert.ToString(dr["QLEsSWFW"]),
                                           QLEsBEAT = Convert.ToString(dr["QLEsBEAT"]),
                                           QLEsSWFW_IsUpdated = Convert.ToString(dr["QLEsSWFW_IsUpdated"]),
                                           QLEsSWFW_UpdatedBy = Convert.ToString(dr["QLEsSWFW_UpdatedBy"]),
                                           QLEsBEAT_IsUpdated = Convert.ToString(dr["QLEsBEAT_IsUpdated"]),
                                           QLEsBEAT_UpdatedBy = Convert.ToString(dr["QLEsBEAT_UpdatedBy"]),
                                       }).ToList();
                    }
                }
                return _lstGet2QMS;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        #endregion
        #region UpdateSecondQMS [Code Owner : Chenthilkumaran (23-07-2023)]
        public object UpdateSecondQMS(ReqUpdateSecondQMS objData)
        {
            string _commandText = "[dbo].[USP_Update2QMS]";

            try
            {
                List<SqlDataRecord> resultTable = new List<SqlDataRecord>();
                List<SqlMetaData> sqlMetaData = new List<SqlMetaData>();
                sqlMetaData.Add(new SqlMetaData("Year", SqlDbType.Int));
                sqlMetaData.Add(new SqlMetaData("QLEsSWFW", SqlDbType.VarChar, 200));
                sqlMetaData.Add(new SqlMetaData("QLEsBEAT", SqlDbType.VarChar, 200));
                sqlMetaData.Add(new SqlMetaData("QLEsSWFW_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsSWFW_UpdatedBy", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsBEAT_IsUpdated", SqlDbType.VarChar, 50));
                sqlMetaData.Add(new SqlMetaData("QLEsBEAT_UpdatedBy", SqlDbType.VarChar, 50));
                foreach (var item in objData.Req)
                {
                    SqlDataRecord row = new SqlDataRecord(sqlMetaData.ToArray());
                    row.SetValues(new object[] {
                        item.Year,
                        item.QLEsSWFW,
                        item.QLEsBEAT,
                        item.QLEsSWFW_IsUpdated,
                        item.QLEsSWFW_UpdatedBy,
                        item.QLEsBEAT_IsUpdated,
                        item.QLEsBEAT_UpdatedBy,
                    });
                    resultTable.Add(row);
                }
                using (var conn = _repoQms.Database.GetDbConnection())
                {
                    conn.Open();
                    SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                    SqlParameter[] parameters = null;
                    parameters = new SqlParameter[] {
                        new SqlParameter() { ParameterName = "@Obj2QMS", SqlDbType = SqlDbType.Structured,TypeName="dbo.Obj2QMS", Direction = ParameterDirection.Input,Value = resultTable.Count>0? resultTable:null},
                    };
                    using (DataSet ds = DatabaseContext.GetDataSetWithUserDefinedTableTypeParameter(cmd, _commandText, parameters))
                    {
                        if (ds != null)
                        {
                            string status = ds.Tables[0].Rows[0]["status_message"].ToString();

                            if (status == Status.ok)
                            {
                                return new MessageStatus { Message = "Updated Successfully", Status = true };
                            }
                            else
                            {
                                return new MessageStatus { Message = status, Status = false };
                            }
                            conn.Close();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        #endregion

    }
}
