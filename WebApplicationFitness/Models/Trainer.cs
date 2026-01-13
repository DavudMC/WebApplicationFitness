using WebApplicationFitness.Models.Common;

namespace WebApplicationFitness.Models
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
