using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddComment(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAll(string queryString, int page, int size)
        {
            var projects = await _dbContext.Projects
                        .Include(p => p.Client)
                        .Include(p => p.Freelancer)
                        .Skip(page * size)
                        .Take(size)
                        .Where(p => !p.IsDeleted && (queryString == "" || p.Title.Contains(queryString) || p.Description.Contains(queryString)))
                        .ToListAsync();

            return projects;
        }

        public async Task<Project?> GetById(int id)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetDetailsById(int id)
        {
            var project = await _dbContext.Projects
                                    .Include(p => p.Client)
                                    .Include(p => p.Freelancer)
                                    .Include(p => p.Comments)
                                    .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task<int> Insert(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task Update(Project project)
        {
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}
