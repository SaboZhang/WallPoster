using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPoster.Models
{
    [Table("AreaInfo")]
    public class AreaModel
    {
        [Key]
        public int Id { get; set; }

        public long AreaCode { get; set; }

        public string ProvinceCode { get; set; }

        public string CityCode { get; set; }

        public string CountyCode { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

        public string CountyName { get; set; }
    }

    [Table("ProvinceInfo")]
    public class AdmModel
    {
        [Key]
        public int Id { get; set; }

        public string CityName { get; set; }

        public string ProvinceCode { get; set; }
    }
}
