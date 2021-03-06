namespace Application.Models.Queries
{

        public class GetAllAdsQueries
        {
            public string Condition { get; set; }

            public string CategoryId { get; set; }

            public decimal MaxPrice { get; set; }
        
            public decimal MinPrice { get; set; }

            public string Bookname { get; set; }

            public string Author { get; set; }

            public string Title { get; set; }
        }
}