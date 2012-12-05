using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvvmLight.Model
{
    public class ContactService : IContactService
    {
        readonly IRepository<Contact> _repository = new InMemoryRepository<Contact>();

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            //introduce a delay to simulate long running process
            await Task.Delay(250);
            return _repository.Items.ToList();
        }

        public async Task AddAsync(Contact contact)
        {
            await Task.Delay(250);
            _repository.Add(contact);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Delay(250);
            _repository.Delete(id);
        }

        public async Task UpdateAsync(Contact contact)
        {
            await Task.Delay(250);
            _repository.Update(contact);
        }
    }
}