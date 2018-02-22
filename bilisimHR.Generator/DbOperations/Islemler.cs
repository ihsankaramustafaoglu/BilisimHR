using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace bilisimHR.Generator.DbOperations
{
    public static class Islemler
    {
        private static OracleParameter hata { get { return new OracleParameter(); } }
        private static string Owner { get { return "MVC"; } }
        private static string ModuleOwner { get { return "MVC"; } }

        public static DataTable GetModules()
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            DataTable dt = cmdBLSM.getDataTable("SELECT * FROM " + ModuleOwner + ".G_MODULES", hata, CommandType.Text);

            return dt;
        }

        public static DataTable GetTables()
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT * 
FROM ALL_TABLES T, " + ModuleOwner + @".G_TABLES I, " + ModuleOwner + @".G_MODULES M
WHERE T.TABLE_NAME = I.TABLE_NAME
AND M.ID = I.G_MODULES_ID
AND OWNER = :P_OWNER", hata, CommandType.Text);

            return dt;
        }

        public static DataTable GetColumnsFull(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT * 
                    FROM ALL_TAB_COLUMNS 
                    WHERE OWNER = :P_OWNER 
                    AND TABLE_NAME = :P_TABLE", hata, CommandType.Text);

            return dt;
        }

        public static DataTable GetColumns(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT * 
                    FROM ALL_TAB_COLUMNS 
                    WHERE OWNER = :P_OWNER 
                    AND TABLE_NAME = :P_TABLE 
                    AND COLUMN_NAME NOT IN ('ID', 'INSERTED_BY', 'INSERTED_DATE', 'UPDATED_BY', 'UPDATED_DATE')", hata, CommandType.Text);

            return dt;
        }

        public static DataTable GetColumnsNotInPK(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT * 
                    FROM ALL_TAB_COLUMNS 
                    WHERE OWNER = :P_OWNER 
                    AND TABLE_NAME = :P_TABLE 
                    AND COLUMN_NAME NOT IN ('ID', 'INSERTED_BY', 'INSERTED_DATE', 'UPDATED_BY', 'UPDATED_DATE')
                    AND COLUMN_NAME NOT IN (SELECT a.column_name
                      FROM all_cons_columns a
                      JOIN all_constraints c ON a.owner = c.owner
                                            AND a.constraint_name = c.constraint_name
                      JOIN all_constraints c_pk ON c.r_owner = c_pk.owner
                                               AND c.r_constraint_name = c_pk.constraint_name
                      JOIN all_cons_columns c_pkc ON c_pkc.constraint_name = c_pk.constraint_name
                     WHERE c.constraint_type = 'R'
                       AND a.owner = :P_OWNER
                       AND a.table_name = :P_TABLE)", hata, CommandType.Text);

            return dt;
        }

        public static DataTable GetFK(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT c.owner, c.r_owner, a.constraint_name, c_pk.constraint_name r_pk,
       a.table_name, a.column_name,
       c_pk.table_name r_table_name,
       c_pkc.column_name r_column_Name
  FROM all_cons_columns a
  JOIN all_constraints c ON a.owner = c.owner
                        AND a.constraint_name = c.constraint_name
  JOIN all_constraints c_pk ON c.r_owner = c_pk.owner
                           AND c.r_constraint_name = c_pk.constraint_name
  JOIN all_cons_columns c_pkc ON c_pkc.constraint_name = c_pk.constraint_name
 WHERE c.constraint_type = 'R'
   AND a.owner = :P_OWNER
   AND a.table_name = :P_TABLE",
        hata, CommandType.Text);

            return dt;
        }

        public static DataTable GetRef(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT C.OWNER, C.R_OWNER, A.CONSTRAINT_NAME, C_PK.CONSTRAINT_NAME R_PK,
       A.TABLE_NAME, A.COLUMN_NAME,
       C_PK.TABLE_NAME R_TABLE_NAME,
       C_PKC.COLUMN_NAME R_COLUMN_NAME
  FROM ALL_CONS_COLUMNS A
  JOIN ALL_CONSTRAINTS C ON A.OWNER = C.OWNER
                        AND A.CONSTRAINT_NAME = C.CONSTRAINT_NAME
  JOIN ALL_CONSTRAINTS C_PK ON C.R_OWNER = C_PK.OWNER
                           AND C.R_CONSTRAINT_NAME = C_PK.CONSTRAINT_NAME
  JOIN ALL_CONS_COLUMNS C_PKC ON C_PKC.CONSTRAINT_NAME = C_PK.CONSTRAINT_NAME
 WHERE C.CONSTRAINT_TYPE = 'R'
   AND C_PK.OWNER = :P_OWNER
   AND C_PK.TABLE_NAME = :P_TABLE",
        hata, CommandType.Text);

            return dt;
        }
    }
}