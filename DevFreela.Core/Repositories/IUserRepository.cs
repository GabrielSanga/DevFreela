using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> Insert(User user);
        Task Update(User user);
        Task InsertSkill(List<UserSkill> userSkills);
        Task<User?> GetById(int id);
        Task<User?> GetByEmailAndPassword(string email, string password);
        Task<User?> GetByEmail(string email);  

    }
}
