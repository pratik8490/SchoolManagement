using SchoolManagement.Helper.Extension;
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
    public class LogbookFillUp : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy"); //Convert.ToDateTime("07-01-2015").ToString("dd-MM-yy");
        private Label _NotAvailData = new Label(), _StudentName, _IsPresent;
        private List<LectureModel> _LectureList = new List<LectureModel>();
        private int _SelectedLectureID = 0;
        private LoadingIndicator _Loader;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Logbook FillUp Page"/> class.
        /// </summary>
        public LogbookFillUp()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsLoading = true;
                    _LectureList = await LectureModel.GetLecture();
                    LogbookFillUpLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        #region LogbookFillUpLayout
        /// <summary>
        /// Logbook FillUp Layout.
        /// </summary>
        public void LogbookFillUpLayout()
        {
            TitleBar lblPageName = new TitleBar("Logbook FillUp");
            StackLayout slTitle = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 5, 0, 0),
                BackgroundColor = Color.White,
                Children = { lblPageName }
            };

            Seperator spTitle = new Seperator();

            Image imgStartDateDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblCurrentDate = new Label { TextColor = Color.Black };
            lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
            DatePicker dtStartDate = new DatePicker { IsVisible = false };

            StackLayout slStartDateDisplay = new StackLayout { Children = { lblCurrentDate, dtStartDate, imgStartDateDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            //Frame layout for start date
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

            dtStartDate.DateSelected += (s, e) =>
            {
                lblCurrentDate.Text = (dtStartDate).Date.ToString("dd-MM-yyyy");
            };

            //dtStartDate.Unfocused += (sender, e) =>
            //{
            //    if (lblCurrentDate.Text == "Date")
            //    {
            //        lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //    }
            //};


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

            //ExtendedEntry txtTeachingAids = new ExtendedEntry
            //{
            //    TextColor = Color.Black,
            //    Placeholder = "Teaching Aids"
            //};

            Label lblTeachingAids = new Label
            {
                Text = "Teaching Aids",
                TextColor = Color.Black
            };

            Editor txtTeachingAids = new Editor
            {
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout slLabelTeachingAids = new StackLayout
            {
                Children = { lblTeachingAids },
                Padding = new Thickness(0, 0, 0, 10)
            };

            StackLayout slTextTeachingAids = new StackLayout
            {
                Children = { txtTeachingAids },
                Padding = new Thickness(0, 0, 0, 10)
            };

            //txtTeachingAids.Focused += (sender, e) =>
            //    {
            //        if (txtTeachingAids.Text == "Teaching Aids")
            //        {
            //            txtTeachingAids.Text = string.Empty;
            //            //txtTeachingAids.TextColor = Color.Black;
            //        }
            //    };


            StackLayout slTeachingAidsLayout = new StackLayout
            {
                Children = { slLabelTeachingAids, slTextTeachingAids },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            //ExtendedEntry txtComment = new ExtendedEntry
            //{
            //    TextColor = Color.Black,
            //    Placeholder = "Comment"
            //};

            Label lblComment = new Label
            {
                Text = "Comment",
                TextColor = Color.Black
            };

            Editor txtComment = new Editor
            {
                //TextColor = Color.Gray,
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout slLabelComment = new StackLayout
            {
                Children = { lblComment },
                Padding = new Thickness(0, 0, 0, 10)
            };

            StackLayout slTextComment = new StackLayout
           {
               Children = { txtComment },
               Padding = new Thickness(0, 0, 0, 10)
           };

            //txtComment.Focused += (sender, e) =>
            //    {
            //        if (txtComment.Text == "Comment")
            //        {
            //            txtComment.Text = string.Empty;
            //            //txtComment.TextColor = Color.Black;
            //        }
            //    };

            StackLayout slEditorLayout = new StackLayout
            {
                Children = { slLabelTeachingAids, slTextTeachingAids },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            //ExtendedEntry txtLessonPlan = new ExtendedEntry
            //{
            //    TextColor = Color.Black,
            //    Placeholder = "Lesson Plan"
            //};

            Label lblLessonPlan = new Label
            {
                Text = "Lesson Plan",
                TextColor = Color.Black
            };

            Editor txtLessonPlan = new Editor
            {
                //TextColor = Color.Gray,
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            StackLayout slLabelLessonPlan = new StackLayout
            {
                Children = { lblLessonPlan },
                Padding = new Thickness(0, 0, 0, 10)
            };

            StackLayout slTextLessonPlan = new StackLayout
            {
                Children = { txtLessonPlan },
                Padding = new Thickness(0, 0, 0, 10)
            };

            //txtLessonPlan.Focused += (sender, e) =>
            //    {
            //        if (txtLessonPlan.Text == "Lesson Plan")
            //        {
            //            txtLessonPlan.Text = string.Empty;
            //            //txtLessonPlan.TextColor = Color.Black;
            //        }
            //    };

            StackLayout slLessonPlanLayout = new StackLayout
            {
                Children = { slLabelLessonPlan, slTextLessonPlan },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
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

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStartDateLayout, slLectureLayout, slTeachingAidsLayout, slLessonPlanLayout, slEditorLayout }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            pcrLecture.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string lectureName = lblLecture.Text = pcrLecture.Items[pcrLecture.SelectedIndex];

                    _SelectedLectureID = _LectureList.FirstOrDefault(x => x.LectureName == lectureName).Id;

                    //Exam list call
                    slStartDateLayout.IsVisible = true;
                });
            };


            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;

                    TeacherLogBookModel teacherLogBook = new TeacherLogBookModel();
                    teacherLogBook.Date = Convert.ToDateTime(lblCurrentDate.Text).Date.ConvetDatetoDateCounter();
                    teacherLogBook.LessonPlan = txtLessonPlan.Text;
                    teacherLogBook.TeachingAids = txtTeachingAids.Text;
                    teacherLogBook.Comment = txtComment.Text;
                    teacherLogBook.LectureId = _SelectedLectureID; //Convert.ToInt32(txtLectureNo.Text);

                    bool isSaveAttendance = await TeacherLogBookModel.SaveLogBook(teacherLogBook);

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

            StackLayout slViewAttendance = new StackLayout
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
                Content = slViewAttendance,
            };
        }
        #endregion
    }
}