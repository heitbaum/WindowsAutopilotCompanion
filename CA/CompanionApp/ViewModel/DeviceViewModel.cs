using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class DeviceViewModel : BaseViewModel
    {
        public DeviceViewModel()
        {
            Title = "Device";
        }

        public Model.Device Device
        {
            get;
            set;
        }
        private bool _entryVisible = false;

        public bool EntryVisible
        {
            get
            {
                return _entryVisible;
            }
            set
            {
                _entryVisible = value;
                OnPropertyChanged("EntryVisible");
            }
        }

        public string PageTitle { get { if (String.IsNullOrWhiteSpace(Device.DeviceName)) { return Device.SerialNumber; } else { return String.Format("{0} ({1})", Device.DeviceName,Device.SerialNumber); }; } }
    }
}
