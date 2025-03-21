// <copyright file="IDialogService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;
using Unicorn.Web.Enums;

namespace Unicorn.Web
{
    public interface IDialogService
    {
        void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok);
    }
}
