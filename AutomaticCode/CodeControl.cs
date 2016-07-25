using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticCode
{
    partial class DataModel
    {
        public List<Column> ColumnList { get; set; }
        public string DataName { get; set; }
        public string ClassName { get; set; }
        //public List<string> PK_Name { get; set; }

        public void IniDataBase()
        {
            ColumnList = new List<Column>();
            
           string connstr= SqlHelper.GetConnSting();
            string sqls = $"DECLARE @P_ERR integer EXEC sp_pkeys @table_name = '{DataName}' SELECT @P_ERR as N'@P_ERR'";
            DataTable dtt = SqlHelper.ExecuteDataset(connstr, CommandType.Text, sqls).Tables[0];
            string sql = $"DECLARE @P_ERR integer EXEC sp_help @objname = N'{DataName}' SELECT @P_ERR as N'@P_ERR'";
            DataTable dt = SqlHelper.ExecuteDataset(connstr, CommandType.Text, sql).Tables[1];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Column c = new Column();
                c.DataType = dt.Rows[i]["Type"].ToString();
                c.Name= dt.Rows[i]["Column_name"].ToString();
                c.Length = Convert.ToInt32(dt.Rows[i]["Length"]);
                c.Nullable = dt.Rows[i]["Nullable"].ToString();
                var s = dtt.Select("COLUMN_NAME='"+c.Name+"'");
                if (s.Count() > 0)
                {
                    c.IsPK = true;
                }
                else
                {
                    c.IsPK = false;
                }
                ColumnList.Add(c);
            }
            
            //PK_Name = new List<string>();
            //for (int j = 0; j < dtt.Rows.Count; j++)
            //{
            //    PK_Name.Add(dtt.Rows[j]["COLUMN_NAME"].ToString());
            //}
        }
    }
    public class Column
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public string Nullable { get; set; }
        public bool IsPK { get; set; }
    }
}
