// <copyright file="WebSettings.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class WebSettings
    {
        public TimeoutSettings TimeoutSettings { get; set; }

        public BrowserSettings Chrome { get; set; }

        public BrowserSettings Firefox { get; set; }

        public BrowserSettings Edge { get; set; }

        public BrowserSettings Safari { get; set; }
    }
}
