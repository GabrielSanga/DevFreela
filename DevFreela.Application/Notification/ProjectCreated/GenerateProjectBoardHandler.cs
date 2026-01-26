using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CRIANDO PAINEL PARA O PROJETO {notification.Title}!");

            return Task.CompletedTask;
        }
    }
}
