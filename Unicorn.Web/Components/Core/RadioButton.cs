// <copyright file="RadioButton.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class RadioButton : Element
    {
        public string Value => WrappedElement.GetDomAttribute("value");

        public bool IsChecked => WrappedElement.Selected;

        public bool IsDisabled => !WrappedElement.Enabled;

        public void Click()
        {
            WrappedElement.Click();
        }
    }
}
