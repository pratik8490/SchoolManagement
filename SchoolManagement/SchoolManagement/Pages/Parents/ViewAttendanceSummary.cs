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

namespace SchoolManagement.Pages.Parents
{
    public class ViewAttendanceSummary : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy"); //Convert.ToDateTime("07-01-2015").ToString("dd-MM-yy");
        private Label _NotAvailData = new Label(), _StudentName, _IsPresent;
        private LoadingIndicator _Loader;
        private List<YearMonthModel> _YearMonth = new List<YearMonthModel>();

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="View Attendance Summary Page"/> class.
        /// </summary>
        public ViewAttendanceSummary()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _YearMonth = await YearMonthModel.GetYerRange();
                    ViewAttendanceSummaryLayout();
                }
                catch (Exception ex)
                {

                }
            });
        }
        #endregion

        #region ViewAttendanceSummaryLayout
        /// <summary>
        /// View Attendance Summary Layout.
        /// </summary>
        public void ViewAttendanceSummaryLayout()
        {
            TitleBar lblPageName = new TitleBar("View Attendance Summary");
            StackLayout slTitle = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 5, 0, 0),
                BackgroundColor = Color.White,
                Children = { lblPageName }
            };

            Seperator spTitle = new Seperator();

            Image imgStartDateDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblCurrentDate = new Label { TextColor = Color.Black, Text = "Select Month" };
            Picker pcrYearRange = new Picker { IsVisible = false, Title = "Year Range" };

            foreach (YearMonthModel item in _YearMonth)
            {
                pcrYearRange.Items.Add(item.Value);
            }

            StackLayout slSelectMonthDisplay = new StackLayout { Children = { lblCurrentDate, pcrYearRange, imgStartDateDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            //Frame layout for start date
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
                pcrYearRange.Focus();
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

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStartDateLayout }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            Label lblTotalWorkingDays = new Label
            {
                TextColor = Color.Black
            };

            StackLayout slTotalWorkingDays = new StackLayout { Children = { lblTotalWorkingDays }, Padding = new Thickness(0, 0, 0, 10) };

            Label lblTotalPresentDay = new Label
            {
                TextColor = Color.Black
            };

            StackLayout slTotalPresentDay = new StackLayout { Children = { lblTotalPresentDay }, Padding = new Thickness(0, 0, 0, 10) };

            Label lblTotalAbsentDay = new Label
            {
                TextColor = Color.Black
            };

            StackLayout slTotalAbsentDay = new StackLayout { Children = { lblTotalAbsentDay }, Padding = new Thickness(0, 0, 0, 10) };

            pcrYearRange.SelectedIndexChanged += (s, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            _Loader.IsShowLoading = true;
                            lblTotalWorkingDays.Text = string.Empty;
                            lblTotalPresentDay.Text = string.Empty;
                            lblTotalAbsentDay.Text = string.Empty;

                            lblCurrentDate.Text = pcrYearRange.Items[pcrYearRange.SelectedIndex].ToString();

                            DateTime dt = Convert.ToDateTime("1-" + lblCurrentDate.Text);

                            int dateCounter = dt.ConvetDatetoDateCounter();

                            AttendanceSummaryModel attendanceSummary = await AttendanceSummaryModel.GetAttendanceSummary(dateCounter);

                            if (!string.IsNullOrEmpty(Convert.ToString(attendanceSummary.AbsentDays)) && !string.IsNullOrEmpty(Convert.ToString(attendanceSummary.PresentDays)) && !string.IsNullOrEmpty(Convert.ToString(attendanceSummary.TotalWorkingDays)))
                            {
                                _NotAvailData.IsVisible = false;

                                lblTotalWorkingDays.Text = "Total WorkingDays: " + attendanceSummary.TotalWorkingDays.ToString();
                                lblTotalPresentDay.Text = "Total PresentDays: " + attendanceSummary.PresentDays.ToString();
                                lblTotalAbsentDay.Text = "Total AbsentDays: " + attendanceSummary.AbsentDays.ToString();
                            }
                            else
                            {
                                _NotAvailData.IsVisible = true;
                                slTotalAbsentDay.IsVisible = false;
                                slTotalPresentDay.IsVisible = false;
                                slTotalWorkingDays.IsVisible = false;
                            }

                            _Loader.IsShowLoading = false;
                        }
                        catch (Exception ex)
                        {

                        }
                    });
            };

            StackLayout slViewAttendance = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader,_NotAvailData, slTotalWorkingDays,slTotalPresentDay,slTotalAbsentDay},
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
