using Microsoft.Extensions.Logging;

/**
Name: Danny Moczynski/ Jordyn Henrich
Description: Lab 3 Remote Database
Date: 9/25/2023
Bugs:  none
Reflection: Connecting to the remote database was difficult to undestand in the lab
due to the speed of which you connected, but after meeting with you, it was much easier
to understand. Didn't have many issues that couldn't be solved quickly either. 
*/
namespace Lab2;

public static class MauiProgram
{
    // Instantiate my business logic object
    public static BusinessLogic BusinessLogic = new BusinessLogic();
    /// <summary>
    /// This method creates the builder and adds the fonts for the app UI
    /// </summary>
    /// <returns> The Application </returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}