// <copyright file="Image.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class Image : Element
    {
        public string Source => WrappedElement.GetDomAttribute("src");

        public void Click()
        {
            WrappedElement.Click();
        }
    }
}
