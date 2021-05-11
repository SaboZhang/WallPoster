using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPoster.Models
{
    [Table("AreaInfo")]
    public class AreaModel
    {
        [Key]
        public int LocationId { get; set; }

        public string CityName { get; set; }

        public string Adm1 { get; set; }

        public string Adm2 { get; set; }

        public int AdCode { get; set; }

        public string FullCityName { get; set; }
    }
}
