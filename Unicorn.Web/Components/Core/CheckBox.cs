// <copyright file="CheckBox.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class CheckBox : Element
    {
        public bool IsChecked => WrappedElement.Selected;

        public string Value => WrappedElement.GetDomAttribute("value");

        public bool IsDisabled => !WrappedElement.Enabled;

        public void Check(bool isChecked = true)
        {
            if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
            {
                WrappedElement.Click();
            }
        }

        public void Uncheck()
        {
            Check(false);
        }
    }
}
