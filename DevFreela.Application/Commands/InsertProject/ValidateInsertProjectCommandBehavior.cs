using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public ValidateInsertProjectCommandBehavior(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = _dbContext.Users.Any(u => u.Id == request.IdCliente);
            var freelancerExists = _dbContext.Users.Any(u => u.Id == request.IdFreelancer);

            if(!clientExists || !freelancerExists)
            {
                return ResultViewModel<int>.Fail("Cliente ou Freelancer inválidos");
            }

            return await next();
        }
    }
}
