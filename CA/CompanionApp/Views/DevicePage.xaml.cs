using CompanionApp.Model;
using CompanionApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicePage : ContentPage
	{
        DeviceViewModel viewModel;


        public DevicePage(Model.Device device)
		{
            
			InitializeComponent();

            if (this.Width > this.Height)
            {
                outerStack.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                outerStack.Orientation = StackOrientation.Vertical;
            }

            viewModel = new DeviceViewModel();
            viewModel.Device = device;
        
            BindingContext = this.viewModel;

            int i = 0;
            foreach (var item in device.CategoryList)
            {
                if (item.Id == device.ManagedDeviceCategoryId)
                    this.DeviceCategory.SelectedIndex = i;
                i++;
            }

        }

        private async void SaveChanges_Clicked(object sender, EventArgs e)
        {
            string returnValue = await viewModel.DataStore.UpdateDeviceAsync(viewModel.Device);
            if (!returnValue.Equals("OK"))
            {
                await DisplayAlert("Device settings update", "Device settings update failed. " + returnValue, "OK");
            }

            await Navigation.PopAsync();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                outerStack.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                outerStack.Orientation = StackOrientation.Vertical;
            }
        }
        private async void ChooseUser_Clicked(object sender, EventArgs e)
        {
            UsersPage user = new UsersPage(viewModel.Device.ZtdId);
            await Navigation.PushModalAsync(user);
            await user.Completed.Task;
            if (user.User != null)
            {
                viewModel.Device.UserPrincipalName = user.User.UserPrincipalName;
                viewModel.Device.AddressableUserName = user.User.DisplayName;
            }
        }

        private async void AssignGroups_Clicked(object sender, EventArgs e)
        {
            GroupsPage group = new GroupsPage(viewModel.Device.ZtdId);
            await Navigation.PushModalAsync(group);
            await group.Completed.Task;
            if (group.Group != null)
            {
                List<Group> g = viewModel.Device.Groups;
                g.Add(group.Group);
                viewModel.Device.Groups = g;
            }
        }

        private void RemoveUser_Clicked(object sender, EventArgs e)
        {
            viewModel.Device.UserPrincipalName = String.Empty;
            viewModel.Device.AddressableUserName = String.Empty;
        }
        private void DeviceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceCategory newCat = (DeviceCategory)DeviceCategory.SelectedItem;
            viewModel.Device.ManagedDeviceCategoryId = newCat.Id;
        }

        private void ShowUnimportantElements_Clicked(object sender, EventArgs e)
        {
            if (viewModel.EntryVisible == true)
            {
                viewModel.EntryVisible = false;
                ShowUnimportantElements.Text = "Show hidden attributes";
            } else
            {
                viewModel.EntryVisible = true;
                ShowUnimportantElements.Text = "Hide unimportant attributes";
            }
            
        }
        
    }
}