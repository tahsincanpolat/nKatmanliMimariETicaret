using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetOrders(string userId);
        void Create(Order entity);
    }
}
