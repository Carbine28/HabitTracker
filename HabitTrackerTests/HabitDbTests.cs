using HabitTracker;

namespace HabitTrackerTests
{
    [TestClass]
    public class HabitDbTests
    {
        [TestMethod]
        public void InsertAndRetreiveValueMatches()
        {
            // Arrange
            string connectionString = "Data Source=habit-tracker-test.db";
            var CodeTracker = new HabbitTracker(connectionString);
            var date = new DateTime(2021, 1, 1);
            var quantity = 1;
            var expected = new Record(1, date, quantity);
            // Act
            CodeTracker.AddRecord(date, quantity);
            var actual = CodeTracker.GetRecord(1);
            // Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}