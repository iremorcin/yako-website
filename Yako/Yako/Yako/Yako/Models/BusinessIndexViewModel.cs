namespace Yako.UI.Models
{
    public class BusinessIndexViewModel
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public List<BusinessModel> Businesses { get; set; } = new List<BusinessModel>();
    }
}
