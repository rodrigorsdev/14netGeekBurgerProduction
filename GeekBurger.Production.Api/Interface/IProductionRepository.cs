using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Production.Interface
{
    /// <summary>
    /// Production repository interface
    /// </summary>
    public interface IProductionRepository
    {
        /// <summary>
        /// Get all production
        /// </summary>
        /// <returns></returns>
        Task<ICollection<Contract.Production>> List();

        /// <summary>
        /// Add a production
        /// </summary>
        /// <param name="model">Production model</param>
        Task Add(Contract.Production model);

        /// <summary>
        /// Update an existing production
        /// </summary>
        /// <param name="model">Production model</param>
        Task Update(Contract.Production model);
    }
}
