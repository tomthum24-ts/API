﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Model
{
    internal sealed class Bootstrapper
    {
        private static readonly Lazy<Bootstrapper> Lazy = new Lazy<Bootstrapper>(() => new Bootstrapper());
        private Bootstrapper()
        {
            if (!Lazy.IsValueCreated)
            {
                RegisterExecutable();
            }
        }
        public static Bootstrapper Instance => Lazy.Value;
        public string WorkingFolder { get; private set; }

        public void RegisterExecutable()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            WorkingFolder = Path.GetFullPath(Path.Combine(new Uri(assembly.Location).LocalPath, $"..{Path.DirectorySeparatorChar.ToString()}"));

            var librariesNameSpace = $"{nameof(BaseCommon)}.{nameof(Common)}.{nameof(Model)}";

            Dictionary<string, string> resources = new Dictionary<string, string>
            {
                { HttpDetectionConstants.DbName, $"{librariesNameSpace}.{HttpDetectionConstants.DbName}" },
            };

            foreach (KeyValuePair<string, string> resource in resources)
            {
                using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource.Value))
                {
                    if (resourceStream == null)
                    {
                        continue;
                    }

                    var toolFullPath = Path.Combine(WorkingFolder, resource.Key);

                    using (FileStream fileStream = File.OpenWrite(toolFullPath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
            }
        }
    }
}
