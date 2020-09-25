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

        public int PostId { get; set; }

        //Initial list to display all tag options to user
        public IEnumerable<Tag> Tag { get; set; }

        public List<int> TagsSelected { get; set; }

        

    }
}
