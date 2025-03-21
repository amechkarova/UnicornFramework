// <copyright file="TextField.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class TextField : Element
    {
        public string InnerText => WrappedElement.Text;

        public bool IsDisabled => !WrappedElement.Enabled;

        public void TypeText(string text)
        {
            WrappedElement.Clear();
            WrappedElement.SendKeys(text);
        }
    }
}
