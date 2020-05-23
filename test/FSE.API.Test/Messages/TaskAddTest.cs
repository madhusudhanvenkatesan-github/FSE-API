using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.Messages
{
    public class TaskAddTest
    {
        [Fact]
        public void GetterSetterTest()
        {
            var taskAdd = new TaskAdd
            {
                EndDate = DateTime.Today.AddDays(2),
                ParentTaskId = "P/1-1",
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskOwnerId = "Usr/1"
            };
            Assert.Equal(DateTime.Today.AddDays(2), taskAdd.EndDate);
            Assert.Equal("P/1-1", taskAdd.ParentTaskId);
            Assert.Equal(1, taskAdd.Priority);
            Assert.Equal(DateTime.Today, taskAdd.StartDate);
            Assert.Equal("Task1", taskAdd.TaskDescription);
            Assert.Equal("Usr/1", taskAdd.TaskOwnerId);
               
        }
    }
}
