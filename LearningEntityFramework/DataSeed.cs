using LearningEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningEntityFramework;

public static class DataSeed
{
   public static void AddUserData(this WebApplication app, MyBoardsContext dbContext)
   {
      var users = dbContext.Users.ToList();
      if (!users.Any())
      {
         var user1 = new User()
         {
            Email = "user1@email.com",
            FullName = "User One",
            Address = new Address()
            {
               City = "Warszawa",
               Street = "Szeroka"
            }
         };


         var user2 = new User()
         {
            Email = "user2@email.com",
            FullName = "User Two",
            Address = new Address()
            {
               City = "Kraków",
               Street = "Szeroka"
            }
         };

         dbContext.Users.AddRange(user1, user2);
         dbContext.SaveChanges();
      }
   }
   public static void AddTagData(this WebApplication app, MyBoardsContext dbContext)
   {
      var tags = dbContext.Tags.ToList();
      if (!tags.Any())
      {
         var newTags = new List<Tag>
         {
            new Tag() { Value = "Web" },
            new Tag() { Value = "UI" },
            new Tag() { Value = "Desktop" },
            new Tag() { Value = "API" },
            new Tag() { Value = "Service" },
         };

         dbContext.Tags.AddRange(newTags);
         dbContext.SaveChanges();
      }
   }
}