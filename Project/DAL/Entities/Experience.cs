using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Experiences")]
    public class Experience
    {
        
            [Key]
            public int Id { get; set; }

            public int ResumeId { get; set; }

            [Required]
            [StringLength(50)]
            public string Company { get; set; }

            [StringLength(50)]
            public string LastPosition { get; set; }

            [Column(TypeName = "date")]
            public DateTime StartDate { get; set; }

            [Column(TypeName = "date")]
            public DateTime? FinishDate { get; set; }

            public virtual Resume Resume { get; set; }
        }
}