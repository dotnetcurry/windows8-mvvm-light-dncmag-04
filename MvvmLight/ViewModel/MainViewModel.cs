using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLight.Model;

namespace MvvmLight.ViewModel
{
    public class MainViewModel : ViewModelBase, ILoadable
    {
        private readonly IContactService _contactService;
        private readonly ICreateContacts _contactCreator;

        private Contact _selectedContact;
        private bool _isBusy;
        private List<Contact> _contacts;

        public List<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    RaisePropertyChanged("Contacts");
                }
            }
        }

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    RaisePropertyChanged("SelectedContact");
                    DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        public MainViewModel(IContactService contactService
            , ICreateContacts contactCreator)
        {
            _contactService = contactService;
            _contactCreator = contactCreator;
            CreateDesignTimeData();
            CreateCommands();
        }

        private void CreateCommands()
        {
            AddCommand = new RelayCommand(AddHandler);
            RefreshCommand = new RelayCommand(RefreshHandler);
            DeleteCommand = new RelayCommand(DeleteHandler,
                                        () => SelectedContact != null);
        }

        private async void DeleteHandler()
        {
            IsBusy = true;
            await _contactService.DeleteAsync(SelectedContact.Id);
            IsBusy = false;
            await LoadAsync();
        }

        private async void RefreshHandler()
        {
            IsBusy = true;
            await LoadAsync();
            IsBusy = false;
        }

        private async void AddHandler()
        {
            IsBusy = true;
            var newContact = await _contactCreator
                                    .CreateContact();
            await _contactService.AddAsync(newContact);
            await LoadAsync();
            IsBusy = false;
        }

        public async Task LoadAsync()
        {
            var contacts = await _contactService.GetAllAsync();
            Contacts = new List<Contact>(contacts);
        }

        [Conditional("DEBUG")]
        private async void CreateDesignTimeData()
        {
            if (IsInDesignMode)
            {
                await LoadAsync();
            }
        }
    }
}