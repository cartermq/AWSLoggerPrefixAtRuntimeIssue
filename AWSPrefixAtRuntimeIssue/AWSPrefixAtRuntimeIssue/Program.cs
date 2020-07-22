using System;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;
using NLog.Targets;

namespace AWSPrefixAtRuntimeIssue
{
   

    class Program
    {
        // Define a static logger variable so that it references the logger instanced named "Scribe"
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            ExtendNLogConfig();

            LogManager.ConfigurationReloaded += LogManager_ConfigurationReloaded;
            LogManager.Configuration = LogManager.Configuration;
            log.Info("Entering Application.");

            Console.WriteLine("Press any key to exit ...");
            Console.Read();
        }


        private static void LogManager_ConfigurationReloaded(object sender, LoggingConfigurationReloadedEventArgs e)
        {
            ExtendNLogConfig();
        }

        /// <summary>
        /// Extend the logging rules in the nlog.config with programmically rules.
        /// </summary>
        private static void ExtendNLogConfig()
        {
            //don't set  LogManager.Configuration because that will overwrite the nlog.config settings
            //var consoleTarget =  new AWS.Logger.AWSLoggerConfig("testing") ;
            var bacon = "bacon";
            var egg = "egg";
        var cheese = "cheese";
            var config = NLog.LogManager.Configuration;
            var awstarget = config.FindTargetByName("aws") as NLog.AWS.Logger.AWSTarget;
            awstarget.LogStreamNamePrefix = $"{bacon}-{egg}-n-{cheese}";
            NLog.LogManager.Configuration = config;
            
            NLog.LogManager.ReconfigExistingLoggers();           
            LogManager.ReconfigExistingLoggers();
        }
    }
}
