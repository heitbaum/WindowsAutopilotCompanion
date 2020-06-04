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
	public partial class InfoPage : ContentPage
	{
		#if __IOS__
				public string ResourcePrefix = "MyApp.iOS.";
		#elif __ANDROID__
				private string ResourcePrefix = "MyApp.Droid.";
		#else
				private string ResourcePrefix = "CompanionApp.";  //note: this part isn't working to get the image in the preview
		#endif

		public InfoPage ()
		{
			InitializeComponent ();
			try
			{
				WelcomeImage.Source = ImageSource.FromResource(ResourcePrefix + "Resources.WPNinjasLogo.png");
			} catch
			{

			}
		}
	}
}