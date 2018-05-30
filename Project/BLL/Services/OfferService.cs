using System;
using AutoMapper;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class OfferService : IOfferService
    {
        IUnitOfWork Database { get; set; }

        public OfferService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            Database = unitOfWork;
        }

        public void CreateOffer(OfferDTO offerDTO)
        {
            if (offerDTO == null)
                throw new ArgumentNullException(nameof(offerDTO));

            if (offerDTO.Id != 0 && Database.Offers.Get(offerDTO.Id) != null)
                throw new ArgumentOutOfRangeException("Found duplicate id offer");

            if (Database.Resumes.Get(offerDTO.ResumeId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument ResumeId");

            if (Database.Vacancies.Get(offerDTO.VacancyId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument VacancyId");

            Database.Offers.Create(Mapper.Map<OfferDTO, Offer>(offerDTO));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void EditOffer(OfferDTO offerDTO)
        {
            if (offerDTO == null)
                throw new ArgumentNullException(nameof(offerDTO));

            Resume resume = Database.Resumes.Get(offerDTO.ResumeId);

            if (resume == null)
                throw new ArgumentOutOfRangeException("Invalid argument ResumeId");

            Vacancy Vacancy = Database.Vacancies.Get(offerDTO.VacancyId);

            if (Vacancy == null)
                throw new ArgumentOutOfRangeException("Invalid argument VacancyId");

            Offer offer = Database.Offers.Get(offerDTO.Id);

            if (offer == null)
                throw new ArgumentOutOfRangeException("Not found offer");

            offer.ResumeId = offerDTO.ResumeId;
            offer.VacancyId = offerDTO.VacancyId;
            offer.DateSend = offerDTO.DateSend;
            offer.Checked = offerDTO.Checked;
            offer.Vacancy = Vacancy;
            offer.Resume = resume;

            Database.Offers.Update(offer);
            Database.Save();
        }

        public void RemoveOffer(OfferDTO offerDTO)
        {
            if (offerDTO == null)
                throw new ArgumentNullException(nameof(offerDTO));

            if (Database.Offers.Get(offerDTO.Id) == null)
                throw new ArgumentOutOfRangeException("Not found offer");

            Database.Offers.Delete(offerDTO.Id);
            Database.Save();
        }

        public OfferDTO GetOfferById(int id)
        {
            return Mapper.Map<Offer, OfferDTO>(Database.Offers.Get(id));
        }

        public IEnumerable<OfferDTO> GetAllOffers()
        {
            return Mapper.Map<IEnumerable<Offer>, List<OfferDTO>>(Database.Offers.GetAll());
        }

        public void RemoveOffer(int id)
        {
            if (Database.Offers.Get(id) == null)
                throw new ArgumentOutOfRangeException("Not found offer");

            Database.Offers.Delete(id);
            Database.Save();
        }
    }
}
