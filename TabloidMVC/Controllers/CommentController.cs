﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{  [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }
        // GET: CommentsController
        public ActionResult Index(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            List<Comment> comments = _commentRepository.GetAllCommentsByPostId(id);
            if (post == null || comments == null)
            {
                return NotFound();
            }

            PostCommentViewModel vm = new PostCommentViewModel
            {
                Post = post,
                Comments = comments
            };

            return View(vm);
        }

        // GET: CommentsController/Details/5
        public ActionResult Details(int id)
        {
            int userId = GetCurrentUserProfileId();
            Comment comment = _commentRepository.GetCommentById(id);
            if (comment == null || comment.UserProfileId != userId)
            {
                return NotFound();
            }
            Post post = _postRepository.GetPublishedPostById(comment.PostId);
        
            PostCommentViewModel vm = new PostCommentViewModel
            {
                Post = post,
                Comment = comment 
            };

            return View(vm);
        }

        // GET: CommentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, Comment comment)
        {  
            try
            {
                int userId = GetCurrentUserProfileId();
                comment.UserProfileId = userId;
                comment.PostId = post.Id;
                comment.CreateDateTime = DateTime.Now;
                _commentRepository.AddComment(comment);
                return RedirectToAction("Index", new { id = post.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Edit/5
        public ActionResult Edit(int id)
        {
            int userId = GetCurrentUserProfileId();
            Comment comment = _commentRepository.GetCommentById(id);
            if (comment.UserProfileId != userId || comment == null )
            {
                return NotFound();
            }
           
            return View(comment);
        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {

            try
            {
                int userId = GetCurrentUserProfileId();
                comment.UserProfileId = userId;
                _commentRepository.UpdateComment(comment);
                return RedirectToAction("Details", new { id = comment.Id });
            }
            catch
            {
                return View(comment);
            }
        }

        // GET: CommentsController/Delete/5
        public ActionResult Delete(int id)
        {
            int userId = GetCurrentUserProfileId();
            Comment comment =  _commentRepository.GetCommentById(id);

            if (comment.UserProfileId != userId || comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: CommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            try
            {
                int userId = GetCurrentUserProfileId();
                Comment aComment = _commentRepository.GetCommentById(id);
                List<Post> posts = _postRepository.GetUserPostsById(userId);
                foreach (Post aPost in posts)
                {
                    if (aPost.Id == aComment.PostId)
                    {
                        _commentRepository.DeleteComment(id);
                    }
                }
                return RedirectToAction("Index", new { id = aComment.PostId });
            }
            catch
            {
                return View(comment);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
