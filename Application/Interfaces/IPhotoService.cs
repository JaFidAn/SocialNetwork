using Application.Features.Profiles.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IPhotoService
{
    Task<PhotoUploadResultDto?> UploadPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}
