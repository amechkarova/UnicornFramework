// <copyright file="WebCoreDriver.Interactions.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web.Services
{
    public partial class WebCoreDriver : IInteractionsService
    {
        public IInteractionsService ClickAndHold(Element element) => throw new NotImplementedException();

        public IInteractionsService DoubleClick(Element element) => throw new NotImplementedException();

        public IInteractionsService DragAndDrop(Element sourceElement, Element destinationElement) => throw new NotImplementedException();

        public IInteractionsService MoveToElement(Element element) => throw new NotImplementedException();

        public void Perform() => throw new NotImplementedException();

        public IInteractionsService Release() => throw new NotImplementedException();
    }
}
