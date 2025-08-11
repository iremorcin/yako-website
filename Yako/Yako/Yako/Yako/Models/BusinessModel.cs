namespace Yako.UI.Models
{
    public class BusinessModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string MapUrl { get; set; }
        public string ImageUrl { get; set; }
        public double? AverageRating { get; set; }

        public string CategoryName { get; set; }
    }
}
