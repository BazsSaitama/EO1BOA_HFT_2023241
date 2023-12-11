using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EO1BOA_HFT_2023241.Models
{
    public class Bakery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BakeryId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Bread> Breads { get; set; }
        

    }
}
