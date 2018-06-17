using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace General.Data
{
    public class DbInitializer
    {
        public static void Initialize(GeneralContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.UserPreference.Any()) //DB has been seeded
                return;
        }
    }
}
