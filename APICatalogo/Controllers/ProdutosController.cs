using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
        {
            var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
            {
                return NotFound();
            }

            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
            {
                return NotFound();
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

            return Ok(produtoDto);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto",
                new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoAtualizadoDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();

            var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produtoDeletado);
        }
    }
}
