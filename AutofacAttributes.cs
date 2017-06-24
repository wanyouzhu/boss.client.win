using System;

namespace boss.client.win
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PageAttribute : Attribute
    {
        public string Code { get; set; }

        public PageAttribute(string code)
        {
            Code = code;
        }
    }
}