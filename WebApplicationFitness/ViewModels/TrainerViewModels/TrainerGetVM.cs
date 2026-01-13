namespace WebApplicationFitness.ViewModels.TrainerViewModels
{
    public class TrainerGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
