using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Helper.Controls
{
    public class LoadingIndicator : ActivityIndicator
    {
        public LoadingIndicator()
        {
            HeightRequest = 25;
            WidthRequest = 25;
            IsShowLoading = false;
        }

        public LoadingIndicator(int height, int width)
        {
            HeightRequest = height;
            WidthRequest = width;

            IsShowLoading = false;
        }

        private bool _isShowLoading;

        public bool IsShowLoading
        {
            get
            {
                return _isShowLoading;
            }
            set
            {
                _isShowLoading = value;
                ShowOrHide();
            }
        }

        public void ShowOrHide()
        {
            if (IsShowLoading)
            {
                IsRunning = true;
                IsVisible = true;
            }
            else
            {
                IsRunning = false;
                IsVisible = false;
            }
        }
    }
}
