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
    public class TestHeadingService
    {
        private IHeadingServices HeadingService;
        private Mock<IUnitOfWork> uow;
        private Mock<IRepository<Heading>> HeadingRepository;
        [SetUp]
        public void Load()
        {
            uow = new Mock<IUnitOfWork>();
            HeadingRepository = new Mock<IRepository<Heading>>();

            uow.Setup(x => x.Headings).Returns(HeadingRepository.Object);

            HeadingService = new HeadingService(uow.Object);
        }
        [Test]
        public void CreateHeading_TryToCreateHeading_ShouldRepositoryCreateOnce()
        {
            var Heading = new HeadingDTO { Id = It.IsAny<int>() };

            // act
            HeadingService.CreateHeading(Heading);

            //assert
            HeadingRepository.Verify(x => x.Create(It.IsAny<Heading>()));
        }
        [Test]
        public void GetHeadingById_TryToGetValue_ShouldReturnSomeValue()
        {
            var Heading = new Heading { Id = It.IsAny<int>() };

            uow.Setup(x => x.Headings.Get(It.IsAny<int>())).Returns(Heading);

            // act & assert
            NUnit.Framework.Assert.IsNotNull(HeadingService.GetHeadingById(It.IsAny<int>()));
        }
        [Test]
        public void GetAllHeadings_TryToGetSomeList_ShouldRepositoryCallOnce_ShouldReturnNotNullList()
        {
            HeadingRepository.Setup(x => x.GetAll()).Returns(new List<Heading>() { });

            //act & assert
            NUnit.Framework.Assert.IsNotNull(HeadingService.GetAllHeadings());
            HeadingRepository.Verify(x => x.GetAll());
        }
        [Test]
        public void EditHeading_EditHeading_ShoudRepositoryEditOnce()
        {//
            var Heading = new HeadingDTO { Id = It.IsAny<int>(), Name = It.IsAny<string>() };
            HeadingRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Heading { Id = It.IsAny<int>(), Name = It.IsAny<string>() });

            //act
            HeadingService.EditHeading(Heading);

            //assert
            HeadingRepository.Verify(x => x.Update(It.IsAny<Heading>()), Times.Once);
        }

        [Test]
        public void DeleteHeading_DeleteRepositoryShouldCallsOnce()
        {
            HeadingRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Heading { Id = It.IsAny<int>(), Name = It.IsAny<string>() });

            //act
            HeadingService.RemoveHeading(It.IsAny<int>());

            //assert
            HeadingRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }
        [Test]
        public void GetHeadingById_GetNullValue_ShouldThrowException()
        {
            //arrange
            HeadingRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Heading>(null);

            // act & assert
            NUnit.Framework.Assert.IsNull(HeadingService.GetHeadingById(It.IsAny<int>()));
        }


        [Test]
        public void EditHeading_PutInEditNullElement_ShouldThrowException()
        {
            // act & assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => HeadingService.EditHeading(null));
        }
        [Test]
        public void EditHeading_NullElement_ShouldThrowException()
        {
            //arrange
            var Heading = new HeadingDTO { Id = It.IsAny<int>() };

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => HeadingService.EditHeading(Heading));
        }


        [Test]
        public void DeleteHeading_DeleteNullValue()
        {
            //arrange
            HeadingRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Heading>(null);

            //act & assert
            NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() => HeadingService.RemoveHeading(It.IsAny<int>()));


        }
    }
}
