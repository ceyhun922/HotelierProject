using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;
using Service.Abstract;

namespace Service.Concrete
{
    public class ContactManager : IGenericService<Contact>
    {
         private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public async Task<List<Contact>> GetALLServiceAsync()
        {
            return await _contactDal.GetALLAsync();
        }

        public async Task<List<Contact>> GetAllServiceFilterAsync(Expression<Func<Contact, bool>> filter)
        {
            return await _contactDal.GetAllFilterAsync(filter);
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _contactDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Contact t)
        {
            await _contactDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Contact t)
        {
            await _contactDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Contact t)
        {
            await _contactDal.UpdateAsync(t);
        }
    }
}