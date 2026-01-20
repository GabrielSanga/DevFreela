using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string queryString = "", int page = 0, int size = 3);
        ResultViewModel<ProjectViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateProjectInputModel inputModel);
        ResultViewModel Update(UpdateProjectInputModel inputModel);
        ResultViewModel Delete(int id); 
        ResultViewModel Start(int id);
        ResultViewModel Complete(int id);
        ResultViewModel InsertComment(int id, CreateProjectCommentInputModel inputModel);
    }
}
