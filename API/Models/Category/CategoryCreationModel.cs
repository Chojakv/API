using System.ComponentModel.DataAnnotations;
using API.Domain;

namespace API.Models.Category
{
    public class CategoryCreationModel
    {
        [Required]
        public string Name { get; set; }
    }
}