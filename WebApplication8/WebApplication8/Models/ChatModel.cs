﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class JsonSendTable
    {
        public string message { get; set; }
        public string tableData { get; set; }
        public decimal total { get; set; }
    }

    //public class testData
    //{
    //    public string UserType { get; set; }
    //    public string Text { get; set; }
    //}
    public class CustomerData
    {
        public int MUSTERI_NO { get; set; }
        public string MUSTERI_ADI { get; set; }
        public Int16 YETKI_KODU { get; set; }
        public string MUSTERI_GRUBU { get; set; }
        public List<Account> ACCOUNTS { get; set; }
        public PaymentPlan PLAN { get; set; }

    }
    public class Account
    {
        public Int16 SUBE_NO { get; set; }
        public string KREDI_KODU { get; set; }
        public string KARTON_KODU { get; set; }
        public int MUSTERI_NO { get; set; }
        public DateTime DATE { get; set; }
        public decimal INTIKAL_TUTARI { get; set; }
        public decimal BAKIYE { get; set; }
        public char TOA_DURUM_KODU { get; set; }
    }
    public class PaymentPlan
    {
        public bool KREDI_YAPILANDIRILMASI { get; set; }
        public decimal PLAN_ONCESI_BAKIYE { get; set; }
        public decimal PLAN_SONRASI_BAKIYE { get; set; }
        public decimal TAKSIT_TUTARI { get; set; }
        public int TAKSIT_SAYISI { get; set; }
        public DateTime BASLANGIC_TARIHI { get; set; }
        public DateTime BITIS_TARIHI { get; set; }
        public double FAIZ_ORANI { get; set; }
        public char TOA_DURUM_KODU { get; set; }

    }
    
    public class ChatModel
    {

        //List<string> test2 = new List<string>();
        public Dictionary<string,string> usersChat { get; set; }
        public Dictionary<string, string> botCorrectPath { get; set; }
        public Dictionary<string, string> botFalsePath { get; set; }
    }
    

}