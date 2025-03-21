// <copyright file="Anchor.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class Anchor : Element
    {
        public string InnerText => WrappedElement.Text;

        public string Href => WrappedElement.GetDomAttribute("href");

        public bool IsDisabled => !WrappedElement.Enabled;

        public void Click()
        {
            WrappedElement.Click();
        }
    }
}
