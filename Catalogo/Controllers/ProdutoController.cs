using Catalogo.Context;
using Catalogo.Interface;
using Catalogo.Models;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    //private readonly IRepository<Produto> _produtoRepository;
    private readonly IUnitOfWork _uof;

    public ProdutoController(IUnitOfWork uof)
    {
        _uof = uof;
    }
    [HttpGet("ListarProdutosPorCategoria")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produtos is null)
        {
           return NotFound();
        }
        return Ok(produtos); 
    }
    [HttpGet("ListarTodosOsProdutos")]
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }
    [HttpGet("{id}", Name = "ListarPorId")]
    public ActionResult<Produto> ListarProdutoId(int id)
    {
        Produto produto = _uof.ProdutoRepository.Get(c => c.CategoriaId == id);
        if (produto is null)
        {
            return NotFound("Produto Não localizado...");
        }
        return Ok(produto);
    }
    [HttpPost]

    public ActionResult<Produto> CriarProduto([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ListarPorId", new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Atualizarproduto(int id, [FromBody] Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }
        var ProdutoAtualizado = _uof.ProdutoRepository.Update(produto);

        return Ok(ProdutoAtualizado);
    }
    [HttpDelete("{id:int}")]
    public ActionResult DeletarProduto(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.CategoriaId == id);
        if(produto is null)
        {
            return NotFound("Produto Não encontrado");
        }
        _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();
        return Ok(produto);
       
    }
}
