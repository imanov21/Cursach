using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Resumes")]
    public class Resume
    {
        public Resume()
        {
            Offers = new HashSet<Offer>();
            Experiences = new HashSet<Experience>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public int? HeadingId { get; set; }

        [StringLength(255)]
        public string Portfolio { get; set; }

        [StringLength(10)]
        public string Payment { get; set; }

        [StringLength(8000)]
        public string Skills { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
        public virtual Heading Heading { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
    }
}
