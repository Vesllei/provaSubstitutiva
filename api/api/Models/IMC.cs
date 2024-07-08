

public class IMC
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double ValorIMC => Peso / (Altura * Altura);
    public string ClassificacaoIMC
    {
        get
        {
            if (ValorIMC < 18.5) return "Abaixo do peso";
            else if (ValorIMC < 25) return "Peso normal";
            else if (ValorIMC < 30) return "Sobrepeso";
            else if (ValorIMC < 35) return "Obesidade grau I";
            else if (ValorIMC < 40) return "Obesidade grau II";
            else return "Obesidade grau III";
        }
    }
}