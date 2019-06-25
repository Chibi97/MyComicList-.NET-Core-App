using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Roles;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Interfaces;
using MyComicList.Application.Requests;
using MyComicList.DataAccess;

namespace MyComicList.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGetUsers getCommand;
        private readonly IGetOneUser getOneCommand;
        private readonly IAddUser addCommand;
        private readonly IUpdateUser updateCommand;
        private readonly IDeleteUser deleteCommand;
        private readonly IEmailSender emailSender;

        public MyComicListContext Context { get; }

        public UsersController(MyComicListContext context, IGetUsers getCommand, IGetOneUser getOneCommand, IAddUser addCommand,
            IUpdateUser updateCommand, IDeleteUser deleteCommand)
        {
            Context = context;
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        // GET: Users
        public ActionResult Index([FromQuery] UserRequest request)
        {
            var result = getCommand.Execute(request);
            return View(result);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var user = getOneCommand.Execute(id);
                return View(user);
            } catch(EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            LoadData();
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAddDTO request)
        {
            LoadData();
            try
            {
                addCommand.Execute(request);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                return View();

            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Please fill up all field in this form.";
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            LoadData();
            LoadById(id);
            try
            {
                var user = getOneCommand.Execute(id);
                return View(user);
            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserUpdateDTO request)
        {
            LoadData();
            LoadById(id);
            try
            {
                request.UserId = id;
                updateCommand.Execute(request);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                return View();

            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Please fill up all field in this form.";
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                deleteCommand.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
        }

        private void LoadData()
        {
            ViewBag.Roles = Context.Roles
                .Where(r => r.DeletedAt == null)
                .Select(r => new RoleDTO {
                    Id = r.Id,
                    Name = r.Name
                });
        }

        private void LoadById(int id)
        {
            ViewBag.RoleSelectedId = Context.Roles
                .Where(r => r.DeletedAt == null)
                .Where(r => r.Users.Any(ru => ru.Id == id))
                .Select(p => p.Id)
                .FirstOrDefault();
        }
    }
}