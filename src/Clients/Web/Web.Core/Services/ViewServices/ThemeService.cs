using MudBlazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Enums;

namespace Web.Core.Services.ViewServices
{
    public class ThemeService
    {
        private IDictionary<ThemeType, MudTheme> _themes;

        public ThemeService()
        {
            _themes = new Dictionary<ThemeType, MudTheme>();

            MudTheme defaultTheme = new()
            {
                //Palette = new()
                //{
                //    Primary = Gold,
                //    Surface = GrayLVL4,
                //    Secondary = GrayLVL2,

                //    TextPrimary = StandartLightColor2,
                //    TextSecondary = StandartLightColor3,
                //    TextDisabled = StandartLightColor1,
                    
                //},
                PaletteDark = new()
                {
                    TextPrimary = StandartLightColor2,
                    TextSecondary = StandartLightColor3,
                    TextDisabled = StandartLightColor1,

                    Primary = Gold,
                    Surface = GrayLVL3,

                    SecondaryDarken = GrayLVL4,
                    Secondary = GrayLVL3,
                    SecondaryLighten = GrayLVL2,

                    TertiaryDarken = GrayLVL1,
                    Tertiary = GrayLVL0,
                    TertiaryLighten = LightLVL0,

                },
            };

            _themes.Add(ThemeType.Default, defaultTheme);
        }

        public MudTheme GetTheme(ThemeType themeType = ThemeType.Default) => _themes.FirstOrDefault(x => x.Key == themeType).Value;

        private readonly string Gold = "#F3CA20";

        private readonly string GrayLVL0 = "#4D4D51";
        private readonly string GrayLVL1 = "#2D2D30";
        private readonly string GrayLVL2 = "#252527";
        private readonly string GrayLVL3 = "#1A1A1B";
        private readonly string GrayLVL4 = "#121213";

        private readonly string LightLVL0 = "#8A8A93";

        private readonly string StandartLightColor1 = "#CCC5C5";
        private readonly string StandartLightColor2 = "#F0E7E7";
        private readonly string StandartLightColor3 = "#FFFFFF";
    }
}
