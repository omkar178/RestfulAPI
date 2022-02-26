﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Model.Dtos
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkCreateDto
    {        
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
