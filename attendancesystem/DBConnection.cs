using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendancesystem
{
    class DBConnection
    {

        public string GetConnection()
        {
            string cn;
            cn = @"Data Source=localhost;port=3306;database=bcams1;username=root;password=root"; // Connection string for Mysql server Server Computer
            //  string cn = @"Data Source=103.0.0.1;port=3306;database=incecddb;username=incecd;password=incecd9999"; // Connection string for Mysql server CLient Computer
            //string cn = @"Data Source=103.0.0.5;port=3306;database=incecddb;username=incecd;password=incecd9999"; // Connection string for Mysql server CLient Computer
            //  cn = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=barangayculiatdb;Integrated Security=True"; 
            //  cn = @"Data Source=KAGAWADCHU-PC\SQLEXPRESS;Initial Catalog=barangayculiatdb;Integrated Security=True";
            //  cn = @"Data Source=BCPC-VAWC\SQLEXPRESS;Initial Catalog=barangayculiatdb;Integrated Security=True";
            //  cn = @"Data Source=192.168.1.1,49170;Initial Catalog=barangayculiatdb;User Id=nel;Password=nels12345";// This Connection string is for Client Machine office
            //  cn = @"Data Source=DESKTOP-5DN3RJN\SQLEXPRESS;Initial Catalog=barangayculiatdb;User Id=nel;Password=nels12345";// This Connection string is for Client Machine office   
            return cn;
        }
    }
}
