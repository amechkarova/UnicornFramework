// <copyright file="WebCoreDriver.Cookies.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web.Services
{
    public partial class WebCoreDriver : ICookiesService
    {
        public void AddCookie(string cookieName, string cookieValue, string path = "/")
        {
            _webDriver.Manage().Cookies.AddCookie(new Cookie(cookieName, cookieValue, path));
        }

        public void DeleteAllCookies()
        {
            _webDriver.Manage().Cookies.DeleteAllCookies();
        }

        public void DeleteCookie(string cookieName)
        {
            _webDriver.Manage().Cookies.DeleteCookieNamed(cookieName);
        }

        public List<Cookie> GetAllCookies()
        {
            List<Cookie> result = new List<Cookie>();
            foreach (var cookie in _webDriver.Manage().Cookies.AllCookies)
            {
                result.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path));
            }

            return result;
        }

        public string GetCookie(string cookieName)
        {
            return _webDriver.Manage().Cookies.GetCookieNamed(cookieName).Value;
        }
    }
}
