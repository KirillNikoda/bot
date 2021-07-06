using System;
using System.Collections.Generic;


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
        List<ReminderItem> FindBy(ReminderItemFilter filter);
    }
}
