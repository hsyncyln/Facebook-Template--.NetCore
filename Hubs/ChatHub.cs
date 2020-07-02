using Facebook.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace Facebook.Hubs
{
    public class ChatHub : Hub
    {
        FacebookContext context = new FacebookContext();
        public async Task SendMessage(string user,string friend, string message)
        {
            Message msg = new Message();
            msg.Content = message;
            msg.SendDate = DateTime.Now;
            msg.FriendId = Convert.ToInt32(friend);
            msg.FriendUser = context.Users.Find(Convert.ToInt32(friend));

            context.Users.Find(Convert.ToInt32(user)).Messages.Add(msg);
            context.SaveChanges();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
         
        public async Task GetMessages(string user, string friend)
        {
            

            List<List<string>> deneme = new List<List<string>>();
/*
                  deneme.Add(new List<string>() { user, "hello" });
                   deneme.Add(new List<string>() { friend, "hi" });
                   deneme.Add(new List<string>() { user, "naber" });
                   deneme.Add(new List<string>() { friend, "iyi sen" });*/

            

            var de = context.Users;

            var msgsf = context.Messages.Where(x => x.FriendId == Convert.ToInt32(user) || x.FriendId == Convert.ToInt32(friend)).OrderBy(y => y.SendDate).ToList();
         /*   var msgsu = context.Users.Find(Convert.ToInt32(user)).Messages;

            List<Message> msgs = new List<Message>();

            foreach (var mf in msgsf)
            {
                if (mf.FriendId == Convert.ToInt32(user))
                {
                    msgs.Add(mf);
                }

            }
            foreach (var mu in msgsu)
            {
                if (mu.FriendId == Convert.ToInt32(friend))
                {
                     msgs.Add(mu);
                }
               
            }

            List<Message> msgsOrdered = new List<Message>();
            Message mnow;
            DateTime now;
            
            for(int i=0;i<msgs.Count; i++)
            {
                now = msgs[i].SendDate;
                mnow = msgs[i];
                for(int j = 0; j < msgs.Count ; j++)
                {
                    if(msgs[j].SendDate < now)
                    {
                        now = msgs[j].SendDate;
                        mnow = msgs[j];

                    }
                }
                msgsOrdered.Add(mnow);
                msgs.Remove(mnow);
            }
            */

            foreach (Message msg in msgsf)
            {
                if(msg.FriendId == Convert.ToInt32(user))
                {
                    deneme.Add(new List<String>() { friend, msg.Content });
                }
                else
                {
                    deneme.Add(new List<String>() { user, msg.Content });
                }
            }

                await Clients.All.SendAsync("WritePastMessage", deneme);
        }


        /*
                public async Task GetUser(string userid, string friendid )
                {

                    var friend = context.Users.Find(Convert.ToInt32(friendid));

                    await Clients.All.SendAsync("CreateMessageBox", friend);
                }
                public async Task GetMessages(string userid, string friendid)
                {

                    var messages1 = context.Users.Find(Convert.ToInt32(userid)).Messages.OrderBy(y => y.SendDate).ToList();
                    var messages2 = context.Users.Find(Convert.ToInt32(friendid)).Messages.OrderBy(y => y.SendDate).ToList();


                    await Clients.All.SendAsync("ShowMessages", messages1);
                }

                public async Task SendMessage(string message, string userid, string friendid)
                {

                    Message msg = new Message();
                    msg.Content = message;
                    msg.SendDate = DateTime.Now;
                    msg.OtherUser = context.Users.Find(Convert.ToInt32(friendid));

                    context.Users.Find(Convert.ToInt32(userid)).Messages.Add(msg);
                    context.SaveChanges();

                    await Clients.All.SendAsync("NewMessage", msg.SendDate, message);
                }

                public override Task OnConnectedAsync()
                {
                    return base.OnConnectedAsync();
                }

                public override async Task OnDisconnectedAsync(Exception exception)
                {
                   string username = Users[Context.ConnectionId];
                    bool isTaken = Users.Where(p => p.Value == username).Count() > 1;
                    if (isTaken) { return; }
                    Users.Remove(Context.ConnectionId);
                    await Clients.All.SendAsync("OnLeft", DateTime.Now, "dcfaews", 2);
                }*/

    }
}
