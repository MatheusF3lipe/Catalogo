using AutoMapper;
using Catalogo.DTO;
using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Catalogo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    //private readonly IRepository<Produto> _produtoRepository;
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutoController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }
    [HttpGet("filter/preco/pagination")]
    public async Task <ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFiltroPreco);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        var metadata = new
        {
            produtos.Count,
            produtos.PageSize,
            produtos.PageCount,
            produtos.TotalItemCount,
            produtos.HasNextPage,
            produtos.HasPreviousPage
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(produtosDto);

    }
    [HttpGet("GetProdutosPagination")]
    public async Task <ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutoParameters produtoParameters)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPaginationAsync(produtoParameters);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        var metadata = new
        {
            produtos.Count,
            produtos.PageSize,
            produtos.PageCount,
            produtos.TotalItemCount,
            produtos.HasNextPage,
            produtos.HasPreviousPage
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));    
        
        return Ok(produtosDto); 
    }
    [HttpGet("ListarProdutosPorCategoria")]
    public async Task <ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosCategoria(int id)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoriaAsync(id);

        if (produtos is null)
        {
            return NotFound();
        }
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }
    [HttpGet("ListarTodosOsProdutos")]
    [Authorize(Policy = "UserOnly")]
    public async Task <ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
    {
        var produtos = await _uof.ProdutoRepository.GetAllAsync();
        if (produtos is null)
        {
            return NotFound();
        }

        var produtoDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
    }
    [HttpGet("{id}", Name = "ListarPorId")]
    public async Task <ActionResult<ProdutoDTO>> ListarProdutoId(int id)
    {
        Produto produto = await _uof.ProdutoRepository.GetAsync(c => c.CategoriaId == id);
        if (produto is null)
        {
            return NotFound("Produto Não localizado...");
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }
    [HttpPost]

    public async Task <ActionResult<ProdutoDTO>> CriarProduto([FromBody] ProdutoDTO produto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var produtoMapeadoOG = _mapper.Map<Produto>(produto);
        var novoProduto = _uof.ProdutoRepository.Create(produtoMapeadoOG);
        await _uof.CommitAsync();

        var novoProdutoMapeado = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ListarPorId", new { id = novoProdutoMapeado.ProdutoId }, novoProdutoMapeado);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Atualizarproduto(int id, ProdutoDTO produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        var produtoMapeadoOG = _mapper.Map<Produto>(produto);

        var ProdutoAtualizado = _uof.ProdutoRepository.Update(produtoMapeadoOG);

        var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(ProdutoAtualizado);

        return Ok(produtoAtualizadoDTO);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDtoUpdateResponse>> Patch(int id, JsonPatchDocument<ProdutoDtoUpdateRequest> patchProdutoDto)
    {
        if (patchProdutoDto is null || id <= 0)
            return BadRequest();

        var produto = await _uof.ProdutoRepository.GetAsync(c => c.ProdutoId.Equals(id));
        if (produto is null)
            return NotFound();

        var produtoUpdateRequest = _mapper.Map<ProdutoDtoUpdateRequest>(produto);
        patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);
        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);
        _uof.ProdutoRepository.Update(produto);
        await _uof.CommitAsync();
        return Ok(_mapper.Map<ProdutoDtoUpdateResponse>(produto));

    }
    [HttpDelete("{id:int}")]
    public async Task <ActionResult<ProdutoDTO>> DeletarProduto(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(c => c.CategoriaId == id);
        if (produto is null)
        {
            return NotFound("Produto Não encontrado");
        }
        _uof.ProdutoRepository.Delete(produto);
        await _uof.CommitAsync();

        var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDeletadoDTO);

    }
}
