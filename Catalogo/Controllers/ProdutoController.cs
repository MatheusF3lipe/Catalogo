using Catalogo.Context;
using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProdutoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("ListarTodosOsProdutos")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }
    [HttpGet("{id}", Name = "ListarPorId")]
    public async Task<ActionResult<Produto>> ListarProdutoId(int id)
    {
        Produto produto = await _context.Produtos.Where(p => p.ProdutoId.Equals(id)).FirstOrDefaultAsync();
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
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return new CreatedAtRouteResult("ListarPorId", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Atualizarproduto(int id, [FromBody] Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return NotFound("Não foi possível localizar este id...");
        }
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(produto);
    }
    [HttpDelete("{id:int}")]
    public ActionResult DeletarProduto(int id)
    {
        Produto produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId.Equals(id));
        if(produto is null)
        {
            return NotFound();
        }
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return Ok();
    }
}
