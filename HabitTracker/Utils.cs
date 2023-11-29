namespace HabitTracker
{
    public static class Utils
    {
        public static DateTime GetDateFromUser()
        {
            DateTime userDate;
            while (true)
            {
                Console.WriteLine("Enter in date in the format (dd/M/yy): ");
                string userInput = Console.ReadLine();
                try
                {
                    userDate = DateTime.ParseExact(userInput, "dd/M/yy", null);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid date format. Please enter the date in the format (dd/M/yy).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An Error occured: {ex.Message}");
                }
            }
            return userDate;
        }
    }
}
