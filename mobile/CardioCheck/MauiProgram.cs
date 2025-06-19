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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
