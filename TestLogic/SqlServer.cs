using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;

namespace TestLogic
{
    public class SqlServer
    {
        public static DataTable GetServersList()
        {
            SqlDataSourceEnumerator inst = SqlDataSourceEnumerator.Instance;
            return inst.GetDataSources();                       
        }
    }
}
