using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvvmLight.Model
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task AddAsync(Contact contact);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Contact contact);
    }
}