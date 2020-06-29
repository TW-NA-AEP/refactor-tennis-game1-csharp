using System;
using System.Collections.Generic;
using System.Linq;
using RefactoringTennisGame1;
using Xunit;

namespace RefactoringTennisGameTest
{
    public class TestFixture
    {
        public int player1Score;
        public int player2Score;
        public string statement;

        private TestFixture(int player1Score, int player2Score, string statement)
        {
            this.player1Score = player1Score;
            this.player2Score = player2Score;
            this.statement = statement;
        }

        public static TestFixture Create(int player1Score, int player2Score, string statement)
        {
            return new TestFixture(player1Score, player2Score, statement);
        }
    }

    public class TennisGameTest
    {
        private static string player1Name = "player1";
        private static string player2Name = "player2";

        public static IEnumerable<object[]> Scores =>
            new List<TestFixture>
            {
                TestFixture.Create(0, 0, "Love-All"),
                TestFixture.Create(1, 1, "Fifteen-All"),
                TestFixture.Create(2, 2, "Thirty-All"),
                TestFixture.Create(3, 3, "Deuce"),
                TestFixture.Create(4, 4, "Deuce"),
                TestFixture.Create(1, 0, "Fifteen-Love"),
                TestFixture.Create(0, 1, "Love-Fifteen"),
                TestFixture.Create(2, 0, "Thirty-Love"),
                TestFixture.Create(0, 2, "Love-Thirty"),
                TestFixture.Create(3, 0, "Forty-Love"),
                TestFixture.Create(0, 3, "Love-Forty"),
                TestFixture.Create(2, 1, "Thirty-Fifteen"),
                TestFixture.Create(1, 2, "Fifteen-Thirty"),
                TestFixture.Create(3, 1, "Forty-Fifteen"),
                TestFixture.Create(1, 3, "Fifteen-Forty"),
                TestFixture.Create(3, 2, "Forty-Thirty"),
                TestFixture.Create(2, 3, "Thirty-Forty"),
                TestFixture.Create(4, 0, "Win for " + player1Name),
                TestFixture.Create(0, 4, "Win for " + player2Name),
                TestFixture.Create(4, 1, "Win for " + player1Name),
                TestFixture.Create(1, 4, "Win for " + player2Name),
                TestFixture.Create(4, 2, "Win for " + player1Name),
                TestFixture.Create(2, 4, "Win for " + player2Name),
                TestFixture.Create(6, 4, "Win for " + player1Name),
                TestFixture.Create(4, 6, "Win for " + player2Name),
                TestFixture.Create(16, 14, "Win for " + player1Name),
                TestFixture.Create(14, 16, "Win for " + player2Name),
                TestFixture.Create(4, 3, "Advantage " + player1Name),
                TestFixture.Create(3, 4, "Advantage " + player2Name),
                TestFixture.Create(5, 4, "Advantage " + player1Name),
                TestFixture.Create(4, 5, "Advantage " + player2Name),
                TestFixture.Create(15, 14, "Advantage " + player1Name),
                TestFixture.Create(14, 15, "Advantage " + player2Name)
            }.Select(fixture => new[] {fixture});


        [Theory]
        [MemberData(nameof(Scores))]
        public void should_check_all_scores_correctly(TestFixture testFixture)
        {
            ITennisGame tennisGame = new TennisGameImpl(player1Name, player2Name);

            int highestScore = Math.Max(testFixture.player1Score, testFixture.player2Score);
            for (int i = 0; i < highestScore; i++)
            {
                if (i < testFixture.player1Score)
                    tennisGame.WonPoint(player1Name);
                if (i < testFixture.player2Score)
                    tennisGame.WonPoint(player2Name);
            }

            Assert.Equal(testFixture.statement, tennisGame.GetScore());
        }
    }
}