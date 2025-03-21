// <copyright file="HomePage.Assert.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.SystemTests.Pages
{
    public partial class HomePage
    {
        public void AssertProducBoxLink(string name, string expectedLink)
        {
            string actualLink = GetProductBoxByName(name).GetAttribute("href");
            Assert.That(actualLink, Is.EqualTo(expectedLink));
        }
    }
}
