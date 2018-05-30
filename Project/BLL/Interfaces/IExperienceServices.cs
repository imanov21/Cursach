using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IExperienceService
    {
        void CreateExperience(ExperienceDTO experienceDTO);
        void EditExperience(ExperienceDTO experienceDTO);
        void RemoveExperience(ExperienceDTO experienceDTO);
        void RemoveExperience(int id);

        ExperienceDTO GetExperienceById(int id);
        IEnumerable<ExperienceDTO> GetAllExperiences();

        void Dispose();
    }
}
