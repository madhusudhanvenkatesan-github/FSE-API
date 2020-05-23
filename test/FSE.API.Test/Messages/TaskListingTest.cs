using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FSE.API.Test.Messages
{
   public class TaskListingTest
    {
        [Fact]
        public void GetterSetter()
        {
            var taskListing = new TaskListing
            {
                ParentTaskId = "P/1-1",
                ParentDescription = "ParentTask",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskOwnerId = "Usr/1",
                TaskId = "P/1-2",
                Status = 0,
                TaskOwnerName = "John"
            };
            Assert.Equal(DateTime.Today.AddDays(2), taskListing.EndDate);
            Assert.Equal("P/1-1", taskListing.ParentTaskId);
            Assert.Equal("ParentTask", taskListing.ParentDescription);
            Assert.Equal(1, taskListing.Priority);
            Assert.Equal(DateTime.Today, taskListing.StartDate);
            Assert.Equal("Task1", taskListing.TaskDescription);
            Assert.Equal("Usr/1", taskListing.TaskOwnerId);
            Assert.Equal("P/1-2", taskListing.TaskId);
            Assert.Equal("P/1", taskListing.ProjectId);
            Assert.Equal(0, taskListing.Status);
            Assert.Equal("John", taskListing.TaskOwnerName);
        }
    }
}
