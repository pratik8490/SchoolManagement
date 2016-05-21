using SchoolManagement.Helper.Extension;
using SchoolManagement.Cells;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Teacher
{
    public class HomeWork : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy");
        private List<LectureModel> _LectureList = new List<LectureModel>();
        private int _SelectedLectureID = 0;
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeWork"/> class.
        /// </summary>
        public HomeWork()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _LectureList = await LectureModel.GetLecture();
                    HoweWorkLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Howe Work
        /// </summary>
        public void HoweWorkLayout()
        {
            TitleBar lblPageName = new TitleBar("Home Work");
            StackLayout slTitle = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 5, 0, 0),
                BackgroundColor = Color.White,
                Children = { lblPageName }
            };
            Seperator spTitle = new Seperator();

            Image imgStartDateDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblCurrentDate = new Label { TextColor = Color.Black, Text = DateTime.Now.ToString("dd-MM-yy") };
            DatePicker dtTimePicker = new DatePicker { IsVisible = false };

            StackLayout slSelectMonthDisplay = new StackLayout { Children = { lblCurrentDate, dtTimePicker, imgStartDateDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            Frame frmSelectMonth = new Frame
            {
                Content = slSelectMonthDisplay,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OutlineColor = Color.Black,
                Padding = new Thickness(10)
            };

            var dateTimePickerTap = new TapGestureRecognizer();

            dateTimePickerTap.NumberOfTapsRequired = 1; // single-tap
            dateTimePickerTap.Tapped += (s, e) =>
            {
                dtTimePicker.Focus();
            };
            frmSelectMonth.GestureRecognizers.Add(dateTimePickerTap);
            slSelectMonthDisplay.GestureRecognizers.Add(dateTimePickerTap);

            StackLayout slStartDateFrmaeLayout = new StackLayout
            {
                Children = { frmSelectMonth }
            };

            StackLayout slStartDateLayout = new StackLayout
            {
                Children = { slStartDateFrmaeLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Image imgLectureDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblLecture = new Label { TextColor = Color.Black, Text = "Lecture" };
            Picker pcrLecture = new Picker { IsVisible = false, Title = "Lecture" };

            foreach (LectureModel item in _LectureList)
            {
                pcrLecture.Items.Add(item.LectureName);
            }

            StackLayout slLectureDisplay = new StackLayout { Children = { lblLecture, pcrLecture, imgLectureDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            Frame frmLecture = new Frame
            {
                Content = slLectureDisplay,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OutlineColor = Color.Black,
                Padding = new Thickness(10)
            };

            var lectureTap = new TapGestureRecognizer();

            lectureTap.NumberOfTapsRequired = 1; // single-tap
            lectureTap.Tapped += (s, e) =>
            {
                pcrLecture.Focus();
            };
            frmLecture.GestureRecognizers.Add(lectureTap);
            slLectureDisplay.GestureRecognizers.Add(lectureTap);

            StackLayout slLectureFrameLayout = new StackLayout
            {
                Children = { frmLecture }
            };

            StackLayout slLectureLayout = new StackLayout
            {
                Children = { slLectureFrameLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };


            //ExtendedEntry txtComment = new ExtendedEntry
            //{
            //    TextColor = Color.Black,
            //    Placeholder = "Enter Home Work"
            //};

            Label lblComment = new Label
            {
                Text = "Enter Home Work",
                TextColor = Color.Black
            };

            Editor txtComment = new Editor
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };

            StackLayout slCommentText = new StackLayout
            {
                Children = { txtComment },
                Padding = new Thickness(0, 0, 0, 10)
            };

            StackLayout slCommentDisplay = new StackLayout
            {
                Children = { lblComment },
                Padding = new Thickness(0, 0, 0, 10)
            };

            //txtComment.Focused += (sender, e) =>
            //    {
            //        if (txtComment.Text == "Enter Home Work")
            //        {
            //            txtComment.Text = string.Empty;
            //            //txtComment.TextColor = Color.Black;
            //        }
            //    };
            //Editor txtComment = new Editor { VerticalOptions = LayoutOptions.FillAndExpand };
            //txtComment.Text = "Enter Comment";
            //txtComment.TextColor = Color.Gray;

            //txtComment.Focused += (sender, e) =>
            //{
            //    txtComment.TextColor = Color.Black;
            //    txtComment.Text = string.Empty;
            //};


            StackLayout slCommentLayout = new StackLayout
            {
                Children = { slCommentDisplay, slCommentText },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStartDateLayout, slLectureLayout, slCommentLayout }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };
            _Loader = new LoadingIndicator();

            dtTimePicker.DateSelected += (s, e) =>
            {
                lblCurrentDate.Text = (dtTimePicker).Date.ToString("dd-MM-yyyy");
            };

            pcrLecture.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string lectureName = lblLecture.Text = pcrLecture.Items[pcrLecture.SelectedIndex];

                    _SelectedLectureID = _LectureList.FirstOrDefault(x => x.LectureName == lectureName).Id;
                });
            };

            //dtTimePicker.Unfocused += (sender, e) =>
            //    {
            //        if (lblCurrentDate.Text == "Date")
            //        {
            //            lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //        }
            //    };

            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;

                    HomeWorkModel homeWorkModel = new HomeWorkModel();
                    homeWorkModel.Date = Convert.ToDateTime(lblCurrentDate.Text).Date.ConvetDatetoDateCounter();
                    homeWorkModel.Comment = txtComment.Text;
                    homeWorkModel.LectureID = _SelectedLectureID;

                    bool isSaveAttendance = await HomeWorkModel.SaveHomeWork(homeWorkModel);

                    if (isSaveAttendance)
                    {
                        await DisplayAlert(string.Empty, "Save Successfully.", Messages.Ok);
                    }
                    else
                    {
                        await DisplayAlert(Messages.Error, "Some problem ocuured when saving data.", Messages.Ok);
                    }
                    _Loader.IsShowLoading = false;
                });
            };

            var cvBtnSave = new ContentView
            {
                Padding = new Thickness(10, 5, 10, 10),
                Content = btnSave
            };

            StackLayout slHomework = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_NotAvailData,_Loader, cvBtnSave},
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slHomework,
            };

        }
    }
}
