namespace NELCCG.NHSUKFrontend.Razor.Models
{
    public class FrontendOptions
    {
        public enum FrontendVersion
        {
            Version__4_1_0,
            Version__5_1_0,
            Version__5_2_0,
            Version__5_2_1,
        }

        public FrontendVersion Version { get; set; } = FrontendVersion.Version__5_2_1;
    }
}
