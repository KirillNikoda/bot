using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Storage
{
    /// <summary>
    /// Определяет основные методы хранилища напоминаний
    /// </summary>
    public interface IReminderStorage
    {
        void Create(ReminderItem item);
        void Update(ReminderItem item);
        ReminderItem FindById(Guid id);
        List<ReminderItem> FindByDateTime(DateTimeOffset dateTime);
    }
}
