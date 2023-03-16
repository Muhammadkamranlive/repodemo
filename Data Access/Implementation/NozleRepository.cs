using RepositoryCourses.Domain.Implementation;
using RepositoryCourses.Domain.Models;
using RepositoryCourses.Domain.Repositories;

namespace RepositoryCourses.Data_Access.Implementation
{
    public class NozleRepository:GenericRepository<Nozles>,INozlesRespository
    {
        public NozleRepository(CourseContext context) : base(context)
        {

        }
    }
}
