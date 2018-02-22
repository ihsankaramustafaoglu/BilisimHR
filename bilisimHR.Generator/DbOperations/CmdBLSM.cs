using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;

namespace bilisimHR.Generator.DbOperations
{
    public class CmdBLSM
    {
        OracleConnection Con = new OracleConnection(ConfigurationManager.AppSettings["conStr"].ToString());

        public List<OracleParameter> _parameters = new List<OracleParameter>();

        /// <summary> 
        /// dbType optional parametredir.Değer verilmeden kullanıldığında default olarak OracleDbType.Varchar2 değerini alır.
        /// </summary> 
        public void AddINParameter(string parameterName, object value, OracleDbType dbType = OracleDbType.Varchar2)
        {
            OracleParameter _parameter = new OracleParameter();
            _parameter.ParameterName = parameterName;
            _parameter.OracleDbType = dbType;
            _parameter.Value = value;
            _parameters.Add(_parameter);
        }

        /// <summary> 
        /// Bu method'da dikkat edilmesi gereken durum, OracleDbType tipi seçimidir. 
        /// <para>--> getString methodunu kullanıyorsanız OracleDbType.Varchar2 seçilmelidir.</para>
        /// <para>--> getDataTable methodunu kullanıyorsanız OracleDbType.RefCursor seçilmelidir.</para>
        /// </summary> 
        public void AddOUTParameter(string parameterName, OracleDbType dbType)
        {
            OracleParameter _parameter = new OracleParameter();
            _parameter.ParameterName = parameterName;
            _parameter.OracleDbType = dbType;
            _parameter.Direction = ParameterDirection.Output;
            _parameters.Add(_parameter);
        }

        /// <summary> 
        /// Bu method'da dikkat edilmesi gereken durum, OracleDbType tipi seçimidir. 
        /// <para>--> getString methodunu kullanıyorsanız OracleDbType.Varchar2 seçilmelidir.</para>
        /// <para>--> getDataTable methodunu kullanıyorsanız OracleDbType.RefCursor seçilmelidir.</para>
        /// </summary> 
        public void AddOUTParameter(string parameterName, OracleDbType dbType, int size)
        {
            OracleParameter _parameter = new OracleParameter();
            _parameter.ParameterName = parameterName;
            _parameter.OracleDbType = dbType;
            _parameter.Size = size;
            _parameter.Direction = ParameterDirection.Output;
            _parameters.Add(_parameter);
        }

        public void AddOUTParameter(string parameterName, OracleParameter value, OracleDbType dbType)
        {
            value.ParameterName = parameterName;
            value.OracleDbType = dbType;
            value.Direction = ParameterDirection.Output;
            _parameters.Add(value);
        }

        public void AddOUTParameter(string parameterName, OracleParameter value, OracleDbType dbType, int size, ParameterDirection param = ParameterDirection.Output)
        {
            value.ParameterName = parameterName;
            value.OracleDbType = dbType;
            value.Size = size;
            value.Direction = param;
            _parameters.Add(value);
        }

        private void ProcessParameters(OracleDataAdapter da)
        {
            foreach (OracleParameter _p in _parameters)
            {
                da.SelectCommand.Parameters.Add(_p);
            }
        }

        private void ProcessParameters(OracleCommand cmd)
        {
            foreach (OracleParameter _p in _parameters)
            {
                cmd.Parameters.Add(_p);
            }
        }

        public DataTable getDataTable(string procedureName, OracleParameter hata, CommandType cmdType = CommandType.StoredProcedure)
        {
            DataTable dt = new DataTable();
            hata.Value = null;
            try
            {
                OracleDataAdapter daa = new OracleDataAdapter(procedureName, Con);
                daa.SelectCommand.CommandType = cmdType;
                this.ProcessParameters(daa);
                daa.Fill(dt);
            }
            catch (Exception h)
            {
                hata.Value = h.Message.ToString();
                dt = null;
            }
            return dt;
        }

        public DataSet getDataSet(string procedureName, OracleParameter hata, CommandType cmdType = CommandType.StoredProcedure)
        {
            DataSet ds = new DataSet();
            hata.Value = null;
            try
            {
                OracleDataAdapter daa = new OracleDataAdapter(procedureName, Con);
                daa.SelectCommand.CommandType = cmdType;
                this.ProcessParameters(daa);
                daa.Fill(ds);
            }
            catch (Exception h)
            {
                hata.Value = h.Message.ToString();
                ds = null;
            }
            return ds;
        }

        public string getString(string procedureName, OracleParameter hata, CommandType cmdType = CommandType.StoredProcedure)
        {
            string gelenDeger = null;
            hata.Value = null;

            OracleCommand cmd = new OracleCommand(procedureName, Con);
            cmd.CommandType = cmdType;
            this.ProcessParameters(cmd);
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                    cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                foreach (OracleParameter _outParameter in _parameters)
                {
                    if (_outParameter.Direction == ParameterDirection.Output)
                    {
                        gelenDeger = _outParameter.Value.ToString();
                    }
                }
            }
            catch (Exception h)
            {
                hata.Value = h.Message.ToString();
                gelenDeger = null;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return gelenDeger;
        }
    }
}