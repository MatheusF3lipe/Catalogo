using Catalogo.Context;
using Catalogo.Filters;
using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CategoriaController> _logger;
    public CategoriaController(AppDbContext context, IConfiguration configuration, ILogger<CategoriaController> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategorias()
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS");
        return await _context.Categorias.ToListAsync();
    }
    [HttpGet("{id:int}", Name = "ListarCategoriaPorId")]
    public async Task<ActionResult<Categoria>> ListarCategoriaPorId(int id)
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS POR ID");

        return await _context.Categorias.Where(p => p.CategoriaId == id).FirstOrDefaultAsync();
    }
    [HttpGet("ListarProd")]
    public ActionResult<IEnumerable<Categoria>> listarProdutosPorCategorias()
    {
        throw new Exception("Excelçai ai retornar prod pelo id");
        return _context.Categorias.Include(p => p.Produtos).ToList();
    }
    [HttpGet("LerArquivoConfigura")]
    public string GetValor()
    {
        var valor1 = _configuration["chave1"];
        var valor2 = _configuration["chave2"];
        var sessao1 = _configuration["secao1:chave2"];

        return $"chave1 = {valor1} \n chave2 = {valor2} \n sessao1{sessao1} ";
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