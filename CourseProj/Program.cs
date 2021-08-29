using CloudinaryDotNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj
{
    public class Program
    {
        public static Cloudinary cloudinary;

        public const string CLOUD_NAME = "duymqmdpp";
        public const string API_KEY = "689235495947759";
        public const string API_SECRET = "34eqfnzFmGuixL1JQrBJwXy_zbE";
        public static void Main(string[] args)
        {
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
