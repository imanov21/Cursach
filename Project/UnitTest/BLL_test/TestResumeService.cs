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
    public class TestResumeService
    {
        private IResumeService resumeService;
        private Mock<IUnitOfWork> uow;
        private Mock<IRepository<Resume>> resumeRepository;
        [SetUp]
        public void Load()
        {
            uow = new Mock<IUnitOfWork>();
            resumeRepository = new Mock<IRepository<Resume>>();

            uow.Setup(x => x.Resumes).Returns(resumeRepository.Object);

            resumeService = new ResumeService(uow.Object);
        }

        [Test]
        public void CreateResume_TryToCreateResume_ShouldRepositoryCreateOnce()
        {
            var Resume = new ResumeDTO { Id = It.IsAny<int>() };

            // act
            resumeService.CreateResume(Resume);

            //assert
            resumeRepository.Verify(x => x.Create(It.IsAny<Resume>()));
        }

        [Test]
        public void AddExperience_TryToAddExperience()
        {
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => resumeService.AddExperience(null));
        }

        [Test]
        public void GetExperiences_TryToGetValue_ShouldReturnExeption()
        {
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => resumeService.GetExperiences(0));
        }
        [Test]
        public void GetOffers_TryToGetValue_ShouldReturnSomeValue()
        {
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => resumeService.GetOffers(0));
        }
        [Test]
        public void GetResumeById_TryToGetValue_ShouldReturnSomeValue()
        {
            var Resume = new Resume { Id = It.IsAny<int>() };

            uow.Setup(x => x.Resumes.Get(It.IsAny<int>())).Returns(Resume);

            // act & assert
            NUnit.Framework.Assert.IsNotNull(resumeService.GetResumeById(It.IsAny<int>()));
        }
        [Test]
        public void GetAllResumes_TryToGetSomeList_ShouldRepositoryCallOnce_ShouldReturnNotNullList()
        {
            resumeRepository.Setup(x => x.GetAll()).Returns(new List<Resume>() { });

            //act & assert
            NUnit.Framework.Assert.IsNotNull(resumeService.GetAllResumes());
            resumeRepository.Verify(x => x.GetAll());
        }
        [Test]
        public void EditResume_EditResume_ShoudRepositoryEditOnce()
        {//
         //arrange
            var resume = new ResumeDTO { Id = It.IsAny<int>(), City = It.IsAny<string>() };
            resumeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Resume { Id = It.IsAny<int>(), City = It.IsAny<string>() });

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => resumeService.EditResume(resume));
        }

        [Test]
        public void DeleteResume_DeleteRepositoryShouldCallsOnce()
        {
            resumeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Resume { Id = It.IsAny<int>(), Name = It.IsAny<string>() });

            //act
            resumeService.RemoveResume(It.IsAny<int>());

            //assert
            resumeRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }
        [Test]
        public void RemoveExperience_DeleteRepositoryShouldCallsOnce()
        {
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => resumeService.RemoveExperience(0, 0));
        }
        [Test]
        public void GetResumeById_GetNullValue_ShouldThrowException()
        {
            //arrange
            resumeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Resume>(null);

            // act & assert
            NUnit.Framework.Assert.IsNull(resumeService.GetResumeById(It.IsAny<int>()));
        }


        [Test]
        public void EditResume_PutInEditNullElement_ShouldThrowException()
        {
            // act & assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => resumeService.EditResume(null));
        }
        [Test]
        public void EditResume_NullElement_ShouldThrowException()
        {
            //arrange
            var Resume = new ResumeDTO { Id = It.IsAny<int>() };

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => resumeService.EditResume(Resume));
        }


        [Test]
        public void DeleteResume_DeleteNullValue()
        {
            //arrange
            resumeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Resume>(null);

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => resumeService.RemoveResume(It.IsAny<int>()));


        }
        [Test]
        public void RemoveResume_DeleteNullValue()
        {
            var resume = new ResumeDTO { Id = It.IsAny<int>(), Name = It.IsAny<string>() };
            resumeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Resume { Id = It.IsAny<int>(), Name = It.IsAny<string>() });

            //act
            resumeService.RemoveResume(resume);

            //assert
            resumeRepository.Verify(x => x.Delete(It.IsAny<int>()));

        }
    }
}
