using Bogus;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Fakes
{
    public class FakesDataHelper
    {

        private static readonly Faker<Project> _projectFaker = new Faker<Project>()
            .CustomInstantiator(Faker => new Project(Faker.Commerce.ProductName(),
                                                     Faker.Lorem.Sentence(),
                                                     Faker.Random.Int(1, 100),
                                                     Faker.Random.Int(1, 100),
                                                     Faker.Random.Decimal(1000, 10000)
            ));

        private static readonly Faker<InsertProjectCommand> _insertProjectCommandFaker = new Faker<InsertProjectCommand>()
            .RuleFor(c => c.Title, f => f.Commerce.ProductName())
            .RuleFor(c => c.Description, f => f.Commerce.ProductName())
            .RuleFor(c => c.IdFreelancer, f => f.Random.Int(1, 100))
            .RuleFor(c => c.IdCliente, f => f.Random.Int(1, 100))
            .RuleFor(c => c.TotalCost, f => f.Random.Decimal(1000, 10000));


        public static Project CreateFakeProject()
        {
            return _projectFaker.Generate();
        }

        public static InsertProjectCommand CreateFakeInsertProjectCommand()
        {
            return _insertProjectCommandFaker.Generate();
        }   

    }
}
