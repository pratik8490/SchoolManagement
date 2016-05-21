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

namespace SchoolManagement.Pages.Parents
{
    public class HomeWork : BasePage
    {
        private Label _NotAvailData = new Label();
        private int _SelectedLectureID = 0;
        private List<LectureModel> _LectureList = new List<LectureModel>();
        private LoadingIndicator _Loader;
        private Seperator spDisplayHeader = new Seperator();
        private ListView HomeWorkListView;
        private StackLayout grid;
        private int _DateCounter = DateTime.Now.ConvetDatetoDateCounter();

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
                    HomeWorkLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        ///Exam Timetable
        /// </summary>
        public void HomeWorkLayout()
        {
            try
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

                Image imgLeftarrow = new Image
                {
                    Source = Constants.ImagePath.ArrowLeft
                };

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

                //dtStartDate.Unfocused += (sender, e) =>
                //    {
                //        if (lblCurrentDate.Text == "Date")
                //        {
                //            lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
                //        }
                //    };

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

                StackLayout slDateLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 0, 0, 10),
                    Children = { imgLeftarrow, slStartDateLayout, imgRightarrow },
                    IsVisible = false
                };

                StackLayout slSearchLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(0, 0, 0, 10),
                    Children = { slLectureLayout, slDateLayout }
                };

                _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

                _Loader = new LoadingIndicator();

                //List view
                HomeWorkListView = new ListView
                {
                    RowHeight = 50,
                    SeparatorColor = Color.Gray
                };

                HomeWorkListView.ItemsSource = Items;
                HomeWorkListView.ItemTemplate = new DataTemplate(() => new ViewAttendanceCell());

                //Grid Header Layout
                Label lblHomeWork = new Label
                {
                    Text = "HomeWork",
                    TextColor = Color.Black
                };

                StackLayout slHomeWorkLabel = new StackLayout
                {
                    Children = { lblHomeWork },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblComment = new Label
                {
                    Text = "Comment",
                    TextColor = Color.Black
                };

                StackLayout slComment = new StackLayout
                {
                    Children = { lblComment },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                grid = new StackLayout
                {
                    Children = { slHomeWorkLabel, slComment },
                    Orientation = StackOrientation.Horizontal,
                    IsVisible = false
                };

                pcrLecture.SelectedIndexChanged += (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        slDateLayout.IsVisible = true;
                        lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
                        string lectureName = lblLecture.Text = pcrLecture.Items[pcrLecture.SelectedIndex];

                        _SelectedLectureID = _LectureList.FirstOrDefault(x => x.LectureName == lectureName).Id;
                    });
                };

                dtStartDate.DateSelected += (s, e) =>
                {
                    Items.Clear();
                    lblCurrentDate.Text = (dtStartDate).Date.ToString("dd-MM-yy");

                    _DateCounter = (dtStartDate).Date.ConvetDatetoDateCounter();

                    LoadData(_DateCounter, _SelectedLectureID);
                };


                //Left button click

                var leftArrowTap = new TapGestureRecognizer();

                leftArrowTap.NumberOfTapsRequired = 1; // single-tap
                leftArrowTap.Tapped += (s, e) =>
                {
                    Items.Clear();
                    _DateCounter = _DateCounter - 1;
                    lblCurrentDate.Text = _DateCounter.GetDateFromDateCount().ToString("dd-MM-yy");
                    dtStartDate.Date = _DateCounter.GetDateFromDateCount();

                    LoadData(_DateCounter, _SelectedLectureID);
                };
                imgLeftarrow.GestureRecognizers.Add(leftArrowTap);

                //Right button click

                var rightArrowTap = new TapGestureRecognizer();

                rightArrowTap.NumberOfTapsRequired = 1; // single-tap
                rightArrowTap.Tapped += (s, e) =>
                {
                    Items.Clear();
                    _DateCounter = _DateCounter + 1;
                    lblCurrentDate.Text = _DateCounter.GetDateFromDateCount().ToString("dd-MM-yy");
                    dtStartDate.Date = _DateCounter.GetDateFromDateCount();

                    LoadData(_DateCounter, _SelectedLectureID);
                };
                imgRightarrow.GestureRecognizers.Add(rightArrowTap);

                StackLayout slHomeWork = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader,_NotAvailData,grid, spDisplayHeader.LineSeperatorView, HomeWorkListView },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slHomeWork,
                };
            }
            catch (Exception ex)
            {

            }
        }
        public void LoadData(int date, int lectureID)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _Loader.IsShowLoading = true;

                    //Service call

                    List<TeacherLogBookModel> lstHomeWorkModel = new List<TeacherLogBookModel>();

                    lstHomeWorkModel = await TeacherLogBookModel.GetHomeWorkModel(date, lectureID);

                    if (lstHomeWorkModel != null)
                    {
                        Items = new ObservableCollection<TeacherLogBookModel>(lstHomeWorkModel);
                        HomeWorkListView.ItemsSource = Items;

                        grid.IsVisible = true;
                        spDisplayHeader.IsVisible = true;
                        _NotAvailData.IsVisible = false;
                    }
                    else
                    {
                        Items.Clear();

                        _NotAvailData.IsVisible = true;
                        grid.IsVisible = false;
                        spDisplayHeader.IsVisible = false;
                    }
                    _Loader.IsShowLoading = false;
                }
                catch (Exception ex)
                {
                    grid.IsVisible = false;
                    _Loader.IsShowLoading = false;
                    spDisplayHeader.IsVisible = false;
                }
            });
        }

        #region Custom Property

        private ObservableCollection<TeacherLogBookModel> _items = new ObservableCollection<TeacherLogBookModel>();
        public ObservableCollection<TeacherLogBookModel> Items
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
