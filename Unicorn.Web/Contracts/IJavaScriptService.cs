// <copyright file="IJavaScriptService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public interface IJavaScriptService
    {
        object Execute(string script);
    }
}
