﻿// <copyright file="BaseTest.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using NUnit.Framework;
using Unicorn.Plugins;

namespace Unicorn.NUnit
{
    [TestFixture]
    public class BaseTest
    {
        private readonly TestExecutionSubject _testExecutionSubject;

        public BaseTest()
        {
            _testExecutionSubject = new TestExecutionSubject();
        }

        public TestContext TestContext { get; set; }

        public string TestName => TestContext.CurrentContext.Test.FullName;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            InitializeTestExecutionBehaviorObservers();
        }

        [SetUp]
        public void SetUp()
        {
            var memberInfo = GetCurrentExecutionTestClassType().GetMethod(TestContext.CurrentContext.Test.Name);
            _testExecutionSubject.PreTestInit(memberInfo);
            TestInit();
            _testExecutionSubject.PostTestInit(memberInfo);
        }

        [TearDown]
        public void TearDown()
        {
            var memberInfo = GetCurrentExecutionTestClassType().GetMethod(TestContext.CurrentContext.Test.Name);
            var outcome = (TestOutcome)TestContext.CurrentContext.Result.Outcome.Status;
            _testExecutionSubject.PreTestCleanup(outcome, memberInfo);
            TestCleanup();
            _testExecutionSubject.PostTestCleanup(outcome, memberInfo);
        }

        public virtual void ClassInit()
        {
        }

        public virtual void TestInit()
        {
        }

        public virtual void TestCleanup()
        {
        }

        private Type GetCurrentExecutionTestClassType()
        {
            Type testClassType = GetType().Assembly.GetType(TestContext.CurrentContext.Test.ClassName);

            return testClassType;
        }

        private void InitializeTestExecutionBehaviorObservers()
        {
            var observers = ServiceContainer.ResolveAll<BaseTestExecutionPluginObserver>();
            foreach (var observer in observers)
            {
                _testExecutionSubject.Attach(observer);
            }
        }
    }
}
