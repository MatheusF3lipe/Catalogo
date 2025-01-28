using Catalogo.Context;
using Catalogo.DTO;
using Catalogo.DTO.Mappings;
using Catalogo.Filters;
using Catalogo.Interface;
using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

    [HttpGet("filter/nome/pagination")]
    public async Task <ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasFiltradas([FromQuery] CategoriaFiltroNome categoriaFiltroNome)
    {
        var categoriasFiltrada = await _uof.CategoryRepository.GetCategoriasFiltroNome(categoriaFiltroNome);
        var categoriaDto = categoriasFiltrada.ToCategoriaDtos();

        var metadata = new
        {
            categoriasFiltrada.Count,
            categoriasFiltrada.PageSize,
            categoriasFiltrada.PageCount,
            categoriasFiltrada.TotalItemCount,
            categoriasFiltrada.HasNextPage,
            categoriasFiltrada.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(categoriaDto);

    }
    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<Categoria>>> pagination([FromQuery] CategoriaParameters categoriaParameters)
    {
        var categorias = await _uof.CategoryRepository.GetCategoriasPagination(categoriaParameters);
        var categoriasDto = categorias.ToCategoriaDtos();

        var metadata = new
        {
            categorias.Count,
            categorias.PageSize,
            categorias.PageCount,
            categorias.TotalItemCount,
            categorias.HasNextPage,
            categorias.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(categoriasDto);

    }
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [Authorize(Policy = "UserOnly")]
    public async Task <ActionResult<IEnumerable<CategoriaDTO>>> ListarCategorias()
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS");
      var categorias = await _uof.CategoryRepository.GetAllAsync();
        var categoriaDTOs = categorias.ToCategoriaDtos();
        return Ok(categoriaDTOs);
    }
    [HttpGet("{id:int}", Name = "ListarCategoriaPorId")]
    public async Task<ActionResult<CategoriaDTO>> ListarCategoriaPorId(int id)
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS POR ID");
        Categoria categoria = await _uof.CategoryRepository.GetAsync(c => c.CategoriaId == id);
        if (categoria is null)
        {
            throw new ArgumentNullException();
        }

        var categoriaDto = categoria.ToCategoriaDto() ;
        return Ok(categoriaDto);
    }
    [HttpPost]
    public async Task <ActionResult<CategoriaDTO>> CriarCategoria(CategoriaDTO categoriaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = categoriaDto.ToCategoria();
        
        var CategoriaCriada = _uof.CategoryRepository.Create(categoria);
        await _uof.CommitAsync();
        var catDto = CategoriaCriada.ToCategoriaDto();

        return new CreatedAtRouteResult("ListarCategoriaPorId",new { id = catDto.CategoriaId }, catDto);
    }
    [HttpPut("{id:int}")]
    public async Task <ActionResult<CategoriaDTO>> AtualizarProduto(int id,CategoriaDTO categoriaDto)
    {

        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest(ModelState);

        }
        var categoria = categoriaDto.ToCategoria();

        var categoriaAtualizada = _uof.CategoryRepository.Update(categoria);
        await _uof.CommitAsync();

        var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDto();

        return Ok(categoriaAtualizadaDto);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task <ActionResult<CategoriaDTO>> Deletar(int id)
    {
        Categoria categoriaExcluida = await _uof.CategoryRepository.GetAsync(c => c.CategoriaId == id);
        if (categoriaExcluida is null)
        {
            _logger.LogWarning($"Categoria do id = {id} não foi localizada");
            return NotFound();
        }

        _uof.CategoryRepository.Delete(categoriaExcluida);
        await _uof.CommitAsync();

        var categoriaDto = categoriaExcluida.ToCategoriaDto();
        return Ok(categoriaDto);
    }
}