using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZCS_FormUI.Function
{
    public static class DataSetFunc
    {
        public static DataSet GroupDataTable(DataTable dt, string colName)
        {
            DataSet ds = new DataSet();
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    var GroupCols = dt.Rows.Cast<DataRow>().Select(p => p[colName].ToString()).Distinct().ToList();
                    if (GroupCols != null)
                    {
                        GroupCols.ForEach(p =>
                        {
                            DataTable dtCol = FilterDataTable(dt, colName + "='" + p + "'");
                            if (dtCol != null) ds.Tables.Add(dtCol);
                        });
                    }
                }
                else
                {
                    ds = null;
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        /// <summary>
        /// 过滤DataTable
        /// </summary>
        /// <param name="dt">原始数据</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public static DataTable FilterDataTable(DataTable dt, string filter)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select(filter);
                return DataRowsCopyTo(drs);
            }
            else return null;
        }

        public static DataTable DataRowsCopyTo(object drs)
        {
            if (drs is DataRow[])
            {
                return (drs == null || ((DataRow[])drs).Length == 0) ? null : ((DataRow[])drs).CopyToDataTable();
            }
            else if (drs is List<DataRow>)
            {
                return (drs == null || ((List<DataRow>)drs).Count == 0) ? null : ((List<DataRow>)drs).CopyToDataTable();
            }
            else return null;
        }
    }
}
