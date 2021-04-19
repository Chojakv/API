using System.ComponentModel.DataAnnotations;

namespace Application.Models.Category
{
    public class CategoryCreationModel
    {
        [Required]
        public string Name { get; set; }
    }
}