using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;
using Google.SignIn;

//https://developer.xamarin.com/samples/monodroid/google-services%5CSigninQuickstart/
using Foundation;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using GoogleSignInForms.Views;
using GoogleSignInForms.iOS.Renderers;

[assembly: ExportRenderer(typeof(GoogleLoginView), typeof(GoogleLoginViewRenderer))]
namespace GoogleSignInForms.iOS.Renderers
{
	public class GoogleLoginViewRenderer : ViewRenderer<GoogleLoginView, SignInButton>
	{
		SignInButton signInButton;

		protected async override void OnElementChanged(ElementChangedEventArgs<GoogleLoginView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || this.Element == null)
				return;

			if (Control == null)
			{
				signInButton = new SignInButton();
				signInButton.Style = ButtonStyle.Wide;
				signInButton.ColorScheme = ButtonColorScheme.Dark;

				SetNativeControl(signInButton);
			}
		}
	}
}

