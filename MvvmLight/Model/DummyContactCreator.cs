using System.Threading.Tasks;

namespace MvvmLight.Model
{
public class DummyContactCreator : ICreateContacts
{
    public async Task<Contact> CreateContact()
    {
        var contact = new Contact()
                            {
                                Name = "Jane Doe",
                                Email = "jane.doe@someservice.com",
                                TwitterHandle = "@janestwitter23"
                            };
        return contact;
    }
}
}