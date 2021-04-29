using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPoster.Models
{
    [Table("MediaFileInfo")]
    public class FilesModel
    {
        [Key]
        public int Id { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public string? Caption { get; set; }
        public string? FileSize { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime FileModifyTime { get; set; }
        public string? Ext { get; set; }
        public string? Duration { get; set; }
        public string? PrivatePwd { get; set; }
        public string? Category { get; set; }
        public string? StoreSite { get; set; }
        public string? Container { get; set; }
    }
}
