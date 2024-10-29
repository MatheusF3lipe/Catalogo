using Catalogo.Context;
using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategorias()
    {
        return await _context.Categorias.ToListAsync();
    }
    [HttpGet("{id:int}", Name = "ListarCategoriaPorId")]
    public async Task<ActionResult<Categoria>> ListarCategoriaPorId(int id)
    {
        return await _context.Categorias.Where(p => p.CategoriaId == id).FirstOrDefaultAsync();
    }
    [HttpGet("ListarProd")]
    public ActionResult<IEnumerable<Categoria>> listarProdutosPorCategorias()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList();
    }
    [HttpPost]
    public ActionResult CriarCategoria([FromBody] Categoria categoria)
    {
        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);
        }
        _context.Add(categoria);
        _context.SaveChanges();
        return new CreatedAtRouteResult("ListarCategoriaPorId", new { id = categoria.CategoriaId }, categoria);
    }
    [HttpPut("{id:int}")]
    public ActionResult AtualizarProduto(int id, [FromBody] Categoria categoria)
    {

        if (id != categoria.CategoriaId)
        {
            return BadRequest(ModelState);

        }
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Deletar(int id)
    {
        Categoria categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId.Equals(id));
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return Ok();
    }
}