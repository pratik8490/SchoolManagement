using SchoolManagement.Core.Models;
using SchoolManagement.Helper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Pages.Parents
{
    public class BusPickupDrop : BasePage
    {
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BusPickupDrop"/> class.
        /// </summary>
        public BusPickupDrop()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    BusPickupDropLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Bus Pickup Drop Layout.
        /// </summary>
        public void BusPickupDropLayout()
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
