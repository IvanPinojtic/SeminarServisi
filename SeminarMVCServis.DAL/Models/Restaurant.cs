using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
    }
}
