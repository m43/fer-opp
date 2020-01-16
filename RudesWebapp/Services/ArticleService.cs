using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;

namespace RudesWebapp.Services
{
    public class ArticleService
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ArticleService(RudesDatabaseContext context, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ServiceResult<ArticleDTO>> PutArticle(ArticleDTO articleDto)
        {
            ServiceResult result;

            if (articleDto.ImageId != null)
            {
                result = await _imageService.ValidateThatImageExists(nameof(articleDto.ImageId),
                    articleDto.ImageId.Value);
                if (!result.Succeeded)
                {
                    return ServiceResult<ArticleDTO>.FromServiceResult(result);
                }
            }

            var tResult = await GetArticle(articleDto.Id);
            if (!tResult.Succeeded)
            {
                return tResult;
            }


            var article = await _context.Article.Include(a=>a.Image).FirstOrDefaultAsync(m => m.Id == articleDto.Id);
            if (!article.Name.Equals(articleDto.Name))
            {
                result = await ValidateThatArticleWithNameDoesNotExist(articleDto);
                if (!result.Succeeded)
                {
                    return ServiceResult<ArticleDTO>.FromServiceResult(result);
                }
            }

            try
            {
                _mapper.Map(articleDto, article);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                tResult = await GetArticle(articleDto.Id);
                if (!tResult.Succeeded)
                {
                    return tResult;
                }
                else
                {
                    throw;
                }
            }

            return ServiceResult<ArticleDTO>.Success();
        }

        public async Task<ServiceResult> CreateArticle(ArticleDTO articleDto)
        {
            ServiceResult result;

            if (articleDto.ImageId != null)
            {
                result = await _imageService.ValidateThatImageExists(nameof(articleDto.ImageId),
                    articleDto.ImageId.Value);
                if (!result.Succeeded)
                {
                    return result;
                }
            }

            result = await ValidateThatArticleWithNameDoesNotExist(articleDto);
            if (!result.Succeeded)
            {
                return result;
            }

            var article = _mapper.Map<Article>(articleDto);

            // TODO not sure if possible exception on next two lines should be caught in a try-catch block or
            //   rather left for the exception middleware to handle. An exception at this point means that
            //   something went wrong on the server side - the new article has at this point been validated
            //   against all known errors. If an error occured, it has something to do with the database.
            //   Not sure what the client would want to see in that case - 500? Note that the client
            //   is the browser of the web shop client who is just shopping.
            _context.Article.Add(article);
            await _context.SaveChangesAsync();

            return ServiceResult.Success;
        }

        private async Task<ServiceResult> ValidateThatArticleWithNameDoesNotExist(ArticleDTO articleDto)
        {
            if ((await _context.Article.FirstOrDefaultAsync(a => a.Name == articleDto.Name)) == null)
            {
                return ServiceResult.Success;
            }

            var result = new ServiceResult(new ServiceError
            {
                Property = nameof(articleDto.Name),
                Description = "Name must be unique. An article with name '" + articleDto.Name + "' already exists."
            }) {StatusCode = StatusCodes.Status409Conflict};
            return result;
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticles()
        {
            return _mapper.Map<IEnumerable<ArticleDTO>>(await _context.Article.Include(a=>a.Image).ToListAsync());
        }

        public async Task<ServiceResult<ArticleDTO>> GetArticle(int? id)
        {
            var article = await _context.Article.Include(a=>a.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                var result = new ServiceResult().WithStatusCode(StatusCodes.Status404NotFound);
                return ServiceResult<ArticleDTO>.FromServiceResult(result);
            }

            return ServiceResult<ArticleDTO>.FromValue(_mapper.Map<ArticleDTO>(article));
        }

        public async Task DeleteIfExists(int id)
        {
            var article = await _context.Article.FirstOrDefaultAsync(m => m.Id == id);
            if (article != null)
            {
                _context.Article.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
    }
}