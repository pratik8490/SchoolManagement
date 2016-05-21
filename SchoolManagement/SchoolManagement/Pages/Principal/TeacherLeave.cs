using Acr.UserDialogs;
using SchoolManagement.Helper.Extension;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using SchoolManagement.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Principal
{
    public class TeacherLeave : Carousel
    {
        private LoadingIndicator _Loader;
        private Label _NotAvailData;
        private List<TeacherModel> _TeacherList = new List<TeacherModel>();
        private List<TeacherLeaveModel> _TeacherLeaveList = new List<TeacherLeaveModel>();
        private int _SelectedTeacherID = 0, _StartDateCounter = 0, _EndDateCounter = 0;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherLeave"/> class.
        /// </summary>
        public TeacherLeave()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _TeacherList = await TeacherModel.GetTeacher();
                    TeacherLeaveLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Teacher Leave
        /// </summary>
        public void TeacherLeaveLayout()
        {
            try
            {
                Children.Clear();
                TitleBar lblPageName = new TitleBar("Teacher Leave");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                Image imgTeacherDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblTeacher = new Label { TextColor = Color.Black, Text = "Teacher" };
                Picker pcrTeacher = new Picker { IsVisible = false, Title = "Teacher" };

                foreach (TeacherModel item in _TeacherList)
                {
                    pcrTeacher.Items.Add(item.Name);
                }

                StackLayout slTeacherDisplay = new StackLayout { Children = { lblTeacher, pcrTeacher, imgTeacherDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmTeacher = new Frame
                {
                    Content = slTeacherDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var teacherTap = new TapGestureRecognizer();

                teacherTap.NumberOfTapsRequired = 1; // single-tap
                teacherTap.Tapped += (s, e) =>
                {
                    pcrTeacher.Focus();
                };
                frmTeacher.GestureRecognizers.Add(teacherTap);
                slTeacherDisplay.GestureRecognizers.Add(teacherTap);

                StackLayout slTeacherFrameLayout = new StackLayout
                {
                    Children = { frmTeacher }
                };

                StackLayout slTeacherLayout = new StackLayout
                {
                    Children = { slTeacherFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                _Loader = new LoadingIndicator();

                _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

                //Frame layout for start date
                Image imgStartDateDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblCurrentDate = new Label { TextColor = Color.Black };
                lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
                DatePicker dtStartDate = new DatePicker { IsVisible = false };

                StackLayout slStartDateDisplay = new StackLayout { Children = { lblCurrentDate, dtStartDate, imgStartDateDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmStartDate = new Frame
                {
                    Content = slStartDateDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var currentDateTap = new TapGestureRecognizer();

                currentDateTap.NumberOfTapsRequired = 1; // single-tap
                currentDateTap.Tapped += (s, e) =>
                {
                    dtStartDate.Focus();
                };
                frmStartDate.GestureRecognizers.Add(currentDateTap);
                slStartDateDisplay.GestureRecognizers.Add(currentDateTap);

                StackLayout slStartDateFrmaeLayout = new StackLayout
                {
                    Children = { frmStartDate }
                };

                StackLayout slStartDateLayout = new StackLayout
                {
                    Children = { slStartDateFrmaeLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };



                Image imgEndDateDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblEndtDate = new Label { TextColor = Color.Black };
                lblEndtDate.Text = DateTime.Now.AddMonths(1).ToString("dd-MM-yy");
                DatePicker dtEndDate = new DatePicker { IsVisible = false };

                StackLayout slEndDateDisplay = new StackLayout { Children = { lblEndtDate, dtEndDate, imgEndDateDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmEndDate = new Frame
                {
                    Content = slEndDateDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var endDateTap = new TapGestureRecognizer();

                endDateTap.NumberOfTapsRequired = 1; // single-tap
                endDateTap.Tapped += (s, e) =>
                {
                    dtEndDate.Focus();
                };
                frmEndDate.GestureRecognizers.Add(endDateTap);
                slEndDateDisplay.GestureRecognizers.Add(endDateTap);

                StackLayout slEndDateFrmaeLayout = new StackLayout
                {
                    Children = { frmEndDate }
                };

                StackLayout slEndDateLayout = new StackLayout
                {
                    Children = { slEndDateFrmaeLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                StackLayout slStartDateEndDate = new StackLayout
                {
                    Children = { slStartDateLayout, slEndDateLayout },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = false
                };

                pcrTeacher.SelectedIndexChanged += async (sender, e) =>
                {
                    //using (UserDialogs.Instance.Loading())
                    //{
                    string teacherName = lblTeacher.Text = pcrTeacher.Items[pcrTeacher.SelectedIndex];

                    _SelectedTeacherID = _TeacherList.FirstOrDefault(x => x.Name == teacherName).ID;

                    slStartDateEndDate.IsVisible = true;

                    _StartDateCounter = Convert.ToDateTime(lblCurrentDate.Text).ConvetDatetoDateCounter();

                    _EndDateCounter = Convert.ToDateTime(lblEndtDate.Text).ConvetDatetoDateCounter();

                    GetData(_SelectedTeacherID, _StartDateCounter, _EndDateCounter);
                    //Get time table list
                    //_TimeTableList = await TimeTableModel.ShowTimeTable(_SelectedTeacherID);

                    //if (_TimeTableList != null && _TimeTableList.Count > 0)
                    //{
                    //    grid.IsVisible = true;
                    //    spDisplayHeader.IsVisible = true;
                    //    Items = new ObservableCollection<TimeTableModel>(_TimeTableList);
                    //    TimeTableListView.ItemsSource = Items;
                    //}
                    //else
                    //{
                    //    grid.IsVisible = false;
                    //    spDisplayHeader.IsVisible = false;
                    //    _NotAvailData.Text = "There is no data for selected standard and class.";
                    //    _NotAvailData.IsVisible = true;
                    //}
                    //_Loader.IsShowLoading = false;
                    //}
                };

                dtStartDate.DateSelected += (s, e) =>
                {
                    lblCurrentDate.Text = (dtStartDate).Date.ToString("dd-MM-yy");
                    _StartDateCounter = (dtStartDate).Date.ConvetDatetoDateCounter();

                    _EndDateCounter = Convert.ToDateTime(lblEndtDate.Text).ConvetDatetoDateCounter();

                    GetData(_SelectedTeacherID, _StartDateCounter, _EndDateCounter);
                };

                dtEndDate.DateSelected += (s, e) =>
                {
                    _StartDateCounter = Convert.ToDateTime(lblCurrentDate.Text).ConvetDatetoDateCounter();

                    lblEndtDate.Text = (dtEndDate).Date.ToString("dd-MM-yy");
                    _EndDateCounter = (dtEndDate).Date.ConvetDatetoDateCounter();

                    GetData(_SelectedTeacherID, _StartDateCounter, _EndDateCounter);
                };

                ContentPage searchContent = new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children = { slTitle, spTitle.LineSeperatorView, slTeacherLayout, slStartDateEndDate },
                        Orientation = StackOrientation.Vertical,
                        Padding = new Thickness(20, Device.OnPlatform(40, 20, 0), 20, 20),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = LayoutHelper.PageBackgroundColor
                    },
                };

                Children.Add(searchContent);
            }
            catch (Exception ex)
            {
            }
        }

        public async void GetData(int teacherID, int startDateCounter, int EndDateCounter)
        {
            using (UserDialogs.Instance.Loading("Loading ..."))
            {
                try
                {
                    //get teacher leave
                    _TeacherLeaveList = await TeacherLeaveModel.GetTeacherLeave(teacherID, 1000, 9960);

                    int index = 0;
                    //Carasul page for list of content

                    foreach (TeacherLeaveModel item in _TeacherLeaveList)
                    {
                        Label lblFromText = new Label
                        {
                            Text = "From: ",
                            TextColor = Color.Black
                        };

                        StackLayout slFromText = new StackLayout
                        {
                            Children = { lblFromText },
                        };

                        Label lblFrom = new Label
                        {
                            TextColor = Color.Black,
                            Text = item.TeacherId.ToString()
                        };

                        StackLayout slFromValue = new StackLayout
                        {
                            Children = { lblFrom },
                        };

                        StackLayout slFrom = new StackLayout
                        {
                            Children = { slFromText, slFromValue },
                            Padding = new Thickness(0, 0, 0, 10),
                            Orientation = StackOrientation.Horizontal
                        };

                        Label lblNoOfDayText = new Label
                        {
                            Text = "No Of Days: ",
                            TextColor = Color.Black
                        };

                        Label lblNoOfDay = new Label
                        {
                            TextColor = Color.Black,
                            Text = item.NoOfDays.ToString()
                        };

                        StackLayout slNoOfDayKey = new StackLayout
                        {
                            Children = { lblNoOfDayText },
                        };

                        StackLayout slNoOfDayValue = new StackLayout
                        {
                            Children = { lblNoOfDay },
                        };

                        StackLayout slNoOfDay = new StackLayout
                        {
                            Children = { slNoOfDayKey, slNoOfDayValue },
                            Orientation = StackOrientation.Horizontal,
                            Padding = new Thickness(0, 0, 0, 10)
                        };

                        Label lblReasonKey = new Label
                        {
                            TextColor = Color.Black,
                            Text = "Reason: "
                        };

                        Label lblReasonValue = new Label
                        {
                            TextColor = Color.Black,
                            Text = item.ReasonOfLeave
                        };

                        StackLayout slReasonKey = new StackLayout
                        {
                            Children = { lblReasonKey },
                        };

                        StackLayout slReasonValue = new StackLayout
                        {
                            Children = { lblReasonValue },
                        };

                        StackLayout slReason = new StackLayout
                        {
                            Children = { slReasonKey, slReasonValue },
                            Orientation = StackOrientation.Horizontal,
                            Padding = new Thickness(0, 0, 0, 10)
                        };

                        Label lblComment = new Label
                        {
                            Text = "Comment: ",
                            TextColor = Color.Black
                        };

                        StackLayout slLabelComment = new StackLayout
                        {
                            Children = { lblComment },
                            HorizontalOptions = LayoutOptions.Start
                        };

                        ExtendedEntry txtComment = new ExtendedEntry
                        {
                            Text = item.Comment,
                            TextColor = Color.Black
                        };

                        StackLayout slTextComment = new StackLayout
                        {
                            Children = { txtComment },
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        };

                        StackLayout slComment = new StackLayout
                        {
                            Children = { slLabelComment, slTextComment },
                            Padding = new Thickness(0, 0, 0, 10),
                            Orientation = StackOrientation.Horizontal,
                        };

                        Button btnAccept = new Button();
                        btnAccept.Text = "Accept";
                        btnAccept.WidthRequest = 80;
                        btnAccept.TextColor = Color.White;
                        btnAccept.BackgroundColor = LayoutHelper.ButtonColor;

                        var cvBtnAccept = new ContentView
                        {
                            Padding = new Thickness(10, 5, 10, 10),
                            Content = btnAccept
                        };

                        Button btnReject = new Button();
                        btnReject.Text = "Reject";
                        btnReject.WidthRequest = 80;
                        btnReject.TextColor = Color.White;
                        btnReject.BackgroundColor = LayoutHelper.ButtonColor;

                        var cvBtnReject = new ContentView
                        {
                            Padding = new Thickness(10, 5, 10, 10),
                            Content = btnReject
                        };

                        var btnStack = new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.EndAndExpand,
                                    Children = {
					                    cvBtnAccept,
					                    cvBtnReject,
				                    }
                                };

                        btnAccept.Clicked += async (s, e) =>
                        {
                            using (UserDialogs.Instance.Loading("Accept ..."))
                            {

                                try
                                {
                                    //Accept method call
                                    UserDialogs.Instance.ShowSuccess(index.ToString());
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        };

                        btnReject.Clicked += async (s, e) =>
                        {
                            using (UserDialogs.Instance.Loading("Reject ..."))
                            {

                                try
                                {
                                    //Reject methdo call
                                    DisplayAlert("", "2323", Messages.Ok);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        };

                        StackLayout slCarosual = new StackLayout
                        {
                            Children = { slFrom, slNoOfDay, slReason, slComment, btnStack },
                            Orientation = StackOrientation.Vertical,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = LayoutHelper.PageBackgroundColor,
                            Padding = new Thickness(20, Device.OnPlatform(40, 20, 0), 20, 20),
                        };

                        ContentPage contentCarosual = new ContentPage
                        {
                            Content = new ScrollView
                            {
                                Content = slCarosual
                            },
                        };
                        Children.Add(contentCarosual);
                        CurrentPage = Children[1];
                        index++;
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
