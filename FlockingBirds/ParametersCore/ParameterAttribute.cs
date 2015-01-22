namespace FlockingBirds.ParametersCore
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal class ParameterAttribute : Attribute
    {
        internal ParameterAttribute(string name, bool expectValue = true, string @default = null)
        {
            this.ExpectValue = expectValue;
            this.Name = name;
            this.@Default = @default;
        }

        public bool ExpectValue { get; set; }

        public string Name { get; set; }

        public string @Default { get; set; }
    }
}
