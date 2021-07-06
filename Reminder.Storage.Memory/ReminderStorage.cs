using System;
using System.Collections.Generic;

namespace Reminder.Storage.Memory
{
    public class ReminderStorage : IReminderStorage
    {
        private readonly Dictionary<Guid, ReminderItem> _map = 
            new Dictionary<Guid, ReminderItem>() ;

        public void Create(ReminderItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_map.ContainsKey(item.Id))
            {
                throw new ArgumentException($"Element with id {item.Id} already exists");
            }

            _map[item.Id] = item;
        }

        public List<ReminderItem> FindByDateTime(DateTimeOffset dateTime)
        {
            throw new NotImplementedException();
        }

        public ReminderItem FindById(Guid id)
        {
            if (!_map.ContainsKey(id))
            {
                throw new ArgumentException($"Element with id {id} not found", nameof(id));
            }

            return _map[id];
        }

        public void Update(ReminderItem item)
        {
            throw new NotImplementedException();
        }
    }
}
