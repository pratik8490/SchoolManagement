using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class ExamTimeTableCell : ViewCell
    {
        private ExamScheduleModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExamTimeTableCell"/> class.
        /// </summary>
        public ExamTimeTableCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (ExamScheduleModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateExamTimeTableCellLayout();

            View = stack;
        }
        #endregion

        #region CreateExamTimeTableLayout
        /// <summary>
        /// Create ExamTime Table Layout.
        /// </summary>
        public StackLayout CreateExamTimeTableCellLayout()
        {
            Label lblDateAndName = new Label
            {
                Text = model.Date.ToString("dd-MM-yy") + "," + model.ExamTypeName,
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 14

            };

            StackLayout slDateAndName = new StackLayout
            {
                Children = { lblDateAndName },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            //Label lblExamName = new Label
            //{
            //    Text = model.ExamTypeName,
            //    TextColor = Color.Black
            //};

            //StackLayout slExamName = new StackLayout
            //{
            //    Children = { lblExamName },
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.CenterAndExpand
            //};

            Label lblTotalMarksAndExamTime = new Label
            {
                Text = model.TotalMarks.ToString() + "," + model.ExamTime.ToString(),
                TextColor = Color.Black,
                FontSize = 12
            };

            StackLayout slTotalMarksAndExamTime = new StackLayout
            {
                Children = { lblTotalMarksAndExamTime },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            StackLayout slGrid = new StackLayout
            {
                Children = { slDateAndName, slTotalMarksAndExamTime },
                Orientation = StackOrientation.Vertical
            };

            //var grid = new Grid();
            //grid.Children.Add(slDateAndName, 0, 0);
            //grid.Children.Add(slExamName, 1, 0);
            //grid.Children.Add(slTotalMarksAndExamTime, 2, 0);

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

