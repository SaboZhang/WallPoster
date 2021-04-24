using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPoster.Models
{
    [Table("media_location")]
    public class PathModel
    {
        [Key]
        public int Id { get; set; }
        public string MoviePath { get; set; }
        public string TVPath { get; set; }
    }
}
