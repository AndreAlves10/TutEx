using Messaging.Models;
using System;
using System.Linq;

namespace Messaging.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MessagingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Messages.Any()) //DB has been seeded
                return;

            var messages = new Message[]
            {
                new Message {
                   UserIDFrom = 1,
                   UserIDTo = 2,
                   NameUserFrom = "User1",
                   NameUserTo = "User2",
                   Content = "Olá User2",
                   MessageUTCCreatedDate = new DateTime(2018, 06, 16),
                   SeenByUserTo = true
                },
                 new Message {
                   UserIDFrom = 2,
                   UserIDTo = 1,
                   NameUserFrom = "User2",
                   NameUserTo = "User1",
                   Content = "Olá User1",
                   MessageUTCCreatedDate = new DateTime(2018, 06, 17),
                   SeenByUserTo = false
                },
                 new Message {
                   UserIDFrom = 1,
                   UserIDTo = 2,
                   NameUserFrom = "User1",
                   NameUserTo = "User2",
                   Content = "Olá!!",
                   MessageUTCCreatedDate = new DateTime(2018, 06, 17),
                   SeenByUserTo = false
                },
                 new Message {
                   UserIDFrom = 1,
                   UserIDTo = 3,
                   NameUserFrom = "User1",
                   NameUserTo = "User3",
                   Content = "Olá User3",
                   MessageUTCCreatedDate = new DateTime(2018, 06, 17),
                   SeenByUserTo = false
                }
            };

            foreach (Message m in messages)
            {
                context.Messages.Add(m);
            }
            context.SaveChanges();
        }
    }
}
