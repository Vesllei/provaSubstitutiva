using System;
using System.ComponentModel.DataAnnotations;

public class Aluno
{
    public int Id { get; set; }


    public required string Nome { get; set; }


    public DateTime DataNascimento { get; set; }
    public float Altura { get; set; }
}
