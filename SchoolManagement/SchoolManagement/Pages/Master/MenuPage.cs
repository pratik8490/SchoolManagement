using SchoolManagement.Cells;
using SchoolManagement.Core.Context;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Pages.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu Page"/> class.
        /// </summary>
        public MenuPage()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            Title = "Menu";
            Icon = Constants.ImagePath.SlideOutMenu;

            MenuLayout();
        }
        #endregion

        /// <summary>
        /// Menu Page Layout.
        /// </summary>
        public void MenuLayout()
        {

            //var listView = new ListView { RowHeight = 40,  };
            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.Black,
                    Text = "Hello," + SchoolManagementContext.UserName,
                }
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            //listView.ItemsSource = MenuModel.ListCategoryMenu();
            //listView.ItemTemplate = new DataTemplate(() => new TextItemCell());

            //Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as SchoolManagement.Core.Models.MenuItem);

            Menu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                SchoolManagement.Core.Models.MenuItem objMenu = (SchoolManagement.Core.Models.MenuItem)e.SelectedItem;

                ((ListView)sender).SelectedItem = null;

                //Page displayPage = (Page)Activator.CreateInstance(objMenu.TargetType);

                //Navigation.PushAsync(displayPage);
                if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Teacher))
                {
                    if (objMenu.itemNumber == 1)
                    {
                        Navigation.PushAsync(App.FillUpAttendance());
                    }
                    else if (objMenu.itemNumber == 2)
                    {
                        Navigation.PushAsync(App.HomeWorkPage());
                    }
                    else if (objMenu.itemNumber == 3)
                    {
                        Navigation.PushAsync(App.StudentBehaviourNotice());
                    }
                    else if (objMenu.itemNumber == 4)
                    {
                        Navigation.PushAsync(App.LogbookFillup());
                    }
                    else if (objMenu.itemNumber == 5)
                    {
                        Navigation.PushAsync(App.ApplyLeave());
                    }
                    else if (objMenu.itemNumber == 6)
                    {
                        Navigation.PushAsync(App.TodaysSchedule());
                    }
                    else if (objMenu.itemNumber == 7)
                    {
                        Navigation.PushAsync(App.EnterStudentMark());
                    }
                    else if (objMenu.itemNumber == 8)
                    {
                        Navigation.PushAsync(App.UploadSamplePaper());
                    }
                    else if (objMenu.itemNumber == 9)
                    {
                        Navigation.PushAsync(App.ViewActivityNotice());
                    }
                    else if (objMenu.itemNumber == 10)
                    {
                        SchoolManagementContext.Clear();
                        Navigation.PushModalAsync(App.LoginPage());
                    }
                }

                else if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Parents))
                {
                    if (objMenu.itemNumber == 1)
                    {
                        Navigation.PushAsync(App.ViewAttendance());
                    }
                    else if (objMenu.itemNumber == 2)
                    {
                        Navigation.PushAsync(App.ViewAttendanceSummary());
                    }
                    else if (objMenu.itemNumber == 3)
                    {
                        Navigation.PushAsync(App.ExamTimetable());
                    }
                    else if (objMenu.itemNumber == 4)
                    {
                        Navigation.PushAsync(App.ViewResult());
                    }
                    else if (objMenu.itemNumber == 5)
                    {
                        Navigation.PushAsync(App.ViewCompain());
                    }
                    else if (objMenu.itemNumber == 6)
                    {
                        Navigation.PushAsync(App.HomeWork());
                    }
                    else if (objMenu.itemNumber == 7)
                    {
                        Navigation.PushAsync(App.Notification());
                    }
                    else if (objMenu.itemNumber == 8)
                    {
                        Navigation.PushAsync(App.CompainBox());
                    }
                    else if (objMenu.itemNumber == 9)
                    {
                        SchoolManagementContext.Clear();
                        Navigation.PushModalAsync(App.LoginPage());
                    }
                }

            };
            //this.Content.BackgroundColor = Color.White;
            //Content = layout;
            this.Content = new StackLayout
            {
                BackgroundColor = Color.White,
                Children = { layout }
            };
        }
    }
}
