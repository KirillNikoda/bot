using System;

namespace Reminder.Storage
{
    public class ReminderItemFilter
    {
        public DateTimeOffset? DateTime { get; private set; }
        public ReminderItemStatus? Status { get; private set; } 

        public ReminderItemFilter At(DateTimeOffset dateTime)
        {
            DateTime = dateTime;
            return this;
        }

        public ReminderItemFilter Created()
        {
            Status = ReminderItemStatus.Created;
            return this;
        }

        public ReminderItemFilter Ready()
        {
            Status = ReminderItemStatus.Ready;
            return this;
        }
    }
}
