using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaTrab.Models;

namespace EscolaTrab.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAlunos();

        Task<Aluno> GetAluno(string id);

        Task<IEnumerable<Aluno>> GetAlunosByName(string name);
        Task CreateAluno(Aluno aluno);

        Task DeleteAluno(Aluno aluno);

        Task UpdateAluno(Aluno aluno);

    }

}