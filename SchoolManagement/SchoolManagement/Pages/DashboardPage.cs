using Acr.UserDialogs;
using SchoolManagement.Core.Context;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using SchoolManagement.Pages.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MenuItem = SchoolManagement.Core.Models.MenuItem;

namespace SchoolManagement.Pages
{
    public class DashboardPage : BasePage
    {
        private List<MenuItem> _MenuItem { get; set; }
        private int Index = 1;
        public DashboardPage()
        {
            try
            {
                IsLoading = true;
                Device.BeginInvokeOnMainThread(async () =>
                  {
                      _MenuItem = await SchoolManagement.Core.Models.MenuItem.GetMenu(Convert.ToInt32(SchoolManagementContext.TokenResponseModel.UserType));

                      foreach (MenuItem item in _MenuItem)
                      {
                          item.ImageUrl = item.ImageUrl + "L.png";
                          item.itemNumber = Index;
                          Index++;
                      }

                      DashboardLayout();
                  });
            }
            catch (Exception ex)
            {

            }
        }

        #region Override Method
        protected override bool OnBackButtonPressed()
        {
            bool isBack = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //isBack = await DisplayAlert(string.Empty, "Do you want to logout?", Messages.Yes, Messages.No);

                isBack = await UserDialogs.Instance.ConfirmAsync(null, "Do you want to logout?", Messages.Yes, Messages.No);

                if (isBack)
                {
                    SchoolManagementContext.Clear();
                    Navigation.PushModalAsync(App.LoginPage());
                }
            });

            base.OnBackButtonPressed();
            return true;

        }
        #endregion

        #region Dashboard Layout
        public void DashboardLayout()
        {
            try
            {
                Grid grid = new Grid { BackgroundColor = Color.Gray, ColumnSpacing = 2, RowSpacing = 2 };

                int noOfCol = 3;

                int totaLoop = (int)Math.Ceiling((double)_MenuItem.Count / noOfCol);
                for (int i = 0; i < totaLoop; i++)
                {
                    string d1 = string.Empty;
                    for (int j = 0; j < noOfCol; j++)
                    {
                        int index = (i * noOfCol) + j;
                        if (_MenuItem.Count <= index)
                        {
                            //need to add empty
                            var image = new Image
                            {
                                VerticalOptions = LayoutOptions.Center,
                            };

                            var cell = new StackLayout
                            {
                                Children = {
                                image,
                            },
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.White
                            };

                            grid.Children.Add(cell, j, i);
                        }
                        else
                        {
                            // d1 += _MenuItem[index].MobileName + ",";

                            var label = new Label
                            {
                                Text = _MenuItem[index].MobileName,
                                TextColor = Color.Black,
                                XAlign = TextAlignment.Center,
                                YAlign = TextAlignment.End
                            };

                            var labelLayout = new StackLayout
                            {
                                Children = { label },
                                VerticalOptions = LayoutOptions.End,
                                //HorizontalOptions = LayoutOptions.CenterAndExpand
                            };

                            var image = new Image
                            {
                                Source = _MenuItem[index].ImageUrl,
                                VerticalOptions = LayoutOptions.Center,
                            };

                            var imageLayout = new StackLayout
                            {
                                Children = { image },
                                Padding = new Thickness(0, 20, 0, 20)
                            };

                            var cell = new StackLayout
                            {
                                Children = {
                                        imageLayout,
                                      labelLayout
                                    },
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.White
                            };

                            var menuClick = new TapGestureRecognizer();
                            menuClick.NumberOfTapsRequired = 1; // single-tap
                            menuClick.Tapped += (s, e) =>
                            {
                                int pageNumber = _MenuItem.Where(x => x.MobileName == _MenuItem[index].MobileName).FirstOrDefault().itemNumber;

                                if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Teacher))
                                {
                                    if (pageNumber == 1)
                                    {
                                        Navigation.PushAsync(App.FillUpAttendance());
                                    }
                                    else if (pageNumber == 2)
                                    {
                                        Navigation.PushAsync(App.HomeWorkPage());
                                    }
                                    else if (pageNumber == 3)
                                    {
                                        Navigation.PushAsync(App.StudentBehaviourNotice());
                                    }
                                    else if (pageNumber == 4)
                                    {
                                        Navigation.PushAsync(App.LogbookFillup());
                                    }
                                    else if (pageNumber == 5)
                                    {
                                        Navigation.PushAsync(App.ApplyLeave());
                                    }
                                    else if (pageNumber == 6)
                                    {
                                        Navigation.PushAsync(App.TodaysSchedule());
                                    }
                                    else if (pageNumber == 7)
                                    {
                                        Navigation.PushAsync(App.EnterStudentMark());
                                    }
                                    else if (pageNumber == 8)
                                    {
                                        Navigation.PushAsync(App.UploadSamplePaper());
                                    }
                                    else if (pageNumber == 9)
                                    {
                                        Navigation.PushAsync(App.ViewActivityNotice());
                                    }
                                    else if (pageNumber == 10)
                                    {
                                        SchoolManagementContext.Clear();
                                        Navigation.PushModalAsync(App.LoginPage());
                                    }
                                }

                                else if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Parents))
                                {
                                    if (pageNumber == 1)
                                    {
                                        Navigation.PushAsync(App.ViewAttendance());
                                    }
                                    else if (pageNumber == 2)
                                    {
                                        Navigation.PushAsync(App.ViewAttendanceSummary());
                                    }
                                    else if (pageNumber == 3)
                                    {
                                        Navigation.PushAsync(App.ExamTimetable());
                                    }
                                    else if (pageNumber == 4)
                                    {
                                        Navigation.PushAsync(App.ViewResult());
                                    }
                                    else if (pageNumber == 5)
                                    {
                                        Navigation.PushAsync(App.ViewCompain());
                                    }
                                    else if (pageNumber == 6)
                                    {
                                        Navigation.PushAsync(App.HomeWork());
                                    }
                                    else if (pageNumber == 7)
                                    {
                                        Navigation.PushAsync(App.Notification());
                                    }
                                    else if (pageNumber == 8)
                                    {
                                        Navigation.PushAsync(App.CompainBox());
                                    }
                                    else if (pageNumber == 9)
                                    {
                                        SchoolManagementContext.Clear();
                                        Navigation.PushModalAsync(App.LoginPage());
                                    }
                                }

                            };
                            cell.GestureRecognizers.Add(menuClick);
                            labelLayout.GestureRecognizers.Add(menuClick);
                            image.GestureRecognizers.Add(menuClick);

                            grid.Children.Add(cell, j, i);
                        }
                    }
                }

                StackLayout dashboardLayout = new StackLayout
                {
                    Children = { grid },
                    Padding = new Thickness(0, 10, 0, 10),
                    BackgroundColor = LayoutHelper.PageBackgroundColor,
                };

                dashboardLayout.Padding = LayoutHelper.IOSPadding(0, 20, 0, 0);

                // Build the page.
                ScrollView scrollDashboardLayout = new ScrollView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Content = dashboardLayout
                };
                Content = scrollDashboardLayout;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
