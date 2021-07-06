using System;

namespace Reminder.Storage
{

    public class ReminderItem
    {
        public Guid Id { get; }
        public string ContactId { get; private set; }
        public ReminderItemStatus Status { get; private set; }
        public string Message { get; private set; }
        public DateTimeOffset MessageDate { get; private set; }
        public ReminderItem(Guid id,
            string contactId,
            string message,
            DateTimeOffset messageDate,
            ReminderItemStatus status = ReminderItemStatus.Created)
        {
            if (id == default)
            {
                throw new ArgumentException("Identifier is empty", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(contactId))
            {
                throw new ArgumentException("Identifier of contact is empty", nameof(contactId));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message is empty", nameof(message));
            }

            if (messageDate == default)
            {
                throw new ArgumentException("Message date is empty", nameof(messageDate));
            }

            Id = id;
            ContactId = contactId;
            Message = message;
            MessageDate = messageDate;
            Status = status;
        }

   

        public ReminderItem ReadyToSend()
        {
            if (Status != ReminderItemStatus.Created)
            {
                throw new InvalidOperationException("Incorrect status");
            }

            Status = ReminderItemStatus.Ready;
            return this;
        }

        public ReminderItem Sent()
        {
            if (Status != ReminderItemStatus.Ready)
            {
                throw new InvalidOperationException("Incorrect status");
            }

            Status = ReminderItemStatus.Sent;
            return this;
        }

        public ReminderItem Failed()
        {
            if (Status != ReminderItemStatus.Ready)
            {
                throw new InvalidOperationException("Incorrect status");
            }

            Status = ReminderItemStatus.Failure;
            return this;
        }
    }
}




