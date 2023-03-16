﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryCourses.Data_Access.DTOS;
using RepositoryCourses.Models;
using RepositoryCourses.Services.Interfaces;

namespace RepositoryCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : GenericController<Teachers, TeachersDTO>
    {
        public ITeacherSertvice _tService { get; set; }
        private readonly IMapper _mapper;
        public TeacherController(ITeacherSertvice teacherService, IMapper mapper) : base(teacherService, mapper)
        {
            _tService = teacherService;
            _mapper = mapper;
        }

    }
}
