using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace VendingDisplay.Resource
{

    class AssetQuery
    {
        PaymentMethod pm = new PaymentMethod();
        
        public string GetPath(string value)
        {
            string result = string.Empty;
            pm.connectDB("db_vending_sample");
            string cmdTxt = $"select value_asset from asset where name_asset = '{value}';";
            NpgsqlCommand cmd = new NpgsqlCommand(cmdTxt, pm.con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                result = reader.GetString(0);
            }
            return result;
        }
    }
}
