using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webshop.Entities;
using webshop.Models;

namespace webshop.Repositories
{
    public interface ICustomerRepository
    {
        CustomerModel GetById(Guid id);
        CustomerModel GetByEmail(string email);
        IList<CustomerModel> GetRange(int startPage = 0, int count = 12);
        Guid Create(CustomerModel product);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext dbContext { get; set; }
        private IAuthorsRepository authorsRepository { get; set; }

        public CustomerRepository(ApplicationDbContext _dbContext, IAuthorsRepository _authorsRepository)
        {
            dbContext = _dbContext;
            authorsRepository = _authorsRepository;
        }

        public CustomerModel GetById(Guid id)
        {
            CustomerEntity entity = dbContext.Customers.FirstOrDefault(e => e.Id == id && !e.IsDeleted);

            if (entity == null)
            {
                return null;
            }

            return Mapper.Map<CustomerModel>(entity);
        }

        public CustomerModel GetByEmail(string email)
        {
            var entities = dbContext.Customers.Where(e => !e.IsDeleted && e.EmailAddress.ToLower().Contains(email.ToLower())).FirstOrDefault();
            if(entities == null)
            {
                return null;
            }
            var customer = Mapper.Map<CustomerModel>(entities);
            return customer;
        }

        // Ordering alphabetically by last name
        public IList<CustomerModel> GetRange(int startPage = 0, int count = 12)
        {
            var items = dbContext.Customers.Where(e => !e.IsDeleted).OrderBy(e => e.LastName).Skip(startPage * count).Take(count).ToList();
            return Mapper.Map<IList<CustomerModel>>(items);
        }

        public Guid Create(CustomerModel customer)
        {
            var match = GetByEmail(customer.EmailAddress);
            if(match != null)
            {
                return match.Id;
            }

            var entity = Mapper.Map<CustomerEntity>(customer);
            dbContext.Customers.Add(entity);

            dbContext.SaveChanges();

            return entity.Id;
        }

        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<CustomerEntity, CustomerModel>();
            config.CreateMap<CustomerModel, CustomerEntity>()
                .ForMember(entity => entity.DateCreated, opts => opts.MapFrom(model => model.DateCreated.HasValue ? model.DateCreated : DateTime.Now))
                .ForMember(entity => entity.Id, opts => opts.MapFrom(model => model.Id == Guid.Empty ? Guid.NewGuid() : model.Id));

            config.CreateMap<AddressModel, AddressEntity>();
            config.CreateMap<AddressEntity, AddressModel>();
        }
    }
}