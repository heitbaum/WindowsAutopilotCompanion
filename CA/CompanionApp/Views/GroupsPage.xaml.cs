using CompanionApp.Model;
using CompanionApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        GroupsViewModel viewModel;
        string ztdid = string.Empty;
        public TaskCompletionSource<bool> Completed
        {
            get; set;
        }

        public GroupsPage(string ztdId)
        {
            InitializeComponent();

            BindingContext = viewModel = new GroupsViewModel();
            this.ztdid = ztdId;

            Completed = new System.Threading.Tasks.TaskCompletionSource<bool>();

        }

        async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as Group;

            if (item == null)
            {
                this.Group = null;
                return;
            }
            this.Group = item;
            await Navigation.PopModalAsync();
            this.Completed.SetResult(true);

            //item.Ztdid = this.ztdid;
            //await Navigation.PushAsync(new UserDetailsPage(new UserDetailViewModel(item)));

            //// Manually deselect item.
            //GroupsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Groups.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        public Group Group { get; set; }

        private void GroupsListSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            viewModel.SearchItemsCommand.Execute(GroupsListSearch.Text);
        }
    }
}