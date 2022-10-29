using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webshop.Entities;
using webshop.Models;

namespace webshop.Repositories
{
    public interface IProductsRepository
    {
        ProductModel GetById(Guid id);
        IList<ProductModel> GetRange(int startPage = 0, int count = 12);
        void Create(ProductModel product);
        void Update(ProductModel product);
        void Delete(ProductModel product);
        void DeleteById(Guid id);
    }

    public class ProductsRepository : IProductsRepository
    {
        private ApplicationDbContext dbContext { get; set; }
        private IMapper mapper { get; set; }

        //public ProductsRepository(ApplicationDbContext _dbContext, IMapper _mapper)
        public ProductsRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
            //mapper = _mapper;
        }

        public ProductModel GetById(Guid id)
        {
            ProductEntity product = dbContext.Products.FirstOrDefault(e => e.Id == id);
            return Mapper.Map<ProductModel>(product);
        }

        public IList<ProductModel> GetRange(int startPage = 0, int count = 12)
        {
            var products = dbContext.Products.ToList();
            if(products.Count() < count)
            {
                count = products.Count();
            }

            
            return Mapper.Map<IList<ProductModel>>(products.GetRange(startPage * count, count));
        }

        public void Create(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<ProductEntity, ProductModel>();
        }
    }
}