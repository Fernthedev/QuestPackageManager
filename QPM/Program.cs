﻿using McMaster.Extensions.CommandLineUtils;
using QuestPackageManager;
using QuestPackageManager.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QPM
{
    [Command("qpm", Description = "Quest package manager")]
    [Subcommand(typeof(PackageCommand), typeof(DependencyCommand), typeof(RestoreCommand)/*, typeof(PublishCommand) */)]
    internal class Program
    {
        private const string PackageFileName = "qpm.json";
        internal static DependencyHandler DependencyHandler { get; private set; }
        internal static PackageHandler PackageHandler { get; private set; }
        internal static RestoreHandler RestoreHandler { get; private set; }

        private static IConfigProvider configProvider;
        private static IUriHandler uriHandler;

        public static int Main(string[] args)
        {
            // Create config provider
            configProvider = new LocalConfigProvider(Environment.CurrentDirectory, PackageFileName);
            // Create handlers
            PackageHandler = new PackageHandler(configProvider);
            DependencyHandler = new DependencyHandler(configProvider);
            //RestoreHandler = new RestoreHandler(configProvider, uriHandler);
            int exit = -1;
            try
            {
                exit = CommandLineApplication.Execute<Program>(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return exit;
        }

        private void OnExecute(CommandLineApplication app) => app.ShowHelp();
    }
}