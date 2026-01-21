using DevFreela.Application.Models;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {

        ResultViewModel<UserViewModel> GetById(int id);

        ResultViewModel<UserViewModel> Insert(CreateUserInputModel inputModel);

        ResultViewModel InsertSkills(UserSkillsInputModel inputModel);

    }
}
