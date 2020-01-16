using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;

namespace RudesWebapp.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly RudesDatabaseContext _context;
        private readonly IFileProvider _fileProvider;

        private const string ImagesFolder = "images";
        private static readonly int[] SupportedSizes = {72, 240, 480, 640, 960, 1280};

        private static readonly string[] PermittedExtensions = {".jpg", ".png", ".jpeg", "gif"};

        public ImageService(IWebHostEnvironment hostEnvironment, RudesDatabaseContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _fileProvider = hostEnvironment.WebRootFileProvider;
        }

        public async Task<IEnumerable<Image>> GetImages()
        {
            return await _context.Image.ToListAsync();
        }


        public async Task<Image> GetImage(int? id)
        {
            return await _context.Image.FindAsync(id);
        }

        public async Task<Image> GetFirstImageOrDefault(int? id)
        {
            return await _context.Image.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteImage(int? id)
        {
            var image = await _context.Image.FindAsync(id);
            DeleteImageFile(image);
            _context.Image.Remove(image);
            await _context.SaveChangesAsync();
        }

        public void DeleteImageFile(Image image)
        {
            var fileInfo = _fileProvider.GetFileInfo(image.GetPath());
            if (fileInfo.Exists)
            {
                File.Delete(fileInfo.PhysicalPath);
            }
        }

        public async Task<Image> SaveImage(AddImageDTO imageDto)
        {
            // CheckIfValidExtension(imageDto.Picture); // TODO
            // CheckIfValidSize(imageDto.Picture); // TODO

            // TODO add Title validation or don't use supplied title in filename 

            var image = new Image
            {
                Name = PersistImage(imageDto.Picture, imageDto.Title),
                Title = imageDto.Title,
                AltText = imageDto.AltText,
                Caption = imageDto.Caption
            };

            _context.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        private static bool CheckIfValidExtension(IFormFile picture)
        {
            return CheckIfValidExtension(picture.FileName);
        }

        private static bool CheckIfValidExtension(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(ext) && PermittedExtensions.Contains(ext);
        }


        private bool CheckIfValidSize(IFormFile picture)
        {
            return picture.Length <= 5 * 1024 * 1024;
        }

        private string PersistImage(IFormFile picture, string imageDtoTitle)
        {
            var fileName = Guid.NewGuid() + "_" + imageDtoTitle + "." +
                           Path.GetExtension(picture.FileName).ToLowerInvariant();

            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, ImagesFolder);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var copyToStream = new FileStream(filePath, FileMode.Create))
            {
                picture.CopyTo(copyToStream);
            }

            return fileName;
        }

        public async Task<Image> Update(int id, AddImageDTO imageDto)
        {
            try
            {
                // CheckIfValidExtension(imageDto.Picture); // TODO
                // CheckIfValidSize(imageDto.Picture); // TODO

                // TODO add Title validation or don't use supplied title in filename 

                var image = await GetImage(id);
                image.Title = imageDto.Title;
                image.AltText = imageDto.AltText;
                image.Caption = imageDto.Caption;

                if (imageDto.Picture != null)
                {
                    DeleteImageFile(image);
                    image.Name = PersistImage(imageDto.Picture, imageDto.Title);
                }

                await _context.SaveChangesAsync();
                return image;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(imageDto.Id))
                {
                    return null; // TODO
                }
                else
                {
                    throw;
                }
            }
        }

        public static int SanitizeSize(int value)
        {
            return value >= 1280 ? 1280 : SupportedSizes.First(size => size >= value);
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.Id == id);
        }

        public async Task<ServiceResult<Image>> ValidateThatImageExists(string propertyName, int imageId)
        {
            var image = await _context.Image.FirstOrDefaultAsync(i => i.Id == imageId);

            return image == null
                ? new ServiceResult<Image>(new ServiceError
                    {Property = propertyName, Description = "The image does not exist."})
                : ServiceResult<Image>.Success();
        }

        public async Task ScanImages()
        {
            var oldImages = await _context.Image.ToListAsync();
            var fileNames = _fileProvider
                .GetDirectoryContents(ImagesFolder)
                .AsEnumerable()
                .Select(info => info.Name)
                .Where(CheckIfValidExtension)
                .ToList();

            foreach (var image in oldImages)
            {
                if (fileNames.Contains(image.Name))
                {
                    fileNames.Remove(image.Name);
                }
                else
                {
                    _context.Remove(image);
                }
            }

            fileNames.Select(fileName => new Image()
                    {Name = fileName, Title = GuessTitleFromFileName(fileName), Caption = "", AltText = ""}).ToList()
                .ForEach(image => _context.Image.Add(image));

            _context.SaveChanges();
        }

        public static string GuessTitleFromFileName(string fileName)
        {
            var result = Path.GetFileNameWithoutExtension(fileName);
            result = result.Substring(result.LastIndexOf('_') + 1);
            return result;
        }
    }
}