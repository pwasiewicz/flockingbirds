namespace FlockingBirds.Configurator.Validators
{
    using System.Windows.Controls;
    using System;
    using System.Globalization;

    public class ResolutionRule : ValidationRule
    {
        public ResolutionRule()
        {
            this.Max = 1920;
            this.Min = 640;
        }

        public int Max { get; set; }

        public int Min { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = Convert.ToString(value);
            var intValue = 0;

            try
            {
                if (stringValue.Length > 0)
                {
                    if (!Int32.TryParse(stringValue, out intValue))
                    {
                        return new ValidationResult(isValid: false, errorContent: "Illegal characters.");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(isValid: false, errorContent: ex.Message);
            }

            if (intValue < this.Min)
            {
                return new ValidationResult(isValid: false, errorContent: string.Format("Value is too low. Minimum: {0}", this.Min));
            }

            return intValue > this.Max
                ? new ValidationResult(isValid: false,
                    errorContent: string.Format("Value is too high. Maximum: {0}", this.Max))
                : new ValidationResult(isValid: true, errorContent: null);
        }
    }
}
