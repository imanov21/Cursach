using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IOfferService
    {
        void CreateOffer(OfferDTO offerDTO);
        void EditOffer(OfferDTO offerDTO);
        void RemoveOffer(OfferDTO offerDTO);
        void RemoveOffer(int id);

        OfferDTO GetOfferById(int id);
        IEnumerable<OfferDTO> GetAllOffers();

        void Dispose();
    }
}
