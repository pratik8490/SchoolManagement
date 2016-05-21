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
    public class ViewResult : BasePage
    {
        private LoadingIndicator _Loader;
        private Label _NotAvailData = new Label();
        private List<StandardExamMasterModel> _StandardExamMasterList = new List<StandardExamMasterModel>();
        private int _SelectedStandardExamMasterID = 0;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewResult"/> class.
        /// </summary>
        public ViewResult()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _StandardExamMasterList = await StandardExamMasterModel.GetStdExamMaster();
                    ViewResultLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// View Result
        /// </summary>
        public void ViewResultLayout()
        {
            try
            {
                TitleBar lblPageName = new TitleBar("View Result");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                Image stdExamMasterDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblStdExamMaster = new Label { TextColor = Color.Black, Text = "Standard Exam Master" };
                Picker pcrStdExamMaster = new Picker { Title = "Standard Exam Master", IsVisible = false };

                foreach (StandardExamMasterModel item in _StandardExamMasterList)
                {
                    pcrStdExamMaster.Items.Add(item.Name);
                }

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
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                StackLayout slSearchLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(0, 0, 0, 10),
                    Children = { slStdExamMasterLayout }
                };

                _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

                _Loader = new LoadingIndicator();

                ListView ViewResultListView = new ListView
                {
                    RowHeight = 80,
                    SeparatorColor = Color.Gray
                };

                ViewResultListView.ItemTemplate = new DataTemplate(() => new ViewResultCell());

                //Grid Header Layout
                Label lblTotalMarks = new Label
                {
                    Text = "Total",
                    TextColor = Color.Black
                };

                StackLayout slTotalMarks = new StackLayout
                {
                    Children = { lblTotalMarks },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblSubjectName = new Label
                {
                    Text = "Subject Name",
                    TextColor = Color.Black
                };

                StackLayout slSubjectName = new StackLayout
                {
                    Children = { lblSubjectName },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                Label lblObtainMarks = new Label
                {
                    Text = "Obtain Marks",
                    TextColor = Color.Black
                };

                StackLayout slObtainMarks = new StackLayout
                {
                    Children = { lblObtainMarks },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                Label lblIsPass = new Label
                {
                    Text = "Is Pass",
                    TextColor = Color.Black
                };

                StackLayout slIsPass = new StackLayout
                {
                    Children = { lblIsPass },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                StackLayout grid = new StackLayout
                {
                    Children = { slTotalMarks, slSubjectName, slObtainMarks, slIsPass },
                    Orientation = StackOrientation.Horizontal,
                    //VerticalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = false
                };

                Seperator spDisplayHeader = new Seperator { IsVisible = false };

                pcrStdExamMaster.SelectedIndexChanged += (sender, e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _Loader.IsShowLoading = true;
                        Items = new ObservableCollection<StudentMarksDetailModel>();

                        string StandardExamMaster = lblStdExamMaster.Text = pcrStdExamMaster.Items[pcrStdExamMaster.SelectedIndex];
                        _SelectedStandardExamMasterID = _StandardExamMasterList.Where(x => x.Name == StandardExamMaster).FirstOrDefault().Id;

                        List<StudentMarksDetailModel> lstExamMarksResult = await StudentMarksDetailModel.ViewResult(_SelectedStandardExamMasterID);

                        if (lstExamMarksResult != null && lstExamMarksResult.Count > 0)
                        {
                            _NotAvailData.IsVisible = false;
                            grid.IsVisible = true;
                            spDisplayHeader.IsVisible = true;

                            Items = new ObservableCollection<StudentMarksDetailModel>(lstExamMarksResult);
                            ViewResultListView.ItemsSource = Items;
                        }
                        else
                        {
                            _NotAvailData.IsVisible = true;
                            grid.IsVisible = false;
                            spDisplayHeader.IsVisible = false;
                        }

                        _Loader.IsShowLoading = false;
                    });
                };

                StackLayout slExamTimeTable = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader,_NotAvailData,ViewResultListView },
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

        private ObservableCollection<StudentMarksDetailModel> _items = new ObservableCollection<StudentMarksDetailModel>();
        public ObservableCollection<StudentMarksDetailModel> Items
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
