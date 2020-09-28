using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
       
        //Constructor - Giving instance of Tag Repository via ASP.NET
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            
        }


        // GET: Tag List
        public ActionResult Index()
        {
            var tags = _tagRepository.GetAllTags();
            return View(tags);
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(tag);
            }
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            return View(tag);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            try
            {
                _tagRepository.UpdateTag(tag);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(tag);
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepository.DeleteTag(id);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(tag);
            }
        }

        // GET: Deleted Tag List
        public ActionResult DeletedIndex()
        {
            var tags = _tagRepository.GetDeletedTags();
            return View(tags);
        }

        // GET: Tag/Delete/5
        public ActionResult ReinstateTag(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReinstateTag(int id, Tag tag)
        {
            try
            {
                _tagRepository.ReinstateTag(id);
                return RedirectToAction("DeletedIndex");
            }
            catch (Exception)
            {
                return View(tag);
            }
        }
    }
}
