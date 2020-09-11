using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using TMS_Microservice.Models;
using TMS_Microservice.Controllers;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TMS_Microservice_TEST
{
    public class TestSubTaskController
    {
        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testTasks = GetTestProducts();
            var controller = new SubTaskTestController(testTasks);

            var result = controller.GetAllTasks() as List<Subtask>;
            Assert.AreEqual(testTasks.Count, result.Count);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            var testTasks = GetTestProducts();
            var controller = new SubTaskTestController(testTasks);

            var result = await controller.GetAllTasksAsync() as List<Subtask>;
            Assert.AreEqual(testTasks.Count, result.Count);
        }

        [Fact]
        public void GetTask_ShouldReturnCorrectTask()
        {
            var testTasks = GetTestProducts();
            var controller = new SubTaskTestController(testTasks);

            var result = controller.GetSubTask(3) as OkNegotiatedContentResult<Subtask>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testTasks[2].Name, result.Content.Name);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTaskAsync_ShouldReturnCorrectProduct()
        {
            var testTasks = GetTestProducts();
            var controller = new SubTaskTestController(testTasks);

            var result = await controller.GetTaskAsync(2) as OkNegotiatedContentResult<Subtask>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testTasks[1].Name, result.Content.Name);
        }

        [Fact]
        public void GetTask_ShouldNotFindProduct()
        {
            var controller = new SubTaskTestController(GetTestProducts());

            var result = controller.GetSubTask(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<Subtask> GetTestProducts()
        {
            var testProducts = new List<Subtask>();
            testProducts.Add(new Subtask
            {
                Id = 1,
                TaskId = 2,
                Name = "Nainsha",
                Description = "Laundry",
                State = "In process",
                Start_date = new DateTime(2020 - 09 - 10),
                Finish_date = new DateTime(2020 - 10 - 10)

            });
            testProducts.Add(new Subtask
            {
                Id = 2,
                TaskId = 1,
                Name = "Yash",
                Description = "Game",
                State = "In process",
                Start_date = new DateTime(2020 - 09 - 10),
                Finish_date = new DateTime(2020 - 10 - 10)
            });
            testProducts.Add(new Subtask
            {
                Id = 3,
                TaskId = 2,
                Name = "Burhan",
                Description = "Laundry",
                State = "Planned",
                Start_date = new DateTime(2020 - 09 - 10),
                Finish_date = new DateTime(2020 - 10 - 10)
            });

            return testProducts;
        }
    }
}

