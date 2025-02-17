using Labb3Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Api.Data
{
    public class SkillServices
    {
        private readonly CvDbContext _db;

        public SkillServices(CvDbContext db)
        {
            _db = db;
        }

        public async Task AddSkill(Skill skill)
        {
            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _db.Skills.ToListAsync();
        }

        public async Task<Skill> UpdateSkill(int id, Skill updatedSkill)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
            if (skill == null) return null;
            skill.Name = updatedSkill.Name;
            skill.YearsOfExperience = updatedSkill.YearsOfExperience;
            skill.Skillevel = updatedSkill.Skillevel;
            await _db.SaveChangesAsync();
            return skill;
        }

        public async Task<Skill> DeleteSkill(int id)
        {
            var deleteSkill = await _db.Skills.FirstOrDefaultAsync(x => x.Id == id);
            _db.Skills.Remove(deleteSkill);
            await _db.SaveChangesAsync();
            return deleteSkill;
        }
    }
}
