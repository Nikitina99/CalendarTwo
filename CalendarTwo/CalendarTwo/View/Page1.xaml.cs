using CalendarTwo.Model;
using System;
using Xamarin.Forms;

namespace CalendarTwo.View
{
   
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            BindingContext = new Page1ViewModal();
        }

        // обработка нажатия кнопки добавления
        private async void CreateEvent(object sender, EventArgs e)
        {
            Event events = new Event();
            EventPage eventPage = new EventPage();
            eventPage.BindingContext = events;
            await Navigation.PushAsync(eventPage);
        }
    }
}