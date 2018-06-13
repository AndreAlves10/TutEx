using Meeting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Meeting.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MeetingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            /*if (context.Teachers.Any()) //DB has been seeded
                return;*/

            
        }
    }
}
