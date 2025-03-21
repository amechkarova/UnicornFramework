// <copyright file="ElementWaitStrategies.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

using Unicorn.Web;
using Unicorn.Web.WaitStrategies;

namespace Unicorn
{
    public static class ElementWaitStrategies
    {

        // Generic extension methods implementing the fluent API
        public static TElement ToExists<TElement>(this TElement element, int? timeoutIntervalSeconds = null, int? sleepIntervalSeconds = null)
            where TElement : Element
        {
            var toExistsWaitStrategy = new ToExistWaitStrategy(timeoutIntervalSeconds, sleepIntervalSeconds);
            element?.EnsureState(toExistsWaitStrategy);
            return element;
        }

        public static TElement ToBeVisible<TElement>(this TElement element, int? timeoutIntervalSeconds = null, int? sleepIntervalSeconds = null)
            where TElement : Element
        {
            var toBeVisibleWaitStrategy = new ToBeVisibleWaitStrategy(timeoutIntervalSeconds, sleepIntervalSeconds);
            element.EnsureState(toBeVisibleWaitStrategy);
            return element;
        }

        public static TElement ToBeClickable<TElement>(this TElement element, int? timeoutIntervalSeconds = null, int? sleepIntervalSeconds = null)
            where TElement : Element
        {
            var toBeClickableWaitStrategy = new ToBeClickableWaitStrategy(timeoutIntervalSeconds, sleepIntervalSeconds);
            element.EnsureState(toBeClickableWaitStrategy);
            return element;
        }
    }
}
