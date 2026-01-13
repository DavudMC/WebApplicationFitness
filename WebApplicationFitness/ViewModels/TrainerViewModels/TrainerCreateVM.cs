using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationFitness.ViewModels.TrainerViewModels
{
    public class TrainerCreateVM
    {
        [Required,MaxLength(256),MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1024), MinLength(3)]
        public string Description { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3)]
        public string Profession { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; } 
    }
}
