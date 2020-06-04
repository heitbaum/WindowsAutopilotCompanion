using CompanionApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class GroupsViewModel : BaseViewModel
    {
        public ObservableCollection<Group> Groups { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }
        public Command AssignUserCommand { get; set; }

        public GroupsViewModel()
        {
            Title = " ";
            Groups = new ObservableCollection<Group>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadGroupsCommand());
            SearchItemsCommand = new Command(async (object arg) => await ExecuteSearchGroupsCommand(arg.ToString()));
        }

        public void SearchGroups(String Groupname)
        {
            
            SearchItemsCommand.Execute(null);
        }

       async Task ExecuteLoadGroupsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Groups.Clear();
                IEnumerable<Group> groups = await DataStore.ListAllGroupsAsync();
                foreach (var item in groups)
                {
                    Groups.Add(item);
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

        async Task ExecuteSearchGroupsCommand(string Groupname)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Groups.Clear();
                IEnumerable<Group> groups = await DataStore.SearchGroupAsync(Groupname);
                foreach (var item in groups)
                {
                    Groups.Add(item);
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
