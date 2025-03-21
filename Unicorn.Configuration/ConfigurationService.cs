// <copyright file="ConfigurationService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Unicorn
{
    public class ConfigurationService
    {
        private static readonly IConfigurationRoot s_configurationRoot;

        static ConfigurationService()
        {
            s_configurationRoot = InitializeConfiguration();
        }

        public static TSection GetSection<TSection>()
            where TSection : class, new()
        {
            string sectionName = typeof(TSection).Name.MakeFirstLetterToLower();
            return s_configurationRoot.GetSection(sectionName).Get<TSection>();
        }

        private static IConfigurationRoot InitializeConfiguration()
        {
            var filesInExecutionDir = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var settingsFile = filesInExecutionDir.FirstOrDefault(f => f.Contains("testFrameworkSettings") && f.EndsWith(".json"));
            var builder = new ConfigurationBuilder();
            if (File.Exists(settingsFile))
            {
                builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
            }

            return builder.Build();
        }
    }
}
