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
    public class ApplyLeave : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy"); //Convert.ToDateTime("07-01-2015").ToString("dd-MM-yy");
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Apply Leave Page"/> class.
        /// </summary>
        public ApplyLeave()
        {
            IsLoading = true;
            ApplyLeaveLayout();
        }
        #endregion

        #region ApplyLeaveLayout
        /// <summary>
        /// Apply LeaveLayout.
        /// </summary>
        public void ApplyLeaveLayout()
        {
            TitleBar lblPageName = new TitleBar("Apply Leave");
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
            //        lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
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

            //ExtendedEntry txtReasonOfLeave = new ExtendedEntry
            //{
            //    TextColor = Color.Black,
            //    Placeholder = "Reason of leave"
            //};

            Label lblReasonOfLeave = new Label
            {
                TextColor = Color.Black,
                Text = "Reason of leave"
            };

            StackLayout slLabelReasonOfLeave = new StackLayout
            {
                Children = { lblReasonOfLeave },
                Padding = new Thickness(0, 0, 0, 10)
            };

            Editor txtReasonOfLeave = new Editor
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
                //TextColor = Color.Gray
            };

            StackLayout slTextReasonOfLeave = new StackLayout
            {
                Children = { txtReasonOfLeave },
                Padding = new Thickness(0, 0, 0, 10)
            };

            StackLayout slReasonOfLeave = new StackLayout
            {
                Children = { slLabelReasonOfLeave, slTextReasonOfLeave },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            //txtReasonOfLeave.Focused += (sender, e) =>
            //    {
            //        if (txtReasonOfLeave.Text == "Reason of leave")
            //        {
            //            txtReasonOfLeave.Text = string.Empty;
            //            //txtReasonOfLeave.TextColor = Color.Black;
            //        }
            //    };

            ExtendedEntry txtNoOfDays = new ExtendedEntry
            {
                TextColor = Color.Black,
                Keyboard = Keyboard.Numeric,
                Placeholder = "Enter no of days"
            };

            StackLayout slNoOfDaysLayout = new StackLayout
            {
                Children = { txtNoOfDays },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStartDateLayout, slNoOfDaysLayout, slReasonOfLeave }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(txtNoOfDays.Text) && !string.IsNullOrEmpty(txtReasonOfLeave.Text))
                        {

                            btnSave.IsVisible = false;
                            _Loader.IsShowLoading = true;

                            TeacherLeaveModel teacherLeaveModel = new TeacherLeaveModel();

                            teacherLeaveModel.ReasonOfLeave = txtReasonOfLeave.Text;
                            teacherLeaveModel.Date = Convert.ToDateTime(lblCurrentDate.Text).Date.ConvetDatetoDateCounter();
                            teacherLeaveModel.NoOfDays = Convert.ToInt32(txtNoOfDays.Text);

                            bool isSaveAttendance = await TeacherLeaveModel.ApplyLeave(teacherLeaveModel);

                            if (isSaveAttendance)
                            {
                                await DisplayAlert(string.Empty, "Save Successfully.", Messages.Ok);
                            }
                            else
                            {
                                await DisplayAlert(Messages.Error, "Some problem ocuured when saving data.", Messages.Ok);
                            }
                        }
                        else
                        {
                            await DisplayAlert(Messages.Error, "Please enter all data.", Messages.Ok);
                        }
                        _Loader.IsShowLoading = false;
                        btnSave.IsVisible = true;
                    }
                    catch (Exception ex)
                    {
                        btnSave.IsVisible = true;
                        _Loader.IsShowLoading = false;
                    }
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