using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class StudentHeadCountCell : ViewCell
    {
        private StudentHeadCountModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentHeadCountCell"/> class.
        /// </summary>
        public StudentHeadCountCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (StudentHeadCountModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateStudentHeadCountCellLayout();

            View = stack;
        }
        #endregion

        #region CreateStudentHeadCountLayout
        /// <summary>
        /// Create Student Head Count Layout.
        /// </summary>
        public StackLayout CreateStudentHeadCountCellLayout()
        {
            Label lblStandard = new Label
            {
                Text = model.StandardId + model.ClassTypeName,
                TextColor = Color.Black
            };

            StackLayout slStandard = new StackLayout
            {
                Children = { lblStandard },
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Start,
            };

            Label lblTotal = new Label
            {
                Text = model.Total.ToString(),
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            StackLayout slTotal = new StackLayout
            {
                Children = { lblTotal },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slGrid = new StackLayout
            {
                Children = { slStandard, slTotal },
                Orientation = StackOrientation.Horizontal
            };

            StackLayout slCategory = new StackLayout { Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand };

            for (int i = 0; i < model.StudentSummary.Count; i++)
            {
                Label lblCategoryName = new Label
                {
                    Text = model.StudentSummary[i].CategoryName,
                    TextColor = Color.Black
                };

                StackLayout slCategoryName = new StackLayout
                {
                    Children = { lblCategoryName },
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblCategoryTotal = new Label
                {
                    Text = model.StudentSummary[i].Total.ToString(),
                    TextColor = Color.Black
                };

                StackLayout slCategoryTotal = new StackLayout
                {
                    Children = { lblCategoryTotal },
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                StackLayout slCategoryNameTotal = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { slCategoryName, slCategoryTotal }
                };

                slCategory.Children.Add(slCategoryNameTotal);
            }

            StackLayout slFinalLayout = new StackLayout
            {
                Children = { slGrid, slCategory },
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            return slFinalLayout;
        }
        #endregion
    }
}

