﻿// <copyright file="Span.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class Span : Element
    {
        public string InnerText => WrappedElement.Text;

        public string InnerHtml => WrappedElement.GetDomAttribute("innerHTML");
    }
}
