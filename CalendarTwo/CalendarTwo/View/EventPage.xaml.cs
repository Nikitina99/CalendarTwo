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

        public Guid DebugId { get; set; }
        public Page1 CalendarPage { get; set; }

        private async void SaveEvent(object sender, EventArgs e)
        {
            var events = (Event)BindingContext;

            string dateValue = events.Date.ToShortDateString();

            if (!String.IsNullOrEmpty(dateValue))
            {
                App.Database.SaveItemAsync(events);
            }
            await this.Navigation.PopAsync();

        }
        private async void DeleteEvent(object sender, EventArgs e)
        {
            var events = (Event)BindingContext;
            App.Database.DeleteItemAsync(events);
            await this.Navigation.PopAsync();
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}