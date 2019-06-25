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
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
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

            try
            {
                request.ImagePath = UploadImage(request.Image);
                addCommand.Execute(request);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                RedirectToAction("Create");

            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                RedirectToAction("Create");
            }
            catch (UnsupportedTypeException e)
            {
                TempData["error"] = e.Message;
                return View();
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
            LoadById(id);

            try
            {
                var comic = getOneCommand.Execute(id);
                return View(comic);
            }
            catch (EntityNotFoundException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
        }

        // POST: Comics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] ComicUpdateDTO comic)
        {
            LoadData();
            LoadById(id);

            try
            {
                if (comic.Image != null)
                {
                    comic.ImagePath = UploadImage(comic.Image);
                }

                comic.ComicId = id;
                updateCommand.Execute(comic);
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
            catch(UnsupportedTypeException e)
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

        private string UploadImage(IFormFile file)
        {
            string extenstion = Path.GetExtension(file.FileName);
            if (!allowedFileUploadTypes.Contains(extenstion))
            {
                throw new UnsupportedTypeException("Image extension");
            }

            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

            file.CopyTo(new FileStream(uploadPath, FileMode.Create));
            return $"https://{HttpContext.Request.Host}/uploads/{fileName}";
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

        private void LoadById(int id)
        {
            ViewBag.GenreSelectedIds = Context.Genres
               .Where(g => g.DeletedAt == null)
               .Where(g => g.ComicGenres.Any(cg => cg.ComicId == id))
               .Select(g => g.Id)
               .ToList();

            ViewBag.AuthorSelectedIds = Context.Authors
               .Where(a => a.DeletedAt == null)
               .Where(a => a.ComicAuthors.Any(ca => ca.ComicId == id))
               .Select(a => a.Id )
               .ToList();

            ViewBag.PublisherSelectedId = Context.Publishers
               .Where(p => p.DeletedAt == null)
               .Where(p => p.Comics.Any(pc => pc.Id == id))
               .Select(p => p.Id )
               .FirstOrDefault();
        }

    }
}