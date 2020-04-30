using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.App.DTOs;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Data.Exceptions;

namespace WebApiProductsProviders.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : BaseController
    {
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderService providerService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _providerService = providerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProviderDTO>>> Get()
        {
            var providers = _mapper.Map<List<ProviderDTO>>(await _providerService.FindAll(true));
            return Ok(providers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProviderDTO>> Get(Guid id)
        {
            var providerDTO = _mapper.Map<ProviderDTO>(await _providerService.FindById(id, true, true));

            if (providerDTO == null) return NotFound();

            return Ok(providerDTO);
        }

        [HttpGet("address/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var address = await _providerService.FindAddressById(id);
            if (address == null)
            {
                return NotFound();
            }

            return CustomResponse(_mapper.Map<AddressDTO>(address));
        }

        [HttpPost]
        public async Task<ActionResult<ProviderDTO>> Post(ProviderDTO providerDTO)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            var provider = await _providerService.Insert(_mapper.Map<Provider>(providerDTO));
            return CustomResponse(_mapper.Map<ProviderDTO>(provider));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProviderDTO>> Put(Guid id, ProviderDTO providerDTO)
        {
            if (id != providerDTO.Id)
            {
                NotifyError("Id's não correspondem");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            var provider = await _providerService.Insert(_mapper.Map<Provider>(providerDTO));
            return CustomResponse(_mapper.Map<ProviderDTO>(provider));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var provider = await _providerService.FindById(id, false, false);
                if (provider == null) return NotFound();

                await _providerService.Remove(id);
                return CustomResponse();
            }
            catch (IntegrityException exception)
            {
                return NotifyException(exception);
            }
        }

        [HttpPut("{address/id:guid}")]
        public async Task<ActionResult<AddressDTO>> Put(Guid id, AddressDTO addressDTO)
        {
            if (id != addressDTO.Id)
            {
                NotifyError("Id's não correspondem");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            var address = await _providerService.UpdateAddress(_mapper.Map<Address>(addressDTO));
            return CustomResponse(_mapper.Map<ProviderDTO>(address));

        }

        private ActionResult NotifyException(Exception ex)
        {
            NotifyError($"{ex.Message} existem produtos pertecentes a este fornecedor");
            return CustomResponse();
        }
    }
}
