using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Engage.Application.Extensions;
using Engage.Application.Interfaces;
using Engage.Application.Models;

namespace Engage.Infrastructure.Services
{
    public class PhysicalFileImageService : IImageService
    {
        public async Task<OperationStatus> CreatePhoto(string baseFolder, Photo photo, CancellationToken token)
        {
            try
            {
                baseFolder.ThrowIfNullOrWhiteSpace(nameof(baseFolder));
                photo.ThrowIfNull(nameof(photo));
                photo.Base64String.ThrowIfNullOrWhiteSpace(nameof(photo.Base64String));

                var base64 = photo.Base64String.IndexOf("base64,") > -1 
                                ? photo.Base64String.Substring(photo.Base64String.IndexOf("base64,") + 7)
                                : photo.Base64String;

                var bytes = Convert.FromBase64String(base64);

                var path = $"{baseFolder}/{photo.Folder}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Only get the file name.
                var photoname = photo.Id.Split("/").Last();

                using var writer = File.Create($"{path}/{photoname}");
                //using var writer = File.Create($"{path}/{photo.Id}");
                await writer.WriteAsync(bytes, token);
                
                return new OperationStatus
                {
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromException("Error while creating photo.", ex);
            }
        }

        public async Task<OperationStatus> BatchCreatePhoto(string baseFolder, List<Photo> photos, CancellationToken token)
        {

            try
            {
                baseFolder.ThrowIfNullOrWhiteSpace(nameof(baseFolder));
                photos.CheckNullOrEmpty(nameof(photos));

                foreach (var photo in photos)
                {
                    await CreatePhoto(baseFolder, photo, token);
                }

                return new OperationStatus
                {
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromException("Error while batch creating photos.", ex);
            }
        }
    }
}
