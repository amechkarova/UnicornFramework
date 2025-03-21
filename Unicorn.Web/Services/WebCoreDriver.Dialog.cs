// <copyright file="WebCoreDriver.Dialog.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;
using Unicorn.Web.Enums;

namespace Unicorn.Web.Services
{
    public partial class WebCoreDriver : IDialogService
    {
        public void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok)
        {
            var alert = _webDriver.SwitchTo().Alert();
            action?.Invoke(alert);
            if (dialogButton == DialogButton.Ok)
            {
                alert.Accept();
                _webDriver.SwitchTo().DefaultContent();
            }
            else
            {
                alert.Dismiss();
            }

            _webDriver.SwitchTo().DefaultContent();
        }
    }
}
