using System;
using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
    public class ReminderStorageTests
    {
        private ReminderItem GenerateMockItem()
        {
            return new ReminderItem(Guid.NewGuid(), "1", "Hello", DateTimeOffset.UtcNow);
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

        public void WhenCreate_IfExistsElementWithKey_ShouldThrowException()
        {
            var item = GenerateMockItem();
            var storage = new ReminderStorage();

            storage.Create(item);

            Assert.Catch<ArgumentException>(() =>
                storage.Create(item));
        }

    }
}