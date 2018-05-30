using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
            Vacancies = new HashSet<VacancyDTO>();
            Resumes = new HashSet<ResumeDTO>();
        }

        public string Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public virtual ICollection<VacancyDTO> Vacancies { get; set; }
        public virtual ICollection<ResumeDTO> Resumes { get; set; }
    }
}
