using Microsoft.Extensions.Options;
using Saunter.Generation;
using Saunter.Generation.SchemaGeneration;
using Shouldly;
using Xunit;

namespace Saunter.Tests.Generation.SchemaGeneration
{
    public class SchemaGenerationTests
    {
        private readonly ISchemaRepository _schemaRepository;
        private readonly SchemaGenerator _schemaGenerator;

        public SchemaGenerationTests()
        {
            _schemaRepository = new SchemaRepository();
            var options = new AsyncApiDocumentGeneratorOptions();

            _schemaGenerator = new SchemaGenerator(Options.Create(options));
        }

        [Fact]
        public void GenerateSchema_GenerateSchemaFromTypeWithProperties_GeneratesSchemaCorrectly()
        {
            var type = typeof(Foo);

            var schema = _schemaGenerator.GenerateSchema(type, _schemaRepository);

            schema.ShouldNotBeNull();
            _schemaRepository.Schemas.ShouldNotBeNull();
            _schemaRepository.Schemas.ContainsKey("foo").ShouldBeTrue();
            _schemaRepository.Schemas["foo"].Properties.Count.ShouldBe(2);
            _schemaRepository.Schemas.ContainsKey("bar").ShouldBeTrue();
            _schemaRepository.Schemas["bar"].Properties.Count.ShouldBe(2);
        }

        [Fact]
        public void GenerateSchema_GenerateSchemaFromTypeWithFields_GeneratesSchemaCorrectly()
        {
            var type = typeof(Book);

            var schema = _schemaGenerator.GenerateSchema(type, _schemaRepository);

            schema.ShouldNotBeNull();
            _schemaRepository.Schemas.ShouldNotBeNull();
            _schemaRepository.Schemas.ContainsKey("book").ShouldBeTrue();
            _schemaRepository.Schemas["book"].Properties.Count.ShouldBe(4);
            _schemaRepository.Schemas.ContainsKey("foo").ShouldBeTrue();
            _schemaRepository.Schemas["foo"].Properties.Count.ShouldBe(2);
        }
    }

    public class Foo
    {
        public int Id { get; set; }

        public Bar Bar { get; set; }
    }

    public class Bar
    {
        public string Name { get; set; }

        public decimal? Cost { get; set; }
    }

    public class Book
    {
        public readonly string Name;

        public readonly string Author;

        public readonly int NumberOfPages;

        public readonly Foo Foo;

        public Book(string name, string author, int numberOfPages, Foo foo)
        {
            Author = author;
            Name = name;
            NumberOfPages = numberOfPages;
            Foo = foo;
        }
    }
}