using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public enum Commands
    {
        Close = 0,
        List = 1,
        Add = 2,
        Delete = 3,
        Update = 4,
        Invalid = 5
    }
    public class HabbitTracker
    {
        private string ConnectionString { get; set; }   
        public HabbitTracker(string connectionString)
        {
            ConnectionString = connectionString;
            SetupDB(ConnectionString);
        }

        public static void SetupDB(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS code_habbit (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DATE TEXT,
                        QUANTITY INTEGER
                )";

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void PrintCommands()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Type 0 to Close Application.");
            Console.WriteLine("Type 1 to List all Records.");
            Console.WriteLine("Type 2 to Add a new Record.");
            Console.WriteLine("Type 3 to Delete a Records.");
            Console.WriteLine("Type 4 to Update a Records.");
            Console.WriteLine("----------------------------");
        }

        public void Start()
        {
            Commands userInput = Commands.Invalid;
            while(userInput != Commands.Close)
            {

                PrintCommands();
                userInput = GetUserInput();
                Console.Clear();
                if(userInput != Commands.Invalid)
                {
                    switch (userInput)
                    {
                        case Commands.Close:
                            break;
                        case Commands.List:
                            ListRecords();
                            break;
                        case Commands.Add:
                            AddRecord();
                            break;
                        case Commands.Delete:
                            DeleteRecord();
                            break;
                        case Commands.Update:
                            UpdateRecord();
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.WriteLine("Closing Tracker.");
        }

        public void UpdateRecord()
        {
            ListRecords();
            Console.WriteLine("Enter in Id of Record to Update: ");
            int id;
            while(!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            try
            {
                Record recordToUpdate = GetRecord(id);
                Console.WriteLine($"Old Record: {recordToUpdate}");
                DateTime date = Utils.GetDateFromUser();
                Console.WriteLine("Enter new Quantity: ");
                int quantity;
                while(!int.TryParse(Console.ReadLine(), out quantity))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                recordToUpdate.Date = date;
                recordToUpdate.Quantity = quantity;

                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE code_habbit SET DATE = $date, QUANTITY = $quantity WHERE Id = $id";
                    cmd.Parameters.AddWithValue("$date", date.ToString("dd/MM/yy"));
                    cmd.Parameters.AddWithValue("$quantity", quantity);
                    cmd.Parameters.AddWithValue("$id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine($"Updated Record: {recordToUpdate}");
                }

            } catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }
        }
        public Record GetRecord(int id)
        {
            Console.WriteLine("Getting Record...");
            using(var connecton = new SqliteConnection(ConnectionString))
            {
                connecton.Open();
                var cmd = connecton.CreateCommand();
                cmd.CommandText = "SELECT * FROM code_habbit WHERE Id = $id";
                cmd.Parameters.AddWithValue("$id", id);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    int idFromDB = reader.GetInt32(0);
                    string date = reader.GetString(1);
                    int quantity = reader.GetInt32(2);
                    if (idFromDB != id)
                    {
                        connecton.Close();
                        throw new Exception("Record not found.");
                    }
                    connecton.Close();
                    return new Record(idFromDB, DateTime.ParseExact(date, "dd/MM/yy", null), quantity);
                }
                connecton.Close();
            }   
            throw new Exception("Record not found.");
        }

        public void DeleteRecord()
        {
            ListRecords();
            Console.WriteLine("Enter Id of Record to Delete: ");
            int id;
            while(!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }

            try
            {
                Record recordToDelete = GetRecord(id);
                Console.WriteLine($"Deleting Record: {recordToDelete}");
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM code_habbit WHERE Id = $id";
                    cmd.Parameters.AddWithValue("$id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Record Deleted.");
            ListRecords();
        }

        public void AddRecord()
        {
            Console.WriteLine("Enter Date: ");
            DateTime date = Utils.GetDateFromUser();
            Console.WriteLine("Enter Coding Quantity: ");
            int quantity;
            while(!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            Console.Clear();
            Console.WriteLine($"Inserting new record with date:{date} and quantity:{quantity} ");
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO code_habbit (DATE, QUANTITY) VALUES ($date, $quantity)";
                cmd.Parameters.AddWithValue("$date", date.ToString("dd/MM/yy"));
                cmd.Parameters.AddWithValue("$quantity", quantity);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddRecord(DateTime date, int quantity)
        {
            Console.WriteLine($"Inserting new record with date:{date} and quantity:{quantity} ");
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO code_habbit (DATE, QUANTITY) VALUES ($date, $quantity)";
                cmd.Parameters.AddWithValue("$date", date.ToString("dd/MM/yy"));
                cmd.Parameters.AddWithValue("$quantity", quantity);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ListRecords()
        {
            Console.WriteLine("Listing Records...");
            Console.WriteLine("-------------------------------------");
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM code_habbit";
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine($"Id: {reader.GetInt32(0)} | Date: {reader.GetString(1)} | Quantity: {reader.GetInt32(2)}");
                }
                connection.Close();
            }
            Console.WriteLine("-------------------------------------");
        }

        public Commands GetUserInput()
        {
            Console.WriteLine("Enter in input: ");
            int input;
            while(!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }

            switch (input)
            {
                case 0:
                    return Commands.Close;
                case 1:
                    return Commands.List;
                case 2:
                    return Commands.Add;
                case 3:
                    return Commands.Delete;
                case 4:
                    return Commands.Update;
                default:
                    Console.WriteLine("Invalid Input. Please try again.");
                    return Commands.Invalid;
            }
        }

    }
}
