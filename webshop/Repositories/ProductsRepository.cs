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
        IList<ProductModel> GetByName(string keyword);
        IList<ProductModel> GetByAuthorName(string keyword);
        IList<ProductModel> GetRange(int startPage = 0, int count = 12);
        Guid Create(ProductModel product);
        void Update(ProductModel product);
        void Delete(ProductModel product);
        void DeleteById(Guid id);
    }

    public class ProductsRepository : IProductsRepository
    {
        private ApplicationDbContext dbContext { get; set; }
        private IAuthorsRepository authorsRepository{ get; set; }

        public ProductsRepository(ApplicationDbContext _dbContext, IAuthorsRepository _authorsRepository)
        {
            dbContext = _dbContext;
            authorsRepository = _authorsRepository;
        }

        public ProductModel GetById(Guid id)
        {
            ProductEntity product = dbContext.Products.FirstOrDefault(e => e.Id == id && !e.IsDeleted);

            if(product == null)
            {
                return null;
            }

            return Mapper.Map<ProductModel>(product);
        }

        public IList<ProductModel> GetByName(string keyword)
        {
            var entities = dbContext.Products.Where(e => !e.IsDeleted && e.Name.ToLower().Contains(keyword.ToLower())).ToList();
            var products = Mapper.Map<IList<ProductModel>>(entities);
            return products;
        }

        public IList<ProductModel> GetByAuthorName(string keyword)
        {
            var entities = dbContext.Products.Where(e => !e.IsDeleted && e.Authors.Count(a => a.Author.Name.ToLower().Contains(keyword.ToLower())) > 0).ToList();
            var products = Mapper.Map<IList<ProductModel>>(entities);
            return products;
        }

        // Ordering from newest to oldest DateCreated
        public IList<ProductModel> GetRange(int startPage = 0, int count = 12)
        {
            var items = dbContext.Products.Where(e => !e.IsDeleted).OrderByDescending(e => e.DateCreated).Skip(startPage * count).Take(count).ToList();
            return Mapper.Map<IList<ProductModel>>(items);
        }

        public Guid Create(ProductModel product)
        {
            var matchingProducts = GetByName(product.Name);
            foreach (var match in matchingProducts)
            {
                if (product.Name.ToLower() == match.Name.ToLower())
                {
                    return match.Id;
                }
            }

            foreach (var author in product.Authors)
            {
                Guid authorId = authorsRepository.Create(author);
                author.Id = authorId;
            }

            var entity = Mapper.Map<ProductEntity>(product);

            foreach(var authoredBy in entity.Authors)
            {
                authoredBy.Id = Guid.NewGuid();
                authoredBy.ProductId = entity.Id;
            }

            dbContext.Products.Add(entity);

            dbContext.SaveChanges();

            return entity.Id;
        }

        public void Update(ProductModel product)
        {
            var entity = dbContext.Products.FirstOrDefault(e => e.Id == product.Id);
            if (entity == null)
            {
                throw new NullReferenceException();
            }

            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.DateModified = DateTime.Now;
            dbContext.SaveChanges();
        }

        public void Delete(ProductModel product)
        {
            DeleteById(product.Id);
        }

        // Personally I'm not a fan of removing a record completely. 
        public void DeleteById(Guid id)
        {
            var entity = dbContext.Products.FirstOrDefault(e => e.Id == id);
            entity.IsDeleted = true;
            entity.DateModified = DateTime.Now;
            
            dbContext.SaveChanges();
        }

        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<ProductEntity, ProductModel>();
            config.CreateMap<ProductModel, ProductEntity>()
                .ForMember(entity => entity.DateCreated, opts => opts.MapFrom(model => model.DateCreated.HasValue ? model.DateCreated : DateTime.Now))
                .ForMember(entity => entity.Id, opts => opts.MapFrom(model => model.Id == Guid.Empty ? Guid.NewGuid() : model.Id));

            config.CreateMap<ProductModel, AuthoredByEntity>();
        }
    }
}