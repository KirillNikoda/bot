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

        public CreateReminderModel(string message, DateTimeOffset messageDate)
        {
            ContactId = "contact";
            Message = message;
            MessageDate = messageDate;
        }
    }

    public class ReminderModelEventArgs : EventArgs
    {
        public string ContactId { get; set; }
        public string Message { get; set; }

        public ReminderModelEventArgs(ReminderItem item)
        {
            Message = item.Message;
            ContactId = item.ContactId;
        }
    }

    public class ReminderService : IDisposable
    {
        public event EventHandler<ReminderModelEventArgs> ReminderItemFired;

        private readonly Timer _createdItemTimer;
        private readonly Timer _readyItemTimer;

        private readonly IReminderStorage _storage;
        

        public ReminderService(IReminderStorage storage, ReminderServiceParameters parameters)
        {
            _storage = storage;

            _createdItemTimer = new Timer(
                OnCreatedItemTimerTick,
                null,
                parameters.CreateTimerDelay,
                parameters.CreateTimerInterval
               );

            _readyItemTimer = new Timer(
                OnReadyItemTimerTick,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(1)
               );
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

        private void OnCreatedItemTimerTick(object state)
        {
            var filter = new ReminderItemFilter()
                .At(DateTimeOffset.Now)
                .Created();

            var items = _storage.FindBy(filter);

            foreach (var item in items)
            {
                _storage.Update(item.ReadyToSend());
            }
        }
        private void OnReadyItemTimerTick(object state)
        {
            var filter = new ReminderItemFilter()
                .Ready();

            var items = _storage.FindBy(filter);

            foreach (var item in items)
            {
                OnReminderItemFired(item);
            }
        }

        public void OnReminderItemFired(ReminderItem item)
        {
            try
            {
                ReminderItemFired?.Invoke(this, new ReminderModelEventArgs(item));
                _storage.Update(item.Sent());
            }
            catch (Exception ex)
            {
                _storage.Update(item.Failed());
            }
        }

        public void Dispose()
        {
            _createdItemTimer.Dispose();
            _readyItemTimer.Dispose();
        }
    }

}
