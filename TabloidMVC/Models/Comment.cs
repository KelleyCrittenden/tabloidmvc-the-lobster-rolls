using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDateTime { get; set; }
        public Post Post { get; set; }

        public UserProfile UserProfile { get; set; }

    }
}
