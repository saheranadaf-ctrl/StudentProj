using DeleteAPI.Controllers;
using DeleteAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;

namespace DeleteTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }


        [Fact]
        public async Task DeleteStudent_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StudentDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new StudentDataContext(options))
            {
                dbContext.Students.Add(new Student { StudentId = "STDN00490", Gender = "F" });
                dbContext.SaveChanges();
            }

            using (var dbContext = new StudentDataContext(options))
            {
                var controller = new DeleteController(dbContext);

                // Act
                var result = await controller.DeleteStudent("STDN00490");

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var deletedStudent = Assert.IsType<Student>(okResult.Value);
                Assert.Equal("STDN00490", deletedStudent.StudentId);
            }
        }

        [Fact]
        public async Task DeleteStudent_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StudentDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new StudentDataContext(options))
            {
                var controller = new DeleteController(dbContext);

                // Act
                var result = await controller.DeleteStudent("NonExistingId");

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }
}