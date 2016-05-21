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

namespace SchoolManagement.Pages.Parents
{
    public class ExamTimetable : BasePage
    {
        private LoadingIndicator _Loader;
        private List<ExamTypeModel> _ExamTypeList = new List<ExamTypeModel>();
        private List<StandardExamMasterModel> _StandardExamMasterList = new List<StandardExamMasterModel>();
        private Label _NotAvailData = new Label();
        private int _SelectedExamTypeID = 0, _SelectedStandardExamMasterID = 0;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExamTimetable"/> class.
        /// </summary>
        public ExamTimetable()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _ExamTypeList = await ExamTypeModel.GetExamType();
                    ExamTimetableLayout();
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
        public void ExamTimetableLayout()
        {
            try
            {
                TitleBar lblPageName = new TitleBar("Exam TimeTable");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                Image imgExamTypeDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblExamType = new Label { TextColor = Color.Black, Text = "Exam Type" };
                Picker pcrExamType = new Picker { IsVisible = false, Title = "Exam Type" };

                foreach (ExamTypeModel item in _ExamTypeList)
                {
                    pcrExamType.Items.Add(item.Name);
                }


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

                StackLayout slExamTypeFrameLayout = new StackLayout
                {
                    Children = { frmExamType }
                };

                StackLayout slExamTypeLayout = new StackLayout
                {
                    Children = { slExamTypeFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                Image stdExamMasterDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblStdExamMaster = new Label { TextColor = Color.Black, Text = "Standard Exam Master" };
                Picker pcrStdExamMaster = new Picker { IsVisible = false, Title = "Standard Exam Master" };

                StackLayout slStdExamMasterDisplay = new StackLayout { Children = { lblStdExamMaster, pcrStdExamMaster, stdExamMasterDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmStdExamMaster = new Frame
                {
                    Content = slStdExamMasterDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var stdExamMasterTap = new TapGestureRecognizer();

                stdExamMasterTap.NumberOfTapsRequired = 1; // single-tap
                stdExamMasterTap.Tapped += (s, e) =>
                {
                    pcrStdExamMaster.Focus();
                };
                frmStdExamMaster.GestureRecognizers.Add(stdExamMasterTap);
                slStdExamMasterDisplay.GestureRecognizers.Add(stdExamMasterTap);

                StackLayout slStdExamMasterFrameLayout = new StackLayout
                {
                    Children = { frmStdExamMaster }
                };

                StackLayout slStdExamMasterLayout = new StackLayout
                {
                    Children = { slStdExamMasterFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = false
                };

                StackLayout slSearchLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(0, 0, 0, 10),
                    Children = { slExamTypeLayout, slStdExamMasterLayout }
                };

                //List view
                ListView ExamTimeTableListView = new ListView
                {
                    RowHeight = 60,
                    SeparatorColor = Color.Gray
                };

                _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

                _Loader = new LoadingIndicator();

                pcrExamType.SelectedIndexChanged += (sender, e) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                            {
                                _Loader.IsShowLoading = true;
                                pcrStdExamMaster.Items.Clear();

                                string ExamType = lblExamType.Text = pcrExamType.Items[pcrExamType.SelectedIndex];
                                _SelectedExamTypeID = _ExamTypeList.Where(x => x.Name == ExamType).FirstOrDefault().Id;

                                _StandardExamMasterList = await StandardExamMasterModel.GetStdExamMaster();

                                if (_StandardExamMasterList != null && _StandardExamMasterList.Count > 0)
                                {
                                    slStdExamMasterLayout.IsVisible = true;
                                    _NotAvailData.IsVisible = false;
                                }
                                else
                                {
                                    slStdExamMasterLayout.IsVisible = false;
                                    _NotAvailData.IsVisible = true;
                                }

                                foreach (StandardExamMasterModel item in _StandardExamMasterList)
                                {
                                    pcrStdExamMaster.Items.Add(item.Name);
                                }

                                _Loader.IsShowLoading = false;
                            });
                    };

                ExamTimeTableListView.ItemTemplate = new DataTemplate(() => new ExamTimeTableCell());

                //Grid Header Layout
                Label lblDate = new Label
                {
                    Text = "Date",
                    TextColor = Color.Black
                };

                StackLayout slDate = new StackLayout
                {
                    Children = { lblDate },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblExamName = new Label
                {
                    Text = "Exam Name",
                    TextColor = Color.Black
                };

                StackLayout slExamName = new StackLayout
                {
                    Children = { lblExamName },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                Label lblTotalMarks = new Label
                {
                    Text = "Total Marks",
                    TextColor = Color.Black
                };

                StackLayout slTotalMarks = new StackLayout
                {
                    Children = { lblTotalMarks },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                var grid = new Grid { IsVisible = false };
                grid.Children.Add(slDate, 0, 0);
                grid.Children.Add(slExamName, 1, 0);
                grid.Children.Add(slTotalMarks, 2, 0);

                Seperator spDisplayHeader = new Seperator { IsVisible = false };

                pcrStdExamMaster.SelectedIndexChanged += (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _Loader.IsShowLoading = true;
                        string StandardExamMaster = lblStdExamMaster.Text = pcrStdExamMaster.Items[pcrStdExamMaster.SelectedIndex];
                        _SelectedStandardExamMasterID = _StandardExamMasterList.Where(x => x.Name == StandardExamMaster).FirstOrDefault().Id;

                        int dayStartDateCounter = DateTime.Now.ConvetDatetoDateCounter();

                        int dayEndDateCounter = DateTime.Now.AddMonths(1).ConvetDatetoDateCounter();

                        List<ExamScheduleModel> lstExamSchedule = await ExamScheduleModel.GetExamTimeTable(dayStartDateCounter, dayEndDateCounter, _SelectedExamTypeID, _SelectedStandardExamMasterID);

                        if (lstExamSchedule != null && lstExamSchedule.Count > 0)
                        {
                            _NotAvailData.IsVisible = false;
                            //grid.IsVisible = true;
                            //spDisplayHeader.IsVisible = true;
                            Items = new ObservableCollection<ExamScheduleModel>(lstExamSchedule);

                            ExamTimeTableListView.ItemsSource = Items;
                        }
                        else
                        {
                            _NotAvailData.IsVisible = true;
                            //grid.IsVisible = false;
                            //spDisplayHeader.IsVisible = false;
                        }

                        _Loader.IsShowLoading = false;
                    });
                };

                StackLayout slExamTimeTable = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader, _NotAvailData,ExamTimeTableListView },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slExamTimeTable,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Custom Property

        private ObservableCollection<ExamScheduleModel> _items = new ObservableCollection<ExamScheduleModel>();
        public ObservableCollection<ExamScheduleModel> Items
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
