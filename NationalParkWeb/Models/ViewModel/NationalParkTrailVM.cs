using System.Collections.Generic;

namespace NationalParkWeb.Models.ViewModel
{
    public class NationalParkTrailVM
    {
        public IEnumerable<NationalPark> nationalParks { get; set; }
        public IEnumerable<Trail> trails { get; set; }  
    }
}
