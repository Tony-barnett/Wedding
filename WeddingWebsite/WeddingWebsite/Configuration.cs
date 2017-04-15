using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingWebsite
{
    public interface IConfiguration
    {
        Ttype GetConfigurationValue<Ttype>(string key);
    }
    public class Configuration: IConfiguration
    {
        private IConfigurationRoot _ConfigurationRoot;
        public Configuration(IConfigurationBuilder builder)
        {

            builder
                 .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _ConfigurationRoot = builder.Build();
        }

        public Ttype GetConfigurationValue<Ttype>(string key)
        {
            return _ConfigurationRoot.GetValue<Ttype>(key);
        }
    }
}