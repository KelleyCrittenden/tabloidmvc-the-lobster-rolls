using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class PostTagController : Controller
    {

        private int GetCurrentPostId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        private readonly IPostTagRepository _postTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;


        public PostTagController(IPostRepository postRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;

        }

        // GET: PostTagController
        public IActionResult Index()
        {
            var tags = _tagRepository.GetAllTags();
            return View(tags);
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostTagController/Create
        public ActionResult Create()
        {
            List<Tag> tags = _tagRepository.GetAllTags();

            List<SelectListItem> listSelectListItem = new List<SelectListItem>();

            //for every tag we have generated a select list item
            //once you have the select item, add it to the collection
            foreach (Tag tag in tags)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = tag.Name,
                    Value = tag.Id.ToString(),
                    Selected = tag.isSelected
                };
                listSelectListItem.Add(selectListItem);
            }

            //creating an instance of the view model class
            PostTagFormViewModel vm = new PostTagFormViewModel()
            {
                PostTag = new PostTag(),
                Tags = tags,
                TagsList = listSelectListItem
            };
            return View(vm);
        }

        // POST: PostTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<string> selectedTags, PostTagFormViewModel postTagForm)
        
        {
            try
            {
                foreach (string idString in selectedTags)
                {

                    postTagForm.PostTag.TagId = int.Parse(idString);
                    _postTagRepository.AddPostTag(postTagForm.PostTag);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                    List<Tag> tags = _tagRepository.GetAllTags();
                    List<SelectListItem> listSelectListItem = new List<SelectListItem>();

                    PostTagFormViewModel vm = new PostTagFormViewModel()
                    {
                        PostTag = new PostTag(),
                        Tags = tags,
                        TagsList = listSelectListItem
                    };
                    return View(vm);

                }
        }

        // GET: PostTagController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostTagController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostTagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
