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
        AuthorModel GetById(Guid id);

        /// <summary>
        /// A rudimentary search of author names
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<AuthorModel> GetByName(string keyword);
        IList<AuthorModel> GetRange(int startPage = 0, int count = 12);
        Guid Create(string authorName);
        Guid Create(AuthorModel author);
        void Update(AuthorModel author);
        void Delete(AuthorModel author);
        void DeleteById(Guid id);
    }

    public class AuthorsRepository : IAuthorsRepository
    {
        private ApplicationDbContext dbContext { get; set; }

        public AuthorsRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public AuthorModel GetById(Guid id)
        {
            var entity = dbContext.Authors.FirstOrDefault(e => e.Id == id);
            if(entity == null)
            {
                return null;
            }

            return Mapper.Map<AuthorModel>(entity);
        }

        public IList<AuthorModel> GetByName(string keyword)
        {
            var entities = dbContext.Authors.Where(e => e.Name.ToLower().Contains(keyword.ToLower())).ToList();
            var authors = Mapper.Map<IList<AuthorModel>>(entities);
            return authors;
        }

        // Currently only sorting by alphabetical order. 
        public IList<AuthorModel> GetRange(int startPage = 0, int count = 12)
        {
            var items = dbContext.Authors.OrderBy(e => e.Name).Skip(startPage * count).Take(count).ToList();
            return Mapper.Map<IList<AuthorModel>>(items);
        }

        public Guid Create(string authorName)
        {
            AuthorModel author = new AuthorModel()
            {
                Id = Guid.NewGuid(),
                Name = authorName
            };

            return Create(author);
        }
        
        public Guid Create(AuthorModel author)
        {
            var matchingAuthors = GetByName(author.Name);
            foreach(var match in matchingAuthors)
            {
                if(author.Name.ToLower() == match.Name.ToLower())
                {
                    return match.Id;
                }
            }

            var entity = Mapper.Map<AuthorEntity>(author);
            dbContext.Authors.Add(entity);

            dbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(AuthorModel author)
        {
            var entity = dbContext.Authors.FirstOrDefault(e => e.Id == author.Id);
            if(entity == null)
            {
                throw new NullReferenceException();
            }

            entity.Name = author.Name;
            dbContext.SaveChanges();
        }

        public void Delete(AuthorModel author)
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
                .ForMember(model => model.Name, opts => opts.MapFrom(entity => entity.Author.Name))
                .ForMember(model => model.Id, opts => opts.MapFrom(entity => entity.AuthorId));

            config.CreateMap<AuthorModel, AuthorEntity>()
                .ForMember(entity => entity.Name, opts => opts.MapFrom(model => model.Name))
                .ForMember(entity => entity.Id, opts => opts.MapFrom(model => model.Id == Guid.Empty ? Guid.NewGuid() : model.Id));

            config.CreateMap<AuthorModel, AuthoredByEntity>()
                .ForMember(entity => entity.AuthorId, opts => opts.MapFrom(model => model.Id == Guid.Empty ? Guid.NewGuid() : model.Id));
        }
    }
}