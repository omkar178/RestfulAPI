using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestfulAPI.Model.Trail;

namespace RestfulAPI.Model.Dtos
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailDtocs
    {
       
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
      
        public DiffcultyType Diffculty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        
        public NationalParkDto NationalPark { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
