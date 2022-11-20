﻿using FluentAssertions;
using NSubstitute;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskoMask.BuildingBlocks.Application.Exceptions;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Resources;
using TaskoMask.BuildingBlocks.Domain.Exceptions;
using TaskoMask.BuildingBlocks.Domain.Resources;
using TaskoMask.Services.Owners.Write.Application.UseCases.Organizations.AddOrganization;
using TaskoMask.Services.Owners.Write.Domain.Events.Organizations;
using TaskoMask.Services.Owners.Write.Domain.ValueObjects.Organizations;
using TaskoMask.Services.Owners.Write.UnitTests.Fixtures;
using TaskoMask.Services.Owners.Write.UnitTests.TestData;
using Xunit;

namespace TaskoMask.Services.Owners.Write.UnitTests.UseCases.Organizations
{
    public class AddOrganizationTests : TestsBaseFixture
    {

        #region Fields

        private AddOrganizationUseCase _addOrganizationUseCase;

        #endregion

        #region Ctor

        public AddOrganizationTests()
        {
        }

        #endregion

        #region Test Methods


        [Fact]
        public async Task Organization_Is_Added()
        {
            //Arrange
            var expectedOwner = Owners.FirstOrDefault();
            var addOrganizationRequest = new AddOrganizationRequest(expectedOwner.Id, "Test_Name", "Test_Description");

            //Act
            var result = await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            result.Message.Should().Be(ContractsMessages.Create_Success);
            result.EntityId.Should().NotBeNull();
            expectedOwner.Organizations.Should().Contain(o => o.Id == result.EntityId);
            await InMemoryBus.Received(1).PublishEvent(Arg.Any<OrganizationAddedEvent>());
            await MessageBus.Received(1).Publish(Arg.Any<OrganizationAdded>());
        }



        [Fact]
        public async Task Add_Organization_Throw_Exception_When_Owner_Not_Exist()
        {
            //Arrange
            var addOrganizationRequest = new AddOrganizationRequest("Some_Owner_Id_That_Not_Exist", "Test_Name", "Test_Description");
            var expectedMessage = string.Format(ContractsMessages.Data_Not_exist, DomainMetadata.Owner);

            //Act
            System.Func<Task> act = async () => await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<ApplicationException>().Where(e => e.Message.Equals(expectedMessage));
        }



        [InlineData("test", "test")]
        [InlineData("تست", "تست")]
        [Theory]
        public async Task Organization_Is_Not_Added_When_Name_And_Description_Are_The_Same(string name, string description)
        {
            //Arrange
            var expectedOwner = Owners.FirstOrDefault();
            var addOrganizationRequest = new AddOrganizationRequest(expectedOwner.Id, name, description);
            var expectedMessage = DomainMessages.Equal_Name_And_Description_Error;

            //Act
            System.Func<Task> act = async () => await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedMessage));
        }



        [InlineData("Th")]
        [InlineData("This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test")]
        [Theory]
        public async Task Organization_Is_Not_Added_When_Name_Lenght_Is_Less_Than_Min_Or_More_Than_Max(string name)
        {
            //Arrange
            var expectedOwner = Owners.FirstOrDefault();
            var addOrganizationRequest = new AddOrganizationRequest(expectedOwner.Id, name, "Test_Description");
            var expectedMessage = string.Format(ContractsMetadata.Length_Error, nameof(OrganizationName), DomainConstValues.Organization_Name_Min_Length, DomainConstValues.Organization_Name_Max_Length);

            //Act
            System.Func<Task> act = async () => await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedMessage));
        }




        [InlineData("This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test This is a Test")]
        [Theory]
        public async Task Organization_Is_Not_Added_When_Description_Lenght_Is_More_Than_Max(string description)
        {
            //Arrange
            var expectedOwner = Owners.FirstOrDefault();
            var addOrganizationRequest = new AddOrganizationRequest(expectedOwner.Id, "Test_Name", description);
            var expectedMessage = string.Format(ContractsMetadata.Max_Length_Error, nameof(OrganizationDescription), DomainConstValues.Organization_Description_Max_Length);

            //Act
            System.Func<Task> act = async () => await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedMessage));
        }



        [Fact]
        public async Task Organization_Is_Not_Added_When_Name_Is_Not_Unique()
        {
            //Arrange
            var expectedMessage = string.Format(DomainMessages.Name_Already_Exist, DomainMetadata.Organization);
            var existedOwner = Owners.FirstOrDefault();
            var existedOrganization = OwnerObjectMother.GetAnOrganization();
            existedOwner.AddOrganization(existedOrganization);
            await OwnerAggregateRepository.UpdateAsync(existedOwner);
            var addOrganizationRequest = new AddOrganizationRequest(existedOwner.Id, existedOrganization.Name.Value, "Test_Description");

            //Act
            System.Func<Task> act = async () => await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedMessage));
        }




        [Fact]
        public async Task Organization_Is_Not_Added_When_Owner_Has_Added_Max_Organizations()
        {
            //Arrange
            var expectedMaxOrganizationsCount = DomainConstValues.Owner_Max_Organizations_Count;
            var expectedMessage = string.Format(DomainMessages.Max_Organizations_Count_Limitiation, expectedMaxOrganizationsCount);
            var expectedOwner = OwnerObjectMother.GetAnOwnerWithMaxOrganizations(OwnerValidatorService, expectedMaxOrganizationsCount);
            await OwnerAggregateRepository.CreateAsync(expectedOwner);
            var addOrganizationRequest = new AddOrganizationRequest(expectedOwner.Id, "Test_Name", "Test_Description");

            //Act
            System.Func<Task> act = async () => await _addOrganizationUseCase.Handle(addOrganizationRequest, CancellationToken.None);

            //Assert
            await act.Should().ThrowAsync<DomainException>().Where(e => e.Message.Equals(expectedMessage));
        }




        #endregion

        #region Fixture

        protected override void TestClassFixtureSetup()
        {
            _addOrganizationUseCase = new AddOrganizationUseCase(OwnerAggregateRepository, MessageBus, InMemoryBus);
        }

        #endregion
    }
}
