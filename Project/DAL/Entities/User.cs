using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Identity.Entities;

namespace DAL.Entities
{
    [Table("Users")]
    public partial class User
    {
        public User()
        {
            Vacancies = new HashSet<Vacancy>();
            Resumes = new HashSet<Resume>();
        }

        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
    
