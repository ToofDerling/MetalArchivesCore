using System.ComponentModel;

namespace MetalArchivesCore.Models.Enums
{
    public enum BandStatus : byte
    {
        [Description("Active")]
        Active = 1,

        [Description("On hold")]
        OnHold = 2,

        [Description("Split-up")]
        SplitUp = 3,

        [Description("Unknown")]
        Unknown = 4,

        [Description("Changed name")]
        ChangedName = 5,

        [Description("Disputed")]
        Disputed = 6
    }
}
