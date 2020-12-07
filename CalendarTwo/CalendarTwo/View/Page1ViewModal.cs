using CalendarTwo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XamForms.Controls;
using SQLite;


namespace CalendarTwo.View
{
    public class Page1ViewModal : BaseViewModel
    {
        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }

        private ObservableCollection<SpecialDate> attendances;
        public ObservableCollection<SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; NotifyPropertyChanged(nameof(Attendances)); }
        }

        public ICommand DateChosen => new Command((obj) =>
        {
            System.Diagnostics.Debug.WriteLine(obj as DateTime?);
        });



        public Page1ViewModal()
        {
            Initialize();
        }

        private async void Initialize()
        {
            var db = App.Database;
            DateTime now = DateTime.Now;
            DateTime dateS  = new DateTime(2020, 01, 1);


            //Дата начала месячных
            var table = await db.GetItemsAsync();
            var dat = new List<DateTime>();
            var datMonth = new List<DateTime>();
            var datNovemder = new List<DateTime>();

            foreach (var s in table)
            {   if(s.Date > dateS)
                {
                    dat.Add(s.Date);
                }
            }
            
                    var startDate = new DateTime(2020,10,1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    for (int b = dat.Count-1; b > 0; b--)
                        {
                            if (dat[b] > startDate && dat[b] < endDate)
                            {
                                datMonth.Add(dat[b]);
                            }                        
                        }

            var startDateNovemder = new DateTime(2020, 11, 1);
            var endDatestartDateNovemder = startDateNovemder.AddMonths(1).AddDays(-1);

            for (int b = dat.Count-1; b > 0; b--)
            {
                if (dat[b] > startDateNovemder && dat[b] < endDatestartDateNovemder)
                {
                    datNovemder.Add(dat[b]);
                }
            }


            //Длительность мсч
            var count = new List<int?>();
            foreach (var s in table)
            {
                if(s.Count>0 && s.Count != null)
                {
                    count.Add(s.Count);
                }                
            }

            //Переодичность (для прогноза)
            var period = new List<int?>();
            foreach (var s in table)
            {
                if (s.Period > 0 && s.Period != null)
                {
                    period.Add(s.Period);
                }
            }


            var dates = new List<SpecialDate>();
            var num = count[count.Count - 1];
            var lastday = datMonth[0];
           

            //выводим сами мсч за октябрь
            for (int i=0 ; i<num; i++)
            {
                var d = lastday.AddDays(i);

                dates.Add(new SpecialDate(d)
                {
                    BackgroundColor = Color.Pink,
                    BorderColor = Color.Fuchsia,
                    BorderWidth = 4,
                    Selectable = true
                });
            }

            bool first = false;
            int index = 0;
            //выводим мсч за ноябрь
            foreach(var b in datNovemder)
            {
                if ((lastday - b).TotalDays <= -20 && first == false)
                {
                    lastday = b;
                    //выводим сами мсч
                    for (int i = 0; i < num; i++)
                    {
                        var d = lastday.AddDays(i);

                        dates.Add(new SpecialDate(d)
                        {
                            BackgroundColor = Color.Pink,
                            BorderColor = Color.Fuchsia,
                            BorderWidth = 4,
                            Selectable = true
                        });
                    }
                    first = true;
                }
            } 
                
          
            //выводим прогноз на 3 месяца
            int j = 0;
            while (j < 3)
            {
               DateTime d = new DateTime();
                var numPrognoz = period[period.Count - 1];
                for (int i = 0; i < num; i++)
                {
                    
                    d = lastday.AddDays((double)numPrognoz + i);

                    dates.Add(new SpecialDate(d)
                    {
                        BackgroundColor = Color.Pink,
                        BorderWidth = 4,
                        Selectable = true
                    });
                }
                Attendances = new ObservableCollection<SpecialDate>(dates);
                j++;
                lastday = d;
            }            
        }
    }
}
