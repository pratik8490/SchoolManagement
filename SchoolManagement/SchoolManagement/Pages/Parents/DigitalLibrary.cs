using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Parents
{
    public class DigitalLibrary : BasePage
    {
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalLibrary"/> class.
        /// </summary>
        public DigitalLibrary()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    DigitalLibraryLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Digital Library
        /// </summary>
        public void DigitalLibraryLayout()
        {
            try
            {
                TitleBar lblPageName = new TitleBar("Digital Library");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                Label lblMessage = new Label
                {
                    Text = "This page is under construction.",
                    FontSize = 14,
                    TextColor = Color.Red
                };

                this.Content = new StackLayout
                {
                    Children = { slTitle, spTitle.LineSeperatorView, lblMessage },
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
