using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmLight.Model;

namespace MvvmLight.Design
{
    public class DesignContactService : IContactService
    {
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var designTimeContacts = new Contact[]
                {
                    new Contact()
                        {
                            DateOfBirth = DateTime.Now.AddYears(-30),
                            Email = "monster@cookiemonster.com",
                            Id = Guid.NewGuid(),
                            Name = "Cookie Monster",
                            TwitterHandle = "@coookiemon2345"
                        },
                    new Contact()
                        {
                            DateOfBirth = DateTime.Now.AddYears(-30),
                            Email = "monster@cookiemonster.com",
                            Id = Guid.NewGuid(),
                            Name = "Cookie Monster",
                            TwitterHandle = "@coookiemon2345"
                        }
                };
            return designTimeContacts;
        }

        public async Task AddAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Contact expense)
        {
            throw new NotImplementedException();
        }
    }
}