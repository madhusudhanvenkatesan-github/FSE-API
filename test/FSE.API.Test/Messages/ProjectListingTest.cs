using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.Messages
{
    public class ProjectListingTest
    {
        [Fact]
        public void GetterSetter()
        {
            var projectListing = new ProjectListing
            {
                EndDate = DateTime.Today.AddDays(2),
                StartDate = DateTime.Today,
                PMUsrId = "Usr/1",
                ProjectTitle = "Project A1",
                ProjId = "P/1",
                CompletedTaskCount = 0,
                PMUsrName = "John",
                TotalTaskCount = 3
            };
            Assert.Equal(DateTime.Today.AddDays(2), projectListing.EndDate);
            Assert.Equal(DateTime.Today, projectListing.StartDate);
            Assert.Equal("Usr/1", projectListing.PMUsrId);
            Assert.Equal("Project A1", projectListing.ProjectTitle);
            Assert.Equal("P/1", projectListing.ProjId);
            Assert.Equal(0, projectListing.CompletedTaskCount);
            Assert.Equal("John", projectListing.PMUsrName);
            Assert.Equal(3, projectListing.TotalTaskCount);
        }
    }
}
