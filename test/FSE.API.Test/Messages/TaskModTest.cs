using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.Messages
{
    public class TaskModTest
    {
        [Fact]
        public void GetterSetterTest()
        {
            var takMod = new TaskMod
            {
                EndDate = DateTime.Today.AddDays(2),
                ParentTaskId = "P/1-1",
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskOwnerId = "Usr/1",
                TaskId = "P/1-2"
            };
            Assert.Equal(DateTime.Today.AddDays(2), takMod.EndDate);
            Assert.Equal("P/1-1", takMod.ParentTaskId);
            Assert.Equal(1, takMod.Priority);
            Assert.Equal(DateTime.Today, takMod.StartDate);
            Assert.Equal("Task1", takMod.TaskDescription);
            Assert.Equal("Usr/1", takMod.TaskOwnerId);
            Assert.Equal("P/1-2", takMod.TaskId);
        }
    }
}
