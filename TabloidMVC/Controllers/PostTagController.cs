using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
     
            return View();
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: PostTagController/Create
        public ActionResult Create(int id)
        {
            IEnumerable<Tag> tags = _tagRepository.GetAllTags();

            List<int> tagsSelected = new List<int>();

            //creating an instance of the view model class
            PostTagFormViewModel vm = new PostTagFormViewModel()
            {
                PostTag = new PostTag(),
                Tag = tags,
                TagsSelected = tagsSelected,
                PostId = id

            };
            return View(vm);
        }

        // POST: PostTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostTagFormViewModel postTagVM)
        
        {
            try
            {
                foreach (int TagId in postTagVM.TagsSelected)
                {

                    PostTag newPostTag = new PostTag
                    {
                        PostId = postTagVM.PostId,
                        TagId = TagId
                    };
                    _postTagRepository.AddPostTag(newPostTag);
                }
                return RedirectToAction("Details", "Post", new { id = postTagVM.PostId });
            }
            catch { 

                IEnumerable<Tag> tags = _tagRepository.GetAllTags();

                List<int> tagsSelected = new List<int>();

                PostTagFormViewModel vm = new PostTagFormViewModel()
                {
                    PostTag = new PostTag(),
                    Tag = tags,
                    TagsSelected = tagsSelected,
                    PostId = postTagVM.PostId
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

            {
                IEnumerable<PostTag> postTags = _postTagRepository.GetAllPostTagsByPostId(id);

                List<int> postTagsSelected = new List<int>();

                //creating an instance of the view model class
                PostTagFormViewModel vm = new PostTagFormViewModel()
                {
                    PostTag = new PostTag(),
                    PostTagsSelected = postTagsSelected,
                    PostId = id,
                    PostTags = postTags

                };
                return View(vm);
            }

        }

        // POST: PostTagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PostTagFormViewModel postTagVM)
        {
            try
            {
                foreach (int PostTagId in postTagVM.PostTagsSelected)
                {

                    
                    _postTagRepository.DeletePostTag(PostTagId);
                }
                return RedirectToAction("Details", "Post", new { id = postTagVM.PostId });
            }
            catch
            {
                IEnumerable<PostTag> postTags = _postTagRepository.GetAllPostTagsByPostId(postTagVM.PostId);

                List<int> postTagsSelected = new List<int>();

                //creating an instance of the view model class
                PostTagFormViewModel vm = new PostTagFormViewModel()
                {
                    PostTag = new PostTag(),
                    PostTagsSelected = postTagsSelected,
                    PostId = postTagVM.PostId,
                    PostTags = postTags

                };
                return View(vm);


            }
        }
    }
}
