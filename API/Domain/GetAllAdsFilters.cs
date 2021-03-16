using API.Services;

namespace API.Domain
{
    public class GetAllAdsFilters
    {
        public AdService.Cond Condition { get; set; }

        public float MaxPrice { get; set; }
        
        public float MinPrice { get; set; }

        public string Bookname { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }
    }
}