// <copyright file="BaseTestExecutionPluginObserver.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using System.Reflection;

namespace Unicorn.Plugins
{
    public class BaseTestExecutionPluginObserver
    {
        private readonly TestExecutionSubject _testExecutionSubject;

        // Passing the subject to each observer gives them access to shared test context(like test names or statuses)
        public BaseTestExecutionPluginObserver(TestExecutionSubject testExecutionSubject)
        {
            _testExecutionSubject = testExecutionSubject;
            _testExecutionSubject.Attach(this);
        }

        public virtual void PreClassInit(MemberInfo memberinfo)
        {
        }

        public virtual void PostClassInit(MemberInfo memberinfo)
        {
        }

        public virtual void PreTestCleanup(TestOutcome testOutcome, MemberInfo memberinfo)
        {
        }

        public virtual void PreTestInit(MemberInfo memberinfo)
        {
        }

        public virtual void TestInstantiated(MemberInfo memberInfo)
        {
        }

        public virtual void PostTestCleanup(TestOutcome testOutcome, MemberInfo memberinfo)
        {
        }

        public virtual void PostTestInit(MemberInfo memberinfo)
        {
        }
    }
}
