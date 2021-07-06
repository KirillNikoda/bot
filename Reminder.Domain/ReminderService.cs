using System;
using System.Threading;
using Reminder.Storage;

namespace Reminder.Domain
{
    public class CreateReminderModel
    {
        public string ContactId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset MessageDate { get; set; }

        public CreateReminderModel(string contactId, string message, DateTimeOffset messageDate)
        {
            ContactId = contactId;
            Message = message;
            MessageDate = messageDate;
        }
    }

    public class ReminderService
    {
        private readonly Timer _timer;
        private readonly IReminderStorage _storage;

        public ReminderService(IReminderStorage storage)
        {
            _storage = storage;
            _timer = new Timer(OnTimerTick, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }
        
        public void Create(CreateReminderModel reminderModel)
        {
            var item = new ReminderItem(
                Guid.NewGuid(),
                reminderModel.ContactId,
                reminderModel.Message,
                reminderModel.MessageDate);
            
            _storage.Create(item);
        }

        private void OnTimerTick(object state) { }
    }

}
