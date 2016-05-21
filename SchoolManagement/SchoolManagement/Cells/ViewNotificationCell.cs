using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class ViewNotificationCell : ViewCell
    {
        private NotificationModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewNotificationCell"/> class.
        /// </summary>
        public ViewNotificationCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (NotificationModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateViewNotificationCellLayout();

            View = stack;
        }
        #endregion

        #region CreateViewNotificationLayout
        /// <summary>
        /// Create View Notification Layout.
        /// </summary>
        public StackLayout CreateViewNotificationCellLayout()
        {
            Label lblComment = new Label
            {
                Text = model.Comment,
                TextColor = Color.Black
            };

            StackLayout slComment = new StackLayout
            {
                Children = { lblComment },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblScheduleDate = new Label
            {
                Text = model.ScheduledDate.ToString("dd-MM-yy"),
                TextColor = Color.Black
            };

            StackLayout slScheduleDate = new StackLayout
            {
                Children = { lblScheduleDate },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slGrid = new StackLayout
            {
                Children = { slComment, slScheduleDate },
                Orientation = StackOrientation.Horizontal
            };

            //var grid = new Grid();
            //grid.Children.Add(slNo, 0, 0);
            //grid.Children.Add(slStudnetName, 1, 0);
            //grid.Children.Add(slIsPresentLayout, 2, 0);

            StackLayout slFinalLayout = new StackLayout
            {
                Children = { slGrid },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            return slFinalLayout;
        }
        #endregion
    }
}

