// <copyright file="TimeoutSettings.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class TimeoutSettings
    {
        public int WaitForAjaxTimeout { get; set; } = 30;
        public int SleepInterval { get; set; } = 1;

        public int ElementToBeVisibleTimeout { get; set; } = 30;

        public int ElementToExistTimeout { get; set; } = 30;

        public int ElementToNotExistTimeout { get; set; } = 1;

        public int ElementToBeClickableTimeout { get; set; } = 30;

        public int ElementNotToBeVisibleTimeout { get; set; } = 10;

        public int ElementToHaveContentTimeout { get; set; } = 30;
    }
}
