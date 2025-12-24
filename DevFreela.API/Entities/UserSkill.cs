namespace DevFreela.API.Entities
{
    public class UserSkill : BaseEntity
    {
        public UserSkill(int idUser, int idSkill) : base()
        {
            this.idUser = idUser;
            this.idSkill = idSkill;
        }

        public int idUser { get; private set; }
        public User  User { get; private set; }
        public int idSkill { get; private set; }
        public Skill Skill { get; private set; }

    }
}
