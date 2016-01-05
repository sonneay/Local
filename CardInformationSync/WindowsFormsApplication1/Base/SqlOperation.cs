using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApplication1.Base
{
    public static class SqlOperation
    {
        /// <summary>
        ///     excutr return a datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static DataTable RunTable(string sql, string connectString)
        {
            using (var conn = new SqlConnection(connectString))
            {
                var da = new SqlDataAdapter(sql, conn);
                var ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        ///     excute sql return  effect count
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static int ExcuteSql(string sql, string connectString)
        {
            using (var conn = new SqlConnection(connectString))
            {
                var cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}