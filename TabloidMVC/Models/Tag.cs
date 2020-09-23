using System.ComponentModel.DataAnnotations;


namespace TabloidMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
