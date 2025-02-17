using Labb3Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Api.Data
{
    public class ProjectServices
    {
        private readonly CvDbContext _db;

        public ProjectServices(CvDbContext db)
        {
            _db = db;
        }

        public async Task AddProject(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _db.Projects.ToListAsync();
        }

        public async Task<Project> UpdateProject(int id, Project updatedProject)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null) return null;
            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            await _db.SaveChangesAsync();
            return project;
        }

        public async Task<Project> DeleteProject(int id)
        {
            var deleteProject = await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            _db.Projects.Remove(deleteProject);
            await _db.SaveChangesAsync();
            return deleteProject;   
        }
    }
}
