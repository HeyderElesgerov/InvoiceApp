using AutoMapper;
using FluentValidation.Results;
using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Domain.Commands.Client;
using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMediator mediator, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Add(ClientViewModel clientViewModel)
        {
            return await _mediator.Send(_mapper.Map<ClientViewModel, CreateClientCommand>(clientViewModel));
        }

        public async Task<ValidationResult> Delete(ClientViewModel clientViewModel)
        {
            return await _mediator.Send(_mapper.Map<ClientViewModel, DeleteClientCommand>(clientViewModel));
        }

        public Task<ClientViewModel> Get(int id)
        {
            var client = _clientRepository.Find(id);
            return Task.FromResult(_mapper.Map<Client, ClientViewModel>(client));
        }

        public async Task<IEnumerable<ClientViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(await _clientRepository.GetAll());
        }

        public async Task<ValidationResult> Update(ClientViewModel clientViewModel)
        {
            return await _mediator.Send(_mapper.Map<ClientViewModel, UpdateClientCommand>(clientViewModel));
        }
    }
}
