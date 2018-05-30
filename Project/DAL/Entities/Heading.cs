using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Headings")]
    public class Heading
    {
        public Heading()
        {
            Vacancy = new HashSet<Vacancy>();
            Resumes = new HashSet<Resume>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Vacancy> Vacancy { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
