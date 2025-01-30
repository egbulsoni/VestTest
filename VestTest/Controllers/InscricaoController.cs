using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class InscricaoController : ControllerBase
{
    private readonly VestTestDbContext _context;
    public InscricaoController(VestTestDbContext context) => _context = context;

    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<IEnumerable<InscricaoDTO>>> GetInscricoesPorCpf(string cpf)
    {
        var inscricoes = await _context.Inscricoes
            .Include(i => i.Candidato)
            .Where(i => i.Candidato.CPF == cpf)
            .Select(i => new InscricaoDTO
            {
                Id = i.Id,
                NumeroInscricao = i.NumeroInscricao,
                Data = i.Data,
                Status = i.Status,
                CandidatoId = i.CandidatoId,
                CandidatoNome = i.Candidato.Nome,
                CandidatoCpf = i.Candidato.CPF,
                ProcessoSeletivoId = i.ProcessoSeletivoId,
                OfertaCursoId = i.OfertaCursoId
            })
            .ToListAsync();

        if (!inscricoes.Any())
        {
            return NotFound("Nenhuma inscrição encontrada para este CPF.");
        }

        return Ok(inscricoes);
    }

    [HttpGet("por-oferta/{ofertaId}")]
    public IActionResult GetInscricoesPorOferta(int ofertaId)
    {
        var inscricoes = _context.Inscricoes
            .Where(i => i.OfertaCursoId == ofertaId)
            .Select(i => new InscricaoDTO
            {
                Id = i.Id,
                NumeroInscricao = i.NumeroInscricao,
                Data = i.Data,
                Status = i.Status,
                CandidatoId = i.CandidatoId,
                ProcessoSeletivoId = i.ProcessoSeletivoId,
                OfertaCursoId = i.OfertaCursoId
            })
            .ToList();

        if (inscricoes.Count == 0)
            return NotFound("Nenhuma inscrição encontrada para essa oferta.");

        return Ok(inscricoes);
    }



    [HttpGet]
    public IActionResult GetAll()
    {
        var inscricoes = _context.Inscricoes.ToList();
        return Ok(inscricoes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var inscricao = _context.Inscricoes.Find(id);

        if (inscricao == null)
        {
            return NotFound();
        }

        return Ok(inscricao);
    }


    [HttpPost]
    public IActionResult Create([FromBody] Inscricao inscricao)
    {
        if (inscricao == null)
        {
            return BadRequest("Inscrição inválida.");
        }

        _context.Inscricoes.Add(inscricao);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = inscricao.Id }, inscricao);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Inscricao inscricao)
    {
        if (id != inscricao.Id)
        {
            return BadRequest("ID do recurso não corresponde.");
        }

        var existingInscricao = _context.Inscricoes.Find(id);
        if (existingInscricao == null)
        {
            return NotFound();
        }

        // Atualizar os dados da inscrição existente
        existingInscricao.NumeroInscricao = inscricao.NumeroInscricao;
        existingInscricao.Data = inscricao.Data;
        existingInscricao.Status = inscricao.Status;
        existingInscricao.OfertaCursoId = inscricao.OfertaCursoId;
        existingInscricao.CandidatoId = inscricao.CandidatoId;
        existingInscricao.ProcessoSeletivoId = inscricao.ProcessoSeletivoId;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var inscricao = _context.Inscricoes.Find(id);
        if (inscricao == null)
        {
            return NotFound();
        }

        _context.Inscricoes.Remove(inscricao);
        _context.SaveChanges();

        return NoContent();
    }

}
