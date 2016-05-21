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

namespace SchoolManagement.Pages
{
    public class TodaysSchedule : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy"); //Convert.ToDateTime("07-01-2015").ToString("dd-MM-yy");
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader;
        private ListView TodaysScheduleListView;
        private StackLayout grid;
        private int _DateCounter = DateTime.Now.ConvetDatetoDateCounter();

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Todays Schedule Page"/> class.
        /// </summary>
        public TodaysSchedule()
        {
            IsLoading = true;
            TodaysScheduleLayout();
        }
        #endregion

        #region TodaysScheduleLayout
        /// <summary>
        /// Todays Schedule Layout.
        /// </summary>
        public void TodaysScheduleLayout()
        {
            TitleBar lblPageName = new TitleBar("Today's Schedule");
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
            lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
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

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { imgLeftarrow, slStartDateLayout, imgRightarrow }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            dtStartDate.DateSelected += (s, e) =>
            {
                lblCurrentDate.Text = (dtStartDate).Date.ToString("dd-MM-yy");

                _DateCounter = (dtStartDate).Date.ConvetDatetoDateCounter();

                LoadData(_DateCounter);
            };

            //dtStartDate.Unfocused += (sender, e) =>
            //{
            //    if (lblCurrentDate.Text == "Date")
            //    {
            //        lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
            //    }
            //};

            //Left button click

            var leftArrowTap = new TapGestureRecognizer();

            leftArrowTap.NumberOfTapsRequired = 1; // single-tap
            leftArrowTap.Tapped += (s, e) =>
            {
                _DateCounter = _DateCounter - 1;
                lblCurrentDate.Text = _DateCounter.GetDateFromDateCount().ToString("dd-MM-yy");
                dtStartDate.Date = _DateCounter.GetDateFromDateCount();

                LoadData(_DateCounter);
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

                LoadData(_DateCounter);
            };
            imgRightarrow.GestureRecognizers.Add(rightArrowTap);

            //Grid Header Layout
            Label lblNo = new Label
            {
                Text = "No",
                TextColor = Color.Black
            };

            StackLayout slNo = new StackLayout
            {
                Children = { lblNo },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblStandard = new Label
            {
                Text = "Standard",
                TextColor = Color.Black
            };

            StackLayout slStandardGrid = new StackLayout
            {
                Children = { lblStandard },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label lblSubject = new Label
            {
                Text = "Subject",
                TextColor = Color.Black
            };

            StackLayout slSubject = new StackLayout
            {
                Children = { lblSubject },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            Label lblLogBook = new Label
            {
                Text = "LogBook",
                TextColor = Color.Black
            };

            StackLayout slLogBook = new StackLayout
            {
                Children = { lblLogBook },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout grid = new StackLayout
            {
                Children = { slNo, slStandardGrid, slSubject, slLogBook },
                Orientation = StackOrientation.Horizontal,
                IsVisible = false
            };

            Seperator spDisplayHeader = new Seperator();

            TodaysScheduleListView = new ListView
            {
                RowHeight = 50,
                SeparatorColor = Color.Gray
            };

            TodaysScheduleListView.ItemsSource = Items;
            TodaysScheduleListView.ItemTemplate = new DataTemplate(() => new TodaysScheduleCell());

            StackLayout slTodaySchedule = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,grid,spDisplayHeader.LineSeperatorView, _Loader, _NotAvailData,TodaysScheduleListView,},
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slTodaySchedule,
            };
        }
        #endregion

        public void LoadData(int dateCounter)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _Loader.IsShowLoading = true;

                    //Service call

                    _Loader.IsShowLoading = false;
                }
                catch (Exception ex)
                {

                }
            });
        }

        #region Custom Property

        private ObservableCollection<Student> _items = new ObservableCollection<Student>();
        public ObservableCollection<Student> Items
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
