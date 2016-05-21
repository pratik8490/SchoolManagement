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
    public class ViewComplain : BasePage
    {
        private LoadingIndicator _Loader;
        private List<ParentComplainModel> _ComplainList = new List<ParentComplainModel>();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Student Complain Page"/> class.
        /// </summary>
        public ViewComplain()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    //Get Data
                    _ComplainList = await ParentComplainModel.GetComplainList();
                    Items = new ObservableCollection<ParentComplainModel>(_ComplainList);
                    ViewStudnetComplainLayout();
                }
                catch (Exception ex)
                {

                }
            });

        }
        #endregion

        /// <summary>
        /// View Studnet Complain Layout.
        /// </summary>
        public void ViewStudnetComplainLayout()
        {

            TitleBar lblPageName = new TitleBar("View Complain");
            StackLayout slTitle = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 5, 0, 0),
                BackgroundColor = Color.White,
                Children = { lblPageName }
            };

            Seperator spTitle = new Seperator();

            Label lblNote = new Label
            {
                Text = "Note:Please click for more details.",
                TextColor = Color.Red,
                FontSize = 12
            };

            StackLayout slNote = new StackLayout
            {
                Children = { lblNote },
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            ListView studentComplainListView = new ListView
            {
                SeparatorColor = Color.FromHex("5B5A5F"),
                RowHeight = 40
            };

            studentComplainListView.ItemsSource = Items;
            studentComplainListView.ItemTemplate = new DataTemplate(() => new ViewComplainCell());

            studentComplainListView.ItemSelected += (sender, e) =>
            {
                //redirect to detail page
                if (e.SelectedItem == null) return;
                ParentComplainModel selectedItem = (ParentComplainModel)e.SelectedItem;

                //Navigation.PushAsync(App.StudentComplainDetailPage(selectedItem.Id));
            };

            Label lblComment = new Label
            {
                Text = "Comment",
                TextColor = Color.Black
            };

            StackLayout slComment = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { lblComment },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Label lblCommentedBy = new Label
            {
                Text = "User name",
                TextColor = Color.Black
            };

            StackLayout slCommentBy = new StackLayout
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Children = { lblCommentedBy },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slHeaderLayout = new StackLayout
            {
                Children = { slComment, slCommentBy },
                Orientation = StackOrientation.Horizontal,
            };

            Seperator spHeader = new Seperator();

            StackLayout slStudentComplain = new StackLayout
            {
                Children = { 
                    new StackLayout{
							Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView,slNote,slHeaderLayout, studentComplainListView },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Orientation = StackOrientation.Vertical
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slStudentComplain,
            };
        }

        #region Custom Property

        private ObservableCollection<ParentComplainModel> _items = new ObservableCollection<ParentComplainModel>();
        public ObservableCollection<ParentComplainModel> Items
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
