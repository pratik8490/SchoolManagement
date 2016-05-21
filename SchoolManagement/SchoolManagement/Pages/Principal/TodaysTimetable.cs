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

namespace SchoolManagement.Pages.Principal
{
    public class TodaysTimetable : BasePage
    {
        private LoadingIndicator _Loader;
        private Label _NotAvailData;
        private string _SelectedRadio = string.Empty;
        private List<TimeTableModel> _TimeTableList = new List<TimeTableModel>();
        private List<TeacherModel> _TeacherList = new List<TeacherModel>();
        private List<StandardModel> _StatndardList = new List<StandardModel>();
        private List<ClassTypeModel> _ClassTypeList = new List<ClassTypeModel>();
        private int _SelectedTeacherID = 0, _SelectedStandardID = 0, _SelectedClassTypeID = 0;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TodaysTimetable"/> class.
        /// </summary>
        public TodaysTimetable()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    TodaysTimetableLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Todays Timetable
        /// </summary>
        public void TodaysTimetableLayout()
        {
            try
            {
                TitleBar lblPageName = new TitleBar("Today's TimeTable");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                BindableRadioGroup radByTeacherOrClass = new BindableRadioGroup();

                radByTeacherOrClass.ItemsSource = new[] { "By Teacher", "By Class" };
                radByTeacherOrClass.HorizontalOptions = LayoutOptions.FillAndExpand;
                radByTeacherOrClass.Orientation = StackOrientation.Horizontal;
                radByTeacherOrClass.TextColor = Color.Black;

                StackLayout slRadio = new StackLayout
                {
                    Children = { radByTeacherOrClass },
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                #region By Teacher

                Image imgTeacherDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblTeacher = new Label { TextColor = Color.Black, Text = "Teacher" };
                Picker pcrTeacher = new Picker { IsVisible = false, Title = "Teacher" };

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
                    IsVisible = false
                };

                Label lblStandardName = new Label
                {
                    Text = "Standard Name",
                    TextColor = Color.Black
                };

                StackLayout slStandardName = new StackLayout
                {
                    Children = { lblStandardName },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };

                Label lblClassTypeName = new Label
                {
                    Text = "Class Type Name",
                    TextColor = Color.Black
                };

                StackLayout slClassTypeName = new StackLayout
                {
                    Children = { lblClassTypeName },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                Label lblStubjectName = new Label
                {
                    Text = "Subject Name",
                    TextColor = Color.Black
                };

                StackLayout slSubjectName = new StackLayout
                {
                    Children = { lblStubjectName },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };
                #endregion

                #region By Class

                Image imgStandardDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblStandard = new Label { TextColor = Color.Black, Text = "Standard" };
                Picker pcrStandard = new Picker { IsVisible = false, Title = "Standard" };

                StackLayout slStandardDisplay = new StackLayout { Children = { lblStandard, pcrStandard, imgStandardDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmStandard = new Frame
                {
                    Content = slStandardDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var standardTap = new TapGestureRecognizer();

                standardTap.NumberOfTapsRequired = 1; // single-tap
                standardTap.Tapped += (s, e) =>
                {
                    pcrStandard.Focus();
                };
                frmStandard.GestureRecognizers.Add(standardTap);
                slStandardDisplay.GestureRecognizers.Add(standardTap);

                StackLayout slStandardFrameLayout = new StackLayout
                {
                    Children = { frmStandard }
                };

                StackLayout slStandardLayout = new StackLayout
                {
                    Children = { slStandardFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                Image imgClassDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblClass = new Label { TextColor = Color.Black, Text = "Class" };
                Picker pcrClass = new Picker { IsVisible = false, Title = "Class" };

                StackLayout slClassDisplay = new StackLayout { Children = { lblClass, pcrClass, imgClassDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmClass = new Frame
                {
                    Content = slClassDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var classTap = new TapGestureRecognizer();

                classTap.NumberOfTapsRequired = 1; // single-tap
                classTap.Tapped += (s, e) =>
                {
                    pcrClass.Focus();
                };
                frmClass.GestureRecognizers.Add(classTap);
                slClassDisplay.GestureRecognizers.Add(classTap);

                StackLayout slClassFrameLayout = new StackLayout
                {
                    Children = { frmClass }
                };

                StackLayout slClassLayout = new StackLayout
                {
                    Children = { slClassFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = false
                };

                //Grid Header
                Label lblTeachername = new Label
                {
                    Text = "Teacher Name",
                    TextColor = Color.Black
                };

                StackLayout slTeacherName = new StackLayout
                {
                    Children = { lblTeachername },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                Label lblStubject = new Label
                {
                    Text = "Subject Name",
                    TextColor = Color.Black
                };

                StackLayout slSubject = new StackLayout
                {
                    Children = { lblStubject },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                #endregion

                Label lblSequensNo = new Label
                {
                    Text = "No",
                    TextColor = Color.Black
                };

                StackLayout slSequensNo = new StackLayout
                {
                    Children = { lblSequensNo },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                StackLayout grid = new StackLayout
                {
                    //Children = { slTeacherName, slSequensNo, slSubjectName },
                    Orientation = StackOrientation.Horizontal,
                    IsVisible = false
                };

                Seperator spDisplayHeader = new Seperator { IsVisible = false };

                StackLayout slSearchinOneCol = new StackLayout
                {
                    Children = { slStandardLayout, slClassLayout },
                    Orientation = StackOrientation.Horizontal,
                    IsVisible = false
                };

                _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

                _Loader = new LoadingIndicator();

                radByTeacherOrClass.CheckedChanged += (sender, e) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                       {
                           var radio = sender as CustomRadioButton;

                           if (radio == null || radio.Id == -1)
                           {
                               return;
                           }

                           if (radio.Text == "By Teacher")
                           {
                               slTeacherLayout.IsVisible = true;
                               slSearchinOneCol.IsVisible = false;

                               foreach (TeacherModel item in _TeacherList)
                               {
                                   pcrTeacher.Items.Add(item.Name);
                               }

                               grid.Children.Add(slSequensNo);
                               grid.Children.Add(slStandardDisplay);
                               grid.Children.Add(slClassTypeName);
                               grid.Children.Add(slSubjectName);

                           }
                           else
                           {
                               slSearchinOneCol.IsVisible = true;
                               slTeacherLayout.IsVisible = false;

                               grid.Children.Add(slSequensNo);
                               grid.Children.Add(slTeacherName);
                               grid.Children.Add(slSubject);

                               _StatndardList = await StandardModel.GetStandard();

                               foreach (StandardModel item in _StatndardList)
                               {
                                   pcrStandard.Items.Add(item.Name);
                               }
                           }
                       });
                    };

                //List view
                ListView TimeTableListView = new ListView
                {
                    RowHeight = 50,
                    SeparatorColor = Color.Gray
                };

                TimeTableListView.ItemTemplate = new DataTemplate(() => new FillUpAttendanceCell());

                pcrStandard.SelectedIndexChanged += (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            _Loader.IsShowLoading = true;
                            pcrClass.Items.Clear();

                            string standardName = lblStandard.Text = pcrStandard.Items[pcrStandard.SelectedIndex];

                            _SelectedStandardID = _StatndardList.Where(x => x.Name == standardName).FirstOrDefault().Id;

                            _ClassTypeList = await ClassTypeModel.GetClassType(_SelectedStandardID);

                            if (_ClassTypeList.Count > 0 && _ClassTypeList != null)
                            {
                                slClassLayout.IsVisible = true;
                                _NotAvailData.IsVisible = false;
                            }
                            else
                            {
                                slClassLayout.IsVisible = false;
                                _NotAvailData.IsVisible = true;
                            }

                            foreach (ClassTypeModel item in _ClassTypeList)
                            {
                                pcrClass.Items.Add(item.Name);
                            }

                            _Loader.IsShowLoading = false;
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                };

                //Class Picker Selected

                pcrClass.SelectedIndexChanged += (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _Loader.IsShowLoading = true;
                        _NotAvailData.IsVisible = false;

                        string className = lblClass.Text = pcrClass.Items[pcrClass.SelectedIndex];

                        _SelectedClassTypeID = _ClassTypeList.FirstOrDefault(x => x.Name == className).Id;

                        //Get time table list
                        _TimeTableList = await TimeTableModel.ShowTimeTable(_SelectedStandardID, _SelectedClassTypeID);

                        if (_TimeTableList != null && _TimeTableList.Count > 0)
                        {
                            grid.IsVisible = true;
                            spDisplayHeader.IsVisible = true;
                            Items = new ObservableCollection<TimeTableModel>(_TimeTableList);
                            TimeTableListView.ItemsSource = Items;
                        }
                        else
                        {
                            grid.IsVisible = false;
                            spDisplayHeader.IsVisible = false;
                            _NotAvailData.Text = "There is no data for selected standard and class.";
                            _NotAvailData.IsVisible = true;
                        }
                        _Loader.IsShowLoading = false;
                    });
                };

                //Class Picker Selected

                pcrTeacher.SelectedIndexChanged += (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _Loader.IsShowLoading = true;
                        _NotAvailData.IsVisible = false;

                        string teacherName = lblTeacher.Text = pcrTeacher.Items[pcrTeacher.SelectedIndex];

                        _SelectedTeacherID = _TeacherList.FirstOrDefault(x => x.Name == teacherName).ID;

                        //Get time table list
                        _TimeTableList = await TimeTableModel.ShowTimeTable(_SelectedTeacherID);

                        if (_TimeTableList != null && _TimeTableList.Count > 0)
                        {
                            grid.IsVisible = true;
                            spDisplayHeader.IsVisible = true;
                            Items = new ObservableCollection<TimeTableModel>(_TimeTableList);
                            TimeTableListView.ItemsSource = Items;
                        }
                        else
                        {
                            grid.IsVisible = false;
                            spDisplayHeader.IsVisible = false;
                            _NotAvailData.Text = "There is no data for selected standard and class.";
                            _NotAvailData.IsVisible = true;
                        }
                        _Loader.IsShowLoading = false;
                    });
                };

                StackLayout slTimeTable = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView,slRadio,slTeacherLayout, slSearchinOneCol,grid,spDisplayHeader.LineSeperatorView, _Loader, _NotAvailData,TimeTableListView},
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slTimeTable,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Custom Property

        private ObservableCollection<TimeTableModel> _items = new ObservableCollection<TimeTableModel>();
        public ObservableCollection<TimeTableModel> Items
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
