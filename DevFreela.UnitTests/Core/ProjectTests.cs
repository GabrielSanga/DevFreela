using DevFreela.Core.Enums;
using DevFreela.Core.Entities;
using Newtonsoft.Json.Bson;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {

        [Fact]
        public void ProjectIsCreated_Start_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            // Act
            project.Start();
            // Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }

        [Fact]
        public void ProjectIsInvalidState_Start_ThrowException()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            project.Start();
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.Start());
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }

        [Fact]
        public void ProjectIsInProgress_Complete_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            project.Start();
            // Act
            project.Complete();
            // Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.CompletedAt);
        }

        [Fact]
        public void ProjectIsPaymentPending_Complete_Success()
        {
            //Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            project.Start();
            project.SetPaymentPending();
            //Act
            project.Complete();
            //Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.CompletedAt);
        }

        [Fact]
        public void ProjectIsInvalidState_Complete_ThrowException()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.Complete());
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }

        [Fact]
        public void ProjectIsInProgress_SetPaymentPending_Success()
        {
            //Arange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            project.Start();
            //Act
            project.SetPaymentPending();
            //Assert
            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
        }

        [Fact]
        public void ProjectIsInvalidState_SetPaymentPending_ThrowException()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.SetPaymentPending());
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }

        [Fact]
        public void ProjectUpdated_Update_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            // Act
            project.Update("Projeto B", "Projeto Freela Atualizado", 15000);
            // Assert
            Assert.Equal("Projeto B", project.Title);
            Assert.Equal("Projeto Freela Atualizado", project.Description);
            Assert.Equal(15000, project.TotalCost);
        }

        [Fact]
        public void ProjectIsInProgress_Cancel_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            project.Start();
            // Act
            project.Cancel();
            // Assert
            Assert.Equal(ProjectStatusEnum.Canceled, project.Status);
        }

        [Fact]
        public void ProjectIsInvalidState_Cancel_ThrowException()
        {
            // Arrange
            var project = new Project("Projeto A", "Projeto Freela", 1, 2, 10000);
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.Cancel());
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }
    }
}