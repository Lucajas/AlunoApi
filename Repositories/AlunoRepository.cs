using System;

using System.Linq;

using EscolaTrab.Contexts;

using EscolaTrab.Interfaces;
using System.Threading.Tasks;
using EscolaTrab.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EscolaTrab.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;
        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByName(string name)
        {
            IEnumerable<Aluno> alunos;
            if(!string.IsNullOrWhiteSpace(name))
            {
                alunos = await _context.Alunos.Where(n => n.Name.Contains(name)).ToListAsync();
            }
            else
            {
                alunos = await GetAlunos();
            }
            return alunos;
        }

        public async Task<Aluno> GetAluno(string id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}