namespace Countries.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Flag { get; set; }
        public string Capital { get; set; }
        public string CountryCode { get; set; }
        public double Population { get; set; }
        public bool IsUnMember { get; set; }
    }
}
