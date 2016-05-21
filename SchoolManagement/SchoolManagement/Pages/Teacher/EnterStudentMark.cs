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
using SchoolManagement.Pages.Teacher;

namespace SchoolManagement.Pages
{
    public class EnterStudentMark : BasePage
    {
        private Label _NotAvailData = new Label();
        private List<StandardModel> _StandardList = new List<StandardModel>();
        private List<ExamTypeModel> _ExamTypeList = new List<ExamTypeModel>();
        private int _SelectedStandardID = 0, _SelectedExamID = 0;
        private LoadingIndicator _Loader;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Enter Student Mark Page"/> class.
        /// </summary>
        public EnterStudentMark()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _StandardList = await StandardModel.GetStandard();
                    EnterStudentMarkLayout();
                }
                catch (Exception ex)
                {
                }
            });

        }
        #endregion

        #region EnterStudentMarkLayout
        /// <summary>
        /// Enter Student Mark Layout.
        /// </summary>
        public void EnterStudentMarkLayout()
        {
            TitleBar lblPageName = new TitleBar("Enter Student Mark");
            StackLayout slTitle = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 5, 0, 0),
                BackgroundColor = Color.White,
                Children = { lblPageName }
            };

            Seperator spTitle = new Seperator();

            Image imgStandardDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblStandard = new Label { TextColor = Color.Black, Text = "Standard" };
            Picker pcrStandard = new Picker { IsVisible = false, Title = "Standard" };

            foreach (StandardModel item in _StandardList)
            {
                pcrStandard.Items.Add(item.Name);
            }

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
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Image imgExamTypeDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblExamType = new Label { TextColor = Color.Black, Text = "Exam Type" };
            Picker pcrExamType = new Picker { IsVisible = false, Title = "Exam Type" };

            StackLayout slExamTypeDisplay = new StackLayout { Children = { lblExamType, pcrExamType, imgExamTypeDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            Frame frmExamType = new Frame
            {
                Content = slExamTypeDisplay,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OutlineColor = Color.Black,
                Padding = new Thickness(10)
            };

            var examTypeTap = new TapGestureRecognizer();

            examTypeTap.NumberOfTapsRequired = 1; // single-tap
            examTypeTap.Tapped += (s, e) =>
            {
                pcrExamType.Focus();
            };
            frmExamType.GestureRecognizers.Add(examTypeTap);
            slExamTypeDisplay.GestureRecognizers.Add(examTypeTap);

            StackLayout slExamTypeFrmaeLayout = new StackLayout
            {
                Children = { frmExamType }
            };

            StackLayout slExamTypeLayout = new StackLayout
            {
                Children = { slExamTypeFrmaeLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

            Image imgExamDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblExam = new Label { TextColor = Color.Black, Text = "Exam" };
            Picker pcrExam = new Picker { IsVisible = false, Title = "Exam" };

            StackLayout slExamDisplay = new StackLayout { Children = { lblExam, pcrExam, imgExamDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            Frame frmExam = new Frame
            {
                Content = slExamDisplay,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OutlineColor = Color.Black,
                Padding = new Thickness(10)
            };

            var examTap = new TapGestureRecognizer();

            examTap.NumberOfTapsRequired = 1; // single-tap
            examTap.Tapped += (s, e) =>
            {
                pcrExam.Focus();
            };
            frmExam.GestureRecognizers.Add(examTap);
            slExamDisplay.GestureRecognizers.Add(examTap);

            StackLayout slExamFrmaeLayout = new StackLayout
            {
                Children = { frmExam }
            };

            StackLayout slExamLayout = new StackLayout
            {
                Children = { slExamFrmaeLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStandardLayout, slExamTypeLayout, slExamLayout }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            //Stanndard Picker Selected
            pcrStandard.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;
                    pcrExam.Items.Clear();
                    pcrExamType.Items.Clear();
                    Items.Clear();

                    string standardName = lblStandard.Text = pcrStandard.Items[pcrStandard.SelectedIndex];

                    _SelectedStandardID = _StandardList.Where(x => x.Name == standardName).FirstOrDefault().Id;

                    _ExamTypeList = await ExamTypeModel.GetExamType();

                    if (_ExamTypeList.Count > 0 && _ExamTypeList != null)
                    {
                        slExamLayout.IsVisible = true;
                        _NotAvailData.IsVisible = false;
                    }
                    else
                    {
                        slExamLayout.IsVisible = false;
                        _NotAvailData.IsVisible = true;
                    }

                    foreach (ExamTypeModel item in _ExamTypeList)
                    {
                        pcrExam.Items.Add(item.Name);
                    }

                    _Loader.IsShowLoading = false;
                });
            };

            pcrExam.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Items.Clear();

                    string ExamName = lblExam.Text = pcrExam.Items[pcrExam.SelectedIndex];
                    _SelectedExamID = _ExamTypeList.FirstOrDefault(x => x.Name == ExamName).Id;

                });
            };

            //List view
            ListView StudentListView = new ListView
            {
                RowHeight = 50,
                SeparatorColor = Color.Gray
            };

            StudentListView.ItemsSource = Items;
            StudentListView.ItemTemplate = new DataTemplate(() => new StudentExamMarksCell());

            //Grid Header Layout
            Label lblAttendance = new Label
            {
                Text = "Attendance",
                TextColor = Color.Black
            };

            StackLayout slAttendance = new StackLayout
            {
                Children = { lblAttendance },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblStudent = new Label
            {
                Text = "Student",
                TextColor = Color.Black
            };

            StackLayout slStudentName = new StackLayout
            {
                Children = { lblStudent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label lblIsPresent = new Label
            {
                Text = "A/P",
                TextColor = Color.Black
            };

            StackLayout slExamMarks = new StackLayout
            {
                Children = { lblIsPresent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout grid = new StackLayout
            {
                Children = { slAttendance, slStudentName, slExamMarks },
                Orientation = StackOrientation.Horizontal,
                IsVisible = false
            };

            Seperator spDisplayHeader = new Seperator();

            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;

                    SaveStudentMarksMaster saveStudentMarksMaster = new SaveStudentMarksMaster();

                    //fillupAttendanceModel.StandardId = _SelectedStandardID;
                    //fillupAttendanceModel.ClassTypeId = _SelectedClassTypeID;
                    //fillupAttendanceModel.Date = Convert.ToDateTime(lblCurrentDate.Text).ToString("dd/MM/yyyy");
                    //fillupAttendanceModel.Students = Items.ToList();

                    bool isSaveAttendance = await SaveStudentMarksMaster.SaveStudentMarks(saveStudentMarksMaster);

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

            StackLayout slExamType = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader,_NotAvailData },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slExamType,
            };
        }
        #endregion

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