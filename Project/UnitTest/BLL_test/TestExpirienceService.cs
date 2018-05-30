using System;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace UnitTest.BLL_test
{
    [TestClass]
    public class TestExpirienceService
    {
        private IExperienceService expService;
        private Mock<IUnitOfWork> uow;
        private Mock<IRepository<Experience>> expRepository;
        [SetUp]
        public void Load()
        {
            uow = new Mock<IUnitOfWork>();
            expRepository = new Mock<IRepository<Experience>>();

            uow.Setup(x => x.Experiences).Returns(expRepository.Object);

            expService = new ExperienceService(uow.Object);
        }
        [Test]
        public void CreateExperience_TryToCreateExperience_ShouldRepositoryCreateOnce()
        {
            var Experience = new ExperienceDTO { Id = It.IsAny<int>(), ResumeId = It.IsAny<int>() };

            // act
            expService.CreateExperience(Experience);

            //assert
            expRepository.Verify(x => x.Create(It.IsAny<Experience>()));
        }
        [Test]
        public void GetExperiencesExperienceById_TryToGetValue_ShouldReturnSomeValue()
        {
            var Experience = new Experience { Id = It.IsAny<int>(), ResumeId = It.IsAny<int>() };

            uow.Setup(x => x.Experiences.Get(It.IsAny<int>())).Returns(Experience);

            // act & assert
            NUnit.Framework.Assert.IsNotNull(expService.GetExperienceById(It.IsAny<int>()));
        }
        [Test]
        public void GetAllExperiences_TryToGetSomeList_ShouldRepositoryCallOnce_ShouldReturnNotNullList()
        {
            expRepository.Setup(x => x.GetAll()).Returns(new List<Experience>() { });

            //act & assert
            NUnit.Framework.Assert.IsNotNull(expService.GetAllExperiences());
            expRepository.Verify(x => x.GetAll());
        }
        [Test]
        public void GetExpirienceById_GetNullValue_ShouldThrowException()
        {
            //arrange
            expRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Resume>(null);

            // act & assert
            NUnit.Framework.Assert.IsNull(expService.GetExperienceById(It.IsAny<int>()));
        }


        [Test]
        public void EditExperience_PutInEditNullElement_ShouldThrowException()
        {
            // act & assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => expService.EditExperience(null));
        }
        [Test]
        public void EditExperience_NullElement_ShouldThrowException()
        {
            //arrange
            var experience = new ExperienceDTO { Id = It.IsAny<int>() };

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => expService.EditExperience(experience));
        }


        [Test]
        public void DeleteExperience_DeleteNullValue()
        {
            //arrange
            expRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Resume>(null);

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => expService.RemoveExperience(It.IsAny<int>()));


        }

        [Test]
        public void DeleteExperience_DeleteRepositoryShouldCallsOnce()
        {
            expRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Experience { Id = It.IsAny<int>(), ResumeId = It.IsAny<int>() });

            //act
            expService.RemoveExperience(It.IsAny<int>());

            //assert
            expRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }
        [Test]
        public void RemoveExperience_Delete()
        {
            var Experience = new ExperienceDTO { Id = It.IsAny<int>(), Company = It.IsAny<string>() };
            expRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Experience { Id = It.IsAny<int>(), Company = It.IsAny<string>() });

            //act
            expService.RemoveExperience(Experience);

            //assert
            expRepository.Verify(x => x.Delete(It.IsAny<int>()));

        }
    }
}
