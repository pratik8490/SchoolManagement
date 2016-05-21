using SchoolManagement.Core.Context;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            LoadingInit();
        }
        ActivityIndicator LoadingIndicator;
        private void LoadingInit()
        {
            LoadingIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Color = Color.Black,
                IsVisible = false
            };
            this.Content = new StackLayout
            {
                Children = {                    
                    LoadingIndicator,                    
                },
                BackgroundColor = Color.White,
            };
        }
        protected override void OnAppearing()
        {
            LoadingIndicator.IsRunning = IsLoading;
            LoadingIndicator.IsVisible = IsLoading;

            BindToolbar();
        }
        public void BindToolbar()
        {
            List<ToolbarItem> lstToolbarItem = new List<ToolbarItem>();

            ExtendedToolbarItem homeToolbarItem = new ExtendedToolbarItem("Home", Constants.ImagePath.ToolbarHomeIcon(), ToolbarItemOrder.Primary, LogoClick);
            ExtendedToolbarItem menuToolbarItem = new ExtendedToolbarItem("Menu", Constants.ImagePath.LeftMenuIcon, ToolbarItemOrder.Primary, CategoryMenu);

            lstToolbarItem.Add(homeToolbarItem);
            lstToolbarItem.Add(menuToolbarItem);

            foreach (ToolbarItem item in lstToolbarItem)
            {
                this.ToolbarItems.Add(item);
            }
        }
        private void LogoClick()
        {
            Navigation.PushModalAsync(App.MainDashboardPage());
        }
        private void CategoryMenu()
        {
            string ViewName = (ParentView.ParentView).GetType().Name;
            if ((ParentView.ParentView).GetType().Name != "NavigationPage")//(ViewName == "SearchListMaster" || ViewName == "MarketingCategoryMaster" || ViewName == "ModelSectionPartListMaster" || ViewName == "ShipOptionSelectMaster")
                ((MasterDetailPage)(ParentView).ParentView).IsPresented = !((MasterDetailPage)(ParentView).ParentView).IsPresented;
            else
                ((MasterDetailPage)ParentView).IsPresented = !((MasterDetailPage)ParentView).IsPresented;
        }

        protected override void OnDisappearing()
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;

            this.ToolbarItems.Clear();
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
    }
}
