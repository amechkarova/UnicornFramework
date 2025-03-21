// <copyright file="LoggingSettings.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Logging
{
    public class LoggingSettings
    {
        public bool IsEnabled { get; set; }

        public bool IsConsoleLoggingEnabled { get; set; }

        public bool IsDebugLoggingEnabled { get; set; }

        public bool IsFileLoggingEnabled { get; set; }

        public string? OutputTemplate { get; set; }
    }
}
