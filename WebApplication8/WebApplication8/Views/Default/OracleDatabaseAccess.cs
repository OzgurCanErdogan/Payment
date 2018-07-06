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
    }
}