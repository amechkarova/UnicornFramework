// <copyright file="IInteractionsService.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public interface IInteractionsService
    {
        IInteractionsService Release();

        IInteractionsService MoveToElement(Element element);

        IInteractionsService DragAndDrop(Element sourceElement, Element destinationElement);

        IInteractionsService DoubleClick(Element element);

        IInteractionsService ClickAndHold(Element element);

        void Perform();


    }
}
