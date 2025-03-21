// <copyright file="LoggingDriverDecorator.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;
using Unicorn.Web.Enums;
using Unicorn.Web.FindStrategies;

namespace Unicorn.Web.Services
{
    public class LoggingDriverDecorator : BaseDriverDecorator
    {
        public LoggingDriverDecorator(IDriver driver)
            : base(driver)
        {
        }

        public void AddCookie(string cookieName, string cookieValue, string path = "/")
        {
            Console.WriteLine("call AddCookie");
            base.AddCookie(cookieName, cookieValue, path);
        }

        public IInteractionsService ClickAndHold(Element element)
        {
            Console.WriteLine("call ClickAndHold");
            return base.ClickAndHold(element);
        }

        public List<TElement> CreateAll<TElement>(FindStrategy findStrategy)
            where TElement : Element
        {
            Console.WriteLine($"call CreateAll");
            return base.CreateAll<TElement>(findStrategy);
        }

        public void DeleteAllCookies()
        {
            Console.WriteLine($"call {nameof(DeleteAllCookies)}");
            base.DeleteAllCookies();
        }

        public void DeleteCookie(string cookieName)
        {
            Console.WriteLine($"call {nameof(DeleteCookie)}");
            base.DeleteCookie(cookieName);
        }

        public IInteractionsService DoubleCkick(Element element)
        {
            Console.WriteLine($"call {nameof(DoubleCkick)}");
            return base.DoubleClick(element);
        }

        public IInteractionsService DragAndDrop(Element sourceElement, Element destinationElement)
        {
            Console.WriteLine($"call {nameof(DragAndDrop)}");
            return base.DragAndDrop(sourceElement, destinationElement);
        }

        public object Execute(string script)
        {
            Console.WriteLine($"call {nameof(Execute)}");
            return base.Execute(script);
        }

        public List<Cookie> GetAllCookies()
        {
            Console.WriteLine($"call {nameof(GetAllCookies)}");
            return base.GetAllCookies();
        }

        public string GetCookie(string cookieName)
        {
            Console.WriteLine($"call {nameof(GetCookie)}");
            return base.GetCookie(cookieName);
        }

        public void GoToUrl(string url)
        {
            Console.WriteLine($"call {nameof(GoToUrl)}");
            base.GoToUrl(url);
        }

        public void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok)
        {
            Console.WriteLine($"call {nameof(Handle)}");
            base.Handle(action, dialogButton);
        }

        public IInteractionsService MoveToElement(Element element)
        {
            Console.WriteLine($"call {nameof(MoveToElement)}");
            return base.MoveToElement(element);
        }

        public void Perform()
        {
            Console.WriteLine($"call {nameof(Perform)}");
            base.Perform();
        }

        public IInteractionsService Release()
        {
            Console.WriteLine($"call {nameof(Release)}");
            return base.Release();
        }

        public void WaitForajax()
        {
            Console.WriteLine($"call {nameof(WaitForajax)}");
            base.WaitForAjax();
        }

        public void WaitUntilPageLoadsCompletely()
        {
            Console.WriteLine($"call {nameof(WaitUntilPageLoadsCompletely)}");
            base.WaitUntilPageLoadsCompletely();
        }
    }
}
