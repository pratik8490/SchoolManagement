using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MenuItem = SchoolManagement.Core.Models.MenuItem;

namespace SchoolManagement.Cells
{
    public class TextItemCell : ViewCell
    {
        private MenuItem model;

        #region Override methods
        protected override void OnBindingContextChanged()
        {
            model = (MenuItem)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CategoryStack();

            View = stack;
        }
        #endregion

        public StackLayout CategoryStack()
        {

            Label lblText = new Label();
            lblText.Text = model.MobileName;
            lblText.TextColor = Color.Black;

            StackLayout slTitle = new StackLayout
            {
                Children = { lblText },
                Padding = new Thickness(20, 10, 0, 10),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Image imgMobile = new Image();
            imgMobile.Source = FileImageSource.FromFile(model.ImageUrl);

            StackLayout slImage = new StackLayout
            {
                Children = { imgMobile },
                Padding = new Thickness(0, 10, 0, 10),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout itemTextLayout = new StackLayout
            {
                Spacing = 10,
                Children = {
                            slImage,
                            slTitle
                           },
                Orientation = StackOrientation.Horizontal
            };

            StackLayout cellLayout = new StackLayout
            {

            };

            cellLayout.Children.Add(itemTextLayout);

            return cellLayout;
        }
    }
}
