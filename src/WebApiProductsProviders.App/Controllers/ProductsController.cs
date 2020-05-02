using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebApiProductsProviders.App.DTOs;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var products = _mapper.Map<List<ProductDTO>>(await _productService.FindAll(false, true));
            return (products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            var productDTO = _mapper.Map<ProductDTO>(await _productService.FindById(id, true, true));
            if (productDTO == null) return NotFound();

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            if (!string.IsNullOrEmpty(productDTO.ImageUpload))
            {
                productDTO.Image = $"{Guid.NewGuid()}_{productDTO.Image}";
                if (!await UploadFile(productDTO.ImageUpload, productDTO.Image)) return CustomResponse();
            }
            else
            {
                productDTO.Image = "sem_imagem";
            }

            var product = await _productService.Insert(_mapper.Map<Product>(productDTO));
            return CustomResponse(product);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Put(Guid id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                NotifyError("Id's não correspondem");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            if (!string.IsNullOrEmpty(productDTO.ImageUpload))
            {
                productDTO.Image = $"{Guid.NewGuid()}_{productDTO.Image}";
                if (!await UploadFile(productDTO.ImageUpload, productDTO.Image)) return CustomResponse();

                var productBase = await _productService.FindById(id, false, false);
                RemoveFile(productBase.Image);
            }

            var product = await _productService.Update(_mapper.Map<Product>(productDTO));
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.FindById(id, false, false);
            if (product == null) return NotFound();

            if (product.Image != "sem_imagem") RemoveFile(product.Image);

            await _productService.Remove(id);
            return CustomResponse();
        }

        private async Task<bool> UploadFile(string file, string fileName)
        {
            var fileDataByteArray = Convert.FromBase64String(file);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", fileName);
            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Já existe um arquivo com este nome");
                return false;
            }

            await System.IO.File.WriteAllBytesAsync(filePath, fileDataByteArray);
            return true;
        }

        private void RemoveFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", fileName);
            System.IO.File.Delete(filePath);
        }
    }
}
