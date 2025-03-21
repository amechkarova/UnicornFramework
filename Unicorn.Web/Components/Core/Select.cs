// <copyright file="Select.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium.Support.UI;

namespace Unicorn.Web
{
    public class Select : Element
    {
        public string Value => WrappedElement.GetDomAttribute("value");

        public bool IsDisabled => !WrappedElement.Enabled;

        public void SelectByText(string text)
        {
            new SelectElement(WrappedElement).SelectByText(text);
        }

        public void SelectByValue(string value)
        {
            new SelectElement(WrappedElement).SelectByValue(value);
        }

        public void SelectByIndex(int index)
        {
            new SelectElement(WrappedElement).SelectByIndex(index);
        }
    }
}
