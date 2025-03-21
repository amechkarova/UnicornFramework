// <copyright file="IBrowserService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public interface IBrowserService
    {
        void WaitForAjax();

        void WaitUntilPageLoadsCompletely();
    }
}
