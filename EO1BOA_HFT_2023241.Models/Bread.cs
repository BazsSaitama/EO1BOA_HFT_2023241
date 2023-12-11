using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Models
{
    public class Bread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BreadId { get; set; }
        public string Name { get; set; }
        public bool IsDessert { get; set; } //Is it Sweets or Regular Bread
        public int Weight { get; set; } //gramms

        [ForeignKey(nameof(Bakery))]
        public int BakeryId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Bakery Bakery{ get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Oven> Ovens { get; set; }

       
    }
}
