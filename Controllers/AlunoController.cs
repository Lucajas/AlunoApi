using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EscolaTrab.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EscolaTrab.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EscolaTrab.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase

    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }
       

        // GET: api/<AlunoController>
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoRepository.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }

        }
        [HttpGet("name")]
        public async Task<ActionResult<Aluno>> GetAlunoByName([FromQuery] string name)
        {
            try
            {
                var aluno = await _alunoRepository.GetAlunosByName(name);

                if (aluno == null) return NotFound($"Não existem alunos com esse nome");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("InvalidRequest");
            }
        }
        // GET api/<AlunoController>/5
        [HttpGet("{id}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(string id)
        {
            try
            {
                var aluno = await _alunoRepository.GetAluno(id);

                if (aluno == null) return NotFound($"Não existe aluno com o ID {id}");
                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Invalid Request");
            }

        }

        // POST api/<AlunoController>
        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            var lorem = new Bogus.DataSets.Lorem();
            var aluno1 = new Aluno
            {
                Idade = aluno.Idade,
                Name = aluno.Name,
                Id = Guid.NewGuid().ToString()
            };
            try
            {
                await _alunoRepository.CreateAluno(aluno1);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno1.Id }, aluno1);
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAluno(string id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoRepository.UpdateAluno(aluno);
                    return Ok($"Aluno com id {id} foi atualizado com sucesso.");
                }
                else
                {
                    return BadRequest("Dados inconsistentes. Tente: o ID inserido deve ser o mesmo");
                }
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAluno(string id)
        {
            try
            {
                var aluno = await _alunoRepository.GetAluno(id);
                if (aluno != null)
                {
                    await _alunoRepository.DeleteAluno(aluno);
                    return Ok($"Aluno com id {id} foi excluído");
                }
                else
                {
                    return Ok($"Aluno com id {id} não encontrado");
                }
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }
    }
}