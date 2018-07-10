using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.Views.Default;

namespace WebApplication8.Controllers
{
    public class DefaultController : Controller
    {

        ChatModel chat = new ChatModel();
        CustomerData customer;
        string StateOfChat;
        OracleDatabaseAccess conn;

        /*
        ValidMüsNo
        InvalidMüsNo
        KartonInfo
        OdemePlanıAccapted
        OdemePlanıDeclined
        ValidTaksit
        InvalidTaksit
        ValidStartDate
        InvalidStartDate
        PaymentAccapted
        paymentDeclined

        

    */
        // GET: Default
        //public testData programming = new testData();
        public void initVal(){
            
            if (TempData.Peek("Chat") == null)
            {
                chat.botCorrectPath = new Dictionary<string, string>();
                chat.botFalsePath = new Dictionary<string, string>();
                chat.usersChat = new Dictionary<string, string>();
                
                
                //chat.botCorrectPath.Add("ValidMüsNo", "Merhaba Özgür Can Erdoğan, karton bilgileriniz size gösterilecektir. Ödeme planı yapmak ister misiniz?");
                chat.botCorrectPath.Add("InvalidMüsNo", "Girdiğiniz verilerle ilgili bir hesap bulunamadı. Bilgilerinizi kontrol edip tekrar giriniz.");
                chat.botCorrectPath.Add("GetMonth", "Kaç ayda ödemek istersiniz?");
                chat.botCorrectPath.Add("GetMonthAgain", "Cevabınızı anlayamadım. Cevap olarak Evet ya da Hayır giriniz.");
                chat.botCorrectPath.Add("GetStartDate", "Ödemeye ne zaman başlayacağınızı belirtiniz. (dd.MM.yyyy formatında olmalı)");
                chat.botCorrectPath.Add("GetStartDateAgain", "Girdiğiniz veriyi kontrol edin. (dd.MM.yyyy formatında olmalı)");
                chat.botCorrectPath.Add("GetStartDateAgainFurther", "Girdiğiniz tarih bugünden ileri tarihli olmalı, lütfen tekrar giriniz.");
                chat.botCorrectPath.Add("GetInstallmentAgain", "Girdiğinizi anlayamadım. Sadece sayı giriniz.");
                chat.botCorrectPath.Add("CreatePaymentPlan", "Ödeme planı tercihiniz onay mekanizmasına dahil edilmiştir.");
                chat.botCorrectPath.Add("CalculatePlanAgain", "Cevabınızı anlayamadım. Cevap olarak Evet ya da Hayır giriniz.");
                
                chat.botCorrectPath.Add("CalculatePlan", "Talebiniz doğrultusunda ödeme planı taslağınız tablodaki gibi oluşturulmuştur. Borcunuzu bu şekilde yapılandırmayı kabul ediyor musunuz?");
            }
            if(TempData.Peek("DatabaseConnection") == null)
                conn = new OracleDatabaseAccess();
            if(TempData["CustomerObj"] == null)
                customer = new CustomerData();

           
            if(TempData.Peek("StateOfChat") == null)
                StateOfChat = "GetMüsNo";


        }
        public ActionResult Index()
        {

            if(TempData.Peek("StateOfChat") == null || TempData.Peek("Chat") == null || TempData.Peek("DatabaseConnection") == null || TempData.Peek("CustomerObj") == null)
                initVal();
            //TempData["Chat"] = chat;
            //TempData["StateOfChat"] = StateOfChat;
            //TempData["CustomerObj"] = customer;
            //TempData["DatabaseConnection"] = conn;
            //TempDataDictionary stateDictionary = new TempDataDictionary();
            
            if(TempData.Peek("Chat") == null)
            {
                TempData["Chat"] = chat;
            }
            if (TempData.Peek("DatabaseConnection") == null)
            {
                TempData["DatabaseConnection"] = conn;
            }
            if (TempData.Peek("CustomerObj") == null)
            {
                TempData["CustomerObj"] = customer;
            }
            if (TempData.Peek("StateOfChat") == null)
            {
                TempData["StateOfChat"] = StateOfChat;
            }

            //if (Session["Chat"] == null)
            //{
            //    Session["Chat"] = chat;
            //}
            //if (Session["DatabaseConnection"] == null)
            //    Session["DatabaseConnection"] = conn;

            //if (Session["CustomerObj"] == null)
            //    Session["CustomerObj"] = customer;


            //if (Session["StateOfChat"] == null)
            //    Session["StateOfChat"] = StateOfChat;

            return View();

        }

