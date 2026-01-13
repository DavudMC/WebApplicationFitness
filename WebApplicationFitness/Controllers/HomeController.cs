using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplicationFitness.Contexts;
using WebApplicationFitness.ViewModels.TrainerViewModels;


namespace WebApplicationFitness.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var trainers = await _context.Trainers.Select(x => new TrainerGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Profession = x.Profession
            }).ToListAsync();
            return View(trainers);
        }
    }
}
