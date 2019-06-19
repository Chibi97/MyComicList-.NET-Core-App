using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.DataTransfer.Genres;
using MyComicList.Application.DataTransfer.Authors;
using MyComicList.Application.Requests;
using MyComicList.DataAccess;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using System.IO;
using MyComicList.Application.Responses;

namespace MyComicList.MVC.Controllers
{
    public class ComicsController : Controller
    {
        private readonly IGetComics getCommand;
        private readonly IGetOneComic getOneCommand;
        private readonly IAddComic addCommand;
        private readonly IUpdateComic updateCommand;
        private readonly IDeleteComic deleteCommand;
        private readonly List<string> allowedFileUploadTypes;

        public MyComicListContext Context { get; }
        public IConfiguration Config { get; }

        public ComicsController(MyComicListContext context, IConfiguration config, IGetComics getCommand, IGetOneComic getOneCommand,
            IAddComic addCommand, IUpdateComic updateCommand, IDeleteComic deleteCommand)
        {
            Context = context;
            Config = config;
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
            this.allowedFileUploadTypes = config.GetSection("AllowedFileUploadTypes")
                                                .AsEnumerable().Where(c => c.Value != null)
                                                .Select(c => c.Value)
                                                .ToList();
        }
        // GET: Comics
        public ActionResult Index(ComicRequest request)
        {
            var result = getCommand.Execute(request);
            return View(result);
        }

        // GET: Comics/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var comic = getOneCommand.Execute(id);
                return View(comic);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Comics/Create
        public ActionResult Create()
        {
            LoadData();
            return View();
        }

        // POST: Comics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]ComicAddDTO request)
        {
            LoadData();

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please fill up all field in this form.";
                RedirectToAction("Create");
            }

            try
            {
                string extenstion = Path.GetExtension(request.Image.FileName);
                if (!allowedFileUploadTypes.Contains(extenstion))
                {
                    TempData["error"] = "Image extension is not right.";
                    RedirectToAction("Create");
                }

                var fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                request.Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
                request.ImagePath = $"https://{HttpContext.Request.Host}/uploads/{fileName}";
                addCommand.Execute(request);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                RedirectToAction("CreateForm");

            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                RedirectToAction("Create");
            }
            catch (Exception)
            {
                TempData["error"] = "Please fill up all field in this form.";
                RedirectToAction("Create");
            }

            return View();
        }

        // GET: Comics/Edit/5
        public ActionResult Edit(int id)
        {
            LoadData();
            try
            {
                var comic = getOneCommand.Execute(id);
                return View(comic);
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }

        // POST: Comics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] ComicUpdateDTO comic)
        {
            LoadData();

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please fill up all field in this form.";
                RedirectToAction("Edit");
            }

            if (comic.Image != null)
            {
                string extenstion = Path.GetExtension(comic.Image.FileName);
                if (!allowedFileUploadTypes.Contains(extenstion))
                {
                    TempData["error"] = "Image extension is not right.";
                    RedirectToAction("Edit");
                }
            }

            try
            {
                if (comic.Image != null)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + comic.Image.FileName;
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                    comic.Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
                    comic.ImagePath = $"https://{HttpContext.Request.Host}/uploads/{fileName}";
                }

                comic.ComicId = id;
                updateCommand.Execute(comic);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                RedirectToAction("Edit");

            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                RedirectToAction("Edit");
            }

            return View();
        }

        // GET: Comics/Delete/5
        public ActionResult Delete(int id)
        {
            var comic = getOneCommand.Execute(id);
            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                deleteCommand.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        private void LoadData()
        {
            ViewBag.Publishers = Context.Publishers
                .Where(p => p.DeletedAt == null)
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name
                });

            ViewBag.Genres = Context.Genres
                .Where(g => g.DeletedAt == null)
                .Select(g => new GenreDTO
                {
                    Id = g.Id,
                    Name = g.Name
                });

            ViewBag.Authors = Context.Authors
                .Where(a => a.DeletedAt == null)
                .Select(a => new AuthorGetDTO
                {
                    Id = a.Id,
                    FullName = a.FirstName + ' ' + a.LastName
                });
        }

    }
}