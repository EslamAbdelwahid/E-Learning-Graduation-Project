using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Courses;
using E_Learning.GraduationProject.Core.Dtos.Students;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Courses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITrackService _trackService;
        private readonly IProgrammingLanguageService _programmingLanguageService;

        public CourseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITrackService trackService,
            IProgrammingLanguageService programmingLanguageService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trackService = trackService;
            _programmingLanguageService = programmingLanguageService;
        }
        public async Task<PaginationResponseToReturn<CourseToReturnDto>?> GetAllCoursesAsync(CourseParams specParams)
        {
            var spec = new CourseSpecifications(specParams);
            var courses = await _unitOfWork.Repository<Course, int>().GetAllWithSpecAsync(spec);
            if (courses is null) return null;

            var specCount = new CourseWithCountSpecifications(specParams);
            var count = await _unitOfWork.Repository<Course, int>().GetCountAsync(specCount);

            var data = _mapper.Map<IEnumerable<CourseToReturnDto>>(courses);

            return new PaginationResponseToReturn<CourseToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data);

        }

        public async Task<CourseToReturnDto?> GetCourseByIdAsync(int id)
        {
            var spec = new CourseSpecifications(id);
            var course = await _unitOfWork.Repository<Course, int>().GetWithSpecAsync(spec);

            if (course is null) return null;

            return _mapper.Map<CourseToReturnDto>(course);
        }

        public async Task<CourseToReturnDto?> CreateCourseAsync(CourseDto model)
        {

            var entity = _mapper.Map<Course>(model);

            if (model.Track is not null)
            {
                //create new Track
                var track = _mapper.Map<Track>(model.Track);
                var createdTrack = await _trackService.CreateTrackAsync(track);
                if (createdTrack is null) throw new InvalidOperationException("There is a problem while Creating the Track");

                entity.TrackId = createdTrack.Id;
            }


            if (model.ProgrammingLanguage is not null)
            {
                // create new programming Lang
                var programmingLanguage = await _programmingLanguageService.CreateProgrammingLanguageAsync(model.ProgrammingLanguage);
                if (programmingLanguage is null) throw new InvalidOperationException("There is a problem while Creating the Programming Language");

                entity.ProgrammingLanguageId = programmingLanguage.Id;
            }




            await _unitOfWork.Repository<Course, int>().AddAsync(entity);
            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<CourseToReturnDto>(entity) : null;
        }
        public async Task<CourseToReturnDto?> UpdateCourseAsync(int id, CourseDto model)
        {
            var existingCourse = await _unitOfWork.Repository<Course, int>().GetByIdAsync(id);
            if (existingCourse == null) return null;

            var entity = _mapper.Map(model, existingCourse);

            #region Updating CourseContent
            //if (model.Track is not null && model.TrackId is not null)
            //{
            //    // check existence
            //    var existingTrack = await _trackService.GetTrackByIdWithSpecAsync(model.TrackId.Value);
            //    if (existingTrack == null) return null;

            //    //update Track
            //    var track = _mapper.Map(model.Track, existingTrack);
            //    var updatedTrack = await _trackService.UpdateTrackAsync(track);
            //    if (updatedTrack is null) throw new InvalidOperationException("There is a problem while Creating the Track");


            //}


            //if (model.ProgrammingLanguage is not null && model.ProgrammingLanguageId is not null)
            //{
            //    // check existence
            //    var existingLang = await _programmingLanguageService.GetProgrammingLanguageByIdWithSpecAsync(model.ProgrammingLanguageId.Value);
            //    if (existingLang == null) return null;

            //    // update programming Lang
            //    var programmingLanguage = await _programmingLanguageService.UpdateProgrammingLanguageAsync(model.ProgrammingLanguageId.Value, model.ProgrammingLanguage);
            //    if (programmingLanguage is null) throw new InvalidOperationException("There is a problem while Creating the Programming Language");

            //}
            #endregion


            _unitOfWork.Repository<Course, int>().Update(entity);
            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<CourseToReturnDto>(entity) : null;
        }
        public async Task<int> DeleteCourseAsync(int id)
        {
            var course = await _unitOfWork.Repository<Course, int>().GetByIdAsync(id);

            if (course is null) return 0;

            _unitOfWork.Repository<Course, int>().Delete(course);

            return await _unitOfWork.CompleteAsync();
        }


    }
}
