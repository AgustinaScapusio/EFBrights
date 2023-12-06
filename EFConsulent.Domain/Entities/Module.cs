using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFConsulent.Domain.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public ICollection<CourseModule>? CourseModules { get; set; }
        public Difficulty? Difficulty { get; set; }
    }
}
