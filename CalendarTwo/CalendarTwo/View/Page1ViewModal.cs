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

        private void Initialize()
        {
            var db = App.Database;

            var table = db.GetItems();
            var dat = new List<DateTime>();
            foreach (var s in table)
            {
                dat.Add(s.Date);
            }
                
            var dates = new List<SpecialDate>();

            foreach(var d in dat)
            {
                Attendances = new ObservableCollection<SpecialDate>(dates) 
                {
                    new SpecialDate(d)
                    {
                         BackgroundColor = Color.Pink,
                         BorderColor = Color.Fuchsia,
                         BorderWidth = 8,
                         Selectable = true 
                    } 
                };
            }
           
        }
    }
}
