using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class StudentExamMarksCell : ViewCell
    {
        private Student model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentExamMarksCell"/> class.
        /// </summary>
        public StudentExamMarksCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (Student)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateStudentExamMarksCellLayout();

            View = stack;
        }
        #endregion

        #region CreateFillUpAttendanceLayout
        /// <summary>
        /// Create Student ExamMarks Layout.
        /// </summary>
        public StackLayout CreateStudentExamMarksCellLayout()
        {
            Label lblStudent = new Label
            {
                Text = model.StudentId.ToString(),
                TextColor = Color.Black
            };

            StackLayout slStudent = new StackLayout
            {
                Children = { lblStudent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblAttendance = new Label
            {
                Text = model.AttendanceId.ToString(),
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            StackLayout slAttendance = new StackLayout
            {
                Children = { lblAttendance },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Switch swcIsPresent = new Switch();

            if (model.IsPresent == 1)
                swcIsPresent.IsToggled = true;
            else
                swcIsPresent.IsToggled = false;

            swcIsPresent.Toggled += (sender, e) =>
                {
                    if (e.Value)
                        model.IsPresent = 1;
                    else
                        model.IsPresent = 0;
                };

            StackLayout slIsPresentLayout = new StackLayout
            {
                Children = { swcIsPresent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slGrid = new StackLayout
            {
                Children = { slStudent, slAttendance, slIsPresentLayout },
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
