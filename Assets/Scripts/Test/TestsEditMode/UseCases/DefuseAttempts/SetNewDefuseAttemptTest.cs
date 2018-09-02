﻿using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.DefuseAttempts
{
    [TestFixture]
    public class SetNewDefuseAttemptTest
    {
        [Test]
        public void Set_ShouldSetNewDefuseAttemptInDefusingState()
        {
            // Given
            var random = Substitute.For<IRandom>();
            random.Range(Arg.Any<int>(), Arg.Any<int>()).Returns(0);
            
            var allPlayers = Substitute.For<AllPlayers>();
            allPlayers.GetAll().Returns(new List<Player>().AsReadOnly());
            
            var defusingState = new DefusingState();
            
            var setNewDefuseAttempt = new SetNewDefuseAttempt(random, allPlayers, defusingState);
            
            // When
            setNewDefuseAttempt.Set();

            // Then
            Assert.IsInstanceOf<DefuseAttempt>(defusingState.CurrentDefuseAttempt);
        }
    }
}