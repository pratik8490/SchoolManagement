using SchoolManagement.Helper.Extension;
using SchoolManagement.Core.Models;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SchoolManagement.Helper;
using SchoolManagement.Cells;

namespace SchoolManagement.Pages.Parents
{
    public class Notification : BasePage
    {
        private LoadingIndicator _Loader;
        private Label _NotAvailData = new Label();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExamTimetable"/> class.
        /// </summary>
        public Notification()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    List<NotificationModel> lstNotification = await NotificationModel.GetNotification();
                    NotificationkLayout();
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
        public void NotificationkLayout()
        {
            try
            {
                TitleBar lblPageName = new TitleBar("View Notifications");
                StackLayout slTitle = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 0),
                    BackgroundColor = Color.White,
                    Children = { lblPageName }
                };

                Seperator spTitle = new Seperator();

                _NotAvailData = new Label { Text = "No data availalble for this search data.", TextColor = Color.Red, IsVisible = false };

                _Loader = new LoadingIndicator();

                ListView ViewNotificationListView = new ListView
                {
                    RowHeight = 50,
                    SeparatorColor = Color.Gray
                };

                ViewNotificationListView.ItemsSource = Items;
                ViewNotificationListView.ItemTemplate = new DataTemplate(() => new ViewNotificationCell());

                if (Items != null && Items.Count > 0)
                {
                    _NotAvailData.IsVisible = true;
                }
                else
                {
                    _NotAvailData.IsVisible = false;
                }

                //Grid Header Layout
                Label lblComment = new Label
                {
                    Text = "Comment",
                    TextColor = Color.Black
                };

                StackLayout slComment = new StackLayout
                {
                    Children = { lblComment },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblScheduledDate = new Label
                {
                    Text = "Schedule Date",
                    TextColor = Color.Black
                };

                StackLayout slScheduleDate = new StackLayout
                {
                    Children = { lblScheduledDate },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                StackLayout grid = new StackLayout
                                {
                                    Children = { slComment, slScheduleDate },
                                    Orientation = StackOrientation.Horizontal,
                                    IsVisible = false
                                };

                Seperator spDisplayHeader = new Seperator();

                StackLayout slViewNotifications = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slTitle, spTitle.LineSeperatorView, grid,spDisplayHeader.LineSeperatorView, _Loader, _NotAvailData,ViewNotificationListView },
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slViewNotifications,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Custom Property

        private ObservableCollection<NotificationModel> _items = new ObservableCollection<NotificationModel>();
        public ObservableCollection<NotificationModel> Items
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
