namespace NELCCG.NHSUKFrontend.Razor.Helpers
{
    using Microsoft.AspNetCore.Hosting;
    using System.IO;
    using System.Threading.Tasks;

    public class SvgIconHelper
    {
        private readonly IWebHostEnvironment _env;

        public enum SvgIcon
        {
            ArrowLeft,
            ArrowRight,
            ArrowRightCircle,
            ChevronLeft,
            ChevronRight,
            Close,
            Cross,
            EmDash,
            EmDashSmall,
            Minus,
            Plus,
            Search,
            Tick,
        }

        public SvgIconHelper(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public async Task<string> GetSvgIconContentAsync(SvgIcon svgIcon)
        {
            string content = null;
            var icon = this._env.WebRootFileProvider.GetFileInfo(this.GetSvgIconSubPath(svgIcon));
            if (icon != null && icon.Exists)
            {
                using var reader = new StreamReader(icon.CreateReadStream());
                content = await reader.ReadToEndAsync();
            }
            return content;

        }

        public string GetSvgIconSubPath(SvgIcon svgIcon)
        {
            var ns = Path.GetFileNameWithoutExtension(this.GetType().Module.Name);
            var iconName = svgIcon switch
            {
                SvgIcon.ArrowLeft => "icon-arrow-left",
                SvgIcon.ArrowRight => "icon-arrow-right",
                SvgIcon.ArrowRightCircle => "icon-arrow-right-circle",
                SvgIcon.ChevronLeft => "icon-chevron-left",
                SvgIcon.ChevronRight => "icon-chevron-right",
                SvgIcon.Close => "icon-close",
                SvgIcon.Cross => "icon-cross",
                SvgIcon.EmDash => "icon-emdash",
                SvgIcon.EmDashSmall => "icon-emdash-small",
                SvgIcon.Minus => "icon-minus",
                SvgIcon.Plus => "icon-plus",
                SvgIcon.Search => "icon-search",
                SvgIcon.Tick => "icon-tick",
                _ => null,
            };

            if (iconName == null) return null;

            return $"_content/{ns}/assets/icons/{iconName}.svg";
        }

        public string GetSvgIconVirtualPath(SvgIcon svgIcon)
        {
            return $"~/{this.GetSvgIconSubPath(svgIcon)}";
        }
    }
}
