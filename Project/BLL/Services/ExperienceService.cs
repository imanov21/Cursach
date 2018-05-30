using System;
using AutoMapper;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ExperienceService : IExperienceService
    {
        IUnitOfWork Database { get; set; }

        public ExperienceService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            Database = unitOfWork;
        }

        public void CreateExperience(ExperienceDTO experienceDTO)
        {
            if (experienceDTO == null)
                throw new ArgumentNullException(nameof(experienceDTO));

            if (experienceDTO.Id != 0 && Database.Experiences.Get(experienceDTO.Id) != null)
                throw new ArgumentOutOfRangeException("Found duplicate id experience");

            if (Database.Resumes.Get(experienceDTO.ResumeId) == null)
                throw new ArgumentOutOfRangeException("Invalid argument ResumeId");

            Database.Experiences.Create(Mapper.Map<ExperienceDTO, Experience>(experienceDTO));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void EditExperience(ExperienceDTO experienceDTO)
        {
            if (experienceDTO == null)
                throw new ArgumentNullException(nameof(experienceDTO));

            Experience experience = Database.Experiences.Get(experienceDTO.Id);

            if (experience == null)
                throw new ArgumentOutOfRangeException("Not found experience");

            Resume resume = Database.Resumes.Get(experienceDTO.ResumeId);

            if (resume == null)
                throw new ArgumentOutOfRangeException("Invalid argument ResumeId");

            experience.ResumeId = experienceDTO.ResumeId;
            experience.Company = experienceDTO.Company;
            experience.LastPosition = experienceDTO.LastPosition;
            experience.StartDate = experienceDTO.StartDate;
            experience.FinishDate = experienceDTO.FinishDate;
            experience.Resume = resume;

            Database.Experiences.Update(experience);
            Database.Save();
        }

        public void RemoveExperience(ExperienceDTO experienceDTO)
        {
            if (experienceDTO == null)
                throw new ArgumentNullException(nameof(experienceDTO));

            if (Database.Experiences.Get(experienceDTO.Id) == null)
                throw new ArgumentOutOfRangeException("Not found experience");

            Database.Experiences.Delete(experienceDTO.Id);
            Database.Save();
        }

        public ExperienceDTO GetExperienceById(int id)
        {
            return Mapper.Map<Experience, ExperienceDTO>(Database.Experiences.Get(id));
        }

        public IEnumerable<ExperienceDTO> GetAllExperiences()
        {
            return Mapper.Map<IEnumerable<Experience>, List<ExperienceDTO>>(Database.Experiences.GetAll());
        }

        public void RemoveExperience(int id)
        {
            if (Database.Experiences.Get(id) == null)
                throw new ArgumentOutOfRangeException("Not found experience");

            Database.Experiences.Delete(id);
            Database.Save();
        }
    }
}