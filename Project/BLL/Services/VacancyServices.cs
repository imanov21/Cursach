using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLl.Services
{
    public class VanancyService : IVacancyService
    {
        IUnitOfWork Database { get; set; }

        public VacancyService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            Database = unitOfWork;
        }

        public void CreateVacancy(VacancyDTO vacancyDTO)
        {
            if (vacancyDTO == null)
                throw new ArgumentNullException(nameof(vacancyDTO));

            if (vacancyDTO.Id != 0 && Database.Vacancy.Get(vacancyDTO.Id) != null)
                throw new ArgumentOutOfRangeException("Found duplicate id career");

            if (Database.Heading.Get(vacancyDTO.HeadingId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument rubricId");

            Database.Heading.Create(Mapper.Map<VacancyDTO, Vacancy>(vacancyDTO));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void EditCareer(CareerDTO careerDTO)
        {
            if (careerDTO == null)
                throw new ArgumentNullException(nameof(careerDTO));

            Vacancy vacancy = Database.Vacancies.Get(careerDTO.Id);

            if (vacancy == null)
                throw new ArgumentOutOfRangeException("Not found career");

            if (Database.Headings.Get(careerDTO.RubricId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument rubricId");

            vacancy.Title = vacancyDTO.Title;
            vacancy.Company = vacancyDTO.Company;
            vacancy.ContName = vacancyDTO.ContactName;
            vacancy.ContPhone = vacancyDTO.ContactPhone;
            vacancy.HeadingId = vacancyDTO.HeadingId;
            vacancy.Desctiption = vacancyDTO.Desctiption;

            //career.UserId = careerDTO.UserId;

            Database.Vacancies.Update(vacancy);
            Database.Save();
        }

        public void RemoveCareer(VacancyDTO vacancyDTO)
        {
            if (vacancyDTO == null)
                throw new ArgumentNullException(nameof(vacancyDTO));

            if (Database.Vacancies.Get(vacancyDTO.Id) == null)
                throw new ArgumentOutOfRangeException("Not found career");

            Database.Vacancies.Delete(vacancyDTO.Id);
            Database.Save();
        }

        public VacancyDTO GetCareerById(int id)
        {
            Career career = Database.Vacancies.Get(id);

            //if (career == null)
            //    throw new ArgumentOutOfRangeException("Not found career");

            return Mapper.Map<Vacancy, VacancyDTO>(career);
        }

        public IEnumerable<VacancyDTO> GetAllCareers()
        {
            return Mapper.Map<IEnumerable<Vacancy>, List<VacancyDTO>>(Database.Vacancies.GetAll());
        }

        public void RemoveCareer(int id)
        {
            if (Database.Vacancies.Get(id) == null)
                throw new ArgumentOutOfRangeException("Not found career");

            Database.Vacancies.Delete(id);
            Database.Save();
        }

        public IEnumerable<OfferDTO> GetOffers(int careerId)
        {
            Vacancy career = Database.Vacancies.Get(careerId);

            if (career == null)
                throw new ArgumentOutOfRangeException("Not found career");

            return Mapper.Map<IEnumerable<Offer>, List<OfferDTO>>(career.Offers);
        }
    }
}
