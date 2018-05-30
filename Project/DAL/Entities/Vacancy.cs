using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Vacancy")]
    public partial class Vacancy
    {
        public Vacancy()
        {
            Offers = new HashSet<Offer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        public string ContName { get; set; }

        [StringLength(15)]  
        public string ContPhone { get; set; }

        public int HeadingId { get; set; }

        [Required]
        [StringLength(5000)]
        public string Desctiption { get; set; }

        public string UserId { get; set; }

        public virtual Heading Heading { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}