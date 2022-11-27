using System.Text.RegularExpressions;

namespace peikcad.mms.domain.shared.personas
{
    public sealed class CompleteName : ValueObject
    {
        private static readonly Regex AllowedPattern = new(@"[a-z]+.\s*[a-z\s-]{1, 20},\s*[a-z\s-]{1, 7}",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public string Name { get; }

        public string Surname { get; }

        public PersonalTitle Title { get; }

        public static Result<CompleteName> Create(string name, string surname, PersonalTitle title)
            => Result<CompleteName>.Return
                .If(() => AllowedPattern.IsMatch($"{title}. {surname}, {name}"))
                .Try(() => new CompleteName(name, surname, title))
                .OrException<FormatException>()
                .As();

        public string Serialize() => $"{Title}. {Surname}, {Name}";

        public static Result<CompleteName> Deserialize(string value)
            => Result<CompleteName>.Return
                .If(() => AllowedPattern.IsMatch(value))
                .Try(() =>
                {
                    var nameAndTitle = value.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var title = Enum.Parse<PersonalTitle>(nameAndTitle.First());
                    var nameAndSurname = nameAndTitle.Last().Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    return new CompleteName(nameAndSurname.Last(), nameAndSurname.First(), title);
                })
                .OrException<FormatException>()
                .As();

        private CompleteName(string name, string surname, PersonalTitle title) : base(name, surname, title)
        {
            Name = name;
            Surname = surname;
            Title = title;
        }
    }
}