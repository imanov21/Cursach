using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class HeadingDTO
    {
        public HeadingDTO()
        {
            Careers = new HashSet<VacancyDTO>();
            Resumes = new HashSet<ResumeDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<VacancyDTO> Careers { get; set; }
        public virtual ICollection<ResumeDTO> Resumes { get; set; }
    }
}
