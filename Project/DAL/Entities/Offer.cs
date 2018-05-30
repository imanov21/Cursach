using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public int ResumeId { get; set; }
        public DateTime DateSend { get; set; }
        public bool Checked { get; set; }

        public virtual Vacancy Vacancy { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
