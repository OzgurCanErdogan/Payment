using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class DefaultController : Controller
    {

        ChatModel chat = new ChatModel();
        string StateOfChat;

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
            chat.botCorrectPath = new Dictionary<string, string>();
            chat.botFalsePath = new Dictionary<string, string>();
            chat.usersChat = new Dictionary<string, string>();
            chat.botCorrectPath.Add("ValidMüsNo", "Merhaba Özgür Can Erdoğan, karton bilgileriniz size gösterilecektir. Ödeme planı yapmak ister misiniz?");
            chat.botCorrectPath.Add("InvalidMüsNo", "Girdiğiniz verilerle ilgili bir hesap bulunamadı. Bilgilerinizi kontrol edip tekrar giriniz.");
            chat.botCorrectPath.Add("GetMonth", "Kaç ayda ödemek istersiniz?");
            chat.botCorrectPath.Add("GetMonthAgain", "Girdiğiniz veri sadece rakamlardan oluşmalı. Tekrar giriniz");
            chat.botCorrectPath.Add("GetStartDate", "Ödemeye ne zaman başlayacağınızı belirtiniz. (dd/MM/yyyy formatında olmalı)");
            chat.botCorrectPath.Add("GetStartDateAgain", "Girdiğiniz veriyi kontrol edin. (dd/MM/yyyy formatında olmalı)");

            chat.botFalsePath.Add("InvalidMüsNo", "Girdiğiniz değerle eşleşen müşteri numarası çıkmadı. Tekrar kontrol edip girer misiniz");
            chat.usersChat.Add("MüsNo", "null");
            StateOfChat = "GetMüsNo";


        }
        public ActionResult Index()
        {
            initVal();
            TempData["Chat"] = chat;
            TempData["StateOfChat"] = StateOfChat;
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
        //4. AJAX Call
        [HttpPost]
        public JsonResult Ajax(string AJAXParameter1)
        {
            chat = (ChatModel)TempData["Chat"];
            string result = "False";
            if (TempData["StateOfChat"].Equals("GetMüsNo") || TempData["StateOfChat"].Equals("InvalidMüsNo"))
            {
                if(AJAXParameter1 == "21300586")
                {
                    chat.usersChat.Add("GetMüsNo", AJAXParameter1);
                    StateOfChat = "ValidMüsNo";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else
                {
                    StateOfChat = "InvalidMüsNo";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }

            }
            else if (TempData["StateOfChat"].Equals("ValidMüsNo"))
            {
                if (AJAXParameter1.Equals("Evet"))
                {
                    chat.usersChat.Add("GetAnsPaymentPlan", AJAXParameter1);
                    StateOfChat = "GetMonth";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else if (AJAXParameter1.Equals("Hayır"))
                {
                    chat.usersChat.Add("GetAnsPaymentPlan", AJAXParameter1);
                    StateOfChat = "DeclinedPlan";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
            }
            else if (TempData["StateOfChat"].Equals("GetMonth") || TempData["StateOfChat"].Equals("GetMonthAgain"))
            {
                if (IsAllDigits(AJAXParameter1))
                {
                    chat.usersChat.Add("GetMonth", AJAXParameter1);
                    StateOfChat = "GetStartDate";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else
                {
                    StateOfChat = "GetMonthAgain";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
            }
            else if (TempData["StateOfChat"].Equals("GetStartDate") || TempData["StateOfChat"].Equals("GetStartDateAgain"))
            {
                if (IsAllDigits(AJAXParameter1))
                {
                    chat.usersChat.Add("GetMonth", AJAXParameter1);
                    StateOfChat = "GetPlan";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
                else
                {
                    StateOfChat = "GetMonthAgain";
                    TempData["StateOfChat"] = StateOfChat;
                    result = "OK";
                }
            }

            List<string> data = new List<string>();
            data.Add("AJAXParameter1=" + AJAXParameter1);
            TempData["Chat"] = chat;
            string chatVal = chat.botCorrectPath[StateOfChat];
            
            return Json(new { chatVal , result });
        }
    }
}