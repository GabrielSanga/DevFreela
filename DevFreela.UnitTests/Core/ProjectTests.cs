using DevFreela.Core.Enums;
using DevFreela.Core.Entities;

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
    }
}