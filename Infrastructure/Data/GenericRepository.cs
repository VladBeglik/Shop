using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Specifications;

namespace Infrastructure.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;
		public GenericRepository(StoreContext context)
		{
			_context = context;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			id += 20;
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> GetEntityWithSpec(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}

		public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}
		 
		public IQueryable<T> ApplySpecification(ISpecifications<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
		}

		public async Task<IReadOnlyList<T>> GetAllAsync(ProductsWithTypesAndBrandsSpecification spec)
		{
			return await _context.Set<T>().ToListAsync();
		}
	}
}