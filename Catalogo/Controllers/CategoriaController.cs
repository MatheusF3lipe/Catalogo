using Catalogo.Context;
using Catalogo.DTO;
using Catalogo.DTO.Mappings;
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
    public ActionResult<IEnumerable<CategoriaDTO>> ListarCategorias()
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS");
      var categorias = _uof.CategoryRepository.GetAll();
        var categoriaDTOs = categorias.ToCategoriaDtos();
        return Ok(categoriaDTOs);
    }
    [HttpGet("{id:int}", Name = "ListarCategoriaPorId")]
    public ActionResult<CategoriaDTO> ListarCategoriaPorId(int id)
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS POR ID");
        Categoria categoria = _uof.CategoryRepository.Get(c => c.CategoriaId == id);
        if (categoria is null)
        {
            throw new ArgumentNullException();
        }

        var categoriaDto = categoria.ToCategoriaDto() ;
        return Ok(categoriaDto);
    }
    [HttpPost]
    public ActionResult<CategoriaDTO> CriarCategoria(CategoriaDTO categoriaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = categoriaDto.ToCategoria();
        
        var CategoriaCriada = _uof.CategoryRepository.Create(categoria);
        _uof.Commit();
        var catDto = CategoriaCriada.ToCategoriaDto();

        return new CreatedAtRouteResult("ListarCategoriaPorId",new { id = catDto.CategoriaId }, catDto);
    }
    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> AtualizarProduto(int id,CategoriaDTO categoriaDto)
    {

        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest(ModelState);

        }
        var categoria = categoriaDto.ToCategoria();

        var categoriaAtualizada = _uof.CategoryRepository.Update(categoria);
        _uof.Commit();

        var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDto();

        return Ok(categoriaAtualizadaDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Deletar(int id)
    {
        Categoria categoriaExcluida = _uof.CategoryRepository.Get(c => c.CategoriaId == id);
        if (categoriaExcluida is null)
        {
            _logger.LogWarning($"Categoria do id = {id} não foi localizada");
            return NotFound();
        }

        _uof.CategoryRepository.Delete(categoriaExcluida);
        _uof.Commit();

        var categoriaDto = categoriaExcluida.ToCategoriaDto();
        return Ok(categoriaDto);
    }
}