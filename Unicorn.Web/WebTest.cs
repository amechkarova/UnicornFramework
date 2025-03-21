// <copyright file="WebTest.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.NUnit;

namespace Unicorn.Web
{
    public class WebTest : BaseTest
    {
        public WebApp App => new WebApp();
    }
}
