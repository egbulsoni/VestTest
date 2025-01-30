using System;

public class InscricaoDTO
{
    public int Id { get; set; }
    public int NumeroInscricao { get; set; }
    public DateTime Data { get; set; }
    public bool Status { get; set; }
    public int CandidatoId { get; set; }
    public string CandidatoNome { get; set; }
    public string CandidatoCpf { get; set; }
    public int ProcessoSeletivoId { get; set; }
    public int OfertaCursoId { get; set; }
}
