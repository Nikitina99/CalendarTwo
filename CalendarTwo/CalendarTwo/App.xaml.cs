using CalendarTwo.Repos;
using CalendarTwo.View;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarTwo
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "calendar.db";
        public static EventRepository database;
        public static EventRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new EventRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Page1());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
