using System;

namespace Application.Models.Ad
{
    public class AdPhotoDetailsModel
    {
        public Guid Id { get; set; }
        public Guid AdId { get; set; }
        public string ImageUrl { get; set; }
    }
}