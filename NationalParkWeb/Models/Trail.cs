using System;
using System.ComponentModel.DataAnnotations;

namespace NationalParkWeb.Models
{
    public class Trail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public double Elevation { get; set; }
        public enum DiffcultyType { Easy, Moderate, Difficult, Expert }
        public DiffcultyType Diffculty { get; set; }
        [Required]
        public int NationalParkId { get; set; }       
        public NationalPark NationalPark { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
