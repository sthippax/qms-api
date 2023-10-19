using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.DataAccess
{
    internal class DatabaseContext
    {
        #region DataSet
        public static DataSet GetDataSet(SqlCommand cmd, string spName, List<SqlParameter> inputParam = null)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                try
                {
                    if (cmd.Parameters.Count != 0)
                        cmd.Parameters.Clear();

                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (inputParam != null)
                        cmd.Parameters.AddRange(inputParam.ToArray());

                    sda.SelectCommand = cmd;
                    cmd.CommandTimeout = 0;
                    Stored_XSS_Fix(ds, sda);
                    cmd.Dispose();
                    sda.Dispose();
                    return ds;
                }
                catch (Exception e)
                {

                    //Logger.Error("GetDataSet: {0} " + e.Message + e.StackTrace);
                    return null;


                }
                finally
                {

                    ds.Dispose();

                }
            }

        }
        private static void Stored_XSS_Fix(DataSet ds, SqlDataAdapter sda)
        {
            sda.Fill(ds);
        }

        #endregion
        #region DataSet - GetDataSetWithUserDefinedTableTypeParameter
        public static DataSet GetDataSetWithUserDefinedTableTypeParameter(SqlCommand cmd, string spName, SqlParameter[] inputParam = null)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                try
                {
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (inputParam != null)
                    {
                        //cmd.Parameters.AddRange(inputParam.ToArray());
                        foreach (SqlParameter p in inputParam)
                            if (p.SqlDbType != SqlDbType.Structured)
                            {
                                cmd.Parameters.Add(new SqlParameter(p.ParameterName, p.SqlValue));
                            }
                            else
                            {
                                var UserDefinedTabletypevalue = new SqlParameter(p.ParameterName, SqlDbType.Structured);
                                UserDefinedTabletypevalue.TypeName = p.TypeName;
                                UserDefinedTabletypevalue.Value = p.Value;
                                cmd.Parameters.Add(UserDefinedTabletypevalue);

                            }
                    }

                    sda.SelectCommand = cmd;
                    cmd.CommandTimeout = 0;
                    Stored_XSS_Fix(ds, sda);
                    cmd.Dispose();
                    sda.Dispose();
                    return ds;
                }

                catch (Exception e)
                {
                    return null;
                }
                finally
                {
                    ds.Dispose();
                }
            }

        }
        #endregion
        public DataTable GetData(SqlCommand cmd, string spName, List<SqlParameter> inputParam = null)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                try
                {
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    if (inputParam != null)
                        cmd.Parameters.AddRange(inputParam.ToArray());
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    cmd.Dispose();
                    sda.Dispose();
                    return dt;
                }
                catch (Exception e)
                {
                    //Logger.Error("GetData: {0} " + e.Message + e.StackTrace);
                    return null;
                }
                finally
                {

                    dt.Dispose();

                }
            }

        }
        public object GetExecuteScalarData(SqlCommand cmd, string spName, List<SqlParameter> inputParam = null)
        {
            object obj = new object();
            try
            {
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (inputParam != null)
                    cmd.Parameters.AddRange(inputParam.ToArray());
                cmd.CommandTimeout = 0;
                obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception e)
            {
                return null;

            }
            finally
            {
                cmd.Dispose();
            }

        }

        public int ExecutNonQuery(SqlCommand cmd, string spName, List<SqlParameter> inputParam = null)
        {
            int value = 0;
            try
            {
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (inputParam != null)
                    cmd.Parameters.AddRange(inputParam.ToArray());
                value = cmd.ExecuteNonQuery();
                return value;
            }
            catch (Exception e)
            {
                //Logger.Error("ExecutNonQuery: {0} " + e.Message + e.StackTrace);
                return 0;
            }
            finally
            {
                cmd.Dispose();
            }

        }
        public static DataSet GetDataSetWithTable(SqlCommand cmd, string Table, List<SqlParameter> inputParam = null)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                try
                {
                    cmd.CommandText = Table;
                    cmd.CommandType = CommandType.Text;
                    if (inputParam != null)
                        cmd.Parameters.AddRange(inputParam.ToArray());

                    sda.SelectCommand = cmd;
                    cmd.CommandTimeout = 0;
                    Stored_XSS_Fix(ds, sda);
                    cmd.Dispose();
                    sda.Dispose();
                    return ds;
                }
                catch (Exception e)
                {

                    //Logger.Error("GetDataSet: {0} " + e.Message + e.StackTrace);
                    return null;


                }
                finally
                {

                    ds.Dispose();

                }
            }

        }
    }
}
