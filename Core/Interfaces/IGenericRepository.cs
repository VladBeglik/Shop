using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Specifications;

namespace Core.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync(ProductsWithTypesAndBrandsSpecification spec);

		Task<T> GetEntityWithSpec(ISpecifications<T> spec);
		Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec); 
	}
}