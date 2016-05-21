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
using System.Collections.ObjectModel;
using SchoolManagement.Cells;
using Acr.UserDialogs;
using SchoolManagement.Core.Context;

namespace SchoolManagement.Pages.Parents
{
    public class ViewAttendance : BasePage
    {
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader;
        private int startDateCounter = 0, endDateCounter = 0;
        private ListView StudentAttendanceListView;
        private Seperator spDisplayHeader;
        private StackLayout grid;
        private int _DateCounter = DateTime.Now.ConvetDatetoDateCounter();

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="View Attendance Page"/> class.
        /// </summary>
        public ViewAttendance()
        {
            IsLoading = true;
            ViewAttendanceLayout();
        }
        #endregion

        #region ViewAttendanceLayout
        /// <summary>
        /// View Attendance Layout.
        /// </summary>
        public void ViewAttendanceLayout()
        {
            TitleBar lblPageName = new TitleBar("View Attendance");
            StackLayout slTitle = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 5, 0, 0),
                BackgroundColor = Color.White,
                Children = { lblPageName }
            };

            Seperator spTitle = new Seperator();

            Label lblIsAbsent = new Label
            {
                TextColor = Color.Black
            };

            Image imgLeftarrow = new Image
            {
                Source = Constants.ImagePath.ArrowLeft
            };


            Image imgStartDateDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblCurrentDate = new Label { TextColor = Color.Black };
            lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");//Convert.ToDateTime("07-01-2015").ToString("dd-MM-yy");
            DatePicker dtStartDate = new DatePicker { IsVisible = false, Date = DateTime.Today };

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

            Image imgRightarrow = new Image
            {
                Source = Constants.ImagePath.ArrowRight
            };

            StackLayout slLeftArrow = new StackLayout
            {
                Children = { imgLeftarrow },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slRightArrow = new StackLayout
            {
                Children = { imgRightarrow },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { imgLeftarrow, slStartDateLayout, imgRightarrow }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            //List view
            StudentAttendanceListView = new ListView
            {
                RowHeight = 50,
                SeparatorColor = Color.Gray
            };

            StudentAttendanceListView.ItemsSource = Items;
            StudentAttendanceListView.ItemTemplate = new DataTemplate(() => new ViewAttendanceCell());

            //Grid Header Layout
            Label lblStudent = new Label
            {
                Text = "Student",
                TextColor = Color.Black
            };

            StackLayout slStudentName = new StackLayout
            {
                Children = { lblStudent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblIsPresent = new Label
            {
                Text = "A/P",
                TextColor = Color.Black
            };

            StackLayout slIsPresent = new StackLayout
            {
                Children = { lblIsPresent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            grid = new StackLayout
            {
                Children = { slStudentName, slIsPresent },
                Orientation = StackOrientation.Horizontal,
                IsVisible = false
            };

            spDisplayHeader = new Seperator { IsVisible = false };

            dtStartDate.DateSelected += (s, e) =>
            {
                lblCurrentDate.Text = (dtStartDate).Date.ToString("dd-MM-yy");

                _DateCounter = (dtStartDate).Date.ConvetDatetoDateCounter();

                startDateCounter = _DateCounter;
                endDateCounter = _DateCounter + 30;

                LoadData(startDateCounter, endDateCounter);
            };

            //Left button click

            var leftArrowTap = new TapGestureRecognizer();

            leftArrowTap.NumberOfTapsRequired = 1; // single-tap
            leftArrowTap.Tapped += (s, e) =>
            {
                _DateCounter = _DateCounter - 1;
                lblCurrentDate.Text = _DateCounter.GetDateFromDateCount().ToString("dd-MM-yy");
                dtStartDate.Date = _DateCounter.GetDateFromDateCount();

                startDateCounter = _DateCounter;
                endDateCounter = _DateCounter + 30;

                LoadData(startDateCounter, endDateCounter);
            };
            imgLeftarrow.GestureRecognizers.Add(leftArrowTap);

            //Right button click

            var rightArrowTap = new TapGestureRecognizer();

            rightArrowTap.NumberOfTapsRequired = 1; // single-tap
            rightArrowTap.Tapped += (s, e) =>
            {
                _DateCounter = _DateCounter + 1;
                lblCurrentDate.Text = _DateCounter.GetDateFromDateCount().ToString("dd-MM-yy");
                dtStartDate.Date = _DateCounter.GetDateFromDateCount();

                startDateCounter = _DateCounter;
                endDateCounter = _DateCounter + 30;

                LoadData(startDateCounter, endDateCounter);
            };
            imgRightarrow.GestureRecognizers.Add(rightArrowTap);

            StackLayout slViewAttendance = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader,_NotAvailData, grid ,spDisplayHeader.LineSeperatorView, StudentAttendanceListView },
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

        public void LoadData(int startDate, int endDate)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _Loader.IsShowLoading = true;

                    //Service call

                    List<ViewAttendanceModel> lstStudentAttendanceModel = new List<ViewAttendanceModel>();

                    lstStudentAttendanceModel = await ViewAttendanceModel.GetStudentAttendance(startDate, endDate);

                    if (lstStudentAttendanceModel != null)
                    {
                        Items = new ObservableCollection<ViewAttendanceModel>(lstStudentAttendanceModel);
                        StudentAttendanceListView.ItemsSource = Items;

                        grid.IsVisible = true;
                        spDisplayHeader.IsVisible = true;
                        _NotAvailData.IsVisible = false;
                    }
                    else
                    {
                        Items = null;

                        _NotAvailData.IsVisible = true;
                        grid.IsVisible = false;
                        spDisplayHeader.IsVisible = false;
                    }
                    _Loader.IsShowLoading = false;
                }
                catch (Exception ex)
                {

                }
            });
        }

        #region Custom Property

        private ObservableCollection<ViewAttendanceModel> _items = new ObservableCollection<ViewAttendanceModel>();
        public ObservableCollection<ViewAttendanceModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
