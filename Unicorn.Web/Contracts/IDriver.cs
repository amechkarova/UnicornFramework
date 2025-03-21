// <copyright file="IDriver.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public interface IDriver : INavigationService, IBrowserService, ICookiesService, IElementCreateService, IDialogService, IJavaScriptService, IInteractionsService
    {
    }
}
