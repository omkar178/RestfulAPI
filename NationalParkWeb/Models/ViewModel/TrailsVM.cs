using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NationalParkWeb.Models.ViewModel
{
    public class TrailsVM
    {
        public IEnumerable<SelectListItem> NationalParkList { get; set; }
        public Trail trail { get; set; }    

    }
}
