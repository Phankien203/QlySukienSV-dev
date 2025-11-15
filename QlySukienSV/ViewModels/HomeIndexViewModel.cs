using QlySukienSV.Models;

namespace QlySukienSV.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CLB> Clbs { get; set; } = Enumerable.Empty<CLB>();
        public IEnumerable<SuKien> NoiBatSuKiens { get; set; } = Enumerable.Empty<SuKien>();
        public IEnumerable<SuKien> UpcomingSuKiens { get; set; } = Enumerable.Empty<SuKien>();
    }
}
