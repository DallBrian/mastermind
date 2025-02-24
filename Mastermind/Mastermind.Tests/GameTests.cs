﻿using Mastermind.Models;

namespace Mastermind.Tests
{
    public class GameTests : BaseTest
    {
        [Test]
        public void CanStartNewGame()
        {
            var app = Start();
            Assert.That(app.App.State.Phase == AppPhase.MainMenu);
            app.SimulateUserInput(ConsoleKey.D1);
            Assert.That(app.App.State.Phase == AppPhase.InGame);
        }
    }
}
