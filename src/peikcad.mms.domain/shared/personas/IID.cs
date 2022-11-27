using System.Text.RegularExpressions;

namespace peikcad.mms.domain.shared.personas
{
    // [I]nfernal [I]d [D]ocument
    public sealed class IID : ValueObject
    {
        private static readonly Regex AllowedPattern = new("[0-9]{3}[a-z]{1}",
            RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex AllowedControlChars = new("[a-z]",
            RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public uint Number { get; }

        public char ControlChar { get; }

        public static Result<IID> Create(uint number, char controlChar)
        {
            if (number < 100)
                return new(new ArgumentOutOfRangeException(nameof(number)));
            
            if (!AllowedControlChars.IsMatch(controlChar.ToString()))
                return new(new ArgumentException(nameof(controlChar)));
            
            return new(new IID(number, controlChar));
        }

        public string Serialize() => $"{Number}/{ControlChar}";

        public static Result<IID> Deserialize(string value)
        {
            if (value.Trim() is {Length: 0})
                return new(new ArgumentNullException(nameof(value)));

            if (!AllowedPattern.IsMatch(value))
                return new(new FormatException());

            var numAndControl = value.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            _ = !uint.TryParse(numAndControl.First(), out var num);

            return new(new IID(num, numAndControl.Last()[0]));
        }

        private IID(uint number, char controlChar) : base(number, controlChar)
        {
            Number = number;
            ControlChar = controlChar;
        }
    }
}