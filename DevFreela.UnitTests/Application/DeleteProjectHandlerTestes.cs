using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class DeleteProjectHandlerTestes
    {
        [Fact]
        public async Task ProjectExists_Delete_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descrição do projeto A", 1, 2, 1000);

            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(1).Returns((Project?) project);
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);
            
            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            await repository.Received(1).GetById(1);
            await repository.Received(1).Update(Arg.Any<Project>());
        }
        
        [Fact]
        public async Task ProjectDoesNotExistis_Delete_Error()
        {
            //Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(1).Returns((Project?) null);

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectDeleted_Delete_Success_MOQ()
        {
            //Arrange
            var project = new Project("Projeto A", "Descrição do projeto A", 1, 2, 1000);

            var mockRepository = new Mock<IProjectRepository>();
            mockRepository.Setup(mockRepository => mockRepository.GetById(1)).ReturnsAsync(project);
            mockRepository.Setup(mockRepository => mockRepository.Update(It.IsAny<Project>())).Returns(Task.CompletedTask);

            var handler = new DeleteProjectHandler(mockRepository.Object);

            var command = new DeleteProjectCommand(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            result.IsSuccess.Should().BeTrue();
            mockRepository.Verify(mockRepository => mockRepository.GetById(1), Times.Once);
            mockRepository.Verify(mockRepository => mockRepository.Update(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExistis_Delete_Error_MOQ()
        {
            //Arrange
            var mockRepository = new Mock<IProjectRepository>();
            mockRepository.Setup(mockRepository => mockRepository.GetById(1)).ReturnsAsync((Project?) null);

            var handler = new DeleteProjectHandler(mockRepository.Object);

            var command = new DeleteProjectCommand(1);
                
            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE);
            mockRepository.Verify(mockRepository => mockRepository.GetById(It.IsAny<int>()), Times.Once);
            mockRepository.Verify(mockRepository => mockRepository.Update(It.IsAny<Project>()), Times.Never);
        }

    }
}
