using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class ViewComplainCell : ViewCell
    {
        private ParentComplainModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComplainCell"/> class.
        /// </summary>
        public ViewComplainCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (ParentComplainModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreatViewComplainLayout();

            View = stack;
        }
        #endregion

        #region CreateViewComplainLayout
        /// <summary>
        /// View Complain Layout.
        /// </summary>
        public StackLayout CreatViewComplainLayout()
        {

            Label lblComment = new Label
            {
                Text = model.Message,
                TextColor = Color.Black
            };

            StackLayout slCommentLayout = new StackLayout
            {
                Children = { lblComment },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblUserName = new Label
            {
                Text = model.UserId.ToString(),
                TextColor = Color.Black
            };

            StackLayout slUserNameLayout = new StackLayout
            {
                Children = { lblUserName },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slFinalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { slCommentLayout, slUserNameLayout },
            };
            return slFinalLayout;
        }
        #endregion
    }
}
