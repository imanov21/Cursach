using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public abstract class IVacancyService
    {
        public abstract void CreateVacancy(VacancyDTO vacancyDTO);
        public abstract void EditVacancy(VacancyDTO vacancyDTO);
        public abstract void RemoveVacancy(VacancyDTO vacancyDTO);
        public abstract void RemoveVacancy(int id);
        public abstract VacancyDTO GetVacancyById(int id);
        public abstract IEnumerable<VacancyDTO> GetAllVacancy();
        public abstract IEnumerable<OfferDTO> GetOffers(int careerId);
        public abstract void Dispose();
    }
}
