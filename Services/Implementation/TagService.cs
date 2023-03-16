﻿namespace RepositoryCourses.Services.Implementation;
using RepositoryCourses.Domain.Repositories;
using RepositoryCourses.Models;
using RepositoryCourses.Services.Interfaces;

public class TagService : GenericService<Tag>, ITagService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Tag> _repository;
    public TagService(IUnitOfWork unitOfWork, IGenericRepository<Tag> genericRepository) : base(unitOfWork, genericRepository)
    {
        _repository = genericRepository;
        _unitOfWork = unitOfWork;
    }
}
