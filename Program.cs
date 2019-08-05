﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>()
        //    .Build();
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //Trieu 2018.10.06
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>().UseDefaultServiceProvider(option => option.ValidateScopes = false);
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>();
    }
}
