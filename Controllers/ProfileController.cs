using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Controllers
{
    public class ProfileController : Controller
    {
        FacebookContext context = new FacebookContext();
        public IActionResult Index(int id)
        {
            ViewMain model = new ViewMain();
            model.MainUser = context.Users.Include("ProfilePhoto").Where(x => x.UserId == id).FirstOrDefault();
            model.Posts = context.Posts.Where(y => y.UserId == id).Include("Comments").Include("Comments.Likes").Include("Likes").Include("User.ProfilePhoto").OrderBy(x => x.CreateDate).ToList();

            var friends1 = context.Friends.Where(x => x.FriendUser1.UserId == id).Select(y => y.FriendUser2).Include("ProfilePhoto").ToList();
            var friends2 = context.Friends.Where(x => x.FriendUser2.UserId == id).Select(y => y.FriendUser1).Include("ProfilePhoto").ToList();

            foreach (User f1 in friends1)
            {
                model.ContactFriends.Add(f1);
            }
            foreach (User f2 in friends2)
            {
                model.ContactFriends.Add(f2);
            }

            model.Photos = context.Users.Find(id).Images.Where( x => x.ImageId != model.MainUser.ProfilePhoto.ImageId ).ToList();
        
            return View(model);
        }

        public IActionResult FriendProfile(int id)
        {
            ViewMain model = new ViewMain();
            model.MainUser = context.Users.Include("ProfilePhoto").Where(x => x.UserId == id).FirstOrDefault();
            model.Posts = context.Posts.Where(y => y.UserId == id).Include("Comments").Include("Comments.Likes").Include("Likes").Include("User.ProfilePhoto").OrderBy(x => x.CreateDate).ToList();

            var friends1 = context.Friends.Where(x => x.FriendUser1.UserId == id).Select(y => y.FriendUser2).Include("ProfilePhoto").ToList();
            var friends2 = context.Friends.Where(x => x.FriendUser2.UserId == id).Select(y => y.FriendUser1).Include("ProfilePhoto").ToList();

            foreach (User f1 in friends1)
            {
                model.ContactFriends.Add(f1);
            }
            foreach (User f2 in friends2)
            {
                model.ContactFriends.Add(f2);
            }

            model.Photos = context.Users.Find(id).Images.Where(x => x.ImageId != model.MainUser.ProfilePhoto.ImageId).ToList();

            return View(model);
        }
    }
}