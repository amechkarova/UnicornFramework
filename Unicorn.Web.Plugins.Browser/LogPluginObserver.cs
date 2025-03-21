// <copyright file="LogPluginObserver.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using System.Reflection;
using Unicorn.Plugins;
using Logger = Unicorn.Logging.Logger;

namespace Unicorn.Web.Plugins.Browser
{
    public class LogPluginObserver : BaseTestExecutionPluginObserver
    {
        public LogPluginObserver(TestExecutionSubject _testExecutionSubject)
            : base(_testExecutionSubject)
        {
        }

        public override void PreTestInit(MemberInfo memberInfo)
        {
            Logger.LogInfo($"Start Test {memberInfo.Name}");
        }

        public override void PostTestInit(MemberInfo memberInfo)
        {
            Logger.LogInfo($"End Test {memberInfo.Name}");
        }
    }
}
