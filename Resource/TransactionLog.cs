using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VendingDisplay.Resource
{
    class TransactionLog
    {
        public void WriteLog(string method, string tujuan, string jumlah, int price, int paid, int totalPaid)
        {
            DateTime date = DateTime.Now;
            string path = $@"D:\K\WPFApps\VendingDisplay\log_file\{date:dd-MM-yyyy}_log.txt";
            try
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine($"Jam {date:HH:mm:ss}");
                    w.WriteLine("========================");
                    w.WriteLine($"Tujuan: {tujuan}");
                    w.WriteLine($"Jumlah: {jumlah} tiket");
                    w.WriteLine($"Harga: {price:C}");
                    w.WriteLine($"Metode Pembayaran: {method}");
                    w.WriteLine($"Bayar: {paid:C}");
                    w.WriteLine($"Kembalian: {totalPaid:C}");
                    w.WriteLine("Status: Berhasil");
                    w.WriteLine("========================");
                    w.WriteLine();
                    w.Close();
                    w.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
