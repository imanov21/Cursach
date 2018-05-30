using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class VacancyDTO
    {
        public VacancyDTO()
        {
            Offers = new HashSet<OfferDTO>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string ContName { get; set; }
        public string ContPhone { get; set; }
        public int HeadingId { get; set; }
        public string Desctiption { get; set; }
        public string UserId { get; set; }

        public virtual HeadingDTO Heading { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ICollection<OfferDTO> Offers { get; set; }
    }
}
