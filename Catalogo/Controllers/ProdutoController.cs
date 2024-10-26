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
    [HttpGet("{id}", Name="Listavida")]
    public async Task<ActionResult<Produto>> ListarProdutoId(int id)
    {
        Produto produto = await _context.Produtos.Where(p => p.ProdutoId.Equals(id)).FirstOrDefaultAsync();
        if (produto is null)
        {
            return NotFound("Produto Não localizado...");
        }
        return Ok(produto);
    }
}
