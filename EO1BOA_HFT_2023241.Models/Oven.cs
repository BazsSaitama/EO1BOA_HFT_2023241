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
    public class Oven
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OvenId { get; set; }
        public int BreadCapacity { get; set; } //How many breads fit
        public int NunmberOfMaterials { get; set; }
        public int BakingTime { get; set; }

        [ForeignKey("Bread")]
        public int BreadId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Bread Bread { get; set; }
    }
}
