using Microsoft.Extensions.Logging;

namespace CardioCheck
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Adicionando fontes do projeto CardioCheck
                    fonts.AddFont("Lato-Regular.ttf", "LatoRegular");
                    fonts.AddFont("Lato-Bold.ttf", "LatoBold");
                    fonts.AddFont("fontello.ttf", "FontIcons");

                    fonts.AddFont("CinzelDecorative-Regular.ttf", "Cinzel");
                    fonts.AddFont("Comfortaa-Regular.ttf", "ComfortaaRegular");
                    fonts.AddFont("Comfortaa-Light.ttf", "ComfortaaLight");
                    fonts.AddFont("Comfortaa-Bold.ttf", "ComfortaaBold");
                    fonts.AddFont("JuliusSansOne-Regular.ttf", "Julius");
                    fonts.AddFont("Pacifico-Regular.ttf", "Pacifico");
                    fonts.AddFont("Quicksand-Regular.ttf", "QuicksandRegular");

                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
