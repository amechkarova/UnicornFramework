// <copyright file="TestsInitialize.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Plugins;
using Unicorn.Web.Plugins.Browser;
using Unicorn.Web.Services;

namespace Unicorn.SystemTests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Adding different plugins here. We can create extension methods for improving readability.
            ServiceContainer.RegisterType<BaseTestExecutionPluginObserver, BrowserLaunchPluginObserver>(Guid.NewGuid().ToString());
            ServiceContainer.RegisterType<BaseTestExecutionPluginObserver, LogPluginObserver>(Guid.NewGuid().ToString());
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            var driverFactory = ServiceContainer.Resolve<DriverFactory>();
            driverFactory.Dispose();
        }
    }
}
