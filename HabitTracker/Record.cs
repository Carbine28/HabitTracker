using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        public Record(int id, DateTime date, int quantity)
        {
            Id = id;
            Date = date;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Date: {Date}, Quantity: {Quantity}";
        }
    }
}