        //public ActionResult AddWithHTMLHelperAndModel()
        //{
        //    var model = new testData();
        //    return View(model);
        //}
        //[HttpPost]
        //public JsonResult Form(FormCollection f, testData testVal, string parameter)
        //{

        //    List<string> formCollection = new List<string>();
        //    List<string> request = new List<string>();
        //    List<string> viewmodel = new List<string>();
        //    List<string> parameters = new List<string>();

        //    //1. FormCollection
        //    foreach (var a in f.AllKeys)
        //    {
        //        formCollection.Add("name=" + a + ",Value=" + f[a]);
        //    }

        //    //2. Strong Type Model Binding
        //    viewmodel.Add("userType=" + testVal.UserType);
        //    viewmodel.Add("text=" + testVal.Text);

        //    //3. Parameter
        //    parameters.Add(parameter);

        //    //4. HTTP Web Request
        //    request.Add(Request["other"].ToString());

        //    return Json(new { formCollection, viewmodel, parameters, request });
        //}

        //[HttpGet]
        //public ActionResult AjaxGet(List<testData> list)
        //{
        //    testData t1 = new testData();
        //    t1.Text = "aaa";
        //    t1.UserType = "bbb";
        //    testData t2 = new testData();
        //    t2.Text = "ccc";
        //    t2.UserType = "ddd";
        //    testData t3 = new testData();
        //    t3.Text = "eee";
        //    t3.UserType = "fff";
        //    list.Add(t1);
        //    list.Add(t2);
        //    list.Add(t3);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public string Conversation(string UserInput)
        {

            return null;
        }
        string[] formats = {"d/M/yyyy", "dd/M/yyyy",
                   "d/MM/yyyy","dd/MM/yyyy" };

        //public DateTime? GetDate(string input)
        //{
        //    var regex = new Regex(@"\b\d{2}\.\d{2}.\d{4}\b");
        //    foreach (Match m in regex.Matches(input))
        //    {
        //        DateTime dt;
        //        if (DateTime.TryParseExact(m.Value, "dd.MM.yyyy", null, DateTimeStyles.None, out dt))
        //            return dt;
        //    }
        //    return null;
        //}
        public bool GetDate(string input,ref DateTime val)
        {
            
            if (DateTime.TryParseExact(input, "dd'.'MM'.'yyyy", null, DateTimeStyles.None, out val))
            {
                return true;
            }

            return false;
        }

        public decimal CalculatedDebt(decimal debt, DateTime date)
        {
            DateTime currentTime = DateTime.Now;
            var difference = date.Month - currentTime.Month;
            double interest;
            if (difference < 12)
            {
                interest = 0.2;
            }
            else if(11 < difference && difference < 24)
            {
                interest = 0.5;
            }
            else if(23 < difference && difference < 36)
            {
                interest = 0.8;
            }
            else
            {
                interest = 1;
            }
            decimal newDebt = debt * (decimal)interest*difference + debt;
            //List<string> dates = new List<string>();

            return newDebt;
        }
        public decimal Installment(decimal debt, int month,DateTime date)
        {

            return CalculatedDebt(debt, date) / month;
        }

        public List<DateTime> FillDateData(DateTime? date, int month)
        {
            List<DateTime> result = new List<DateTime>();
            for(int i =1; i <= month; i++)
            {
                DateTime temp = (DateTime)date;
                result.Add(temp);
                temp = temp.AddMonths(1);
            }
            return result;
        }
        public decimal TotalDebt(CustomerData data)
        {
            decimal total = 0;
            foreach(var temp in data.ACCOUNTS)
            {
                total += temp.BAKIYE;
            }
            return total;
        }

