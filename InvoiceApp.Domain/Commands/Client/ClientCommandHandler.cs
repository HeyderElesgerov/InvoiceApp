using FluentValidation.Results;
using InvoiceApp.Domain.Core.CommandBase;
using InvoiceApp.Domain.Core.ErrorConst;
using InvoiceApp.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Commands.Client
{
    public class ClientCommandHandler :
        CommandHandler,
        IRequestHandler<CreateClientCommand, ValidationResult>,
        IRequestHandler<DeleteClientCommand, ValidationResult>,
        IRequestHandler<UpdateClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            await _clientRepository.Add(request.Client);
            await _clientRepository.Commit();

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            int clientId = request.ClientId;
            var client = _clientRepository.Find(clientId);
            bool clientExists = client != null;

            if(clientExists)
            {
                await _clientRepository.Delete(client);
                await _clientRepository.Commit();
            }
            else
            {
                AddError(ValidationErrors.NotFound);
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;


            var client = _clientRepository.Find(request.Client.Id);

            if(client == null)
            {
                AddError(ValidationErrors.NotFound);
            }
            else
            {
                client.ChangeFirstName(request.Client.FirstName);
                client.ChangeLastName(request.Client.LastName);

                await _clientRepository.Update(client);
                await _clientRepository.Commit();
            }

            return ValidationResult;
        }
    }
}
