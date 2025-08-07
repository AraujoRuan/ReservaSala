namespace ReservaSalaAPI.Models;

public class Reserva {
  public int Id { get; set; }
  public required string Local { get; set; }
  public required string Sala { get; set; }
  public DateTime DataHoraInicio { get; set; }
  public DateTime DataHoraFim { get; set; }
  public required string Responsavel { get; set; }
  public bool Cafe { get; set; }
  public int? QuantidadePessoasCafe { get; set; }
  public string? Descricao { get; set; }
}
