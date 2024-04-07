using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Tests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests
{
    public sealed class EnrolledFacadeTests : FacadeTestsBase
    {
        private readonly IEnrolledFacade _enrolledFacadeSUT;

        public EnrolledFacadeTests(ITestOutputHelper output) : base(output)
        {
            _enrolledFacadeSUT = new EnrolledFacade(UnitOfWorkFactory, EnrolledModelMapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEnrolledSubjects()
        {
            // Act
            var result = await _enrolledFacadeSUT.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAsync_ExistingId_ReturnsEnrolledSubject()
        {
            // Arrange
            var expectedId = EnrolledSeeds.EnrolledEntity.Id;

            // Act
            var result = await _enrolledFacadeSUT.GetByIdAsync(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async Task GetAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = await _enrolledFacadeSUT.GetByIdAsync(nonExistingId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_DeletesEnrolledSubject()
        {
            // Arrange
            var existingId = EnrolledSeeds.EnrolledEntity.Id;

            // Act & Assert 
            await _enrolledFacadeSUT.DeleteAsync(existingId);


            var resultAfterDelete = await _enrolledFacadeSUT.GetByIdAsync(existingId);
            Assert.Null(resultAfterDelete);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid(); 

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _enrolledFacadeSUT.DeleteAsync(nonExistingId));
        }



    }
}
