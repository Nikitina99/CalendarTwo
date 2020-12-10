using CalendarTwo.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;

namespace CalendarTwo.Repos
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class EventRepository
    {
        SQLiteAsyncConnection database; 
        public EventRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        /// <summary>
        /// Создание таблицы в БД
        /// </summary>\
        public async Task CreateTable()
        {
            await database.CreateTableAsync<Event>();
        }

        /// <summary>
        /// Получить всю таблицу 
        /// </summary>
        public async Task<List<Event>> GetItemsAsync()
        {
            return await database.Table<Event>().ToListAsync();
        }

        /// <summary>
        /// Получить строку в таблице
        /// </summary>
        public async Task<Event> GetItemAsync(int id)
        {
            return await database.GetAsync<Event>(id);
        }

        /// <summary>
        /// Сохранение события
        /// </summary>
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
