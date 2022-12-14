using System;
using System.IO;
using System.Windows;

namespace VendingDisplay.Resource
{
    class TransactionLog
    {
        public void WriteLog(string transaction, string method, string tujuan, string jumlah, int price, int paid, int totalPaid)
        {
            DateTime date = DateTime.Now;
            string path = $@"D:\K\WPFApps\VendingDisplay\log_file\{date:dd-MM-yyyy}_log.txt";
            try
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine($"Jam {date:HH:mm:ss}");
                    w.WriteLine("========================");
                    w.WriteLine($"Jenis Transaksi: {transaction}");
                    w.WriteLine($"Metode Pembayaran: {method}");
                    w.WriteLine($"Tujuan: {tujuan}");
                    w.WriteLine($"Jumlah: {jumlah} tiket");
                    w.WriteLine($"Harga: {price:C}");
                    w.WriteLine($"Bayar: {paid:C}");
                    w.WriteLine($"Kembali: {totalPaid:C}");
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

        public void WriteToDB(string transaction, string method, string tujuan, string jumlah, int price, int paid, int totalPaid)
        {
            string beli = "insert into data_transaksi_beli " +
                "(metode_payment, tujuan, jumlah_tiket, harga_tiket, bayar_tiket, kembalian, status_pembayaran) " +
                $"values('{method}', '{tujuan}', '{jumlah}', {price}, {paid}, {totalPaid}, 'sukses');";
            string topup = "insert into data_transaksi_topup " +
                "(metode_payment, nilai_topup, bayar_topup, kembalian, status_pembayaran) " +
                $"values('{method}', '{price}', '{paid}', {totalPaid}, 'sukses');";
            PaymentMethod pm = new PaymentMethod();
            pm.connectDB("db_vending_sample");
            string cmd = transaction.ToLower().Contains("beli")? beli : topup;
            pm.InsertToDB(cmd);
        }
    }
}
