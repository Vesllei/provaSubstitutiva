

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // Listar todos IMCs
        app.MapGet("/api/imc/listar", ([FromServices] DbContext ctx) =>
        {
            return Results.Ok(ctx.Set<IMC>().Include(i => i.Aluno).ToList());
        });

        // Listar IMC por Aluno
        app.MapGet("/api/imc/listarporaluno/{id}", ([FromServices] DbContext ctx, int id) =>
        {
            var imc = ctx.Set<IMC>().Include(i => i.Aluno).FirstOrDefault(i => i.AlunoId == id);

            if (imc == null)
            {
                return Results.NotFound("IMC não encontrado!");
            }

            return Results.Ok(imc);
        });

        // Criar IMC
        app.MapPost("/api/imc/cadastrar", ([FromServices] DbContext ctx, [FromBody] IMC imc) =>
        {
            ctx.Set<IMC>().Add(imc);
            ctx.SaveChanges();
            return Results.Created("IMC cadastrado com sucesso", imc);
        });

        // Atualizar IMC
        app.MapPut("/api/imc/alterar/{id}", ([FromServices] DbContext ctx, int id, [FromBody] IMC imcAlterado) =>
        {
            var imc = ctx.Set<IMC>().Find(id);

            if (imc == null)
            {
                return Results.NotFound("IMC não encontrado!");
            }

            imc.Valor = imcAlterado.Valor;

            ctx.Set<IMC>().Update(imc);
            ctx.SaveChanges();
            return Results.Ok("IMC alterado com sucesso!");
        });

        // Cadastrar Aluno
        app.MapPost("/api/aluno/cadastrar", ([FromServices] DbContext ctx, [FromBody] Aluno aluno) =>
        {
            ctx.Set<Aluno>().Add(aluno);
            ctx.SaveChanges();
            return Results.Created("Aluno cadastrado com sucesso", aluno);
        });

        app.Run();
    }
}
