
using Labb3Api.Data;
using Labb3Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CvDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddScoped<ProjectServices>();
            builder.Services.AddScoped<SkillServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapPost("/skill", async (Skill skill, SkillServices service) =>
            {
                await service.AddSkill(skill);
                return Results.Ok();
            });

            app.MapPost("/project", async (Project project, ProjectServices service) =>
            {
                await service.AddProject(project);
                return Results.Ok();
            });

            app.MapGet("/skills", async (Skill skill, SkillServices service) =>
            {
                var getAll = await service.GetSkills();
                return Results.Ok();
            });

            app.MapGet("/projects", async (Project project, ProjectServices service) =>
            {
                var getAll = await service.GetProjects();
                return Results.Ok();    
            });

            app.MapPut("skill/{id}", async (int id, Skill skill, SkillServices service) =>
            {
                var updateSkill = await service.UpdateSkill(id, skill);
                if (updateSkill == null)
                    return Results.NotFound("Skill not found");

                return Results.Ok(updateSkill);
            });

            app.MapPut("/project/{id}", async (int id, Project project, ProjectServices service) =>
            {
                var updateProject = await service.UpdateProject(id, project);
                if (updateProject == null)
                    return Results.NotFound("Could not find project");

                return Results.Ok(updateProject);
            });

            app.MapDelete("/skill/{id}", async (int id, SkillServices service) =>
            {
                var deletedSkill = await service.DeleteSkill(id);   
                if (deletedSkill == null)
                    return Results.NotFound("Skill not found");

                return Results.Ok(deletedSkill);    
            });

            app.MapDelete("/skill/{id}", async (int id, ProjectServices service) =>
            {
                var deletedProject = await service.DeleteProject(id);
                if (deletedProject == null)
                    return Results.NotFound("Project not found");

                return Results.Ok(deletedProject);
            });

            app.Run();
        }
    }
}
