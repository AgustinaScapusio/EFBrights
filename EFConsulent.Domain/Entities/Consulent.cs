using EFConsulent.Domain.Entities;

namespace EFConsulent.Domain
{
    public class Consulent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }= string.Empty;
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Email> Emails { get; set; } = new List<Email>();

    }
}
