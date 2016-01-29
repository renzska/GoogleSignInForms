using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using CoreGraphics;
using Google.SignIn;
using GoogleSignInForms.iOS.Renderers;
using GoogleSignInForms.Views;
using UIKit;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(GoogleSignInPage), typeof(GoogleSignInPageRenderer))]

namespace GoogleSignInForms.iOS.Renderers
{
	public class GoogleSignInPageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer, ISignInUIDelegate, ISignInDelegate
	{
		public void DidSignIn(SignIn signIn, GoogleUser gUser, Foundation.NSError error)
		{
			Device.BeginInvokeOnMainThread(async () => {

				UIAlertView alert = new UIAlertView("Login", "In DidSignIn", null, "OK", null);
				alert.Show ();
					Debug.WriteLine("DidSignIn");

					if (error != null)
					{
						Debug.WriteLine("In DidSignIn: Failure Google Error: " + error.Description, "Login");
						return;
					}

					if (gUser == null)
					{
						Debug.WriteLine("In DidSignIn: Failure Google User == null", "Login");
						return;
					}

					if (gUser != null)
					{
//						//Azure Login Process:
//						try
//						{
//							var jToken = JObject.FromObject(new {
//								access_token = SignIn.SharedInstance.CurrentUser.Authentication.AccessToken,
//								authorization_code = SignIn.SharedInstance.CurrentUser.ServerAuthCode,
//								id_token = SignIn.SharedInstance.CurrentUser.Authentication.IdToken,
//							});
//							var user = await DependencyService.Get<IMobileClient>().Authenticate(MobileServiceAuthenticationProvider.Google, jToken);
//							if (user == null)
//							{
//								Debug.WriteLine("Azure Google User == null. Logging out.");
//								App.Logout();
//							}
//						}
//						catch (Exception ex)
//						{
//							Debug.WriteLine("Azure Google Signin Exception: " + ex.ToString());
//						}
					}
				});
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.GestureRecognizers.OfType<UITapGestureRecognizer>().First().CancelsTouchesInView = false; //fix mentioned by AdamKemp here for the button TouchUpInside event not firing: https://forums.xamarin.com/discussion/comment/171084/#Comment_171084
			SignIn.SharedInstance.UIDelegate = this; 
			SignIn.SharedInstance.Delegate = this;
		}
	}
}