using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
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

    }
}
