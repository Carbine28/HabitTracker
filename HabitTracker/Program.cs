namespace HabitTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=habit-tracker.db";
            var CodeTracker = new HabbitTracker(connectionString);
            CodeTracker.Start();
        }
    }
}
