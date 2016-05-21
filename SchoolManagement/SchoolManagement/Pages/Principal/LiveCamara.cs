using SchoolManagement.Core.Models;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Principal
{
    public class LiveCamara : BasePage
    {
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LiveCamara"/> class.
        /// </summary>
        public LiveCamara()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    LiveCamaraLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Live Camara
        /// </summary>
        public void LiveCamaraLayout()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
