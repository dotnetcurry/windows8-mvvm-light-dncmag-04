using System.Threading.Tasks;

namespace MvvmLight.ViewModel
{
    public interface ILoadable
    {
        Task LoadAsync();
    }
}