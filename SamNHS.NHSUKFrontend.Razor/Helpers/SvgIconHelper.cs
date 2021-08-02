namespace SamNHS.NHSUKFrontend.Razor.Helpers
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
            string iconName = null;
            switch (svgIcon)
            {
                case SvgIcon.ArrowLeft:
                    iconName = "icon-arrow-left";
                    break;
                case SvgIcon.ArrowRight:
                    iconName = "icon-arrow-right";
                    break;
                case SvgIcon.ArrowRightCircle:
                    iconName = "icon-arrow-right-circle";
                    break;
                case SvgIcon.ChevronLeft:
                    iconName = "icon-chevron-left";
                    break;
                case SvgIcon.ChevronRight:
                    iconName = "icon-chevron-right";
                    break;
                case SvgIcon.Close:
                    iconName = "icon-close";
                    break;
                case SvgIcon.Cross:
                    iconName = "icon-cross";
                    break;
                case SvgIcon.EmDash:
                    iconName = "icon-emdash";
                    break;
                case SvgIcon.EmDashSmall:
                    iconName = "icon-emdash-small";
                    break;
                case SvgIcon.Minus:
                    iconName = "icon-minus";
                    break;
                case SvgIcon.Plus:
                    iconName = "icon-plus";
                    break;
                case SvgIcon.Search:
                    iconName = "icon-search";
                    break;
                case SvgIcon.Tick:
                    iconName = "icon-tick";
                    break;
                default:
                    break;
            }

            if (iconName == null) return null;

            return $"_content/{ns}/assets/icons/{iconName}.svg";
        }

        public string GetSvgIconVirtualPath(SvgIcon svgIcon)
        {
            return $"~/{this.GetSvgIconSubPath(svgIcon)}";
        }
    }
}
