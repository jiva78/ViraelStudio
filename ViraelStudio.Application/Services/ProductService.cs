using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.DTOs.Product;
using ViraelStudio.Application.Mappings;
using ViraelStudio.Application.Models;
using ViraelStudio.Application.Repositories;
using ViraelStudio.Domain.Entities;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductMediaService _mediaService;

        public ProductService(
            IProductRepository repository,
            IProductMediaService mediaService)
        {
            _repository = repository;
            _mediaService = mediaService;
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var product = dto.ToEntity();
            await _repository.AddAsync(product);
            return product.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product is null)   return false;

            foreach (var media in product.Media)
            {
                await _mediaService.DeleteAsync(media.Url);
            }

            return await _repository.DeleteAsync(id);
        }

      
        public async Task<PagedResultDto<ProductDTO>> GetAllAsync(ProductQuery query)
        {
            var result = await _repository.GetAllAsync(query);
            return new PagedResultDto<ProductDTO>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.ConvertAll(p => p.ToDto()),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / query.PageSize)
            };
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product?.ToDto();
            
        }

        public async Task<ProductDTO?> UpdateAsync(int id, UpdateProductDTO dto)
        {
            var product = dto.ToEntity();
            var updatedProduct = await _repository.UpdateAsync(id, product);
            return updatedProduct?.ToDto();
        }

        public async Task<ProductMediaDTO?> AddMediaAsync(
            int productId,
            Stream fileStream,
            string fileName,
            ProductMediaType mediaType,
            bool isPrimary,
            int sortOrder,
            CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(productId);

            if (product is null)  return null;

            var url = await _mediaService.SaveAsync(
                fileStream,
                fileName,
                cancellationToken);

            var media = new ProductMedia
            {
                ProductId = productId,
                MediaType = mediaType,
                Url = url,
                IsPrimary = isPrimary,
                SortOrder = sortOrder
            };


            ProductMedia? savedMedia;
            try
            {
                savedMedia = await _repository.AddMediaAsync(productId, media);
            }
            catch
            {
                await _mediaService.DeleteAsync(url, cancellationToken);
                throw;
            }


            if (savedMedia is null)
            {
                await _mediaService.DeleteAsync(url, cancellationToken);
                return null;
            }

            return new ProductMediaDTO
            {
                Id = savedMedia.Id,
                MediaType = savedMedia.MediaType,
                Url = savedMedia.Url,
                IsPrimary = savedMedia.IsPrimary,
                SortOrder = savedMedia.SortOrder
            };
        }

        public async Task<bool> DeleteMediaAsync(int productId, int mediaId)
        {
            var media = await _repository.DeleteMediaAsync(productId, mediaId);

            if (media is null)  return false;

            await _mediaService.DeleteAsync(media.Url);

            return true;
        }
    }
}
