using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Xunit;
using Task = TMS_Microservice.Models.Task;
using TMS_Microservice.Controllers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
namespace TMS_Microservice_TEST
{

    public class TestTaskController
    {
        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testTasks = GetTestProducts();
            var controller = new TaskTestController(testTasks);

            var result = controller.GetAllTasks() as List<Task>;
            Assert.AreEqual(testTasks.Count, result.Count);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            var testTasks = GetTestProducts();
            var controller = new TaskTestController(testTasks);

            var result = await controller.GetAllTasksAsync() as List<Task>;
            Assert.AreEqual(testTasks.Count, result.Count);
        }

        [Fact]
        public void GetTask_ShouldReturnCorrectTask()
        {
            var testTasks = GetTestProducts();
            var controller = new TaskTestController(testTasks);

            var result = controller.GetTask(3) as OkNegotiatedContentResult<Task>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testTasks[2].Name, result.Content.Name);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTaskAsync_ShouldReturnCorrectProduct()
        {
            var testTasks = GetTestProducts();
            var controller = new TaskTestController(testTasks);

            var result = await controller.GetTaskAsync(2) as OkNegotiatedContentResult<Task>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testTasks[1].Name, result.Content.Name);
        }

        [Fact]
        public void GetTask_ShouldNotFindProduct()
        {
            var controller = new TaskTestController(GetTestProducts());

            var result = controller.GetTask(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<Task> GetTestProducts()
        {
            var testProducts = new List<Task>();
            testProducts.Add(new Task
            {
                Id = 1,
                Name = "Nainsha",
                Description = "Laundry",
                State = "In process",
                Start_date = new DateTime(2020 - 09 - 10),
                Finish_date = new DateTime(2020 - 10 - 10)

            });
            testProducts.Add(new Task
            {
                Id = 2,
                Name = "Yash",
                Description = "Game",
                State = "In process",
                Start_date = new DateTime(2020 - 09 - 10),
                Finish_date = new DateTime(2020 - 10 - 10)
             });
            testProducts.Add(new Task
            {
                Id = 3,
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
