using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotNetBelt.Models;
using System.Linq;

namespace DotNetBelt.Controllers
{
  public class HomeController: Controller
    {
        private ProjectContext DbContext;
        public HomeController(ProjectContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("home")]
        public IActionResult ActivityCenter()
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            ViewBag.UserFirstName = CurrentUser.FirstName;
            ViewBag.UserId = CurrentUser.UserId;
            List<Activity> AllActivities = DbContext.Activities
                .Where(a => a.Date >= DateTime.Now)
                .Include(a => a.Participants)
                .Include(a => a.Coordinator)
                .OrderBy(a => a.Date)
                .ToList();
            return View(AllActivities);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult ActivityForm()
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            ViewBag.UserFirstName = CurrentUser.FirstName;
            ViewBag.UserId = CurrentUser.UserId;
            return View();
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult CreateActivity(Activity newdata)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();

            if (ModelState.IsValid)
            {
                Activity NewActivity = new Activity
                {
                    Title = newdata.Title,
                    Description = newdata.Description,
                    CoordinatorId = CurrentUser.UserId,
                    Duration = newdata.Duration,
                    DurationType = newdata.DurationType,
                    Date = newdata.Date.Date,
                    Time = newdata.Time,
                };
                DbContext.Activities.Add(NewActivity);
                DbContext.SaveChanges();
                return RedirectToAction("ActivityCenter", "Home");
            }
            ViewBag.UserFirstName = CurrentUser.FirstName;
            ViewBag.UserId = CurrentUser.UserId;
            return View("ActivityForm"); 
        }

        [HttpGet]
        [Route("activity/{id}")]
        public IActionResult ActivityDetail(int id)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            ViewBag.UserFirstName = CurrentUser.FirstName;
            ViewBag.UserId = CurrentUser.UserId;
            Activity ActivityData =DbContext.Activities
                .Include(a => a.Coordinator)
                .Include(a => a.Participants)
                .ThenInclude(p => p.User)
                .SingleOrDefault(a => a.ActivityId == id);
            return View(ActivityData);
        }

        [HttpGet]
        [Route("join/{id}")]
        public IActionResult Join(int id)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            ViewBag.UserFirstName = CurrentUser.FirstName;
            ViewBag.UserId = CurrentUser.UserId;

            Activity ActivityData =DbContext.Activities
                .Include(a => a.Participants)
                .ThenInclude(p => p.User)
                .SingleOrDefault(a => a.ActivityId == id);
            
            Participant ThisParticipant = DbContext.Participants
                .SingleOrDefault(p => p.UserId == CurrentUser.UserId 
                && p.ActivityId == id);
            if (ThisParticipant != null)
            {
                ActivityData.Participants.Remove(ThisParticipant);
            }
            else
            {
                Participant NewParticipant = new Participant 
                {
                    UserId = CurrentUser.UserId,
                    ActivityId = ActivityData.ActivityId,
                };
                ActivityData.Participants.Add(NewParticipant);
            }
            DbContext.SaveChanges();
            return RedirectToAction("ActivityCenter", "Home");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            ViewBag.UserFirstName = CurrentUser.FirstName;
            ViewBag.UserId = CurrentUser.UserId;
            Activity DeleteData =DbContext.Activities
                .SingleOrDefault(a => a.ActivityId == id);
            DbContext.Activities.Remove(DeleteData);
            DbContext.SaveChanges();
            return RedirectToAction("ActivityCenter", "Home");
        }


        // genernal methods
        public bool IsUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return false;
            }
            return true;
        }
        public User setCurrentUser()
        {
            User CurrentUser = DbContext.Users
                .SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));
            return CurrentUser;
        }


    }
}
