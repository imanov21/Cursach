using System;
using AutoMapper;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class HeadingService : IHeadingServices
    {
        IUnitOfWork Database { get; set; }

        public HeadingService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            Database = unitOfWork;
        }

        public void CreateHeading(HeadingDTO HeadingDTO)
        {
            if (HeadingDTO == null)
                throw new ArgumentNullException(nameof(HeadingDTO));

            if (HeadingDTO.Id != 0 && Database.Headings.Get(HeadingDTO.Id) != null)
                throw new ArgumentOutOfRangeException("Found duplicate id Heading");

            Database.Headings.Create(Mapper.Map<HeadingDTO, Heading>(HeadingDTO));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void EditHeading(HeadingDTO HeadingDTO)
        {
            if (HeadingDTO == null)
                throw new ArgumentNullException(nameof(HeadingDTO));

            Heading Heading = Database.Headings.Get(HeadingDTO.Id);

            if (Heading == null)
                throw new ArgumentOutOfRangeException("Not found Heading");

            Heading.Name = HeadingDTO.Name;

            Database.Headings.Update(Heading);
            Database.Save();
        }

        public void RemoveHeading(HeadingDTO HeadingDTO)
        {
            if (HeadingDTO == null)
                throw new ArgumentNullException(nameof(HeadingDTO));

            if (Database.Headings.Get(HeadingDTO.Id) == null)
                throw new ArgumentOutOfRangeException("Not found Heading");

            Database.Headings.Delete(HeadingDTO.Id);
            Database.Save();
        }

        public IEnumerable<HeadingDTO> GetAllHeadings()
        {
            return Mapper.Map<IEnumerable<Heading>, List<HeadingDTO>>(Database.Headings.GetAll());
        }

        public HeadingDTO GetHeadingById(int id)
        {
            return Mapper.Map<Heading, HeadingDTO>(Database.Headings.Get(id));
        }

        public void RemoveHeading(int id)
        {
            if (Database.Headings.Get(id) == null)
                throw new ArgumentOutOfRangeException("Not found Heading");

            Database.Headings.Delete(id);
            Database.Save();
        }
    }
}
