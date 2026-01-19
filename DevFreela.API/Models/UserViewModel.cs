using DevFreela.Core.Entities;

namespace DevFreela.API.Models
{
    public class UserViewModel
    {

        public UserViewModel(int id, string fullName, string email, DateTime birthDate, List<String> skills)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<String> Skills { get; private set; }

        public static UserViewModel FromEntity(User entity)
        {
            return new(entity.Id, entity.FullName, entity.Email, entity.BirthDate, entity.Skills.Select(s => s.Skill.Description).ToList());
        }

    }
}
