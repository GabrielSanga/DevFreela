using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.UnitTests.Application
{
    public class UpdateProjectHandlerTests
    {

        [Fact]
        public async Task UpdatedDataAreOk_Update_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Descrição do projeto A", 1, 2, 1000);

            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(1).Returns((Project?) project);
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new UpdateProjectHandler(repository);

            var command = new UpdateProjectCommand
            {
                IdProject = 1,
                Title = "Projeto B",
                Description = "Descrição do Projeto B",
                TotalCost = 2000
            };

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ProjectNotExists_Update_Error()
        {
            //Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(1).Returns((Project?) null);

            var handler = new UpdateProjectHandler(repository);

            var command = new UpdateProjectCommand
            {
                IdProject = 1,
                Title = "Projeto B",
                Description = "Descrição do Projeto B",
                TotalCost = 2000
            };

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task UpdateDataAreOk_Update_Success_MOQ()
        {
            //Arrange
            var project = new Project("Projeto A", "Descrição do projeto A", 1, 2, 1000);

            var repository = new Mock<IProjectRepository>();
            repository.Setup(r => r.GetById(1)).ReturnsAsync((Project?) project);
            repository.Setup(r => r.Update(It.IsAny<Project>())).Returns(Task.CompletedTask);

            var command = new UpdateProjectCommand
            {
                IdProject = 1,
                Title = "Projeto B",
                Description = "Descrição do Projeto B",
                TotalCost = 2000
            };

            var handler = new UpdateProjectHandler(repository.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            repository.Verify(r => r.GetById(1), Times.Once);
            repository.Verify(r => r.Update(It.IsAny<Project>()), Times.Once);
        }

    }
}
