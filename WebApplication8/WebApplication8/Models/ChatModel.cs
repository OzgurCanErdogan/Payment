using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{

    //public class testData
    //{
    //    public string UserType { get; set; }
    //    public string Text { get; set; }
    //}
    public class Result
    {
        
    }
    public class ChatModel
    {

        //List<string> test2 = new List<string>();
        public Dictionary<string,string> usersChat { get; set; }
        public Dictionary<string, string> botCorrectPath { get; set; }
        public Dictionary<string, string> botFalsePath { get; set; }
    }
    

}