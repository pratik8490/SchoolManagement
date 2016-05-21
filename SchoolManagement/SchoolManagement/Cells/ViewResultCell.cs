using SchoolManagement.Core.Models;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Cells
{
    public class ViewResultCell : ViewCell
    {
        private StudentMarksDetailModel model;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewResultCell"/> class.
        /// </summary>
        public ViewResultCell()
        {

        }
        #endregion

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (StudentMarksDetailModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateViewResultCellLayout();

            View = stack;
        }
        #endregion

        #region CreateViewResultLayout
        /// <summary>
        /// Create View Result Layout.
        /// </summary>
        public StackLayout CreateViewResultCellLayout()
        {

            Label lblSubjectName = new Label
            {
                Text = model.SubjectName,
                TextColor = Color.Black,
                FontSize = 14
            };

            StackLayout slSubjectName = new StackLayout
            {
                Children = { lblSubjectName },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblTotalMarks = new Label
            {
                Text = model.TotalMarks.ToString(),
                TextColor = Color.Black
            };

            StackLayout slTotalMarks = new StackLayout
            {
                Children = { lblTotalMarks },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblObtainMarks = new Label
            {
                Text = model.TotalObtainedMarks.ToString(),
                TextColor = Color.Black
            };

            Label lblContainsMark = new Label
            {
                Text = lblObtainMarks.Text + " Out of " + lblTotalMarks.Text,
                TextColor = Color.Black,
                FontSize = 14

            };

            StackLayout slObtainMarks = new StackLayout
            {
                Children = { lblContainsMark },
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            StackLayout slFirstLine = new StackLayout
            {
                Children = { slSubjectName, slObtainMarks },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Label lblExamDate = new Label
            {
                TextColor = Color.Black,
                Text = model.ExamDate.ToString("dd-MM-yy")
            };

            StackLayout slExamDate = new StackLayout
            {
                Children = { lblExamDate },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };


            Image imgIsPass = new Image();

            if (model.IsPass == 1)
            {
                imgIsPass.Source = FileImageSource.FromFile(Constants.ImagePath.PassIcon);
            }
            else
            {
                imgIsPass.Source = FileImageSource.FromFile(Constants.ImagePath.FailIcon);
            }

            StackLayout slIsPassLayout = new StackLayout
            {
                Children = { imgIsPass },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };


            StackLayout slSecondLine = new StackLayout
            {
                Children = { lblExamDate, slIsPassLayout },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            //StackLayout slGrid = new StackLayout
            //{
            //    Children = { slTotalMarks, slSubjectName, slObtainMarks, slIsPassLayout },
            //    Orientation = StackOrientation.Horizontal
            //};

            //var grid = new Grid { };
            //grid.Children.Add(slTotalMarks, 0, 0);
            //grid.Children.Add(slSubjectName, 1, 0);
            //grid.Children.Add(slObtainMarks, 2, 0);
            //grid.Children.Add(slIsPassLayout, 3, 0);

            StackLayout slFinalLayout = new StackLayout
            {
                Children = { slFirstLine, slSecondLine },
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            return slFinalLayout;
        }
        #endregion
    }
}

