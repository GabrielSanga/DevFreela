using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class InsertProjectHandlerTests
    {

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            //Arrange
            const int ID = 1;
            var repository = Substitute.For<IProjectRepository>();
            repository.Insert(Arg.Any<Project>()).Returns(Task.FromResult(ID));

            var mediator = Substitute.For<IMediator>();

            var command = new InsertProjectCommand
            {
                Title = "Projeto A",
                Description = "Descrição do Projeto A",
                IdCliente = 1,
                IdFreelancer = 2,
                TotalCost = 1000
            };

            var handler = new InsertProjectHandler(mediator, repository);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            await repository.Received(1).Insert(Arg.Any<Project>());
        }


        [Fact]
        public async Task InputDataAreOk_Insert_Success_MOQ()
        {
            //Arrange
            const int ID = 1;
            var mockRepository = new Mock<IProjectRepository>();
            mockRepository.Setup(mock => mock.Insert(It.IsAny<Project>())).ReturnsAsync(ID);

            var mockMediator = new Mock<IMediator>();

            var command = new InsertProjectCommand
            {
                Title = "Projeto A",
                Description = "Descrição do Projeto A",
                IdCliente = 1,
                IdFreelancer = 2,
                TotalCost = 1000
            };

            var handler = new InsertProjectHandler(mockMediator.Object, mockRepository.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            mockRepository.Verify(mock => mock.Insert(It.IsAny<Project>()), Times.Once);
        }

    }
}
