namespace peikcad.mms.domain.shared.personas
{
    public sealed class CompleteName : ValueObject
    {
        public string Name { get; }
        
        public string Surname { get; }
        
        public PersonalTitle Title { get; }

        public static Result<CompleteName> Create(string name, string surname, PersonalTitle title)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                name.Length > 7 ||
                name.ToLower().Contains('z'))
                return new Result<CompleteName>(new FormatException(nameof(name)));
            
            if (string.IsNullOrWhiteSpace(surname) ||
                surname.Length > 20)
                return new Result<CompleteName>(new FormatException(nameof(surname)));

            return new Result<CompleteName>(
                new CompleteName(name, surname, title));
        }

        public static Result<CompleteName> Deserialize(string value)
        {
            throw new NotImplementedException();
        }

        private CompleteName(string name, string surname, PersonalTitle title) : base(name, surname, title)
        {
            Name = name;
            Surname = surname;
            Title = title;
        }
    }
}