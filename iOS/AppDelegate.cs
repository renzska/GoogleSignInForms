using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Google.Core;
using Google.SignIn;
using System.IO;

namespace GoogleSignInForms.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var settings = UIUserNotificationSettings.GetSettingsForTypes (
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet ());
				UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);
			}

			LoadApplication(new App());

			AppDomain.CurrentDomain.UnhandledException += AppDomain_CurrentDomain_UnhandledException;

			NSError configureError;
			Context.SharedInstance.Configure(out configureError);
			if (configureError != null)
			{
				
				Console.WriteLine("Error configuring the Google context: {0}", configureError);
			}

			SignIn.SharedInstance.ClientID = "[add].apps.googleusercontent.com";
			SignIn.SharedInstance.ServerClientID = "[add].apps.googleusercontent.com";

			return base.FinishedLaunching(app, options);
		}

		void AppDomain_CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var filePath = Path.Combine(documentsPath, "error.txt");
			File.WriteAllText(filePath, e.ExceptionObject.ToString());

		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			return SignIn.SharedInstance.HandleUrl(url, sourceApplication, annotation);
		}
	}
}

