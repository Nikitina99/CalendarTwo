using SQLite;
using System;

namespace CalendarTwo.Model
{
    [Table("Event")]
    public class Event
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int? Count { get; set; }
        public int? Period { get; set; }
    }
}
