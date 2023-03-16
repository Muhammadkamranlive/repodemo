using RepositoryCourses.Domain.Models;
using RepositoryCourses.Domain.Repositories;
using RepositoryCourses.Models;
using RepositoryCourses.Services.Interfaces;

namespace RepositoryCourses.Services.Implementation
{
    public class NozleService:GenericService<Nozles>,INozlesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Nozles> _repository;

        public NozleService(IUnitOfWork unitOfWork, IGenericRepository<Nozles> genericRepository) : base(unitOfWork, genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
