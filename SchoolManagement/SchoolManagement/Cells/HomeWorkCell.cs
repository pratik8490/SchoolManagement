using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class HomeWorkCell : ViewCell
    {
        private TeacherLogBookModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeWorkCell"/> class.
        /// </summary>
        public HomeWorkCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (TeacherLogBookModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreatViewHomeWorkLayout();

            View = stack;
        }
        #endregion

        #region CreateHomeWorkLayout
        /// <summary>
        /// View HomeWork Layout.
        /// </summary>
        public StackLayout CreatViewHomeWorkLayout()
        {

            Label lblHomeWork = new Label
            {
                Text = model.HomeWork,
                TextColor = Color.Black
            };

            StackLayout slHomeWorkLayout = new StackLayout
            {
                Children = { lblHomeWork },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblComment = new Label
            {
                Text = model.Comment.ToString(),
                TextColor = Color.Black
            };

            StackLayout slCommentLayout = new StackLayout
            {
                Children = { lblComment },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slFinalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { slHomeWorkLayout, slCommentLayout },
            };
            return slFinalLayout;
        }
        #endregion
    }
}
