using System;
using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
    public class ReminderStorageTests
    {
        private ReminderItem GenerateMockItem(
            Guid? id = default,
            string contactId = default,
            string message = default,
            DateTimeOffset? messageDate = default,
            ReminderItemStatus? status = default)
        {
            if (!id.HasValue)
            {
                id = Guid.NewGuid();
            }

            if (contactId == null)
            {
                contactId = "123";
            }

            if (message == null)
            {
                message = "hello";
            }

            if (!status.HasValue)
            {
                status = ReminderItemStatus.Created;
            }

            if (!messageDate.HasValue)
            {
                messageDate = DateTimeOffset.Now;
            }

            return new ReminderItem(id.Value, contactId, message, messageDate.Value, status.Value);
        }

        [Test]
        public void WhenCreate_IfEmptyStorage_Should_FindItemById()
        {
            // Arrange
            var item = GenerateMockItem();
            var storage = new ReminderStorage();

            // Act
            storage.Create(item);

            // Assert
            ReminderItem resultItem = storage.FindById(item.Id);
            Assert.AreEqual(item.Id, resultItem.Id);

        }

        [Test]
        public void WhenCreate_IfNullSpecified_ShouldThrowException()
        {
            var storage = new ReminderStorage();

            Assert.Catch<ArgumentNullException>(() =>
                storage.Create(null));
        }

        [Test]
        public void WhenCreate_IfExistsElementWithKey_ShouldThrowException()
        {
            var item = GenerateMockItem();
            var storage = new ReminderStorage();

            storage.Create(item);

            Assert.Catch<ArgumentException>(() =>
                storage.Create(item));
        }

        [Test]
        public void WhenFindByDateTime_IfNullFilterSpecified_ShouldThrowException()
        {
            var storage = new ReminderStorage();

            Assert.Catch<ArgumentNullException>(() => storage.FindBy(default));
        }

        [Test]
        public void WhenFindByDateTime_IfStatusSpecified_ShouldFilterByStatus()
        {
            var storage = new ReminderStorage();
            var item = GenerateMockItem(status: ReminderItemStatus.Created);

            storage.Create(item);


            var items = storage.FindBy(
                new ReminderItemFilter().Created());


            Assert.IsNotEmpty(items);
        }

    }
}