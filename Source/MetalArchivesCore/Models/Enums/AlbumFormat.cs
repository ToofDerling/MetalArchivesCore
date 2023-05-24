using System.ComponentModel;

namespace MetalArchivesCore.Models.Enums
{
    public enum AlbumFormat : byte
    {
        [Description("CD")]
        Cd = 1,

        [Description("Cassette")]
        Cassette,
        [Description("Vinyl")]

        Vinyl,
        [Description("VHS")]
        Vhs,

        [Description("DVD")]
        Dvd,

        [Description("Digital")]
        Digital,

        [Description("Blu-ray")]
        BluRay,

        [Description("Other")]
        Other = 255
    }
}
