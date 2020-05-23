using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.Messages
{
    public class ProjectModTest
    {
        [Fact]
        public void GettSetter()
        {
            var projectMod = new ProjectMod
            {
                EndDate = DateTime.Today.AddDays(2),
                StartDate = DateTime.Today,
                PMUsrId = "Usr/1",
                ProjectTitle = "Project A1",
                ProjId = "P/1"
            };
            Assert.Equal(DateTime.Today.AddDays(2), projectMod.EndDate);
            Assert.Equal(DateTime.Today, projectMod.StartDate);
            Assert.Equal("Usr/1", projectMod.PMUsrId);
            Assert.Equal("Project A1", projectMod.ProjectTitle);
            Assert.Equal("P/1", projectMod.ProjId);


        }
    }
}
