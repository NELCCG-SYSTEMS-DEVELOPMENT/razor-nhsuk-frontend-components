namespace SamNHS.NHSUKFrontend.Razor.Models
{
    public class FrontendOptions
    {
        public enum FrontendVersion
        {
            Version__4_1_0,
            Version__5_1_0,
        }

        public FrontendVersion Version { get; set; } = FrontendVersion.Version__5_1_0;
    }
}
