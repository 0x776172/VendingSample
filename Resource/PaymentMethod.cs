using System;
using System.Collections.Generic;
using Npgsql;

namespace VendingDisplay.Resource
{
    public class PaymentMethod
    {
        public int? Id { get; set; }
        public object Value { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }


        private NpgsqlConnection con;

        public void connectDB(string db_name)
        {
            string CONNECTION_STRING = $"Host=localhost;Username=postgres;Password='admin';Database={db_name}";
            con = new NpgsqlConnection(CONNECTION_STRING);
            con.Open();
        }
        public void closeDB()
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
        }
        private PaymentMethod readData(NpgsqlDataReader reader)
        {
            int? id = reader.GetInt16(0);
            object value = reader.GetValue(1);
            /*string value = reader.GetInt32(1).ToString();*/
            int? type = reader.GetValue(2) as int?;
            int? status = reader.GetValue(3) as int?;
            PaymentMethod pm = new PaymentMethod
            {
                Id = id,
                Value = value,
                Type = type,
                Status = status
            };
            return pm;

        }

        public List<PaymentMethod> GetData(string input_command)
        {
            List<PaymentMethod> pm = new List<PaymentMethod>();
            string cmdTxt = input_command;
            NpgsqlCommand cmd = new NpgsqlCommand(cmdTxt, con);
            NpgsqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                pm.Add(readData(read));
            }
            return pm;
        }
    }
}
