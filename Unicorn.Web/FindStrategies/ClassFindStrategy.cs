using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Web.FindStrategies
{
    public class ClassFindStrategy : FindStrategy
    {
        public ClassFindStrategy(string value)
            : base(value)
        {
        }

        public override By Convert() => By.XPath($"//*[@class='{Value}']");
    }
}
