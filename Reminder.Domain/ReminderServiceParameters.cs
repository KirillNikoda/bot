using System;

namespace Reminder.Domain
{
    public class ReminderServiceParameters
    {

        public TimeSpan CreateTimerInterval { get; } =
            TimeSpan.FromSeconds(1);
        public TimeSpan CreateTimerDelay { get; } =
            TimeSpan.Zero;
        public TimeSpan ReadyTimerInterval { get; } =
            TimeSpan.FromSeconds(1);
        public TimeSpan ReadyTimerDelay { get; } =
            TimeSpan.FromSeconds(1);

    }

}
