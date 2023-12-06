using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFConsulent.Domain.Entities
{
    public class CourseModule
    {
        public int CourseId { get; set; }
        public int ModuleId { get; set; }
        public Course? Course { get; set; }
        public Module? Module { get; set; }
    }
}
