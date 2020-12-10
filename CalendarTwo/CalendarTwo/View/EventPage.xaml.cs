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
      
        /// <summary>
        /// Метод сохранения
        /// </summary>
        private async void SaveEvent(object sender, EventArgs e)
        {
            var events = (Event)BindingContext;

            string dateValue = events.Date.ToShortDateString();

            if (events.Date < DateTime.UtcNow)
            {
                if (!String.IsNullOrEmpty(dateValue))
                {
                    App.Database.SaveItemAsync(events);
                }
            }
            await this.Navigation.PopAsync();
        }

        /// <summary>
        /// Метод отмены
        /// </summary>
        private void Cancel(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}