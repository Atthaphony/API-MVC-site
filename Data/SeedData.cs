using System.Text.Json;
using Head_brands.web.Models;

namespace Head_brands.web.Data
{
    public class SeedData
    {
         public static async Task LoadOmgData(HeadIndexContext context)
        {
            // Steg 1.
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Steg 2. Vill endast ladda data om vår vehicles tabell är tom...
            if (context.Heads.Any()) return;

            // Steg 3. Läsa in json informationen ifrån vår vehicles.json fil...
            var json = System.IO.File.ReadAllText("Data/json/Omg.json");

            // Steg 4. Omvandla json objekten till en lista av VehicleModel objekt...
            var heads = JsonSerializer.Deserialize<List<HeadModel>>(json, options);

            // Steg 5. Skicka listan med VehicleModel objekt till databasen...
            if (heads is not null && heads.Count > 0)
            {
                await context.Heads.AddRangeAsync(heads);
                await context.SaveChangesAsync();
            }
        }
    }
}