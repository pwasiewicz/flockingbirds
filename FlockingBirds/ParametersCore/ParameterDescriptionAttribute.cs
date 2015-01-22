namespace FlockingBirds.ParametersCore
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class ParameterDescriptionAttribute : Attribute
    {
        public ParameterDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
