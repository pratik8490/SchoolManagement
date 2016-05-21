using SchoolManagement.Helper;
using SchoolManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Util
{
    public class Global
    {
        public static double Pad = Format.Val(40);
        public static int ButtonSizeWidthLarge = 475;
        public static int ButtonSizeWidthMedium = 370;
        public static int ButtonSizeWidthSmall = 100;//251;
        public static int ButtonSizeHeight = 80;
        public static double ButtonWidth = Format.Val(ButtonSizeWidthMedium);
        public static double ButtonHeight = Format.Val(ButtonSizeHeight);

        public static Color PlaceHolderColor { get { return Color.FromRgb(140, 149, 151); } }

        public static StackLayout GetAcceptRejectStack()
        {
            var btnAccept = Global.GetButton("Accept", sz.Normal);
            var btnReject = Global.GetButton("Reject", sz.Normal);

            return new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Spacing = Global.Pad,
                Padding = Global.Pad,
                Children = {
					btnAccept,
					btnReject,
				}
            };
        }

        public static Button GetButton(string text, sz _sz)
        {
            int w = 0, d = 35;
            if (_sz == sz.Small)
            {
                w = Global.ButtonSizeWidthSmall;
                d = 30;
            }
            else if (_sz == sz.Normal)
            {
                w = Global.ButtonSizeWidthMedium;
                d = 30;
            }
            else if (_sz == sz.Large)
            {
                w = Global.ButtonSizeWidthLarge;
                d = 30;
            }

            return new Button
            {
                Text = text,
                WidthRequest = Format.Val(w + d),
                HeightRequest = ButtonHeight * Device.OnPlatform(1.5, 1, 1),
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }

    }
}
