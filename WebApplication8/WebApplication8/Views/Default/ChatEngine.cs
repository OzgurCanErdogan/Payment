using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Views.Default
{
    public class ChatEngine : Hub
    {
        static List<string> UserMessages = new List<string>();
        static List<string> BotMessages = new List<string>();

        public List<string> bot()
        {
            return BotMessages;
        }
        public void Send(string message)
        {
            Clients.All.sendMessage(message);
        }
    }

}