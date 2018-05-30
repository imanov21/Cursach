using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ExperienceDTO
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string Company { get; set; }
        public string LastPosition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public virtual ResumeDTO Resume { get; set; }
    }
}
