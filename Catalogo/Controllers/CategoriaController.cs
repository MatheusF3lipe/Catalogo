using Catalogo.Context;
using Catalogo.Filters;
using Catalogo.Interface;
using Catalogo.Models;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CategoriaController> _logger;
    private readonly IUnitOfWork _uof;
    public CategoriaController(IConfiguration configuration, ILogger<CategoriaController> logger, IUnitOfWork uof)
    {
        _configuration = configuration;
        _logger = logger;
        _uof = uof;
    }
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Categoria>> ListarCategorias()
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS");
        return Ok(_uof.CategoryRepository.GetAll());
    }
    [HttpGet("{id:int}", Name = "ListarCategoriaPorId")]
    public async Task<ActionResult<Categoria>> ListarCategoriaPorId(int id)
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS POR ID");
        Categoria categoria = _uof.CategoryRepository.Get(c => c.CategoriaId == id);
        if (categoria is null)
        {
            throw new ArgumentNullException();
        }

        return Ok(categoria);
    }
    [HttpPost]
    public ActionResult CriarCategoria([FromBody] Categoria categoria)
    {
        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);
        }
        _uof.CategoryRepository.Create(categoria);
        _uof.Commit();
        return new CreatedAtRouteResult("ListarCategoriaPorId", new { id = categoria.CategoriaId }, categoria);
    }
    [HttpPut("{id:int}")]
    public ActionResult AtualizarProduto(int id, [FromBody] Categoria categoria)
    {

        if (id != categoria.CategoriaId)
        {
            return BadRequest(ModelState);

        }
        _uof.CategoryRepository.Update(categoria);
        _uof.Commit();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Deletar(int id)
    {
        Categoria categoriaExcluida = _uof.CategoryRepository.Get(c => c.CategoriaId == id);
        if (categoriaExcluida is null)
        {
            _logger.LogWarning($"Categoria do id = {id} não foi localizada");
            return NotFound();
        }

        _uof.CategoryRepository.Delete(categoriaExcluida);
        _uof.Commit();
        return Ok(categoriaExcluida);
    }
}