using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string queryString = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .Skip(page * size)
                    .Take(size)
                    .Where(p => !p.IsDeleted && (queryString == "" || p.Title.Contains(queryString) || p.Description.Contains(queryString))).ToList();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var project = _dbContext.Projects
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .Include(p => p.Comments)
                    .SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Projeto não encontrado");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> Insert(CreateProjectInputModel inputModel)
        {
            var project = inputModel.ToEntity();

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return ResultViewModel<ProjectViewModel>.Success(ProjectViewModel.FromEntity(project));
        }

        public ResultViewModel Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.IdProject);

            if (project == null)
            {
                return ResultViewModel.Fail("Projeto não encontrado");
            }

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return ResultViewModel.Fail("Projeto não encontrado");
            }

            project.SetAsDeleted();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return ResultViewModel.Fail("Projeto não encontrado");
            }

            project.Start();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return ResultViewModel.Fail("Projeto não encontrado");
            }

            project.Complete();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == inputModel.IdUser);

            if (project is null)
            {
                return ResultViewModel.Fail("Projeto não encontrado");
            }

            if (user is null) { 
                return ResultViewModel.Fail("Usuário não encontrado");
            }

            var comment = inputModel.ToEntity();

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
