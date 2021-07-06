using System;
using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
    public class ReminderStorageTests
    {
        private ReminderItem GenerateMockItem(DateTimeOffset dateTime = default)
        {
            return new ReminderItem(Guid.NewGuid(), "1", "Hello", dateTime == default ? DateTimeOffset.UtcNow : dateTime);
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
        public void WhenFindByDateTime_IfEmptyDateTimeSpecified_ShouldThrowException()
        {
            var storage = new ReminderStorage();

            Assert.Catch<ArgumentException>(() => storage.FindByDateTime(default));
        }

        [Test]
        public void WhenFindByDateTime_IfSpecifiedDateTime_ShouldFilterById()
        {
            var storage = new ReminderStorage();
            var dateTime = DateTimeOffset.Parse("12.11.2021 14:28:00.120");
            var item = GenerateMockItem(dateTime);

            storage.Create(item);
   

            var items = storage.FindByDateTime(dateTime);
        

            Assert.IsNotEmpty(items);
        }

    }
}