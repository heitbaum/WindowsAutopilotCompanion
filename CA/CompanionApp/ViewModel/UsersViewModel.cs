using CompanionApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class UsersViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public Command AssignUserCommand { get; set; }

        public UsersViewModel()
        {
            Title = " ";
            Users = new ObservableCollection<User>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadUsersCommand());
            SearchItemsCommand = new Command(async (object arg) => await ExecuteSearchUsersCommand(arg.ToString()));
        }

        public void SearchUser(String Username)
        {
            
            SearchItemsCommand.Execute(null);
        }

       async Task ExecuteLoadUsersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Users.Clear();
                IEnumerable<User> users = await DataStore.ListAllUsersAsync();
                foreach (var item in users)
                {
                    Users.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteSearchUsersCommand(string Username)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Users.Clear();
                IEnumerable<User> users = await DataStore.SearchUserAsync(Username);
                foreach (var item in users)
                {
                    Users.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
