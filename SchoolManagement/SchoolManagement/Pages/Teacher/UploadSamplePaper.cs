using Plugin.Media;
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
    public class UploadSamplePaper : BasePage
    {
        private string _StartDate = DateTime.Now.ToLocalTime().AddMonths(-1).ToString("dd-MM-yy"); //Convert.ToDateTime("07-01-2015").ToString("dd-MM-yy");
        private List<StandardModel> _StandardList = new List<StandardModel>();
        private List<SubjectModel> _SubjectList = new List<SubjectModel>();
        private int _SelectedStandardID = 0, _SelectedSubjectID = 0;
        private Label _NotAvailData = new Label(), _StudentName, _IsPresent;
        private LoadingIndicator _Loader;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Upload Sample Paper Page"/> class.
        /// </summary>
        public UploadSamplePaper()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    _StandardList = await StandardModel.GetStandard();
                    UploadSamplePaperLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        #region UploadSamplePaperLayout
        /// <summary>
        /// Upload Sample Paper Layout.
        /// </summary>
        public void UploadSamplePaperLayout()
        {
            TitleBar lblPageName = new TitleBar("Upload Sample Paper");
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

            Image imgSubjectDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            Label lblSubject = new Label { TextColor = Color.Black, Text = "Subject" };
            Picker pcrSubject = new Picker { IsVisible = false, Title = "Subject" };

            StackLayout slSubjectDisplay = new StackLayout { Children = { lblSubject, pcrSubject, imgSubjectDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

            Frame frmSubject = new Frame
            {
                Content = slSubjectDisplay,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                OutlineColor = Color.Black,
                Padding = new Thickness(10)
            };

            var subjectTap = new TapGestureRecognizer();

            subjectTap.NumberOfTapsRequired = 1; // single-tap
            subjectTap.Tapped += (s, e) =>
            {
                pcrSubject.Focus();
            };
            frmSubject.GestureRecognizers.Add(subjectTap);
            slSubjectDisplay.GestureRecognizers.Add(subjectTap);

            StackLayout slSubjectFrmaeLayout = new StackLayout
            {
                Children = { frmSubject }
            };

            StackLayout slSubjectLayout = new StackLayout
            {
                Children = { slSubjectFrmaeLayout },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

            ExtendedEntry txtName = new ExtendedEntry
            {
                TextColor = Color.Black,
                Placeholder = "Name",
                Text = "Name"
            };

            StackLayout slNameLayout = new StackLayout
            {
                Children = { txtName },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Label lblUploadPaper = new Label
            {
                Text = "Upload Paper",
                TextColor = Color.Black
            };

            StackLayout slUploadPaper = new StackLayout
            {
                Children = { lblUploadPaper },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };

            var uploadTapGesture = new TapGestureRecognizer();
            uploadTapGesture.NumberOfTapsRequired = 1; // single-tap
            uploadTapGesture.Tapped += async (s, e) =>
            {
                var action = await DisplayActionSheet("Choose any one?", "Cancel", null, "Camera", "Gallery");

                Device.BeginInvokeOnMainThread(async () =>
                {

                    if (action == "Camera")
                    {
                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {
                            await DisplayAlert("No Camera", ":( No camera available.", "OK");
                            return;
                        }

                        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = txtName.Text + ".jpg"// "test.jpg"
                        });

                        if (file == null)
                            return;

                        //await ACT.Core.Providers.Parse.User.ImageSave(file.GetStream());
                    }
                    else if (action == "Gallery")
                    {
                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                            return;
                        }
                        var file = await CrossMedia.Current.PickPhotoAsync();

                        if (file == null)
                            return;

                        //Save image to parse database
                        //await ACT.Core.Providers.Parse.User.ImageSave(file.GetStream());
                    }
                });
            };

            StackLayout slSearchLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 0, 0, 10),
                Children = { slStandardLayout, slSubjectLayout, slNameLayout, lblUploadPaper }
            };

            _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

            _Loader = new LoadingIndicator();

            //Stanndard Picker Selected
            pcrStandard.SelectedIndexChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;

                    string standardName = lblStandard.Text = pcrStandard.Items[pcrStandard.SelectedIndex];

                    _SelectedStandardID = _StandardList.Where(x => x.Name == standardName).FirstOrDefault().Id;

                    //_SubjectList = await StudentModel.();

                    if (_SubjectList.Count > 0 && _SubjectList != null)
                    {
                        slSubjectFrmaeLayout.IsVisible = true;
                        _NotAvailData.IsVisible = false;
                    }
                    else
                    {
                        _NotAvailData.IsVisible = true;
                    }

                    foreach (SubjectModel item in _SubjectList)
                    {
                        pcrSubject.Items.Add(item.Name);
                    }

                    _Loader.IsShowLoading = false;
                });
            };

            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.TextColor = Color.White;
            btnSave.BackgroundColor = LayoutHelper.ButtonColor;

            btnSave.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _Loader.IsShowLoading = true;
                    bool isSaveAttendance = true;//await FillUpAttendanceModel.SaveStudentAttendance(Items);

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

            StackLayout slUplodSamplePaper = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, slSearchLayout,_Loader, _NotAvailData, cvBtnSave },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slUplodSamplePaper,
            };
        }
        #endregion
    }
}