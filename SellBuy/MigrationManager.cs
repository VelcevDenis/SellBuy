using Microsoft.EntityFrameworkCore;

namespace SellBuy
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host) {
            using (var scope = host.Services.CreateScope()) {
                using (var appContext = scope.ServiceProvider.GetRequiredService<SBDbContext>()) {
                    appContext.Database.Migrate();                    
                }
            }
            return host;
        }
    }
}
