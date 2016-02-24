using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDynamics.Stores.Web.Infrastructure.Configuration
{
    public class VersionOptions
    {
        public string Version { get; set; }

        public VersionOverrideOptions[] VersionOverrides { get; set; }
    }
}
