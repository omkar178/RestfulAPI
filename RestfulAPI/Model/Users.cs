using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPI.Model
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Users

    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
