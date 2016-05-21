using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class ViewAttendanceCell : ViewCell
    {
        private ViewAttendanceModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAttendanceCell"/> class.
        /// </summary>
        public ViewAttendanceCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (ViewAttendanceModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateViewAttendanceCellLayout();

            View = stack;
        }
        #endregion

        #region CreateViewAttendanceLayout
        /// <summary>
        /// Create View Attendance Layout.
        /// </summary>
        public StackLayout CreateViewAttendanceCellLayout()
        {
            Label lblStudent = new Label
            {
                Text = model.StudentName.ToString(),
                TextColor = Color.Black
            };

            StackLayout slStudent = new StackLayout
            {
                Children = { lblStudent },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
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
                Children = { slStudent, slIsPresentLayout },
                Orientation = StackOrientation.Horizontal
            };

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
