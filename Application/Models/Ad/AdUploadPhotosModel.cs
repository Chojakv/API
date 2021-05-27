using System;
using System.Collections.Generic;
using Domain.Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Models.Ad
{
    public class AdUploadPhotosModel
    {
        public ICollection<IFormFile> Images { get; set; }

    }
}