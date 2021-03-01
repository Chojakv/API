using System.ComponentModel.DataAnnotations;

namespace API.Models.Category
{
    public class CategoryCreationModel
    {
        [Required]
        public string Name { get; set; }
    }
}