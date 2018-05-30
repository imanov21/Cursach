using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ResumeDTO
    {
        public ResumeDTO()
        {
            Offers = new HashSet<OfferDTO>();
            Experiences = new HashSet<ExperienceDTO>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int HeadingId { get; set; }
        public string Portfolio { get; set; }
        public string Payment { get; set; }
        public string Skills { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<OfferDTO> Offers { get; set; }
        public virtual VacancyDTO Rubric { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ICollection<ExperienceDTO> Experiences { get; set; }
    }
}
