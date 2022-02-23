using System.ComponentModel.DataAnnotations;
using static RestfulAPI.Model.Trail;

namespace RestfulAPI.Model.Dtos
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailCreateDtos
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DiffcultyType Diffculty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
