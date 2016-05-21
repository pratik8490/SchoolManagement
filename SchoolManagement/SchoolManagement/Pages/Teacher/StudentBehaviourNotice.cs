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
    public class StudentBehaviourNotice : BasePage
    {
        private Label _NotAvailData = new Label();
        private LoadingIndicator _Loader;
        private List<StandardModel> _StandardList = new List<StandardModel>();
        private List<ClassTypeModel> _ClassTypeList = new List<ClassTypeModel>();
        private List<StudentModel> _StudentModelList = new List<StudentModel>();
        private int _SelectedStandardID = 0, _SelectedClassTypeID = 0, _SelectedStudentID = 0;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Student BehaviourNotice Page"/> class.
        /// </summary>
        public StudentBehaviourNotice()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsLoading = true;
                    _StandardList = await StandardModel.GetStandard();
                    StudentBehaviourLayout();
                }
                catch (Exception ex)
                {
                }
            });

        }
        #endregion

        #region StudentBehaviourNoticeLayout
        /// <summary>
        /// Student BehaviourNotice Layout.
        /// </summary>
        public void StudentBehaviourLayout()
        {
            TitleBar lblPageName = new TitleBar("Student Behaviour Notice");
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

            Image imgStudentNameDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblStudentName = new Label { TextColor = Color.Black, Text = "Student Name" };
            Picker pcrStudentName = new Picker { IsVisible = false, Title = "Student Name" };

            StackLayout slStudentNameDisplay = new StackLayout { Children = { lblStudentName, pcrStudentName, imgStudentNameDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            Frame frmStudentName = new Frame
            {
                Content = slStudentNameDisplay,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OutlineColor = Color.Black,
                Padding = new Thickness(10)
            };

            var studentNameTap = new TapGestureRecognizer();

            studentNameTap.NumberOfTapsRequired = 1; // single-tap
            studentNameTap.Tapped += (s, e) =>
            {
                pcrStudentName.Focus();
            };
            frmStudentName.GestureRecognizers.Add(studentNameTap);
            slStudentNameDisplay.GestureRecognizers.Add(studentNameTap);

            StackLayout slStudentNameFrmaeLayout = new StackLayout
            {
                Children = { frmStudentName }
            };

            StackLayout slStudentNameLayout = new StackLayout
            {
                Children = { slStudentNameFrmaeLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

            //ExtendedEntry txtComment = new ExtendedEntry
            //{
            //    Placeholder = "Comment",
            //    TextColor = Color.Black
            //};

            Label lblComment = new Label
            {
                Text = "Comment",
                TextColor = Color.Black
            };

            StackLayout slLableComment = new StackLayout
            {
                Children = { lblComment },
                Padding = new Thickness(0, 0, 0, 10)
            };

            Editor txtComment = new Editor
            {
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout slTextComment = new StackLayout
            {
                Children = { txtComment },
                Padding = new Thickness(0, 0, 0, 10)
            };

            //txtComment.Focused += (sender, e) =>
            //{
            //    if (txtComment.Text == "Comment")
            //    {
            //        txtComment.Text = string.Empty;
            //        //txtComment.TextColor = Color.Black;
            //    }
            //};

            StackLayout slComment = new StackLayout
            {
                Children = { slLableComment, slTextComment },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            StackLayout slSearchinOneCol = new StackLayout
            {
                Children = { slStandardLayout, slClassLayout },
                Orientation = StackOrientation.Horizontal
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStudentNameLayout, slComment }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            //Stanndard Picker Selected
            pcrStandard.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;
                    pcrClass.Items.Clear();
                    pcrStudentName.Items.Clear();

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
                        _NotAvailData.IsVisible = true;
                    }

                    foreach (ClassTypeModel item in _ClassTypeList)
                    {
                        pcrClass.Items.Add(item.Name);
                    }

                    _Loader.IsShowLoading = false;
                });
            };

            //Class Picker Selected

            pcrClass.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;
                    pcrStudentName.Items.Clear();

                    string className = lblClass.Text = pcrClass.Items[pcrClass.SelectedIndex];

                    _SelectedClassTypeID = _ClassTypeList.FirstOrDefault(x => x.Name == className).Id;

                    List<StudentModel> lstStudentList = await StudentModel.GetStudent(_SelectedStandardID, _SelectedClassTypeID);

                    if (lstStudentList != null && lstStudentList.Count > 0)
                    {
                        slStudentNameLayout.IsVisible = true;
                        _NotAvailData.IsVisible = false;

                        foreach (StudentModel item in _StudentModelList)
                        {
                            pcrStudentName.Items.Add(item.Name);
                        }
                    }
                    else
                    {
                        _NotAvailData.Text = "There is no student for this class and standard";
                        _NotAvailData.IsVisible = true;
                    }
                    _Loader.IsShowLoading = false;

                });
            };

            Button btnSave = new Button { IsVisible = false };
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            pcrStudentName.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //btnSave.IsVisible = true;
                    string studentName = lblStudentName.Text = pcrStudentName.Items[pcrStudentName.SelectedIndex];

                    _SelectedStudentID = _StudentModelList.FirstOrDefault(x => x.Name == studentName).Id;

                    //Exam list call
                    slStudentNameLayout.IsVisible = true;
                });
            };

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;

                    StudentBehaviourNoticeModel studentBehaviourNoticeModel = new StudentBehaviourNoticeModel();
                    studentBehaviourNoticeModel.ClassTypeId = _SelectedClassTypeID;
                    studentBehaviourNoticeModel.StandardId = _SelectedStandardID;
                    studentBehaviourNoticeModel.StudentId = _SelectedStudentID;
                    studentBehaviourNoticeModel.Comment = txtComment.Text;

                    bool isSaveAttendance = await StudentBehaviourNoticeModel.SaveStudentBehaviour(studentBehaviourNoticeModel);

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

            StackLayout slStudentBehaviourNotice = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView,slSearchinOneCol, slSearchLayout,_Loader,cvBtnSave,_NotAvailData },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slStudentBehaviourNotice,
            };
        }
        #endregion
    }
}