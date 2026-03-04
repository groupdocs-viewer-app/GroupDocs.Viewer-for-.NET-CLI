using GroupDocs.Viewer.UI.Core;

namespace GroupDocs.Viewer.Cli;

public static class ServerBootstrap
{
    public static WebApplication Build(CliOptions options)
    {
        var builder = WebApplication.CreateBuilder();

        builder.WebHost.UseUrls($"http://{options.Bind}:{options.Port}");

        if (!options.Verbose)
        {
            builder.Logging.ClearProviders();
        }

        builder.Services.AddGroupDocsViewerUI(config =>
        {
            config.RenderingMode = options.ViewerType.ToRenderingMode();

            if (options.IsFileMode)
            {
                config.InitialFile = options.FileName;
                config.EnableFileBrowser = false;
                config.EnableFileUpload = false;
            }
        });

        builder.Services
            .AddControllers()
            .AddGroupDocsViewerSelfHostApi(config =>
            {
                config.SetViewerType(options.ViewerType);

                if (options.LicensePath != null)
                    config.SetLicensePath(options.LicensePath);
            })
            .AddLocalStorage(options.StoragePath)
            .AddLocalCache(Path.Combine(Path.GetTempPath(), "groupdocs-viewer-cache"));

        var app = builder.Build();

        app.UseRouting()
           .UseEndpoints(endpoints =>
           {
               endpoints.MapGroupDocsViewerUI(opts =>
               {
                   opts.UIPath = "/";
                   opts.ApiEndpoint = "/api";
               });
               endpoints.MapGroupDocsViewerApi(opts =>
               {
                   opts.ApiPath = "/api";
               });
           });

        return app;
    }
}
