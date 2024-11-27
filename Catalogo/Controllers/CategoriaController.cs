using Catalogo.Context;
using Catalogo.Filters;
using Catalogo.Interface;
using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CategoriaController> _logger;
    private readonly ICategoryRepository _categoryRepository;
    public CategoriaController(IConfiguration configuration, ILogger<CategoriaController> logger, ICategoryRepository categoryRepository)
    {
        _configuration = configuration;
        _logger = logger;
        _categoryRepository = categoryRepository;
    }
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Categoria>> ListarCategorias()
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS");
        return Ok(_categoryRepository.GetCategorias());
    }
    [HttpGet("{id:int}", Name = "ListarCategoriaPorId")]
    public async Task<ActionResult<Categoria>> ListarCategoriaPorId(int id)
    {
        _logger.LogInformation("================ API/LISTAR CATEGORIAS POR ID");
        Categoria categoria = _categoryRepository.IdCategory(id);
        if (categoria is null)
        {
            throw new ArgumentNullException();                
        }

        return Ok(categoria);
    }
    [HttpGet("ListarProd")]
    public ActionResult<IEnumerable<Categoria>> listarProdutosPorCategorias()
    {
        return _categoryRepository.listProductByCategory();
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
        _categoryRepository.CreateCategory(categoria);
        return new CreatedAtRouteResult("ListarCategoriaPorId", new { id = categoria.CategoriaId }, categoria);
    }
    [HttpPut("{id:int}")]
    public ActionResult AtualizarProduto(int id, [FromBody] Categoria categoria)
    {

        if (id != categoria.CategoriaId)
        {
            return BadRequest(ModelState);

        }
      _categoryRepository.UpdateCategory(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Deletar(int id)
    {
        _categoryRepository.DeleteCategory(id);
        return Ok();
    }
}