using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VendingDisplay.Resource
{

    class GenerateElement
    {
        private readonly PaymentMethod pm = new PaymentMethod();
        private List<PaymentMethod> getData(string table_name, int? type_payment = null)
        {
            string cmd;
            pm.connectDB("db_vending_sample");
            if (type_payment == null)
            {
                cmd = $"SELECT * FROM {table_name} where status_payment = 1";
            }
            else cmd = $"SELECT * FROM {table_name} WHERE type_payment = {type_payment} and status_payment = 1;";
            return pm.GetData(cmd);
        }

        private Button generateBtn(PaymentMethod pm)
        {
            Button newBtn = new Button
            {
                Name = $"_{pm.Value.ToString().ToLower()}PaymentBtn",
                Margin = new Thickness(5)
            };

            try
            {
                newBtn.Content = int.Parse(pm.Value.ToString()).ToString("C");
                newBtn.Tag = int.Parse(pm.Value.ToString());
            }

            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    newBtn.Content = pm.Value.ToString();
                    newBtn.Tag = pm.Type;
                }

                else MessageBox.Show(ex.Message);
            }
            return newBtn;
        }

        public List<Button> GenerateButton(string db_name, int? typePayment = null)
        {
            List<PaymentMethod> dataBtn = new List<PaymentMethod>();
            dataBtn = getData(db_name, typePayment);
            List<Button> btn = new List<Button>();
            foreach (PaymentMethod item in dataBtn)
            {
                btn.Add(generateBtn(item));
            }
            pm.closeDB();
            return btn;
        }
    }
}
