using System.ComponentModel.DataAnnotations;


namespace TabloidMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
