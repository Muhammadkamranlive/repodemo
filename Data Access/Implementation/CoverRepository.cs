﻿using RepositoryCourses.Domain.Implementation;
using RepositoryCourses.Domain.Repositories;
using RepositoryCourses.Models;

namespace RepositoryCourses.Data_Access.Implementation
{
    public class CoverRepository : GenericRepository<Cover>, ICoverRepository
    {
        public CoverRepository(CourseContext context) : base(context)
        {

        }
    }
}
