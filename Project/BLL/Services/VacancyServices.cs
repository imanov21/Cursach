using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class VacancyService : IVacancyService
    {
        IUnitOfWork Database { get; set; }

        public VacancyService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            Database = unitOfWork;
        }

        public override void CreateVacancy(VacancyDTO vacancyDTO)
        {
            if (vacancyDTO == null)
                throw new ArgumentNullException(nameof(vacancyDTO));

            if (vacancyDTO.Id != 0 && Database.Vacancies.Get(vacancyDTO.Id) != null)
                throw new ArgumentOutOfRangeException("Found duplicate id career");

            if (Database.Headings.Get(vacancyDTO.HeadingId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument rubricId");

            Database.Vacancies.Create(Mapper.Map<VacancyDTO, Vacancy>(vacancyDTO));
            Database.Save();
        }

        public override void Dispose()
        {
            Database.Dispose();
        }

        public override void EditVacancy(VacancyDTO vacancyDTO)
        {
            if (vacancyDTO == null)
                throw new ArgumentNullException(nameof(vacancyDTO));

            Vacancy vacancy = Database.Vacancies.Get(vacancyDTO.Id);

            if (vacancy == null)
                throw new ArgumentOutOfRangeException("Not found career");

            if (Database.Headings.Get(vacancyDTO.HeadingId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument headingId");

            vacancy.Title = vacancyDTO.Title;
            vacancy.Company = vacancyDTO.Company;
            vacancy.ContName = vacancyDTO.ContName;
            vacancy.ContPhone = vacancyDTO.ContPhone;
            vacancy.HeadingId = vacancyDTO.HeadingId;
            vacancy.Desctiption = vacancyDTO.Desctiption;

            //career.UserId = careerDTO.UserId;

            Database.Vacancies.Update(vacancy);
            Database.Save();
        }

        public override void RemoveVacancy(VacancyDTO vacancyDTO)
        {
            if (vacancyDTO == null)
                throw new ArgumentNullException(nameof(vacancyDTO));

            if (Database.Vacancies.Get(vacancyDTO.Id) == null)
                throw new ArgumentOutOfRangeException("Not found career");

            Database.Vacancies.Delete(vacancyDTO.Id);
            Database.Save();
        }

        public override VacancyDTO GetVacancyById(int id)
        {
            Vacancy career = Database.Vacancies.Get(id);

            //if (career == null)
            //    throw new ArgumentOutOfRangeException("Not found career");

            return Mapper.Map<Vacancy, VacancyDTO>(career);
        }

        public override IEnumerable<VacancyDTO> GetAllVacancy()
        {
            return Mapper.Map<IEnumerable<Vacancy>, List<VacancyDTO>>(Database.Vacancies.GetAll());
        }

        public override void RemoveVacancy(int id)
        {
            if (Database.Vacancies.Get(id) == null)
                throw new ArgumentOutOfRangeException("Not found career");

            Database.Vacancies.Delete(id);
            Database.Save();
        }

        public override IEnumerable<OfferDTO> GetOffers(int careerId)
        {
            Vacancy vacancy = Database.Vacancies.Get(careerId);

            if (vacancy == null)
                throw new ArgumentOutOfRangeException("Not found career");

            return Mapper.Map<IEnumerable<Offer>, List<OfferDTO>>(vacancy.Offers);
        }
    }
}
