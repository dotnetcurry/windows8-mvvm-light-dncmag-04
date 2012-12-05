/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="using:ProjectForTemplates.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmLight.Design;
using MvvmLight.Model;

namespace MvvmLight.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IContactService, DesignContactService>();
            }
            else
            {
                SimpleIoc.Default.Register<IContactService, ContactService>();
            }
            SimpleIoc.Default.Register<ICreateContacts, DummyContactCreator>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// Main view model
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current
                    .GetInstance<MainViewModel>();
            }
        }
    }
}