using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Controllers
{
    public class MainController : Controller
    {
       
        FacebookContext context = new FacebookContext();
        

        public IActionResult Index(int id)
        {
            ViewMain model = new ViewMain();
            model.MainUser = context.Users.Include("ProfilePhoto").Where(x => x.UserId==id).FirstOrDefault();
            model.Posts = context.Posts.Include("Comments.Likes").Include("Likes").Include("User.ProfilePhoto").OrderBy(x=> x.CreateDate).ToList();
            //   model.ContactFriends = context.Users.Include("Friends").Include("ProfilePhoto").Where( x => x.UserId==id).FirstOrDefault().Friends.ToList();
            var friends1= context.Friends.Include("Users.ProfilePhoto").Where(x => x.FriendUser1.UserId == id).Select( y => y.FriendUser2).ToList();
            var friends2= context.Friends.Include("Users.ProfilePhoto").Where(x => x.FriendUser2.UserId == id).Select(y => y.FriendUser1).ToList();

            foreach (User f1 in friends1)
            {
                model.ContactFriends.Add(f1);
            }
             foreach (User f2 in friends2)
            {
                model.ContactFriends.Add(f2);
            }
                

            model.AllUsers = context.Users.Include("ProfilePhoto").Take(3).ToList();
            if (model.AllUsers.Contains(model.MainUser))
            {
                model.AllUsers.Remove(model.MainUser);
            }
           
            //context.Users.Include("ProfilePhoto").Where(a=> !context.Users.Where(x => x.UserId==id).FirstOrDefault().Friends.Select(b=> b.FriendId).ToList().Contains(a.UserId)).Take(3).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult SharePost(string content, int userid)
        {
            Post post = new Post();

            post.Content = content;
            post.CreateDate = DateTime.Now;

            var user = context.Users.Find(userid);
            post.User = user;
            post.UserId = user.UserId;

            user.Posts.Add(post);
            context.SaveChanges();

            int id = userid;
            return RedirectToAction("Index","Main", new { id });
        }
        public IActionResult ShareComment(string content, int postid,int userid)
        {
            var post = context.Posts.Find(postid);

            Comment comment = new Comment();
            comment.Content = content;
            comment.CommentUser = context.Users.Find(userid);
            comment.UserId = userid;
            comment.CreateDate = DateTime.Now;

            post.Comments.Add(comment);
            context.SaveChanges();

            int id = userid;
            return RedirectToAction("Index","Main", new { id });
        }
       
        public IActionResult AddFriend(int friendid, int userid)
        {
            int id = userid;

            if((context.Friends.Where(x => x.FriendUser1.UserId == id && x.FriendUser2.UserId == friendid).Count() == 0)
                 && (context.Friends.Where(x => x.FriendUser1.UserId == friendid && x.FriendUser2.UserId == id).Count() == 0))
            {
                Friend friend = new Friend();
                friend.CreateDate = DateTime.Now;
                friend.FriendUser1 = context.Users.Find(id);
                friend.FriendUser2 = context.Users.Find(friendid);
                context.Friends.Add(friend);     
                context.SaveChanges();
            }

            else if ((context.Friends.Where(x => x.FriendUser1.UserId == id && x.FriendUser2.UserId == friendid).Count() == 1))
            {
                Friend friend = context.Friends.Where(x => x.FriendUser1.UserId == id && x.FriendUser2.UserId == friendid).First();
                context.Friends.Remove(friend);
                context.SaveChanges();
            }
            else if((context.Friends.Where(x => x.FriendUser1.UserId == friendid && x.FriendUser2.UserId == id).Count() == 1))
            {
                Friend friend = context.Friends.Where(x => x.FriendUser2.UserId == id && x.FriendUser1.UserId == friendid).First();
                context.Friends.Remove(friend);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Main", new { id });
        }

        public IActionResult MakePostLike(string userid,string postid)
        {
            int _postid = Convert.ToInt32(postid);
            int id = Convert.ToInt32(userid);

            Post post = context.Posts.Include("Likes").Where(x => x.Id == _postid).First();

            if(post.Likes.Where( y => y.LikeUserId == id ).Count() == 0 )
            {
                

                Like like = new Like();
                like.CreateDate = DateTime.Now;
                like.LikeUser = context.Users.Find(id);
                like.LikeUserId = id;

                post.Likes.Add(like);

                context.SaveChanges();

            
            }
            else if(post.Likes.Where(y => y.LikeUserId == id).Count() == 1)
            {
                Like like = context.Posts.Where(x => x.Id == _postid).First().Likes.Where(y => y.LikeUserId == id).First();
                context.Posts.Where(x => x.Id == _postid).First().Likes.Remove(like);
                context.SaveChanges();
               
            }

            return RedirectToAction("Index", "Main", new { id });

        }

        public IActionResult Messages(string friendid,string userid)
        {
            int id = Convert.ToInt32(userid); 

            ViewMain model = new ViewMain();
            model.MainUser = context.Users.Include("ProfilePhoto").Where(x => x.UserId == id).FirstOrDefault();
            model.Posts = context.Posts.Include("Comments.Likes").Include("Likes").Include("User.ProfilePhoto").OrderBy(x => x.CreateDate).ToList();
            //   model.ContactFriends = context.Users.Include("Friends").Include("ProfilePhoto").Where( x => x.UserId==id).FirstOrDefault().Friends.ToList();
            var friends1 = context.Friends.Include("Users.ProfilePhoto").Where(x => x.FriendUser1.UserId == id).Select(y => y.FriendUser2).ToList();
            var friends2 = context.Friends.Include("Users.ProfilePhoto").Where(x => x.FriendUser2.UserId == id).Select(y => y.FriendUser1).ToList();

            foreach (User f1 in friends1)
            {
                model.ContactFriends.Add(f1);
            }
            foreach (User f2 in friends2)
            {
                model.ContactFriends.Add(f2);
            }


            model.AllUsers = context.Users.Include("ProfilePhoto").Take(3).ToList();
            if (model.AllUsers.Contains(model.MainUser))
            {
                model.AllUsers.Remove(model.MainUser);
            }

            //context.Users.Include("ProfilePhoto").Where(a=> !context.Users.Where(x => x.UserId==id).FirstOrDefault().Friends.Select(b=> b.FriendId).ToList().Contains(a.UserId)).Take(3).ToList();

            model.IsMessageBoxOpen = true;

            var messages1 = context.Users.Find(id ).Messages.OrderBy(y => y.SendDate).ToList();
            var messages2 = context.Users.Find(Convert.ToInt32(friendid)).Messages.OrderBy(y => y.SendDate).ToList();
            model.Messages.AddRange(messages1);            
            model.Messages.AddRange(messages2);            

            model.MessageFriend=context.Users.Find(Convert.ToInt32(friendid));

            return View("Index",model);

        }


        public IActionResult SendMessage(string content, string userid,string friendid)
        {

            int id = Convert.ToInt32(userid);

            Message message = new Message();
            message.Content = content;
            message.SendDate=DateTime.Now;
       //     message.OtherUser = context.Users.Find(Convert.ToInt32(friendid));
            context.Users.Find(id).Messages.Add(message);
            context.SaveChanges();

            ViewMain model = new ViewMain();
            model.MainUser = context.Users.Include("ProfilePhoto").Where(x => x.UserId == id).FirstOrDefault();
            model.Posts = context.Posts.Include("Comments.Likes").Include("Likes").Include("User.ProfilePhoto").OrderBy(x => x.CreateDate).ToList();
            //   model.ContactFriends = context.Users.Include("Friends").Include("ProfilePhoto").Where( x => x.UserId==id).FirstOrDefault().Friends.ToList();
            var friends1 = context.Friends.Include("Users.ProfilePhoto").Where(x => x.FriendUser1.UserId == id).Select(y => y.FriendUser2).ToList();
            var friends2 = context.Friends.Include("Users.ProfilePhoto").Where(x => x.FriendUser2.UserId == id).Select(y => y.FriendUser1).ToList();

            foreach (User f1 in friends1)
            {
                model.ContactFriends.Add(f1);
            }
            foreach (User f2 in friends2)
            {
                model.ContactFriends.Add(f2);
            }


            model.AllUsers = context.Users.Include("ProfilePhoto").Take(3).ToList();
            if (model.AllUsers.Contains(model.MainUser))
            {
                model.AllUsers.Remove(model.MainUser);
            }

            //context.Users.Include("ProfilePhoto").Where(a=> !context.Users.Where(x => x.UserId==id).FirstOrDefault().Friends.Select(b=> b.FriendId).ToList().Contains(a.UserId)).Take(3).ToList();

            model.IsMessageBoxOpen = true;

        /*     var messages1 = context.Users.Find(id).Messages.OrderBy(y => y.SendDate).ToList();
           var messages2 = context.Users.Find(Convert.ToInt32(friendid)).Messages.OrderBy(y => y.SendDate).ToList();
            model.Messages.AddRange(messages1);
            model.Messages.AddRange(messages2);*/
            model.Messages=context.Users.Where( x => x.UserId == id || x.UserId == Convert.ToInt32(friendid)).First().Messages.OrderBy(y => y.SendDate).ToList();

            model.MessageFriend = context.Users.Find(Convert.ToInt32(friendid));

            return View("Index",model);

        }
    }
}