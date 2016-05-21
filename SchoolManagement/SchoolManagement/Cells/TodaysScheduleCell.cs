using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class TodaysScheduleCell : ViewCell
    {
        private Student model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TodaysScheduleCell"/> class.
        /// </summary>
        public TodaysScheduleCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (Student)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateTodaysScheduleCellLayout();

            View = stack;
        }
        #endregion

        #region CreateTodaysScheduleLayout
        /// <summary>
        /// Create Todays Schedule Layout.
        /// </summary>
        public StackLayout CreateTodaysScheduleCellLayout()
        {
            Label lblNo = new Label
            {
                //Text = model.IndexNo.ToString() + ")",
                TextColor = Color.Black
            };

            StackLayout slNo = new StackLayout
            {
                Children = { lblNo },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblStandard = new Label
            {
                //Text = model.Name,
                TextColor = Color.Black,
                XAlign = TextAlignment.Start
            };

            StackLayout slStandard = new StackLayout
            {
                Children = { lblStandard },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label lblSubject = new Label
            {
                //Text = model.Name,
                TextColor = Color.Black,
                XAlign = TextAlignment.Start
            };

            StackLayout slSubject = new StackLayout
            {
                Children = { lblSubject },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            Label lblLogBook = new Label
            {
                //Text = model.Name,
                TextColor = Color.Black,
                XAlign = TextAlignment.Start
            };

            StackLayout slLogBook = new StackLayout
            {
                Children = { lblLogBook },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slGrid = new StackLayout
            {
                Children = { slNo, slStandard, slSubject, slLogBook },
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
