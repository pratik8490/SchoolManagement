using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using SchoolManagement.Pages;
using SchoolManagement.Pages.Parents;
using SchoolManagement.Pages.Principal;
using SchoolManagement.Pages.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SchoolManagement
{
    public class App : Application
    {
        static NavigationPage navPage;
        public static Color BarTextColor()
        {
            return Color.White;
        }
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            MainPage = LoginPage();
        }
        #endregion

        #region Account Pages
        /// <summary>
        /// Login Page.
        /// </summary>
        public static Page LoginPage()
        {
            return new Login();
        }
        #endregion

        public static Page MainDashboardPage()
        {
            //return new DashboardPage();
            navPage = new NavigationPage(new MasterPage(new DashboardPage()));
            navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
            navPage.BarTextColor = LayoutHelper.BarBackTextColor;
            return navPage;
        }

        #region Parents
        /// <summary>
        /// ViewAttendance Page.
        /// </summary>
        public static Page ViewAttendance()
        {
            //navPage = new NavigationPage(new MasterPage(new ViewAttendance()));
            //navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
            //navPage.BarTextColor = LayoutHelper.BarBackTextColor;
            //return navPage;
            return new MasterPage(new ViewAttendance());
        }
        /// <summary>
        /// BusPickupDrop Page.
        /// </summary>
        public static Page BusPickupDrop()
        {
            return new MasterPage(new BusPickupDrop());
        }
        /// <summary>
        /// CompainBox Page.
        /// </summary>
        public static Page CompainBox()
        {
            return new MasterPage(new ComplainBox());
        }
        /// <summary>
        /// DigitalLibrary Page.
        /// </summary>
        public static Page DigitalLibrary()
        {
            return new MasterPage(new SchoolManagement.Pages.Parents.DigitalLibrary());
        }
        /// <summary>
        /// ExamTimetable Page.
        /// </summary>
        public static Page ExamTimetable()
        {
            return new MasterPage(new ExamTimetable());
        }
        /// <summary>
        /// HomeWork Page.
        /// </summary>
        public static Page HomeWork()
        {
            return new MasterPage(new SchoolManagement.Pages.Parents.HomeWork());
        }
        /// <summary>
        /// Notification Page.
        /// </summary>
        public static Page Notification()
        {
            return new MasterPage(new Notification());
        }

        /// <summary>
        /// ViewAttendanceSummary Page.
        /// </summary>
        public static Page ViewAttendanceSummary()
        {
            return new MasterPage(new ViewAttendanceSummary());
        }
        /// <summary>
        /// ViewCompain Page.
        /// </summary>
        public static Page ViewCompain()
        {
            return new MasterPage(new ViewComplain());
        }
        /// <summary>
        /// ViewResult Page.
        /// </summary>
        public static Page ViewResult()
        {
            return new MasterPage(new ViewResult());
        }
        /// <summary>
        /// ViewUploaded Page.
        /// </summary>
        public static Page ViewUploaded()
        {
            return new MasterPage(new ViewUploaded());
        }
        #endregion

        #region Principal
        /// <summary>
        /// Admission Page
        /// </summary>
        public static Page Admission()
        {
            navPage = new NavigationPage(new MasterPage(new Admission()));
            navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
            navPage.BarTextColor = LayoutHelper.BarBackTextColor;
            return navPage;
        }
        /// <summary>
        /// Dashboard Page
        /// </summary> 
        public static Page Dashboard()
        {
            return new MasterPage(new Dashboard());
        }
        /// <summary>
        /// Enquiry Page
        /// </summary> 
        public static Page Enquiry()
        {
            return new MasterPage(new Enquiry());
        }
        /// <summary>
        /// LiveCamara Page
        /// </summary> 
        public static Page LiveCamara()
        {
            return new MasterPage(new LiveCamara());
        }
        /// <summary>
        /// ProxtLecture Page
        /// </summary> 
        public static Page ProxtLecture()
        {
            return new MasterPage(new ProxtLecture());
        }
        /// <summary>
        /// TeacherLeave Page
        /// </summary> 
        public static Page TeacherLeave()
        {
            return new MasterPage(new TeacherLeave());
        }
        /// <summary>
        /// TodaysTimetable Page
        /// </summary> 
        public static Page TodaysTimetable()
        {
            return new MasterPage(new TodaysTimetable());
        }
        /// <summary>
        /// ViewTeacherLogbook Page
        /// </summary> 
        public static Page ViewTeacherLogbook()
        {
            return new MasterPage(new ViewTeacherLogbook());
        }
        /// <summary>
        /// HeadCount Page
        /// </summary> 
        public static Page ViewHeadCountPage()
        {
            return new MasterPage(new HeadCount());
        }
        #endregion

        #region Teacher
        /// <summary>
        /// FillUpAttendance Page
        /// </summary>
        public static Page FillUpAttendance()
        {
            //navPage = new NavigationPage(new MasterPage(new FillUpAttendance()));
            //navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
            //navPage.BarTextColor = LayoutHelper.BarBackTextColor;
            //return navPage;
            return new MasterPage(new FillUpAttendance());
        }
        /// <summary>
        /// DigitalLibrary Page
        /// </summary>
        public static Page DigitalLibraryPage()
        {
            return new MasterPage(new SchoolManagement.Pages.Teacher.DigitalLibrary());
        }
        /// <summary>
        /// EnterStudentMark Page
        /// </summary>
        public static Page EnterStudentMark()
        {
            return new MasterPage(new EnterStudentMark());
        }
        /// <summary>
        /// ApplyLeave Page
        /// </summary>
        public static Page ApplyLeave()
        {
            return new MasterPage(new ApplyLeave());
        }
        /// <summary>
        /// HomeWork Page
        /// </summary>
        public static Page HomeWorkPage()
        {
            return new MasterPage(new SchoolManagement.Pages.Teacher.HomeWork());
        }
        /// <summary>
        /// LogbookFillup Page
        /// </summary>
        public static Page LogbookFillup()
        {
            return new MasterPage(new LogbookFillUp());
        }
        /// <summary>
        /// StudentBehaviourNotice Page
        /// </summary>
        public static Page StudentBehaviourNotice()
        {
            return new MasterPage(new StudentBehaviourNotice());
        }
        /// <summary>
        /// TodaysSchedule Page
        /// </summary>
        public static Page TodaysSchedule()
        {
            return new MasterPage(new TodaysSchedule());
        }
        /// <summary>
        /// UploadSamplePaper Page
        /// </summary>
        public static Page UploadSamplePaper()
        {
            return new MasterPage(new UploadSamplePaper());
        }
        /// <summary>
        /// ViewActivityNotice Page
        /// </summary>
        public static Page ViewActivityNotice()
        {
            return new MasterPage(new ViewActivityNotice());
        }
        #endregion
    }
}
