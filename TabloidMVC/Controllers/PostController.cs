using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System.Collections.Generic;
using System;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostTagRepository _postTagRepository;



        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _postTagRepository = postTagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult UserIndex()
        {
            var posts = _postRepository.GetUserPostsById(GetCurrentUserProfileId());
            return View(posts);
        }

        public IActionResult Details(int id)
        {

                var post = new Post();

                post = _postRepository.GetPublishedPostById(id);

            if (post == null)
            {
                
            
            


              
          
            

           
                return NotFound();
            }
            else
            {
                post.TagNames = _postTagRepository.GetAllPostTagsByPostId(id);
                return View(post);
            }
            
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        public IActionResult Edit(int id)
        {
            var post = new Post();
            int userId = GetCurrentUserProfileId();
            post = _postRepository.GetUserPostById(id, userId);
            var categories = _categoryRepository.GetAll();
           
                PostCreateViewModel vm = new PostCreateViewModel
                {
                    Post = post,
                    CategoryOptions = categories
                };

                return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, Post post)
        {


            try
            {
                _postRepository.UpdatePost(post);
                return RedirectToAction("Details", new { id = post.Id });
            }
            catch
            {

          
           
            
                var postToView = _postRepository.GetPublishedPostById(id);
                var categories = _categoryRepository.GetAll();
                PostCreateViewModel vm = new PostCreateViewModel
                {
                    Post = postToView,
                    CategoryOptions = categories
                };
                return View(vm);

            }
        }

        public IActionResult Delete(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);

            return View(post);
        }

        [HttpPost]
        public IActionResult Delete(Post post)
        {
            try
            {
                _postRepository.DeletePost(post.Id);
                return RedirectToAction($"Details", new { id = post.Id });
            }
            catch
            {
                return View(post);
            }

            
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
