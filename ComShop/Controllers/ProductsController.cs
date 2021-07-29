using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComShop.Dtos;
using Core.Entities;
using Core.Entities.Specifications;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComShop.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IGenericRepository<ProductBrand> _productsBrandRepo;
		private readonly IGenericRepository<ProductType> _productsTypeRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo,
						IGenericRepository<ProductBrand> productsBrandRepo,
						IGenericRepository<ProductType> productsTypeRepo, IMapper mapper)
		{
			_productsRepo = productsRepo;
			_productsBrandRepo = productsBrandRepo;	
			_productsTypeRepo = productsTypeRepo;
			_mapper = mapper;
		}
		
		[HttpGet]
		public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductsWithTypesAndBrandsSpecification();
			var products = await _productsRepo.ListAsync(spec);
			return Ok(_mapper
				.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductsWithTypesAndBrandsSpecification(id);

			var product = await _productsRepo.GetEntityWithSpec(spec);

			return _mapper.Map<Product, ProductToReturnDto>(product);
		}

		[HttpGet("brands")]
		public async Task<ActionResult<List<ProductBrand>>> GetProductBrand()
		{
			var spec = new ProductsWithTypesAndBrandsSpecification();
			return Ok(await _productsBrandRepo.GetAllAsync(spec));
		}

		[HttpGet("types")]
		public async Task<ActionResult<List<ProductType>>> GetTypesBrand()
		{
			var spec = new ProductsWithTypesAndBrandsSpecification();
			return Ok(await _productsTypeRepo.GetAllAsync(spec));
		}
	}
}
