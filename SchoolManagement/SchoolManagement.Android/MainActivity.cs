using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Acr.UserDialogs;



namespace SchoolManagement.Droid
{
    [Activity(Label = "Digital Pathsala", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                Xamarin.Forms.Forms.Init(this, bundle);
                UserDialogs.Init(() => (Activity)Forms.Context);
                this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void OnPause()
        {
            base.OnPause(); // Always call the superclass first
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}

