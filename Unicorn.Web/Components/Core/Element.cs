// <copyright file="Element.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using OpenQA.Selenium;
using System.Drawing;
using Unicorn.Web.FindStrategies;
using Unicorn.Web.WaitStrategies;

namespace Unicorn.Web
{
    public abstract class Element : IElementCreateService
    {
        private List<WaitStrategy> _waitStrategies;
        protected IWebDriver Driver;

        public Element()
        {
            Driver = ServiceContainer.Resolve<IWebDriver>();
            _waitStrategies = new List<WaitStrategy>();
            _waitStrategies.Add(new ToExistWaitStrategy());
        }

        public By By { get; set; }

        public IWebElement WrappedElement => GetAndWaitElement();

        public IWebElement ParentElement { get; set; }

        public Size Size => WrappedElement.Size;

        public Point Location => WrappedElement.Location;

        public bool IsVisible => WrappedElement.Displayed;

        // In the case when we don't want to perform an action but still want to wait for the element this method is used
        public void WaitToBe()
        {
            GetAndWaitElement();
        }

        public string GetCssValue(string propertyName)
        {
            return WrappedElement.GetCssValue(propertyName);
        }

        public string GetAttribute(string attributeName)
        {
            return WrappedElement.GetAttribute(attributeName);
        }

        public void EnsureState(WaitStrategy waitStrategy)
        {
            _waitStrategies?.Add(waitStrategy);
        }

        public TElement Create<TElement>(FindStrategy findStrategy)
            where TElement : Element
        {
            TElement element = Activator.CreateInstance<TElement>();
            element.By = findStrategy.Convert();
            element.ParentElement = WrappedElement;

            return element;
        }

        public List<TElement> CreateAll<TElement>(FindStrategy findStrategy)
            where TElement : Element => throw new NotImplementedException();

        public List<TElement> CreateAllByClass<TElement>(string cssClass)
            where TElement : Element => throw new NotImplementedException();

        public List<TElement> CreateAllByCss<TElement>(string css)
            where TElement : Element => throw new NotImplementedException();

        public List<TElement> CreateAllById<TElement>(string id)
            where TElement : Element => throw new NotImplementedException();

        public List<TElement> CreateAllByLinkText<TElement>(string linkText)
            where TElement : Element => throw new NotImplementedException();

        public List<TElement> CreateAllByTag<TElement>(string tag)
            where TElement : Element => throw new NotImplementedException();

        public List<TElement> CreateAllByXPath<TElement>(string xpath)
            where TElement : Element => throw new NotImplementedException();

        public TElement CreateByClass<TElement>(string cssClass)
            where TElement : Element
        {
            return Create<TElement>(new ClassFindStrategy(cssClass));
        }

        public TElement CreateByCss<TElement>(string css)
            where TElement : Element
        {
            return Create<TElement>(new CssFindStrategy(css));
        }

        public TElement CreateById<TElement>(string id)
            where TElement : Element
        {
            return Create<TElement>(new IdFindStrategy(id));
        }

        public TElement CreateByLinkText<TElement>(string linkText)
            where TElement : Element
        {
            return Create<TElement>(new LinkTextFindStrategy(linkText));
        }

        public TElement CreateByTag<TElement>(string tag)
            where TElement : Element
        {
            return Create<TElement>(new TagFindStrategy(tag));
        }

        public TElement CreateByXPath<TElement>(string xpath)
            where TElement : Element
        {
            return Create<TElement>(new XPathFindStrategy(xpath));
        }

        private IWebElement GetAndWaitElement()
        {
            foreach (var strategy in _waitStrategies)
            {
                if (ParentElement != null)
                {
                    strategy.WaitUntil(ParentElement, Driver, By);
                }
                else
                {
                    strategy.WaitUntil(Driver, Driver, By);
                }
            }

            IWebElement webElement = default;
            webElement = ParentElement != null ? ParentElement.FindElement(By) : Driver.FindElement(By);

            return webElement;
        }
    }
}
