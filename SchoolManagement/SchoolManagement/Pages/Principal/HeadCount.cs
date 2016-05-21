using Acr.UserDialogs;
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
    public class HeadCount : BasePage
    {
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader = new LoadingIndicator();
        private List<StandardModel> _StandardList = new List<StandardModel>();
        private List<StudentHeadCountModel> _StudentHeadCountModelList = new List<StudentHeadCountModel>();
        private EmployeeModel _EmployeeModel = new EmployeeModel();

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Head Count Page"/> class.
        /// </summary>
        public HeadCount()
        {
            Device.BeginInvokeOnMainThread(async () =>
           {
               IsLoading = true;
               _EmployeeModel = await EmployeeModel.GetEmployeeCount();
               HeadCountLayout();
           });
        }
        #endregion

        /// <summary>
        /// HeadCount Layout
        /// </summary>
        public void HeadCountLayout()
        {
            try
            {

                TitleBar lblPageName = new TitleBar("Head Count");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                Image imgTypeDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblType = new Label { TextColor = Color.Black };
                lblType.Text = "Employee";
                Picker pcrType = new Picker { IsVisible = false };

                StackLayout slTypeDisplay = new StackLayout { Children = { lblType, pcrType, imgTypeDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                //Frame layout for start date
                Frame frmType = new Frame
                {
                    Content = slTypeDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var typeTap = new TapGestureRecognizer();

                typeTap.NumberOfTapsRequired = 1; // single-tap
                typeTap.Tapped += (s, e) =>
                {
                    pcrType.Focus();
                };
                frmType.GestureRecognizers.Add(typeTap);
                slTypeDisplay.GestureRecognizers.Add(typeTap);

                StackLayout slTypeFrmaeLayout = new StackLayout
                {
                    Children = { frmType }
                };

                StackLayout slTypeLayout = new StackLayout
                {
                    Children = { slTypeFrmaeLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                pcrType.Items.Add("Student");
                pcrType.Items.Add("Employee");

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
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = false
                };

                #region Teacher
                Label lblTotal = new Label
                {
                    Text = "Total",
                    TextColor = Color.Black
                };

                Label lblTotalValue = new Label
                {
                    TextColor = Color.Black
                };

                Label lblTotalMale = new Label
                {
                    Text = "Total Male",
                    TextColor = Color.Black
                };

                Label lblTotalMaleValue = new Label
                {
                    TextColor = Color.Black
                };

                Label lblTotalFeMale = new Label
                {
                    Text = "Total FeMale",
                    TextColor = Color.Black
                };
                Label lblTotalFeMalValue = new Label
                {
                    TextColor = Color.Black
                };

                StackLayout slTotal = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { lblTotal, lblTotalValue },
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                StackLayout slTotalMale = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { lblTotalMale, lblTotalMaleValue },
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                StackLayout slTotalFeMale = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { lblTotalFeMale, lblTotalFeMalValue },
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                StackLayout slEmployee = new StackLayout
                {
                    Children = { slTotal, slTotalMale, slTotalFeMale },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = false
                };

                if (!string.IsNullOrEmpty(Convert.ToString(_EmployeeModel.Total)) && !string.IsNullOrEmpty(Convert.ToString(_EmployeeModel.TotalMale)) && !string.IsNullOrEmpty(Convert.ToString(_EmployeeModel.TotalFemale)))
                {
                    lblTotalValue.Text = Convert.ToString(_EmployeeModel.Total);
                    lblTotalMaleValue.Text = Convert.ToString(_EmployeeModel.TotalMale);
                    lblTotalFeMalValue.Text = Convert.ToString(_EmployeeModel.TotalFemale);

                    slEmployee.IsVisible = true;
                }
                else
                {
                    _NotAvailData.IsVisible = false;
                }

                #endregion

                #region Student

                ListView studentHeadCountListView = new ListView();
                studentHeadCountListView.HasUnevenRows = true;
                studentHeadCountListView.SeparatorColor = Color.Gray;

                studentHeadCountListView.ItemTemplate = new DataTemplate(() => new StudentHeadCountCell());

                #endregion

                pcrType.SelectedIndexChanged += async (sender, e) =>
                    {
                        try
                        {
                            using (UserDialogs.Instance.Loading("Loading"))
                            {
                                string type = lblType.Text = pcrType.Items[pcrType.SelectedIndex];

                                if (type == "Student")
                                {
                                    slStandardLayout.IsVisible = true;
                                    slEmployee.IsVisible = false;
                                    _NotAvailData.IsVisible = false;

                                    _StandardList = await StandardModel.GetStandard();

                                    if (_StandardList != null && _StandardList.Count > 0)
                                    {
                                        foreach (StandardModel item in _StandardList)
                                        {
                                            pcrStandard.Items.Add(item.Name);
                                        }
                                    }
                                    else
                                    {
                                        _NotAvailData.IsVisible = true;
                                    }
                                }
                                else
                                {
                                    slStandardLayout.IsVisible = false;
                                    _EmployeeModel = await EmployeeModel.GetEmployeeCount();

                                    if (!string.IsNullOrEmpty(Convert.ToString(_EmployeeModel.Total)) && !string.IsNullOrEmpty(Convert.ToString(_EmployeeModel.TotalMale)) && !string.IsNullOrEmpty(Convert.ToString(_EmployeeModel.TotalFemale)))
                                    {
                                        lblTotalValue.Text = Convert.ToString(_EmployeeModel.Total);
                                        lblTotalMaleValue.Text = Convert.ToString(_EmployeeModel.TotalMale);
                                        lblTotalFeMalValue.Text = Convert.ToString(_EmployeeModel.TotalFemale);

                                        slEmployee.IsVisible = true;
                                    }
                                    else
                                    {
                                        _NotAvailData.IsVisible = false;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    };

                pcrStandard.SelectedIndexChanged += async (sender, e) =>
                    {
                        using (UserDialogs.Instance.Loading("Loading"))
                        {
                            string standardName = lblStandard.Text = pcrStandard.Items[pcrStandard.SelectedIndex];

                            int standardID = _StandardList.Where(x => x.Name == standardName).FirstOrDefault().Id;

                            _StudentHeadCountModelList = await StudentHeadCountModel.GetStudentHeadCount(9);

                            if (_StudentHeadCountModelList != null && _StudentHeadCountModelList.Count > 0)
                            {
                                Items = new ObservableCollection<StudentHeadCountModel>(_StudentHeadCountModelList);
                                _NotAvailData.IsVisible = false;
                                studentHeadCountListView.ItemsSource = Items;
                            }
                            else
                            {
                                _NotAvailData.IsVisible = true;
                            }
                        }
                    };

                StackLayout slStudent = new StackLayout
                {
                    Children = { slStandardLayout, studentHeadCountListView },
                    Orientation = StackOrientation.Vertical
                };

                StackLayout slHeadCount = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slTypeLayout, _NotAvailData,slEmployee,slStudent },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slHeadCount,
                };
            }
            catch (Exception ex)
            {

            }
        }

        #region Custom Property

        private ObservableCollection<StudentHeadCountModel> _items = new ObservableCollection<StudentHeadCountModel>();
        public ObservableCollection<StudentHeadCountModel> Items
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
