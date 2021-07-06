using System;
using Reminder.Domain;
using Reminder.Storage.Memory;

namespace Reminder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Reminder Notifier].. starting");

            var storage = new ReminderStorage();
            var service = new ReminderService(storage);

            service.ReminderItemFired += OnReminderItemFired;
            service.Create(
                new CreateReminderModel("First", DateTimeOffset.UtcNow.AddMinutes(1)
                    )
                );

            service.Create(
               new CreateReminderModel("First", DateTimeOffset.UtcNow.AddMinutes(2)
                   )
               );


        }

        private static void OnReminderItemFired(object sender, ReminderModelEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
