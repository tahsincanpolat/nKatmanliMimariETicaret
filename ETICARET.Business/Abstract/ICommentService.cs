using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.Business.Abstract
{
    public interface ICommentService
    {
        Comment GetById(int id);
        void Create(Comment entity);
        void Update(Comment entity);
        void Delete(Comment entity);
    }
}
