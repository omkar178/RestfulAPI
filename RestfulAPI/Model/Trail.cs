using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPI.Model
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Trail

    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance  { get; set; }
        public enum DiffcultyType { Easy,Moderate,Difficult,Expert }
        
        public DiffcultyType Diffculty { get; set; }
        [Required]        
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }   
        public DateTime CreatedDate { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
