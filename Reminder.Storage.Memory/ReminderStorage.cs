using System;
using System.Linq;
using System.Collections.Generic;

namespace Reminder.Storage.Memory
{
    public class ReminderStorage : IReminderStorage
    {
        private readonly Dictionary<Guid, ReminderItem> _map =
            new Dictionary<Guid, ReminderItem>();

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

        public List<ReminderItem> FindBy(ReminderItemFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var result = _map.Values.AsEnumerable();

            if (filter.Status.HasValue)
            {
                result = result.Where(item => item.Status == filter.Status.Value);
            }

            if (filter.DateTime.HasValue)
            {
                result = result.Where(item => item.MessageDate == filter.DateTime.Value);
            }

            return result.ToList();
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
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _map[item.Id] = item;
        }
    }
}
