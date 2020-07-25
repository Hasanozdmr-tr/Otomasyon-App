using Northwind.Entities.Concrete;
using Northwind.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Abstract;
using Northwind.Business.Abstract;
using System.Data.Entity.Infrastructure;
using Northwind.Business.ValidationRules.FluentValidation;
using FluentValidation;
using Northwind.Business.Utilities;

namespace Northwind.Business.Concrete
{
    public class ProductManager:IProductService
    {
        // EfProductDal _productDal = new EfProductDal(); //Data Access katmanına erişmeye çalışılıyor şuan.
        // Normali böyle fakat Nhibernate frameworkü de kullanılarak bağlantı sağlanabilir. 
        //Buyüzden IProductDal dan çekiyoruz.

        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)  //construction tanımlıyoruz.Bu şu demek. ProductManager newlendiğinde IProductDal türünde
        {                                              //productDal nesnesi ver.Bu sayede entityframework ya da nhibernate kodu görülmez.
                                                       // ikisini de kullanabilirsin.
            _productDal = productDal;
        }

        public void Add(Product product)
        {

            ValidationTool.Validate(new ProductValidator(), product);

            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
            }
            catch                                 // (DbUpdateException e) yaparsak sadece bu hataya bağlı kalırız. Güvenlik zaafiyeti vermeyiz.
            {                                    // sadece tek bir hataya bağlı kalmamak için catch e çevirdik.

               throw new Exception("Silme İşlemi gerçekleşemedi.");   //bunu forma yazmama sebebimiz kodun tek elden kontrolü.  
            }                                //ileride mobil versiyon yapılmak istenirse onu alert olarak da gösterebilir. o dönüşümü formda yaparız.

        }

        public List<Product> GetAll()
        {
            //Business Code ları yazılır.
            return _productDal.GetAll();
        }

        public List<Product> GetProductsByCategory(int categoryId)  //listenin içindeki şey dönecek değer.
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public void Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Update(product);
        }
    }
}
