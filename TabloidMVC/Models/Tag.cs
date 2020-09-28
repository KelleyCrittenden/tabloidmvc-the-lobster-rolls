//Created By Kelley Crittenden
//This file holds a template for the Tag

using System.ComponentModel.DataAnnotations;


namespace TabloidMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //bool statement for PostTag multi select option
        public bool isSelected { get; set; }
    }
}
