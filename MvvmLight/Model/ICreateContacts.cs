using System.Threading.Tasks;

namespace MvvmLight.Model
{
    public interface ICreateContacts
    {
        Task<Contact> CreateContact();
    }
}