using System;

namespace API.Models.Photo
{
    public class PhotoDetailsModel
    {
        public Guid Id { get; set; }
        public Guid AdId { get; set; }
        public string PhotoUrl { get; set; }
    }
}