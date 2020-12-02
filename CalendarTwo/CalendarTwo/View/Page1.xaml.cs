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

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateTable();
            // привязка данных
            BindingContext = new Page1ViewModal();
            base.OnAppearing();
        }

        // обработка нажатия кнопки добавления
        private async void CreateEvent(object sender, EventArgs e)
        {
            Event events = new Event();
            EventPage eventPage = new EventPage()
            {
                DebugId = Guid.NewGuid()
            };
            eventPage.BindingContext = events;
            await Navigation.PushAsync(eventPage);
        }
    }
}
