// <copyright file="Button.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class Button : Element
    {
        public string InnerText => WrappedElement.Text;

        public string Value => WrappedElement.GetDomAttribute("value");

        public bool IsDisabled => !WrappedElement.Enabled;

        public void Click()
        {
            WrappedElement.Click();
        }
    }
}
