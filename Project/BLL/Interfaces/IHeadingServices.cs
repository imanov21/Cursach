using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHeadingServices
    {
        void CreateHeading(HeadingDTO headingDTO);
        void EditHeading(HeadingDTO headingDTO);
        void RemoveHeading(HeadingDTO headingDTO);
        void RemoveHeading(int id);

        HeadingDTO GetRubricById(int id);
        IEnumerable<HeadingDTO> GetAllRubrics();

        void Dispose();
    }
}
