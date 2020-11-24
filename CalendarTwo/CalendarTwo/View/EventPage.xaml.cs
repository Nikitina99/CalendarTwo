using CalendarTwo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarTwo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventPage : ContentPage
    {
        public EventPage()
        {
            InitializeComponent();
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            var events = (Event)BindingContext;

            string dateValue = events.Date.ToShortDateString();

            if (!String.IsNullOrEmpty(dateValue))
            {
                App.Database.SaveItem(events);
            }
            this.Navigation.PopAsync();
        }
        private void DeleteEvent(object sender, EventArgs e)
        {
            var events = (Event)BindingContext;
            App.Database.DeleteItem(events.Id);
            this.Navigation.PopAsync();
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}