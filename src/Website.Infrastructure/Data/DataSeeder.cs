using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(WebsiteDbContext db)
    {
        if(!db.Messages.Any())
        {
            var messages = new ContactUsMessage[]
            {
                ContactUsMessage.Create("First title", "First content", "Kamran Sadin", "MrSadin@Gmail.Com", "+989308638095"),
                ContactUsMessage.Create("Second title", "Second content", "Kamran Sadin", "MrSadin@Gmail.Com", "+989308638095")
            };
        
            db.Messages.AddRange(messages);
            db.SaveChanges();
        }
    }
}
