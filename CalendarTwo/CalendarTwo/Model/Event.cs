using SQLite;
using System;

namespace CalendarTwo.Model
{
    [Table("Event")]
    public class Event
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        /// <summary>
        /// Дата начала менструации
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Длительность менструации
        /// </summary>
        public int? Count { get; set; }
        /// <summary>
        /// Переодичность менструации
        /// </summary>
        public int? Period { get; set; }
    }
}
