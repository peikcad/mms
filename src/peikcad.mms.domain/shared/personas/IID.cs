using System.Text.RegularExpressions;

namespace peikcad.mms.domain.shared.personas
{
    // [I]nfernal [I]d [D]ocument
    public sealed class IID : ValueObject
    {
        private static readonly Regex AllowedControlChars = new("[a-z]", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
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

        public static Result<IID> Deserialize(string value)
        {
            throw new NotImplementedException();
        }

        private IID(uint number, char controlChar) : base(number, controlChar)
        {
            Number = number;
            ControlChar = controlChar;
        }
    }
}