using GeekBurger.Production.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Production.Interface
{
    public interface IProductionRepository
    {
        Task<ICollection<Contract.Production>> List();
        Task Add(Contract.Production model);
    }
}
