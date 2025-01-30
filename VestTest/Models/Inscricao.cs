using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Inscricao
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int NumeroInscricao { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [Required]
    public Boolean Status { get; set; }

    [Required]
    public int CandidatoId { get; set; }
    [ForeignKey("CandidatoId")]
    public Candidato Candidato { get; set; }

    [Required]
    public int ProcessoSeletivoId { get; set; }
    [ForeignKey("ProcessoSeletivoId")]
    public ProcessoSeletivo ProcessoSeletivo { get; set; }

    [Required]
    public int OfertaCursoId { get; set; }
    [ForeignKey("OfertaCursoId")]
    public OfertaCurso OfertaCurso { get; set; }
}