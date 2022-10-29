using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webshop.Entities;
using webshop.Models;

namespace webshop.Repositories
{
    public interface IAuthorsRepository
    {
        AuthorEntity GetById(Guid id);

        /// <summary>
        /// A rudimentary search of author names
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<AuthorEntity> GetByName(string keyword);
        IList<AuthorEntity> GetRange(int startPage = 0, int count = 12);
        void Create(AuthorEntity product);
        void Update(AuthorEntity product);
        void Delete(AuthorEntity product);
        void DeleteById(Guid id);
    }

    public class AuthorsRepository : IAuthorsRepository
    {
        private ApplicationDbContext dbContext { get; set; }

        public AuthorsRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public AuthorEntity GetById(Guid id)
        {
            return dbContext.Authors.FirstOrDefault(e => e.Id == id);
        }

        public IList<AuthorEntity> GetByName(string keyword)
        {
            return dbContext.Authors.Where(e => e.Name.ToLower().Contains(keyword.ToLower())).ToList();
        }

        public IList<AuthorEntity> GetRange(int startPage = 0, int count = 12)
        {
            return dbContext.Authors.ToList().GetRange(startPage * count, count);
        }

        public void Create(AuthorEntity product)
        {
            throw new NotImplementedException();
        }

        public void Update(AuthorEntity product)
        {
            throw new NotImplementedException();
        }

        public void Delete(AuthorEntity product)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<AuthorEntity, AuthorModel>();
            config.CreateMap<AuthoredByEntity, AuthorModel>()
                .ForMember(model => model.Name, opts => opts.MapFrom(entity => entity.Author.Name));
        }
    }
}