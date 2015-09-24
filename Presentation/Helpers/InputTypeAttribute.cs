using System;

namespace Tcbcsl.Presentation.Helpers
{
    public enum InputType
    {
        Email,
        Number,
        Text,
        Url
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class InputTypeAttribute : Attribute
    {
        public InputTypeAttribute(InputType inputType)
        {
            InputType = inputType.ToString().ToLower();
        }

        public string InputType { get; }
    }
}