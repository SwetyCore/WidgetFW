using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using static Model.AISchedule;

namespace TestPlugin.VM
{
    partial class AISchedule:ObservableObject
    {
        public AISchedule()
        {
            targetTime = DateTime.Now; 
        }

        private DateTime targetTime;

        [ObservableProperty]
        private List<CoursesItem> _cI;


        [ObservableProperty]
        private string week;



        [ObservableProperty]
        private string day;


        public Setting setting { get; set; }



        private bool IsIn(string[] a, string b)
        {
            Console.WriteLine(a);
            foreach (string a2 in a)
            {
                if (a2 == b) { return true; }
            }
            return false;
        }
        public int Week2Int(DayOfWeek w)
        {
            var a = Convert.ToInt32(w);
            if (a == 0)
            {
                return 7;
            }
            else
            {
                return a;
            }
        }

        [ObservableProperty]
        private string errMsg;

        [ObservableProperty]
        private Visibility errVi;

        public async void LoadTable()
        {
            try
            {
                string table = await File.ReadAllTextAsync("./Config/table.json");
                TableRoot tr = JsonConvert.DeserializeObject<TableRoot>(table);
                setting = tr.data.setting;

                Widgets.AISchedule.sections = JsonConvert.DeserializeObject<List<Widgets.AISchedule.sectionTime>>(setting.sectionTimes);
                
                var week = Update();

                CI = tr.data.courses.Where(x => IsIn(x.weeks.Split(','), week) && x.day == Week2Int(targetTime.DayOfWeek)).ToList();

                ErrMsg = "";
                ErrVi = Visibility.Collapsed;


            }
            catch (Exception ex)
            {
                
                ErrMsg = ex.Message;


                ErrVi = Visibility.Visible;

            }

        }
        public string Week2CN()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(targetTime.DayOfWeek)];

            return week;
        }
        private string Update()
        {
            string Start = setting.startSemester;
            DateTime start = StampToDateTime(Start);
            TimeSpan ts = targetTime - start;
            int week = (int)Math.Floor((double)ts.Days / 7) + 1;
            Week = $"第 {week} 周 ";
            Day = $"{Week2CN()}";
            return week.ToString();
        }

        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        [RelayCommand]
        private void Previous()
        {
            targetTime = targetTime.AddDays(-1);
            LoadTable();
        }

        [RelayCommand]
        private void Next()
        {
            targetTime = targetTime.AddDays(+1);
            LoadTable();
        }

    }
}
