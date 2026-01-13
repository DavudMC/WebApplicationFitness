using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplicationFitness.Contexts;
using WebApplicationFitness.Helpers;
using WebApplicationFitness.Models;
using WebApplicationFitness.ViewModels.TrainerViewModels;

namespace WebApplicationFitness.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;
        public TrainerController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "images");
        }
        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x=> new TrainerGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Profession = x.Profession
            }).ToListAsync();
            return View(trainers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            if(!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "Bura sadece image gondere bilersiniz!");
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Bura sadece olcusu max 2 mb olan image gondere bilersiniz!");
            }
            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
            Trainer trainer = new()
            {
                Name = vm.Name, 
                Description = vm.Description,
                Profession = vm.Profession,
                ImagePath = uniqueFileName
            };
            await _context.AddAsync(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer is null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            string deletedImagePath = Path.Combine(_folderPath, trainer.ImagePath);
            ExtensionMethods.DeleteFile(deletedImagePath);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer is null)
                return NotFound();
            TrainerUpdateVM updateVM = new TrainerUpdateVM()
            {
                Name = trainer.Name,
                Description = trainer.Description,
                Profession = trainer.Profession
                
            };
            return View(updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TrainerUpdateVM vm)
        {
            if (!ModelState.IsValid) 
            {
                return View(vm);
            }
            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "Bura sadece image gondere bilersiniz!");
            }
            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "Bura sadece olcusu max 2 mb olan image gondere bilersiniz!");
            }
            var isexistTrainer = await _context.Trainers.FindAsync(vm.Id);
            if(isexistTrainer is null)
            {
                return BadRequest();
            }
            isexistTrainer.Name = vm.Name;
            isexistTrainer.Profession = vm.Profession;
            isexistTrainer.Description = vm.Description;
            if(vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
                string oldImagePath = Path.Combine(_folderPath, isexistTrainer.ImagePath);
                ExtensionMethods.DeleteFile(oldImagePath);
                isexistTrainer.ImagePath = newImagePath;
            }
            _context.Trainers.Update(isexistTrainer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
