using NUnit.Framework;
using Reminder.Storage.Memory;
using System;

namespace Reminder.Domain.Tests
{
    public class RemiderServiceTests
    {
        [Test]
        public void Test1()
        {
            var storage = new ReminderStorage();
            var parameters = new ReminderServiceParameters();
            var service = new ReminderService(storage, parameters);
            var eventRaised = false;

            service.ItemSent += (sender, args) => eventRaised = true;
            service.Create(CreateReminderModel());


            Assert.Fail();
        }

        private CreateReminderModel CreateReminderModel(
            string message = default,
            DateTimeOffset dateTime = default)
        {
            if (message == default)
            {
                message = "Message";
            }

            if (dateTime == default)
            {
                dateTime = DateTimeOffset.UtcNow;
            }

            return new CreateReminderModel(message, dateTime);
        }
    }
}