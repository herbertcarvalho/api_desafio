using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Enums;
using Backend.Erp.Skeleton.Domain.Repositories;
using NUnit.Framework;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.IntegrationTest
{
    public class PersonsIntegrationDataTest(
        IPersonsRepository personsRepository,
        IUnitOfWork unitOfWork) : ITest
    {
        private readonly IPersonsRepository _personsRepository = personsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("PersonsIntegrationDataTest");
            await MapPerson();
            await ExecuteAssert(ExecutePostPerson(), report, "ExecutePostPerson");
            await ExecuteAssert(AnyByCpf(), report, "AnyByCpf");
            await ExecuteAssert(GetIdUser(), report, "GetIdUser");
            await ExecuteAssert(UpdatePerson(), report, "UpdatePerson");
            await ExecuteAssert(DeletePerson(), report, "DeletePerson");
        }

        private Persons newPerson = new();
        private const string CPF = "79221995038";
        private const string NEW_PERSON_NAME = "Person new name";

        private const string CPF_AUX = "92097130011";
        private const string NEW_PERSON_NAME_AUX = "Person new name XXX";

        private async Task MapPerson()
        {
            var person = await _personsRepository.AddAsync(new Persons()
            {
                IdUser = 1,
                IdUserType = (int)UserTypeEnum.Company,
                IdCompany = 1,
                Cpf = "02873851082",
                Name = "Person mapping"
            });

            await _unitOfWork.SaveChangesAsync();
        }

        [Test]
        private async Task ExecutePostPerson()
        {
            var person = await _personsRepository.AddAsync(new Persons()
            {
                IdUser = 2,
                IdUserType = (int)UserTypeEnum.Company,
                IdCompany = 1,
                Cpf = CPF,
                Name = NEW_PERSON_NAME
            });

            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(person.Name, NEW_PERSON_NAME);
            Assert.AreEqual(person.Cpf, CPF);

            newPerson = person;
        }

        [Test]
        private async Task AnyByCpf()
        {
            var exits = await _personsRepository.Any(CPF);

            Assert.AreEqual(exits, true);
        }

        [Test]
        private async Task GetIdUser()
        {
            var person = await _personsRepository.Get(newPerson.IdUser);

            Assert.AreEqual(person.Name, NEW_PERSON_NAME);
            Assert.AreEqual(person.Cpf, CPF);
        }

        [Test]
        private async Task UpdatePerson()
        {
            newPerson.Name = NEW_PERSON_NAME_AUX;
            newPerson.Cpf = CPF_AUX;

            await _personsRepository.UpdateAsync(newPerson);
            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(newPerson.Name, NEW_PERSON_NAME_AUX);
            Assert.AreEqual(newPerson.Cpf, CPF_AUX);
        }

        [Test]
        private async Task DeletePerson()
        {
            await _personsRepository.DeleteAsync(newPerson);
            await _unitOfWork.SaveChangesAsync();

            var person = await _personsRepository.GetByIdAsync(newPerson.Id);

            Assert.IsNull(person);
        }
    }
}
