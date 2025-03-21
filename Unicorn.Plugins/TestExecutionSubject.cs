// <copyright file="TestExecutionSubject.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using System.Reflection;

namespace Unicorn.Plugins
{
    public class TestExecutionSubject
    {
        private readonly List<BaseTestExecutionPluginObserver> _testExecutionPlugins;

        public TestExecutionSubject()
        {
            _testExecutionPlugins = new List<BaseTestExecutionPluginObserver>();
        }

        public void Attach(BaseTestExecutionPluginObserver observer)
        {
            _testExecutionPlugins.Add(observer);
        }

        public void Detach(BaseTestExecutionPluginObserver observer)
        {
            _testExecutionPlugins.Remove(observer);
        }

        public void PreClassInit(MemberInfo memberinfo)
        {
            _testExecutionPlugins.ForEach(p => p.PreClassInit(memberinfo));
        }

        public void PostClassInit(MemberInfo memberinfo)
        {
            _testExecutionPlugins.ForEach(p => p.PostClassInit(memberinfo));
        }

        public void PostTestCleanup(TestOutcome testOutcome, MemberInfo memberinfo)
        {
            _testExecutionPlugins.ForEach(p => p.PostTestCleanup(testOutcome, memberinfo));
        }

        public void PostTestInit(MemberInfo memberinfo)
        {
            _testExecutionPlugins.ForEach(p => p.PostTestInit(memberinfo));
        }

        public void PreTestCleanup(TestOutcome testOutcome, MemberInfo memberinfo)
        {
            _testExecutionPlugins.ForEach(p => p.PreTestCleanup(testOutcome, memberinfo));
        }

        public void PreTestInit(MemberInfo memberinfo)
        {
            _testExecutionPlugins.ForEach(p => p.PreTestInit(memberinfo));
        }

        public void TestInstantiated(MemberInfo memberInfo)
        {
            _testExecutionPlugins.ForEach(p => p.TestInstantiated(memberInfo));
        }
    }
}
