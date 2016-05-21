using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Helper.Controls
{
    public class TitleBar : ContentView
    {
        public TitleBar(string title)
        {
            Padding = new Thickness(5, 5, 5, 5);
            HorizontalOptions = LayoutOptions.StartAndExpand;
            Content = new Xamarin.Forms.Label
            {
                Text = title,
                FontSize = 15,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = LayoutHelper.TitleColor
            };
        }

        public TitleBar(string title, bool isClickable = false)
        {
            Padding = new Thickness(5, 5, 5, 5);
            HorizontalOptions = LayoutOptions.StartAndExpand;

            Xamarin.Forms.Label lblTitlebar = new Label();
            lblTitlebar.Text = title;
            lblTitlebar.FontSize = 15;

            if (isClickable)
            {
                lblTitlebar.TextColor = LayoutHelper.LinkColor;
            }
            else
            {
                lblTitlebar.TextColor = LayoutHelper.TitleColor;
            }

            Content = lblTitlebar;
        }

        public TitleBar(string title, int fontSize, Color textColor, Thickness padding)
        {
            Padding = padding;
            HorizontalOptions = LayoutOptions.StartAndExpand;
            Content = new Xamarin.Forms.Label
            {
                Text = title,
                FontSize = fontSize,
                TextColor = textColor
            };
        }
    }
}
