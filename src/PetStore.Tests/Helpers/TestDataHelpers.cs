using PetStore.Tests.DTOs;
using System.Numerics;

namespace PetStore.Tests.Helpers
{
    internal class TestDataHelpers
    {
        internal static User CreateNewUser()
        {
            User user = new();
            GeneratePropertyValues(user);
            return user;
        }

        internal static void GeneratePropertyValues(User user)
        {
            user.FirstName = Guid.NewGuid().ToString();
            user.LastName = Guid.NewGuid().ToString();
            user.Password = Guid.NewGuid().ToString();
            user.Phone = Guid.NewGuid().ToString();
            user.UserName = Guid.NewGuid().ToString();
        }

        internal static void EditUserFieds(User user)
        {
            user.FirstName = Guid.NewGuid().ToString();
            user.LastName = Guid.NewGuid().ToString();
            user.Password = Guid.NewGuid().ToString();
            user.Phone = Guid.NewGuid().ToString();
        }
    }
}