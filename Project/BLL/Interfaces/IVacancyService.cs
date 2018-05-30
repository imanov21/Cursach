using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IVacancyService
    {
        void CreateCareer(VacancyDTO careerDTO);
        void EditCareer(VacancyDTO careerDTO);
        void RemoveCareer(VacancyDTO careerDTO);
        void RemoveCareer(int id);

        VacancyDTO GetCareerById(int id);
        IEnumerable<VacancyDTO> GetAllCareers();

        IEnumerable<OfferDTO> GetOffers(int careerId);

        void Dispose();
    }
}
