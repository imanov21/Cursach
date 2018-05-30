using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public int ResumeId { get; set; }
        public DateTime DateSend { get; set; }
        public bool Checked { get; set; }

        public virtual VacancyDTO Vacancy { get; set; }
        public virtual ResumeDTO Resume { get; set; }
    }
}
