using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostTagFormViewModel
    {
        public PostTag PostTag { get; set; }

        //Initial list to display all tag options to user
        public List<Tag> Tags { get; set; }

        public IEnumerable<SelectListItem> TagsList { get; set; }

        //List to Store Tags Selected by User to attach to Post
        public IEnumerable<string> SelectedTags { get; set; }
    }
}
