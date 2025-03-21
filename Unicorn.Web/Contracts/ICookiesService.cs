// <copyright file="ICookiesService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;

namespace Unicorn.Web
{
    public interface ICookiesService
    {
        void AddCookie(string cookieName, string cookieValue, string path = "/");

        void DeleteAllCookies();

        void DeleteCookie(string cookieName);

        List<Cookie> GetAllCookies();

        string GetCookie(string cookieName);
    }
}
