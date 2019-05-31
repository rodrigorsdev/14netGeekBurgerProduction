using GeekBurger.Production.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Production.Interface
{
    public interface IAreaRepository
    {
        Task<ICollection<Area>> List();
        Task Add(Area model);
    }
}
