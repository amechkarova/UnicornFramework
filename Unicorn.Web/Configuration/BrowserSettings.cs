// <copyright file="BrowserSettings.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class BrowserSettings
    {
        public int PageLoadTimeout { get; set; } = 30;

        public int ScriptTimeout { get; set; } = 30;
    }
}
