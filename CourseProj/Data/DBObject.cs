using CourseProj.Data;
using CourseProj.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public static class DBObject
    {
        public static void Initial(DBContent content)
        {
            if (!content.User.Any())
                content.User.AddRange(Users.Select(c => c.Value));
            content.SaveChanges();
        }

        public static Dictionary<string, User> Users;
    }
}
