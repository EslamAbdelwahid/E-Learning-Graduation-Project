using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Specifications.Instructors;
using E_Learning.GraduationProject.Service.Services;
using Moq;

namespace Services.Test
{
    public class InstructorServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly InstructorService _instructorService;

        public InstructorServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _instructorService = new InstructorService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllInstructorsAsync_ReturnsPaginationResponse()
        {
            // Arrange
            var specParams = new InstructorParams();
            var instructors = new List<Instructor>
            {
                new Instructor { Id = 1,  User = {  FirstName=  "John Doe" } },
                new Instructor { Id = 2,User = {  FirstName=  "John Doe" }  }
            };
            var instructorDtos = new List<InstructorToReturnDto>
            {
                new InstructorToReturnDto { Id = 1, FirstName = "John Doe"   },
                new InstructorToReturnDto { Id = 2, FirstName=  "John Doe"  }
            };

            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetAllWithSpecAsync(It.IsAny<InstructorSpecifications>()))
                .ReturnsAsync(instructors);
            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetCountAsync(It.IsAny<InstructorWithCountSpecifications>()))
                .ReturnsAsync(2);
            _mapperMock.Setup(m => m.Map<IEnumerable<InstructorToReturnDto>>(instructors))
                .Returns(instructorDtos);

            // Act
            var result = await _instructorService.GetAllInstructorsAsync(specParams);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(specParams.PageIndex, result.PageIndex);
            Assert.Equal(specParams.PageSize, result.PageSize);
        }

        [Fact]
        public async Task GetInstructorByIdAsync_ReturnsInstructor_WhenExists()
        {
            // Arrange
            var instructorId = 1;
            var instructor = new Instructor { Id = instructorId, User = { FirstName = "John Doe" } };
            var instructorDto = new InstructorToReturnDto { Id = instructorId, FirstName = "John Doe" };

            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetWithSpecAsync(It.IsAny<InstructorSpecifications>()))
                .ReturnsAsync(instructor);
            _mapperMock.Setup(m => m.Map<InstructorToReturnDto>(instructor))
                .Returns(instructorDto);

            // Act
            var result = await _instructorService.GetInstructorByIdAsync(instructorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(instructorId, result.Id);
        }

        [Fact]
        public async Task GetInstructorByIdAsync_ReturnsNull_WhenNotExists()
        {
            // Arrange
            var instructorId = 999;
            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetWithSpecAsync(It.IsAny<InstructorSpecifications>()))
                .ReturnsAsync((Instructor)null);

            // Act
            var result = await _instructorService.GetInstructorByIdAsync(instructorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateInstructorAsync_ReturnsInstructorDto_WhenSuccess()
        {
            // Arrange
            var instructorDto = new InstructorDto { FirstName = "New Instructor" };
            var instructor = new Instructor { Id = 1, User = { FirstName = "John Doe" } };
            var returnDto = new InstructorToReturnDto { Id = 1, FirstName = "New Instructor" };

            _mapperMock.Setup(m => m.Map<Instructor>(instructorDto))
                .Returns(instructor);
            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().AddAsync(instructor))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<InstructorToReturnDto>(instructor))
                .Returns(returnDto);

            // Act
            var result = await _instructorService.CreateInstructorAsync(instructorDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateInstructorAsync_ReturnsUpdatedDto_WhenSuccess()
        {
            // Arrange
            var instructorId = 1;
            var updateDto = new InstructorDto { FirstName = "Updated Name" };
            var existingInstructor = new Instructor { Id = instructorId, User = { FirstName = "Original Name" } };
            var updatedInstructor = new Instructor { Id = instructorId, User = { FirstName = "Updated Name" } };
            var returnDto = new InstructorToReturnDto { Id = instructorId, FirstName = "Updated Name" };

            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetByIdAsync(instructorId))
                .ReturnsAsync(existingInstructor);
            _mapperMock.Setup(m => m.Map(updateDto, existingInstructor))
                .Returns(updatedInstructor);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<InstructorToReturnDto>(updatedInstructor))
                .Returns(returnDto);

            // Act
            var result = await _instructorService.UpdateInstructorAsync(instructorId, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.FirstName);
            _unitOfWorkMock.Verify(u => u.Repository<Instructor, int>().Update(updatedInstructor), Times.Once);
        }

        [Fact]
        public async Task DeleteInstructorAsync_ReturnsRowsAffected_WhenSuccess()
        {
            // Arrange
            var instructorId = 1;
            var instructor = new Instructor { Id = instructorId };
            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetByIdAsync(instructorId))
                .ReturnsAsync(instructor);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _instructorService.DeleteInstructorAsync(instructorId);

            // Assert
            Assert.Equal(1, result);
            _unitOfWorkMock.Verify(u => u.Repository<Instructor, int>().Delete(instructor), Times.Once);
        }

        [Fact]
        public async Task DeleteInstructorAsync_ReturnsZero_WhenNotFound()
        {
            // Arrange
            var instructorId = 999;
            _unitOfWorkMock.Setup(u => u.Repository<Instructor, int>().GetByIdAsync(instructorId))
                .ReturnsAsync((Instructor)null);

            // Act
            var result = await _instructorService.DeleteInstructorAsync(instructorId);

            // Assert
            Assert.Equal(0, result);
        }
    }
}