using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Abstract
{
    public interface ICartDal : IRepository<Cart>
    {
        void ClearCart(string cartId);
        void DeleteFromCart(int cartId, int productId);
        Cart GetCartByUserId(string userId);
    }
}
