using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaiVoaApi.Model;

namespace VaiVoaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaContext _context;

        public PessoaController(PessoaContext context)
        {
            _context = context;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoaItems()
        {
            return await _context.PessoaItems.ToListAsync();
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.PessoaItems.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        // GET: api/Pessoa/5
        [HttpGet("{email}")]
        public async Task<ActionResult<List<Cartao>>> GetPessoa(string email)
        {
            var pessoa = await _context.PessoaItems.FindAsync(email);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa.CartoesVirtuais;
        }

        // PUT: api/Pessoa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pessoa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            int rand_num;
            string Geracao = "";
            Random rd = new Random();
            Cartao Virtual;
            for (int i = 0; i < 12; i++)
            {
                rand_num = rd.Next(0, 9);
                Geracao += rand_num.ToString();
            }
            var PessoaExistente = await _context.PessoaItems.FindAsync(pessoa.Email);

            if(PessoaExistente!=null)
            {
                if (PessoaExistente.CartoesVirtuais == null)
                    PessoaExistente.CartoesVirtuais = new List<Cartao>();
                Virtual = new Cartao(Int64.Parse(Geracao), DateTime.Now);
                PessoaExistente.CartoesVirtuais.Add(Virtual);
                _context.PessoaItems.Add(pessoa);
            }
            else
            {
                pessoa.CartoesVirtuais = new List<Cartao>();
                Virtual = new Cartao(Int64.Parse(Geracao), DateTime.Now);
                pessoa.CartoesVirtuais.Add(Virtual);
                _context.PessoaItems.Add(pessoa);
            }
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoa", new { id = Virtual.NumeroCartao }, pessoa);
        }

        // DELETE: api/Pessoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.PessoaItems.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.PessoaItems.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.PessoaItems.Any(e => e.Id == id);
        }
    }
}
