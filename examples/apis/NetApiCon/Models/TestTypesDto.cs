using System.Reflection;
using Bogus;

namespace NetApiCon.Models
{
    public enum TestTypesEnum
    {
        America,
        Europe,
        Africa,
        Asia,
        Oceania,
        Antarctica
    }

    public class TestTypesDto
    {
        public byte TypeByte { get; set; }
        public short TypeShort { get; set; }
        public int Typeint { get; set; }
        public long TypeLong { get; set; }
        public float TypeFloat { get; set; }
        public double TypeDouble { get; set; }
        public decimal TypeDecimal { get; set; }
        public char TypeChar { get; set; }
        public bool TypeBool { get; set; }

        public TestTypesEnum TypeEnum { get; set; }

        public DateTime TypeDateTime { get; set; }
        public string TypeString { get; set; } = null!;
        public object TypeObject { get; set; } = null!;

        public byte[] TypeByteArray { get; set; } = null!;
        public int[] TypeIntArray { get; set; } = null!;
        public string[] TypeStringArray { get; set; } = null!;
        public object[] TypeObjectgArray { get; set; } = null!;

        public List<object> TypeList { get; set; } = null!;
        public Dictionary<string, object?> TypeDictionary { get; set; } = null!;

        public static TestTypesDto Generate()
        {
            var faker = new Faker();

            var fakerEntityDto = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            var oneEntityDto = fakerEntityDto.Generate(1)[0];

            var dto = new TestTypesDto
            {
                TypeByte = faker.Random.Byte(),
                TypeShort = faker.Random.Short(),
                Typeint = faker.Random.Int(),
                TypeLong = faker.Random.Long(),
                TypeFloat = faker.Random.Float(),
                TypeDouble = faker.Random.Double(),
                TypeDecimal = faker.Random.Decimal(),
                TypeChar = faker.Random.Char(),
                TypeBool = faker.Random.Bool(),

                TypeEnum = faker.PickRandom(Enum.GetValues<TestTypesEnum>()),

                TypeDateTime = DateTime.Now,
                TypeString = faker.Random.String2(16),
                TypeObject = fakerEntityDto.Generate(2),

                TypeByteArray = faker.Random.Bytes(16),
                TypeIntArray = Enumerable.Range(1, 8).Select(_ => faker.Random.Int(1)).ToArray(),
                TypeStringArray = Enumerable.Range(1, 8).Select(_ => faker.Random.Word()).ToArray(),
                TypeObjectgArray = Enumerable.Range(1, 2).Select(_ => fakerEntityDto.Generate(1)).ToArray(),

                TypeList = Enumerable.Range(1, 2).Select(_ => fakerEntityDto.Generate(1)).ToList<object>(),
                TypeDictionary = oneEntityDto.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(oneEntityDto, null))
            };

            return dto;
        }
    }
}
