using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace sqlite_test
{
    public static class Fsq
    {

        static string db_f = $"C:\\Users\\zouja\\Desktop\\temp\\TDATA.db";

        public static IFreeSql _freeSql;

        public static void InitSQL()
        {
            _freeSql = new FreeSql.FreeSqlBuilder()
                        .UseConnectionString(FreeSql.DataType.Sqlite, $@"Data Source={db_f};Password=admin.778989")
                        .Build();
            return;
        }

        public static ISqlSugarClient _sqlSugar;
        public static void InitSqlSugar()
        {
            _sqlSugar = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.Sqlite,
                ConnectionString = $"Data Source={db_f};Password=admin.778989",
                IsAutoCloseConnection = false,
                InitKeyType = InitKeyType.Attribute ,
            });
        }
    }
}
