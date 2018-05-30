using System;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Infrastructure;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTest.BLL_test
{
    [TestFixture]
    public class TestIVacancyService
    {
        private IVacancyService vacService;
        private Mock<IUnitOfWork> uow;
        private Mock<IRepository<Vacancy>> VacancyRepository;

        static TestIVacancyService()
        {
            Mapper.Initialize(cfg =>
           BLL.Infrastructure.AutoMapperConfig.Configure(cfg)
           );
        }

        [SetUp]
        public void Load()
        {
            uow = new Mock<IUnitOfWork>();
            VacancyRepository = new Mock<IRepository<Vacancy>>();

            uow.Setup(x => x.Vacancies).Returns(VacancyRepository.Object);
            uow.Setup(x => x.Offers.Get(It.IsAny<int>())).Returns(new Offer { Id = It.IsAny<int>() });

            vacService = new VacancyService(uow.Object);
        }
        [Test]
        public void CreateVacancy_TryToCreateVacancy_ShouldRepositoryCreateOnce()
        {
            var Vacancy = new VacancyDTO { Id = It.IsAny<int>() };

            // act
            vacService.CreateVacancy(Vacancy);

            //assert
            VacancyRepository.Verify(x => x.Create(It.IsAny<Vacancy>()));
        }
        [Test]
        public void CreateVacancy_TryToCreateNullValue_ShouldThrowException()
        {

            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => vacService.CreateVacancy(null));
        }
        [Test]
        public void GetVacancyById_GetNullValue_ShouldThrowException()
        {
            //arrange
            VacancyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Vacancy>(null);

            // act & assert
            NUnit.Framework.Assert.IsNull(vacService.GetVacancyById(It.IsAny<int>()));
        }
        [Test]
        public void GetVacancyById_GetValue_ShouldReturnSomeValue()
        {
            var Vacancy = new Vacancy { Id = It.IsAny<int>() };

            uow.Setup(x => x.Vacancies.Get(It.IsAny<int>())).Returns(Vacancy);

            // act & assert
            NUnit.Framework.Assert.IsNotNull(vacService.GetVacancyById(It.IsAny<int>()));
        }
        [Test]
        public void GetOffers_TryToGetValue_ShouldReturnSomeValue()
        {
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => vacService.GetOffers(0));
        }
        [Test]
        public void GetOffers_GetValue_ShouldReturnSomeValue()
        {

        }
        [Test]
        public void EditVacancy_EditVacancy_ShoudRepositoryEditOnce()
        { //arrange
            var Vacancy = new VacancyDTO { Id = It.IsAny<int>(), ContName = It.IsAny<string>(), ContPhone = It.IsAny<string>() };
            VacancyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vacancy { Id = It.IsAny<int>(), ContPhone = It.IsAny<string>() });

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => vacService.EditVacancy(Vacancy));

        }

        [Test]
        public void EditVacancy_PutInEditNullElement_ShouldThrowException()
        {
            // act & assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => vacService.EditVacancy(null));
        }

        [Test]
        public void EditVacancy_NullElement_ShouldThrowException()
        {
            //arrange
            var Vacancy = new VacancyDTO { Id = It.IsAny<int>(), ContName = It.IsAny<string>(), ContPhone = It.IsAny<string>() };
            VacancyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Vacancy>(null);

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => vacService.EditVacancy(Vacancy));
        }

        [Test]
        public void RemoveVacancyById_RemoveRepositoryById_ShouldCallsOnce()
        {
            VacancyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vacancy { Id = It.IsAny<int>(), ContName = It.IsAny<string>(), ContPhone = It.IsAny<string>() });

            //act
            vacService.RemoveVacancy(It.IsAny<int>());

            //assert
            VacancyRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }
        [Test]
        public void RemoveVacancy_RemoveRepository_ShouldCallsOnce()
        {
            var Vacancy = new VacancyDTO { Id = It.IsAny<int>(), ContName = It.IsAny<string>(), ContPhone = It.IsAny<string>() };
            VacancyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vacancy { Id = It.IsAny<int>(), ContName = It.IsAny<string>(), ContPhone = It.IsAny<string>() });

            //act
            vacService.RemoveVacancy(Vacancy);

            //assert
            VacancyRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }
        [Test]
        public void DeleteVacancy_DeleteFalseId_ShoudThrowExeption()
        {
            //arrange
            VacancyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Vacancy>(null);

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => vacService.RemoveVacancy(It.IsAny<int>()));
        }
        [Test]
        public void GetAllVacancies_GetSomeList_ShouldRepositoryCallOnce_ShouldReturnNotNullList()
        {
            VacancyRepository.Setup(x => x.GetAll()).Returns(new List<Vacancy>() { });

            //act & assert
            NUnit.Framework.Assert.IsNotNull(vacService.GetAllVacancy());
            VacancyRepository.Verify(x => x.GetAll());
        }
    }
}
