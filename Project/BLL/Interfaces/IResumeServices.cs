using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IResumeService
    {
        void CreateResume(ResumeDTO resumeDTO);
        void EditResume(ResumeDTO resumeDTO);
        void RemoveResume(ResumeDTO resumeDTO);
        void RemoveResume(int id);

        void AddExperience(ExperienceDTO experienceDTO);
        void RemoveExperience(int resumeId, int experienceId);

        ResumeDTO GetResumeById(int id);
        IEnumerable<ResumeDTO> GetAllResumes();

        IEnumerable<ExperienceDTO> GetExperiences(int resumeId);
        IEnumerable<OfferDTO> GetOffers(int resumeId);

        void Dispose();
    }
}
