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

        public Page1ViewModal()
        {
            Initialize();
        }

        private async void Initialize()
        {
            var db = App.Database;
            DateTime now = DateTime.Now;

            int monthStart = 0;
            int monthEnd = 0;

            //Дата начала месячных
            var table = await db.GetItemsAsync(); //вытаскиваем таблицу из БД.
            var dat = new List<DateTime>();       //Лист всех дат из БД     
            var datMonth = new List<DateTime>();  //Лист помесяцам.

            foreach (var s in table)
            {   if(s.Date > new DateTime(2020, 01, 1))
                {
                    dat.Add(s.Date);
                }
            }

            //Длительность менструации
            var count = new List<int?>();
            foreach (var s in table)
            {
                if (s.Count > 0 && s.Count != null)
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

            //Лист дат, которые будем окрашивать
            var dates = new List<SpecialDate>(); 
            //Длительность менструации
            var num = count[count.Count - 1];
            //Первый день наступления менструации
            DateTime firstday = dat[dat.Count-1];

            //Сортируемый лист храним в отдельном листе, чтобы не изменять порядок дат, пришедших из БД
            var datSort = dat;

            //Находим наибольшую и наименьшую дату, чтобы определить диапазон месяцев.
            datSort.Sort();
            monthStart = datSort[0].Month;
            monthEnd = datSort[datSort.Count-1].Month;

            //Обозначаем диапазон дат, для проверки принадлежности к месяцу.
            var dateStart = new DateTime(datSort[0].Year, datSort[0].Month, 1);
            var dateEnd = dateStart.AddMonths(1).AddDays(-1);

            //Находим число иттераций для цикла по месяцам.
            var monthItter = monthEnd - monthStart;

            //Собираем даты по месяцам
            for(int i = 0; i < monthItter; i++)
            {
                datMonth.Clear();

                //Заполняем лист с датами за 1 месяц
                for (int b = dat.Count - 1; b >= 0; b--)
                {
                    if (dat[b] > dateStart && dat[b] < dateEnd)
                    {
                        datMonth.Add(dat[b]);
                    }
                }
                //Получаем дату начала менструации
                firstday = datMonth[datMonth.Count-1];
                
                if(firstday<now)
                {
                    //Окрашиваем дни, когда шли месячные
                    for (int m = 0; m < num; m++)
                    {
                        var d = firstday.AddDays(m);

                        dates.Add(new SpecialDate(d)
                        {
                            BackgroundColor = Color.Pink,
                            BorderColor = Color.DarkRed,
                            BorderWidth = 2,
                            Selectable = true
                        });
                    }
                    Attendances = new ObservableCollection<SpecialDate>(dates);
                }

                
                dateStart = dateStart.AddMonths(1);
                dateEnd = dateStart.AddMonths(1).AddDays(-1);
                //Сохраним дату начала менструации для сравнения с прошлым началом менструации
                var predday = firstday;

                if (i==monthItter-1)
                {
                    //выводим прогноз на 3 месяца
                    int j = 0;
                    while (j < 3)
                    {
                        DateTime d = new DateTime();
                        var numPrognoz = period[period.Count - 1];
                        for (int p = 0; p < num; p++)
                        {
                            d = predday.AddDays((double)numPrognoz + p);

                            dates.Add(new SpecialDate(d)
                            {
                                BackgroundColor = Color.LightPink,
                                BorderColor = Color.LightCoral,
                                BorderWidth = 2,
                                Selectable = true
                            });
                        }
                        Attendances = new ObservableCollection<SpecialDate>(dates);
                        j++;
                        predday = d;
                    }
                }              
            }

            //Окрашиваем сегодняшний день для удобства
            dates.Add(new SpecialDate(now)
            {
                 BorderColor = Color.DarkGreen,
                 BorderWidth = 3,
                 Selectable = true
            });

            Attendances = new ObservableCollection<SpecialDate>(dates);
        }
    }
}
