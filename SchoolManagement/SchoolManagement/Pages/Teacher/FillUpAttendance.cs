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
using Acr.UserDialogs;
using SchoolManagement.Core.Context;

namespace SchoolManagement.Pages.Teacher
{
    public class FillUpAttendance : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy");
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader;
        private List<StandardModel> _StandardList = new List<StandardModel>();
        private List<ClassTypeModel> _ClassTypeList = new List<ClassTypeModel>();
        private FillUpAttendanceModel _FillUpAttendanceModel = new FillUpAttendanceModel();
        private int _SelectedStandardID = 0, _SelectedClassTypeID = 0;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FillUpAttendance"/> class.
        /// </summary>
        public FillUpAttendance()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsLoading = true;
                    _StandardList = await StandardModel.GetStandard();
                    FillUpAttendanceLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        #region FillUpAttendanceLayout
        /// <summary>
        /// Fill Up Attendance Layout.
        /// </summary>
        public void FillUpAttendanceLayout()
        {
            TitleBar lblPageName = new TitleBar("FillUp Attendance");
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

            Seperator spStandard = new Seperator();

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

            StackLayout slClassFrmaeLayout = new StackLayout
            {
                Children = { frmClass }
            };

            StackLayout slClassLayout = new StackLayout
            {
                Children = { slClassFrmaeLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

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
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

            StackLayout slSearchinOneCol = new StackLayout
            {
                Children = { slStandardLayout, slClassLayout },
                Orientation = StackOrientation.Horizontal
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStartDateLayout }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            //List view
            ListView FillUpAttendanceListView = new ListView
            {
                RowHeight = 50,
                SeparatorColor = Color.Gray
            };

            FillUpAttendanceListView.ItemsSource = Items;
            FillUpAttendanceListView.ItemTemplate = new DataTemplate(() => new FillUpAttendanceCell());

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
                HorizontalOptions = LayoutOptions.CenterAndExpand
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
                HorizontalOptions = LayoutOptions.StartAndExpand
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
                Children = { slStudentName, slAttendance, slExamMarks },
                Orientation = StackOrientation.Horizontal,
                IsVisible = false
            };

            Seperator spDisplayHeader = new Seperator { IsVisible = false };

            Button btnSave = new Button { IsVisible = false };
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            //Stanndard Picker Selected
            pcrStandard.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        _Loader.IsShowLoading = true;
                        btnSave.IsVisible = false;
                        pcrClass.Items.Clear();
                        lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");

                        string standardName = lblStandard.Text = pcrStandard.Items[pcrStandard.SelectedIndex];

                        _SelectedStandardID = _StandardList.Where(x => x.Name == standardName).FirstOrDefault().Id;

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
                    btnSave.IsVisible = false;
                    Items.Clear();
                    lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");

                    string className = lblClass.Text = pcrClass.Items[pcrClass.SelectedIndex];

                    _SelectedClassTypeID = _ClassTypeList.FirstOrDefault(x => x.Name == className).Id;

                    slStartDateLayout.IsVisible = true;
                    _Loader.IsShowLoading = false;
                });
            };

            //dtTimePicker.Unfocused += (sender, e) =>
            //{
            //    if (lblCurrentDate.Text == "Date")
            //    {
            //        lblCurrentDate.Text = DateTime.Now.ToString("dd-MM-yy");
            //    }
            //};

            dtTimePicker.DateSelected += (sender, e) =>
            {
                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                                  {
                                      _Loader.IsShowLoading = true;
                                      btnSave.IsVisible = false;
                                      lblCurrentDate.Text = dtTimePicker.Date.ToString("dd-MM-yy");

                                      int dateCounter = dtTimePicker.Date.ConvetDatetoDateCounter();

                                      //get student list
                                      _FillUpAttendanceModel = await FillUpAttendanceModel.GetFillUpAttendance(_SelectedStandardID, _SelectedClassTypeID, dateCounter);

                                      if (_FillUpAttendanceModel.Students != null)
                                      {

                                          Items = new ObservableCollection<Student>(_FillUpAttendanceModel.Students);

                                          if (Items.Count > 0 && Items != null)
                                          {
                                              FillUpAttendanceListView.ItemsSource = Items;
                                              grid.IsVisible = true;
                                              btnSave.IsVisible = true;
                                              spDisplayHeader.IsVisible = true;
                                              _NotAvailData.IsVisible = false;
                                          }
                                          else
                                          {
                                              grid.IsVisible = false;
                                              spDisplayHeader.IsVisible = false;
                                              btnSave.IsVisible = false;
                                              _NotAvailData.IsVisible = true;
                                          }
                                      }
                                      else
                                      {
                                          grid.IsVisible = false;
                                          spDisplayHeader.IsVisible = false;
                                          _NotAvailData.IsVisible = true;
                                          btnSave.IsVisible = false;
                                      }
                                      _Loader.IsShowLoading = false;
                                  });

                }
                catch (Exception ex)
                {
                    grid.IsVisible = false;
                    spDisplayHeader.IsVisible = false;
                    _NotAvailData.IsVisible = true;
                    btnSave.IsVisible = false;
                    _Loader.IsShowLoading = false;
                }

            };

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        btnSave.IsVisible = false;
                        _Loader.IsShowLoading = true;

                        FillUpAttendanceModel fillupAttendanceModel = new FillUpAttendanceModel();

                        fillupAttendanceModel.StandardId = _SelectedStandardID;
                        fillupAttendanceModel.ClassTypeId = _SelectedClassTypeID;
                        fillupAttendanceModel.Date = Convert.ToDateTime(lblCurrentDate.Text).ToString("dd/MM/yyyy");
                        fillupAttendanceModel.Students = Items.ToList();

                        bool isSaveAttendance = await FillUpAttendanceModel.SaveStudentAttendance(fillupAttendanceModel);

                        if (isSaveAttendance)
                        {
                            await DisplayAlert(string.Empty, "Save Successfully.", Messages.Ok);

                            //_StudentAttendanceList = await StudentAttendanceModel.GetStudentAttendance(_SelectedBatchID, 9);

                            //if (_StudentAttendanceList.Count > 0)
                            //{
                            //    _NotAvailData.IsVisible = false;
                            //    Items = new ObservableCollection<StudentAttendanceModel>(_StudentAttendanceList);

                            //    if (Items.Count > 0)
                            //    {
                            //        int noCount = 1;

                            //        for (int i = 0; i < Items.Count; i++)
                            //        {
                            //            Items[i].IndexNo = noCount;
                            //            noCount++;
                            //        }

                            //        grid.IsVisible = true;
                            //        _Loader.IsShowLoading = false;
                            //        _NotAvailLecture.IsVisible = false;

                            //        studentAttendanceListView.ItemsSource = Items;
                            //    }
                            //    else
                            //    {
                            //        grid.IsVisible = false;
                            //        _Loader.IsShowLoading = false;
                            //        _NotAvailLecture.IsVisible = true;
                            //    }
                            //}
                        }
                        else
                        {
                            await DisplayAlert(Messages.Error, "Some problem ocuured when saving data.", Messages.Ok);
                        }
                        _Loader.IsShowLoading = false;

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
						    Children = {slTitle, spTitle.LineSeperatorView,slSearchinOneCol, slSearchLayout,grid,spDisplayHeader.LineSeperatorView, _Loader, _NotAvailData,FillUpAttendanceListView,cvBtnSave },
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
