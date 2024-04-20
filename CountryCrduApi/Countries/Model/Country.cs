using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryCrduApi.Countries.Model
{
    [Table("country")]
    public class Country
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]  
        [Column("capital")]
        public string Capital { get; set; }

        [Required]
        [Column("population")]
        public int Population { get; set; }



    }
}
