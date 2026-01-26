using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class FreelancerNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("CRIAR ROTINA PARA NOTIFICAR FREELANCERS SOBRE NOVO PROJETO");
            
            return Task.CompletedTask;
        }
    }
}
