﻿using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskoMask.Application.Core.Exceptions;
using TaskoMask.Application.Share.Resources;
using TaskoMask.Application.Tests.Unit.TestData;
using TaskoMask.Application.Workspace.Owners.Commands.Handlers;
using TaskoMask.Application.Workspace.Owners.Commands.Models;
using TaskoMask.Domain.Share.Resources;
using TaskoMask.Domain.DomainModel.Workspace.Owners.Data;
using TaskoMask.Domain.DomainModel.Workspace.Owners.Entities;
using TaskoMask.Domain.DomainModel.Workspace.Owners.Events.Owners;
using Xunit;

namespace TaskoMask.Application.Tests.Unit.Workspace
{
    public class OwnerCommandHandlersUnitTests : TestsBase
    {

        #region Fields

        private IOwnerAggregateRepository _ownerAggregateRepository;
        private OwnerCommandHandlers _ownerCommandHandlers;
        private List<Owner> _owners;


        #endregion

        #region Test Methods




        [Fact]
        public async Task Owner_Is_Created()
        {
            //Arrange
            var expectedUserId = ObjectId.GenerateNewId().ToString();
            var createOwnerCommand = new CreateOwnerCommand(expectedUserId, "Test_DisplayName", "Test@email.com", "Test_Password");

            //Act
            var result = await _ownerCommandHandlers.Handle(createOwnerCommand, CancellationToken.None);

            //Assert
            result.EntityId.Should().Be(expectedUserId);
            var createdUser = _owners.FirstOrDefault(u => u.Id == result.EntityId);
            createdUser.Email.Value.Should().Be(createOwnerCommand.Email);
            await _inMemoryBus.Received(1).Publish(Arg.Any<OwnerCreatedEvent>());
        }




        [Fact]
        public async Task Owner_Is_Updated()
        {
            //Arrange
            var ownerToUpdate = _owners.First();
            var updateOwnerCommand = new UpdateOwnerCommand(ownerToUpdate.Id, "New_DisplayName", "New@email.com");

            //Act
            var result = await _ownerCommandHandlers.Handle(updateOwnerCommand, CancellationToken.None);

            //Assert
            result.EntityId.Should().Be(ownerToUpdate.Id);
            ownerToUpdate.Email.Value.Should().Be(updateOwnerCommand.Email);
            await _inMemoryBus.Received(1).Publish(Arg.Any<OwnerUpdatedEvent>());
        }



        [Fact]
        public async Task Owner_Is_Deleted()
        {
            //Arrange
            var ownerToDelete = _owners.First();
            var deleteOwnerCommand = new DeleteOwnerCommand(ownerToDelete.Id);

            //Act
            var result = await _ownerCommandHandlers.Handle(deleteOwnerCommand, CancellationToken.None);

            //Assert
            result.EntityId.Should().Be(ownerToDelete.Id);
            ownerToDelete.IsDeleted.Should().BeTrue();
            await _inMemoryBus.Received(1).Publish(Arg.Any<OwnerDeletedEvent>());
        }



        [Fact]
        public async Task Owner_Is_Not_Deleted_When_Id_Not_Exist()
        {
            //Arrange
            var notExistOwnerId = ObjectId.GenerateNewId().ToString();
            var expectedMessage = string.Format(ApplicationMessages.Data_Not_exist, DomainMetadata.Owner);
            var deleteOwnerCommand = new DeleteOwnerCommand(notExistOwnerId);

            //Act
            Func<Task> func = async () => { await _ownerCommandHandlers.Handle(deleteOwnerCommand, CancellationToken.None); };

            //Assert
            await func.Should().ThrowAsync<Core.Exceptions.ApplicationException>().Where(e => e.Message.Equals(expectedMessage));
        }



        #endregion

        #region Private Methods



        protected override void TestClassFixtureSetup()
        {
            _owners = DataGenerator.GenerateOwnerList();

            _ownerAggregateRepository = Substitute.For<IOwnerAggregateRepository>();
            _ownerAggregateRepository.GetByIdAsync(Arg.Is<string>(x => _owners.Any(u => u.Id == x))).Returns(args => _owners.First(u => u.Id == (string)args[0]));
            _ownerAggregateRepository.CreateAsync(Arg.Any<Owner>()).Returns(async args => _owners.Add((Owner)args[0]));
            _ownerAggregateRepository.UpdateAsync(Arg.Is<Owner>(x => _owners.Any(u => u.Id == x.Id))).Returns(async args =>
            {
                var existUser = _owners.FirstOrDefault(u => u.Id == ((Owner)args[0]).Id);
                if (existUser != null)
                {
                    _owners.Remove(existUser);
                    _owners.Add(((Owner)args[0]));
                }
            });

            _ownerCommandHandlers = new OwnerCommandHandlers(_ownerAggregateRepository, _inMemoryBus);

        }



        #endregion
    }
}
