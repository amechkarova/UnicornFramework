// <copyright file="TestOutcome.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Plugins
{
    public enum TestOutcome
    {
        NotSet = 100,
        Failed = 0,
        Inconclusive = 1,
        Passed = 2,
        InProgress = 3,
        Error = 4,
        Timeout = 5,
        Aborted = 6,
        Unknown = 7,
    }
}
