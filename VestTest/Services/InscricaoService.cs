using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class InscricaoService
{
    private readonly VestTestDbContext _context;

    public InscricaoService(VestTestDbContext context)
    {
        _context = context;
    }

    // Método para obter todas as inscrições
    public IEnumerable<InscricaoDTO> GetAll()
    {
        return _context.Inscricoes
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
    }

    public InscricaoDTO GetById(int id)
    {
        var inscricao = _context.Inscricoes
            .Where(i => i.Id == id)
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
            .FirstOrDefault();

        return inscricao;
    }

    public InscricaoDTO Create(InscricaoDTO inscricaoDto)
    {
        var inscricao = new Inscricao
        {
            NumeroInscricao = inscricaoDto.NumeroInscricao,
            Data = inscricaoDto.Data,
            Status = inscricaoDto.Status,
            CandidatoId = inscricaoDto.CandidatoId,
            ProcessoSeletivoId = inscricaoDto.ProcessoSeletivoId,
            OfertaCursoId = inscricaoDto.OfertaCursoId
        };

        _context.Inscricoes.Add(inscricao);
        _context.SaveChanges();

        inscricaoDto.Id = inscricao.Id;
        return inscricaoDto;
    }

    public InscricaoDTO Update(int id, InscricaoDTO inscricaoDto)
    {
        var inscricao = _context.Inscricoes.Find(id);

        if (inscricao == null)
        {
            return null;
        }

        inscricao.NumeroInscricao = inscricaoDto.NumeroInscricao;
        inscricao.Data = inscricaoDto.Data;
        inscricao.Status = inscricaoDto.Status;
        inscricao.CandidatoId = inscricaoDto.CandidatoId;
        inscricao.ProcessoSeletivoId = inscricaoDto.ProcessoSeletivoId;
        inscricao.OfertaCursoId = inscricaoDto.OfertaCursoId;

        _context.SaveChanges();

        return inscricaoDto;
    }

    public bool Delete(int id)
    {
        var inscricao = _context.Inscricoes.Find(id);

        if (inscricao == null)
        {
            return false;
        }

        _context.Inscricoes.Remove(inscricao);
        _context.SaveChanges();

        return true;
    }
    public async Task<List<InscricaoDTO>> ObterInscricoesPorCpfAsync(string cpf)
    {
        var inscricoes = await _context.Inscricoes
            .Where(i => i.Candidato.CPF == cpf)
            .Include(i => i.ProcessoSeletivo)
            .ToListAsync();

        // Transformar as inscrições em DTOs com dados adicionais
        var inscricoesDTO = inscricoes.Select(i => new InscricaoDTO
        {
            Id = i.Id,
            NumeroInscricao = i.NumeroInscricao,
            Data = i.Data,
            Status = i.Status,
            CandidatoId = i.CandidatoId,
            ProcessoSeletivoId = i.ProcessoSeletivoId,
            ProcessoSeletivoNome = i.ProcessoSeletivo.Nome,
        }).ToList();

        return inscricoesDTO;
    }

}
