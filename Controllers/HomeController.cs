using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facebook.Models;

namespace Facebook.Controllers
{
    public class HomeController : Controller
    {
        FacebookContext context = new FacebookContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = context.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            else if((user.Email == email || user.PhoneNumber == email) && user.Password==password )
            {
                
                int id = user.UserId;
                return RedirectToAction("Index", "Main", new { id });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateAccount(string firstname, string lastname, string tel_email,string pswrd,string gender,string day,string month,string year)
        {
            User user = new User();
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Password = pswrd;
            user.Gender = gender;

            if(day!=null && month!=null && year != null) { 
                DateTime date = new DateTime();
                date.AddYears(Convert.ToInt32(year));
                date.AddMonths(Convert.ToInt32(month));
                date.AddDays(Convert.ToInt32(day));
                user.BirthDate = date;
            }
            
            if (tel_email.Contains("@"))
            {
                user.Email = tel_email;
            }
            else
            {
                user.PhoneNumber = tel_email;
            }
            Image image = new Image();
            if (gender == "erkek" || gender=="özel")
            {
                image.ImageLink = "iconfinder_user_male2_172626.png";
            }
            else  if(gender=="kadın"){
                image.ImageLink = "iconfinder_user_female2_172622.png";
            }
               
            context.Users.Add(user);
            context.SaveChanges();
            user.ProfilePhoto = image;
            user.Images.Add(image);   
            context.SaveChanges();

            return RedirectToAction("Index");
        }        

    }
}
