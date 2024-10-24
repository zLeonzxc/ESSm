using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System;

namespace ess_prototype.Views.Popups
{
	public partial class PopupView : Popup
	{
		public PopupView()
		{
			InitializeComponent();
		}
        private void OnOkClicked(object sender, EventArgs e)
        {
#if ANDROID || IOS || MACCATALYST || WINDOWS
			Close();
#else
            Dismiss();
#endif
        }
    }	
}