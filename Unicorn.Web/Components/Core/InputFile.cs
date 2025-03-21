// <copyright file="InputFile.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.Web
{
    public class InputFile : Element
    {
        public void Upload(string file)
        {
            WrappedElement.SendKeys(file);
        }
    }
}
