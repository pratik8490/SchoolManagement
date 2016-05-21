using SchoolManagement.Cells;
using SchoolManagement.Core.Context;
using SchoolManagement.Core.Models;
using SchoolManagement.Pages.Parents;
using SchoolManagement.Pages.Principal;
using SchoolManagement.Pages.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MenuItem = SchoolManagement.Core.Models.MenuItem;

namespace SchoolManagement.Pages.Master
{
    public class MenuListView : ListView
    {
        private int Index = 1;
        public MenuListView()
        {
            Device.BeginInvokeOnMainThread(async () =>
           {
               List<MenuItem> data = await SchoolManagement.Core.Models.MenuItem.GetMenu(Convert.ToInt32(SchoolManagementContext.TokenResponseModel.UserType));

               foreach (MenuItem item in data)
               {
                   item.ImageUrl = item.ImageUrl + ".png";
                   item.itemNumber = Index;
                   Index++;
               }

               ItemsSource = data;

               VerticalOptions = LayoutOptions.FillAndExpand;
               BackgroundColor = Color.Transparent;
               SeparatorColor = Color.FromHex("#5B5A5F");
              
               ItemTemplate = new DataTemplate(() => new TextItemCell());
               //var cell = new DataTemplate(typeof(MenuCell));
               //cell.SetBinding(MenuCell.TextProperty, "MobileName");
               //cell.SetBinding(MenuCell.ImageSourceProperty, "ImageUrl");
               //ItemTemplate = cell;
           });
        }
    }
    public class MenuCell : ImageCell
    {
        public MenuCell()
            : base()
        {
            this.TextColor = Color.FromHex("AAAAAA");
        }
    }

    public class MenuListData : List<SchoolManagement.Core.Models.MenuItem>
    {
        public MenuListData()
        {
            //if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Parents))
            //{
            //this.Add(new MenuItem { MobileName = "View Attendance", itemNumber = 1, TargetType = typeof(ViewAttendance), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "View Attendance Summary", itemNumber = 2, TargetType = typeof(ViewAttendanceSummary), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "Exam Timetable", itemNumber = 3, TargetType = typeof(ExamTimetable), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "View Result", itemNumber = 4, TargetType = typeof(ViewResult), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "View Compain", itemNumber = 5, TargetType = typeof(ViewCompain), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "Home Work", itemNumber = 6, TargetType = typeof(SchoolManagement.Pages.Parents.HomeWork), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "Notification", itemNumber = 7, TargetType = typeof(Notification), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "Compain Box", itemNumber = 8, TargetType = typeof(CompainBox), ImageUrl = "ApplyLeave.png" });
            //this.Add(new MenuItem { MobileName = "Log Out", itemNumber = 9, TargetType = typeof(Login), ImageUrl = "ApplyLeave.png" });
            //}
            //else if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Principal))
            //{
            //    this.Add(new MenuItem { MobileName = "Today's Timetable", itemNumber = 1, TargetType = typeof(TodaysTimetable), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Proxt Lecture", itemNumber = 2, TargetType = typeof(ProxtLecture), ImageUrl = "ApplyLeave.png" });
            //    //this.Add(new MenuItem { MobileName = "View Teacher Logbook", itemNumber = 3, TargetType = typeof(TeacherLogBook) });
            //    this.Add(new MenuItem { MobileName = "Live Camara", itemNumber = 3, TargetType = typeof(LiveCamara), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Dashboard", itemNumber = 4, TargetType = typeof(Dashboard), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Enquiry", itemNumber = 5, TargetType = typeof(Enquiry), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Admission", itemNumber = 6, TargetType = typeof(Admission), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Teacher Leave", itemNumber = 7, TargetType = typeof(TeacherLeave), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Log Out", itemNumber = 8, TargetType = typeof(Login), ImageUrl = "ApplyLeave.png" });
            //}
            //else if (SchoolManagementContext.TokenResponseModel.UserType == Convert.ToInt32(UserType.Teacher))
            //{
            //    this.Add(new MenuItem { MobileName = "FillUp Attendance", itemNumber = 1, TargetType = typeof(FillUpAttendance), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Howe Work", itemNumber = 2, TargetType = typeof(SchoolManagement.Pages.Teacher.HomeWork), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Student behaviour Notice", itemNumber = 3, TargetType = typeof(StudentBehaviourNotice), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Logbook Fillup", itemNumber = 4, TargetType = typeof(LogbookFillUp), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Apply Leave", itemNumber = 5, TargetType = typeof(ApplyLeave), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Today's Schedule", itemNumber = 6, TargetType = typeof(TodaysSchedule), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Enter Student Mark", itemNumber = 7, TargetType = typeof(EnterStudentMark), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Upload Sample Paper", itemNumber = 8, TargetType = typeof(UploadSamplePaper), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "View Activity / Notice", itemNumber = 9, TargetType = typeof(ViewActivityNotice), ImageUrl = "ApplyLeave.png" });
            //    this.Add(new MenuItem { MobileName = "Log Out", itemNumber = 10, TargetType = typeof(Login), ImageUrl = "ApplyLeave.png" });
            //}
        }
    }
}