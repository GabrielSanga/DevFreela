using DevFreela.Core.Entities;

namespace DevFreela.API.Models
{
    public class CreateSkillInputModel
    {

        public string Description { get; set; }

        public Skill ToEntity()
        {
            return new Skill(Description);
        }


    }
}
