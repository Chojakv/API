namespace Application.Models.Queries
{
    
    public class GetAllAdsQueries
    {
        public Cond Condition { get; set; }

        public string CategoryId { get; set; }

        public float MaxPrice { get; set; }
        
        public float MinPrice { get; set; }

        public string Bookname { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }
    }
    
    public enum Cond
    {
        All, New, Used
    }
}