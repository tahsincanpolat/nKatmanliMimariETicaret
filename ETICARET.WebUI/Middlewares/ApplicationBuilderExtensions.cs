using Microsoft.Extensions.FileProviders;

namespace ETICARET.WebUI.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CustomStaticFiles(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),"node_modules");

            var options = new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/modules"
            };

            app.UseStaticFiles(options);

            return app;
        }
    }
}
