using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryCourses.Data_Access.DTOS;
using RepositoryCourses.Domain.Models;
using RepositoryCourses.Domain.Repositories;
using RepositoryCourses.Services.Interfaces;
using System.Collections.Generic;

namespace RepositoryCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NozleController : GenericController<Nozles,Nozledto>
    {
        private readonly INozlesServices _categoryService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        public NozleController(INozlesServices categoryService, IMapper mapper,IUnitOfWork unitOfWork) : base(categoryService, mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            this.unitOfWork=unitOfWork;
        }

        [HttpGet()]
        [Route("getReading")]
        public async Task<IActionResult> Find(string date, int reading)
        {

            try
            {

                var singleRecord = await unitOfWork.NozlesRespository.Find(c => c.date == date && c.readingNo == reading);
                List<Nozledto> result = _mapper.Map<List<Nozledto>> (singleRecord);
                return Ok(result.FirstOrDefault());
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }

        [HttpPost]
        [Route("postReading")]
        public async Task<IActionResult> Add([FromBody] Nozledto nozledto)
        {

            try
            {

                await unitOfWork.NozlesRespository.Add(_mapper.Map<Nozles>(nozledto));
                await unitOfWork.Save();
                
                return Ok(nozledto);

            }
            catch (Exception e)
            {

                throw new Exception(e.InnerException?.Message);
            }


        }
    }
}
