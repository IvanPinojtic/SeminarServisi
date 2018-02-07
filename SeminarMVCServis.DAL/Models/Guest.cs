using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
