using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _prodcutsRepo;
        private readonly IGenericRepository<ProductBrand> _prodcutBrannd;
        private readonly IGenericRepository<ProductType> _prodcutsType;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> prodcutsRepo,
        IGenericRepository<ProductBrand> prodcutBrannd,
        IGenericRepository<ProductType> prodcutsType,
        IMapper mapper
        ) 
        {
            _prodcutsRepo = prodcutsRepo;
            _prodcutBrannd = prodcutBrannd;
            _prodcutsType = prodcutsType;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts() 
        {
            var spec = new ProductsWithTypesAndBrandsSpecification (); 
            var products = await _prodcutsRepo.ListAsync(spec);
            // return products.Select(product => new ProductToReturnDto 
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // }).ToList();
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponce), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _prodcutsRepo.GetEntityWithSpec(spec);
            // return new ProductToReturnDto 
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };
            if(product == null) return NotFound(new ApiResponce(404));
            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() 
        {
            var brands = await _prodcutBrannd.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() 
        {
            var types = await _prodcutsType.ListAllAsync();
            return Ok(types);
        }
    }
}