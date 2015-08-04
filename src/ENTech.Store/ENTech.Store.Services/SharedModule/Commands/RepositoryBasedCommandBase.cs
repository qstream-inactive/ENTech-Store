using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Repositories;

namespace ENTech.Store.Services.SharedModule.Commands
{
    
   public abstract class RepositoryBasedCommandBase<TRequest, TResponse> : CommandBase<TRequest, TResponse>
	
		where TRequest : IInternalRequest
		where TResponse : InternalResponse
	{
        protected IRepository Repository;

        protected RepositoryBasedCommandBase(IRepository repositoy)
            : base(false)
		{
            Repository = repositoy;
		}
	}
}
