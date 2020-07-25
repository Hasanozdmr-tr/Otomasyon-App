using Northwind.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Abstract
{
  public interface IEntityRepository<T> where T:class , IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>filter=null);        //getall da bir özellik daha eklenmek istersen dönüş türü bool ve koşul konulabilir.
        T Get(Expression<Func<T,bool>> filter);                    // filter=null filtre verilmeyebilir demek.hiçbir şey vermezse tümünü getirecek.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
