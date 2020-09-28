using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
//Maintained by Brett Stoudt
namespace TabloidMVC.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;

        public CategoryController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }

        //GET: Category/Index
        [Authorize(Roles = "Admin, Author")]
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }

        //GET: Category/Edit/1
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
               
                    return NotFound();
                
            }
            return View(category);
        }

        //POST: Categroy/Edit/1
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepository.UpdateCategory(category);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }

        //GET: Category/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
           
            return View();
        }

        //POST : Category/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Category newCategory)
        {

                try
                {
                
                    _categoryRepository.Add(newCategory);

                    return RedirectToAction("Index");
                }
                catch
                {

                    return View(newCategory);
                }
            
        }





        // GET: Owners/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {

                return NotFound();

            } else if (category.Id == 1)
            {

                return NotFound();

            }
            return View(category);
        }

        // POST: Owners/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            if (id == 1)
            {

                return NotFound();

            }
            try
            {
                _categoryRepository.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }

    private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
