using FluentValidation;
using MediatR;
using MGK.Acceptance;
using MGK.ServiceBase.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MGK.ServiceBase.Infrastructure
{
    public class RequestValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handle to validate request input and check errors
        /// </summary>
        /// <param name="request"> Request to validate</param>
        /// <param name="cancellationToken">MediatR Cancelation Token</param>
        /// <param name="next">Delegate response</param>
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (errors?.Any() == true)
            {
                var errorBuilder = new StringBuilder();

                foreach (var error in errors)
                {
                    errorBuilder.AppendLine(error.ErrorMessage);
                }

                // Throw exception with all errors
                Raise.Error.Generic<InvalidRequestException>(
                    BaseResources.MessagesResources.ErrorValidatorsTitle,
                    errorBuilder.ToString());
            }

            return next();
        }
    }
}
