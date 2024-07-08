using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AcessoTotal",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.MapGet("/", () => "Prova Substitutiva - Nicolas");
app.UseCors("AcessoTotal");


// app.MapPut("/produtos/alterar/{id}", ([FromServices] Context ctx, [FromBody] Produto produtoAlterado) =>
// {
//     Produto? produto = ctx.Produtos.Find(produtoAlterado.id);
    
//     if (produto is null) return Results.NotFound("Produto não encontrado!");
    
//     produto.Nome = produtoAlterado.Nome;
//     produto.Descricao = produtoAlterado.Descricao;
//     produto.Tamanho = produtoAlterado.Tamanho;
//     produto.Tecido = produtoAlterado.Tecido;
//     produto.Valor = produtoAlterado.Valor;

//     ctx.Produtos.Update(produto);
//     ctx.SaveChanges();

//     return Results.Ok("Produto alterado com sucesso!");
// });



// if (folha is null)
// {
//     return Results.NotFound();
// }
// return Results.Ok(folha);


// lISTAR
app.MapGet("/api/folha/listar", ([FromServices] Context ctx) =>
{
    
// ([FromServices] Context ctx, [FromRoute] int mes, [FromRoute] int ano, [FromRoute] string cpf) =>
// Folha? folha = ctx.Folhas.Include(x => x.Funcionario).
//     FirstOrDefault(f => f.Funcionario.CPF == cpf && f.Mes == mes && f.Ano == ano);
// Buscar uma folha por cpf do funcionário/ mes / ano da folha
// return Results.Ok(folha);

    return Results.Ok(ctx.Folhas.Include(x => x.Funcionario).ToList()); //inclui informação da folha e do funcionário junto
});

app.MapGet("/produtos/listar", ([FromServices] Context ctx) =>
{
    if (ctx.Produtos.Any())
    {
        return Results.Ok(ctx.Produtos.ToList());
    }
    return Results.NotFound("Nenhum produto encontrado");
});

// CADASTRAR
app.MapPost("/produtos/cadastrar", ([FromServices] Context ctx, [FromBody] Produto produto) =>
{
    
// Funcionario? funcionario = ctx.Funcionarios.Find(folha.FuncionarioId); 
// verifica se usuario da folha existe para depois cadastrar
// if (funcionario is null) return Results.NotFound("Funcionário não encontrado");

    ctx.Produtos.Add(produto);
    ctx.SaveChanges();
    return Results.Created("Cadastrado com sucesso", produto);
});

// DELETAR
app.MapDelete("/produtos/deletar/{id}", ([FromServices] Context ctx, [FromRoute] int id) =>
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    
    ctx.Produtos.Remove(produto);
    ctx.SaveChanges();

    return Results.Ok("Produto deletado com sucesso!");
});

app.Run();
