using CalendarTwo.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarTwo.Repos
{
    public class EventRepository
    {
        SQLiteAsyncConnection database; 
        public EventRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<Event>();
        }

        public async Task<List<Event>> GetItemsAsync()
        {
            return await database.Table<Event>().ToListAsync();

        }

        public async Task<Event> GetItemAsync(int id)
        {
            return await database.GetAsync<Event>(id);
        }

        public async Task<int> SaveItemAsync(Event item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}