        [HttpGet]
        public void DeleteAjax()
        {
            TempData["Chat"] = null;
            TempData["CustomerObj"] = null;
            TempData["DatabaseConnection"] = null;
            TempData["StateOfChat"] = null;
        }
        //4. AJAX Call
        [HttpPost]
        public JsonResult Ajax(string AJAXParameter1)
        {
            chat = (ChatModel)TempData["Chat"];
            string result = "OK";
            customer = (CustomerData)TempData["CustomerObj"];
            JsonSendTable tableInfo = new JsonSendTable();
            JsonSendTable paymentPlanTable = new JsonSendTable();
            AJAXParameter1 = AJAXParameter1.Trim();
            StateOfChat = (string)TempData["StateOfChat"];
            conn = (OracleDatabaseAccess)TempData["DatabaseConnection"];


            if (TempData["StateOfChat"].Equals("GetMüsNo") || TempData["StateOfChat"].Equals("InvalidMüsNo"))
            {
                
                if(IsAllDigits(AJAXParameter1) && !String.IsNullOrEmpty(AJAXParameter1))
                {
                    customer = conn.GetCustomerInfo(Int32.Parse(AJAXParameter1));
                    if (customer.MUSTERI_NO != 0)
                    {
                        customer.ACCOUNTS = conn.GetCustomerAccounts(customer.MUSTERI_NO);
                        chat.usersChat.Add("GetMüsNo", AJAXParameter1);
                        StateOfChat = "ValidMüsNo";
                        TempData["StateOfChat"] = StateOfChat;

                        chat.botCorrectPath.Add("ValidMüsNo", "Merhaba "+customer.MUSTERI_ADI+", karton bilgileriniz size gösterilecektir.");
                        if (customer.ACCOUNTS.Count > 0)
                        {
                            result = "Table";
                            tableInfo.message = chat.botCorrectPath[StateOfChat];
                            decimal total = customer.ACCOUNTS[0].BAKIYE;
                            tableInfo.tableData = "<tr>" +
                                "<td>" + customer.ACCOUNTS[0].KARTON_KODU + "</td>" +
                                "<td>" + customer.ACCOUNTS[0].BAKIYE + "</td>" +
                              "</tr>";
                            for(int i=1; i< customer.ACCOUNTS.Count; i++)
                            {
                                total += customer.ACCOUNTS[i].BAKIYE;
                                tableInfo.tableData += "<tr>" +
                                                        "<td>"+customer.ACCOUNTS[i].KARTON_KODU+"</td>" +
                                                        "<td>"+customer.ACCOUNTS[i].BAKIYE+"</td>" +
                                                        "</tr>";
                            }
                            tableInfo.total = total;
                        }
                        else
                        {
                            StateOfChat = "NoDebt";
                            TempData["StateOfChat"] = StateOfChat;
                            chat.botCorrectPath.Add("NoDebt", "Merhaba " + customer.MUSTERI_ADI + ". Aktif borcunuz bulunmuyor. Sistemden çıkış yapabilirsiniz");
                            result = "Finished";
                            //Borç un olmadğı state
                        }
                    }
                    else
                    {
                        StateOfChat = "InvalidMüsNo";
                        TempData["StateOfChat"] = StateOfChat;
                        result = "OK";
                    }    
                }
                else
                {
                    StateOfChat = "InvalidMüsNo";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }

            }
            else if (TempData["StateOfChat"].Equals("ValidMüsNo") || TempData["StateOfChat"].Equals("GetMonthAgain"))
            {
                string tempComp = AJAXParameter1.ToLower();
                if (tempComp.Equals("evet") && String.IsNullOrEmpty(AJAXParameter1) == false)
                {
                    chat.usersChat.Add("GetAnsPaymentPlan", AJAXParameter1);
                    //StateOfChat = "FillTable";
                    StateOfChat = "GetMonth";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else if (tempComp.Equals("hayır") || tempComp.Equals("hayir") && !String.IsNullOrEmpty(AJAXParameter1))
                {

                    chat.usersChat.Add("GetAnsPaymentPlan", AJAXParameter1);
                    chat.botCorrectPath.Add("DeclinedPlan", customer.MUSTERI_ADI + ", borcunuz ay sonu itibariyle "+ TotalDebt(customer) +" TL olacaktır. Borcunuza temerrüt faizi her gün işlenmektedir. Banka icra yoluna gidebilir");
                    StateOfChat = "DeclinedPlan";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else
                {
                    StateOfChat = "GetMonthAgain";
                    TempData["StateOfChat"] = StateOfChat;
                }
            }
            else if (TempData["StateOfChat"].Equals("GetMonth") || TempData["StateOfChat"].Equals("GetInstallmentAgain"))
            {
                if (IsAllDigits(AJAXParameter1) && !String.IsNullOrEmpty(AJAXParameter1))
                {
                    chat.usersChat.Add("GetMonth", AJAXParameter1);
                    StateOfChat = "GetStartDate";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else
                {
                    StateOfChat = "GetInstallmentAgain";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
            }
            else if (TempData["StateOfChat"].Equals("GetStartDate") || TempData["StateOfChat"].Equals("GetStartDateAgain") || TempData["StateOfChat"].Equals("GetStartDateAgainFurther"))
            {
                DateTime dateData = new DateTime();
                if (GetDate(AJAXParameter1,ref dateData) && !String.IsNullOrEmpty(AJAXParameter1))
                {
                    if(dateData > DateTime.Today) { 
                        chat.usersChat.Add("GetStartDate", AJAXParameter1);
                        StateOfChat = "CalculatePlan";
                        Session["StateOfChat"] = StateOfChat;
                        result = "PaymentTable";
                        int month = Int32.Parse(chat.usersChat["GetMonth"]);
                        List<DateTime> dates = new List<DateTime>();
                        dates = FillDateData(dateData, month);
                        decimal totalDebt = TotalDebt(customer);
                        decimal monthlyPayment = Installment(totalDebt, month, dateData);
                        for (int i = 0; i < dates.Count; i++)
                        {
                            tableInfo.tableData += "<tr>" +
                                                    "<td>" + dates[i] + " TL</td>" +
                                                    "<td>" + Math.Round(monthlyPayment,2) + "</td>" +
                                                    "</tr>";
                        }
                        tableInfo.total = Math.Round(CalculatedDebt(totalDebt, dateData),2);
                        tableInfo.message = chat.botCorrectPath[StateOfChat];
                    }
                    else
                    {
                        StateOfChat = "GetStartDateAgainFurther";
                        Session["StateOfChat"] = StateOfChat;
                        result = "OK";
                    }
                }
                else
                {

                    StateOfChat = "GetStartDateAgain";
                    Session["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
            }
            else if(TempData["StateOfChat"].Equals("CalculatePlan") || TempData["StateOfChat"].Equals("CalculatePlanAgain"))
            {
                string tempComp = AJAXParameter1.ToLower();
                if (tempComp.Equals("evet") && String.IsNullOrEmpty(AJAXParameter1) == false)
                {
                    chat.usersChat.Add("CalculatePlanAns", AJAXParameter1);
                    //StateOfChat = "FillTable";
                    StateOfChat = "CreatePaymentPlan";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else if (tempComp.Equals("hayır") || tempComp.Equals("hayir") && !String.IsNullOrEmpty(AJAXParameter1))
                {
                    chat.usersChat.Add("CalculatePlanAns", AJAXParameter1);
                    chat.botCorrectPath.Add("DeclinedPlan2", customer.MUSTERI_ADI + ", borcunuz ay sonu itibariyle " + TotalDebt(customer) + " TL olacaktır. Borcunuza temerrüt faizi her gün işlenmektedir. Banka icra yoluna gidebilir");
                    StateOfChat = "DeclinedPlan2";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else
                {
                    StateOfChat = "CalculatePlanAgain";
                    TempData["StateOfChat"] = StateOfChat;
                }
            }
            //else
            //{
            //    //StateOfChat = "NoAction";
            //    //TempData["StateOfChat"] = StateOfChat;
            //    //chat.botCorrectPath.Add("NoAction", "Sistemden çıkış yapabilirsiniz");
            //    //Button u disable et
            //}

            List<string> data = new List<string>();
            data.Add("AJAXParameter1=" + AJAXParameter1);
            TempData["Chat"] = chat;
            TempData["CustomerObj"] = customer;
            TempData["DatabaseConnection"] = conn;
            TempData["StateOfChat"] = StateOfChat;
            string chatVal = chat.botCorrectPath[StateOfChat];


            if(result.Equals("Table") || result.Equals("PaymentTable"))
                return Json(new { tableInfo, result });

            return Json(new { chatVal , result });
        }
        
    }
}