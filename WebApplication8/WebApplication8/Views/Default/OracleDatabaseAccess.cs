using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication8.Models;

namespace WebApplication8.Views.Default
{

    class OracleDatabaseAccess
    {
        OracleConnection con;

        public OracleDatabaseAccess()
        {
            Connect();
        }
        public void Connect()
        {
            string connectionString = "User Id=PRJSKYADM;Password=skyy_pass01;Data Source=(DESCRIPTION =" +
                                                                                        "(ADDRESS =" +
                                                                                          "(PROTOCOL = TCP)" +
                                                                                          "(HOST = kapuborad1.isbank)" +
                                                                                          "(PORT = 1521)" +
                                                                                        ")" +
                                                                                        "(CONNECT_DATA =" +
                                                                                          "(SERVER = dedicated)" +
                                                                                          "(SERVICE_NAME = SRV_SKYY_OLDEV)" +
                                                                                        ")" +
                                                                                      ")";
            con = new OracleConnection(connectionString);

        }
        //public void OpenConnection()
        //{
        //    con.Open();
        //}
        //void Close()
        //{
        //    con.Close();
        //    con.Dispose();
        //}
        public CustomerData GetCustomerInfo(int müstNo)
        {
            con.Open();
            CustomerData result = new CustomerData();
            string query = "select * from CUST.TOA_MUSTERI_BILGI where MUSTERI_NO =" + müstNo;
            OracleCommand command = new OracleCommand(query, con);
            command.CommandType = CommandType.Text;
            OracleDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {

                result.MUSTERI_ADI = dataReader["MUSTERI_ADI"].ToString();
                result.MUSTERI_NO = (int)dataReader["MUSTERI_NO"];
                result.MUSTERI_GRUBU = (string)dataReader["MUSTERI_GRUBU"];
                result.YETKI_KODU = (Int16)dataReader["YETKI_KODU"];
                result.ACCOUNTS = new List<Account>();
            }
            dataReader.Close();
            //this.Close();
            con.Close();
            //con.Dispose();
            return result;
        }
        public List<Account> GetCustomerAccounts(int müstNo)
        {
            con.Open();
            List<Account> accounts = new List<Account>();
            string query = "select * from ACC.TOA_KARTON_BILGI where MUSTERI_NO =" + müstNo + " and TOA_DURUM_KODU = 1";
            OracleCommand command = new OracleCommand(query, con);
            command.CommandType = CommandType.Text;
            OracleDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Account temp = new Account();
                temp.MUSTERI_NO = (int)dataReader["MUSTERI_NO"];
                temp.SUBE_NO = (Int16)dataReader["SUBE_NO"];
                temp.KREDI_KODU = (string)dataReader["KREDI_KODU"];
                temp.KARTON_KODU = (string)dataReader["KARTON_NO"];
                temp.DATE = (DateTime)dataReader["INTIKAL_TARIHI"];
                temp.TOA_DURUM_KODU = '1';
                temp.INTIKAL_TUTARI = (decimal)dataReader["INTIKAL_TUTARI"];
                temp.BAKIYE = (decimal)dataReader["BAKIYE"];
                accounts.Add(temp);
            }
            dataReader.Close();
            con.Close();
            //con.Dispose();
            return accounts;
        }
        public string AddPaymentPlan(CustomerData cust)
        {
            con.Open();

            
            string query = "select * from GNRC.STAJ_PAYMENT_PLAN where MUSTERI_NO =" + cust.MUSTERI_NO;
            OracleCommand command = new OracleCommand(query, con);
            command.CommandType = CommandType.Text;
            OracleDataReader dataReader = command.ExecuteReader();
            int müstNo=0;
            while (dataReader.Read())
            {
                müstNo = (int)dataReader["MUSTERI_NO"];
            }
            dataReader.Close();
            string result;

            if (müstNo == 0)
            {
                System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";

                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                string query2 = @"INSERT INTO GNRC.STAJ_PAYMENT_PLAN (MUSTERI_NO, MUSTERI_ADI,
                    YETKI_KODU, MUSTERI_GRUBU, PLAN_ONCESI_BAKIYE,
                    PLAN_SONRASI_BAKIYE, TAKSIT_TUTARI, TAKSIT_SAYISI, BASLANGIC_TARIHI,
                    BITIS_TARIHI, FAIZ_ORANI, TOA_DURUM_KODU)
                    VALUES (" + cust.MUSTERI_NO + ", '" + cust.MUSTERI_ADI + "'," +
                    cust.YETKI_KODU + "," + cust.MUSTERI_GRUBU + "," + cust.PLAN.PLAN_ONCESI_BAKIYE + "," +
                    cust.PLAN.PLAN_SONRASI_BAKIYE + "," + cust.PLAN.TAKSIT_TUTARI + "," + cust.PLAN.TAKSIT_SAYISI + ", '" + cust.PLAN.BASLANGIC_TARIHI.ToString("dd/MM/yyyy") + "'," +
                    "'" + cust.PLAN.BITIS_TARIHI.ToString("dd/MM/yyyy") + "'," + cust.PLAN.FAIZ_ORANI + "," + "'1')";
                OracleCommand command2 = new OracleCommand(query, con);

                command2.ExecuteNonQuery();
                result = "Ödeme planı tercihiniz onay mekanizmasına dahil edilmiştir.";
            }
            else
                result = "Bilgileriniz ile daha önceden sisteme kayıt yapılmıştır.";

            con.Close();
            return result;


        }
    }
}