using DevFreela.Core.Entities;

namespace DevFreela.API.Models
{
    public class CreateProjectCommentInputModel
    {

        public string Content { get; set; }

        public int IdProject { get; set; }

        public int IdUser { get; set; }

        public ProjectComment ToEntity()
        {
            return new(Content, IdProject, IdUser);
        }

    }
}
