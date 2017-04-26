using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using UIKit;

namespace MobileTest.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate
			{
				Analytics.TrackEvent("Button Clicked");
				var title = string.Format("{0} clicks!", count++);
				Button.SetTitle(title, UIControlState.Normal);

				if (count == 5)
				{
					throw new NotImplementedException();
				}
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}




		partial void BtnTestEvent_TouchUpInside(UIButton sender)
		{
			Analytics.TrackEvent("Test Button Click", new Dictionary<string, string>
			{
				["Event Name"] = "Button Click",
				["Button Name"] = btnTestEvent.TitleLabel.Text
			});

			try
			{
				var list = new List<byte[]>();
				while (true)
				{
					list.Add(new byte[1024]); // Change the size here.
					Thread.Sleep(100); // Change the wait time here.
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		bool ShouldProcess(ErrorReport report)
		{

			// Check the report in here and return true or false depending on the ErrorReport.
			Crashes.ShouldAwaitUserConfirmation = () =>
			{
			    return true; // Return true if the SDK should await user confirmation, otherwise false.
			};

			Crashes.NotifyUserConfirmation(UserConfirmation.Send);

			return true;
		}


	}
}
