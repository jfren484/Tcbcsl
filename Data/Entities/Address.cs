﻿using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class Address : EntityModifiable
    {
        public int AddressId { get; set; }

        [MaxLength(100)]
        public string Street1 { get; set; }

        [MaxLength(100)]
        public string Street2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        public int? StateId { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        public virtual State State { get; set; }
    }
}
