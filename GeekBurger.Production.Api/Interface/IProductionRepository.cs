using GeekBurger.Production.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Production.Interface
{
    /// <summary>
    /// Production repository interface
    /// </summary>
    public interface IProductionRepository
    {
        Task<ICollection<Contract.Production>> List();
        Task Add(Contract.Production model);
        Task Update(Contract.Production model);
    }
}
