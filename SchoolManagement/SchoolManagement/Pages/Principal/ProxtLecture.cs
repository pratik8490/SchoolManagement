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
    public class ProxtLecture : BasePage
    {
        private LoadingIndicator _Loader;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProxtLecture"/> class.
        /// </summary>
        public ProxtLecture()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    ProxtLectureLayout();
                }
                catch (Exception ex)
                {
                }
            });
        }
        #endregion

        /// <summary>
        /// Proxt Lecture
        /// </summary>
        public void ProxtLectureLayout()
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
