using NUnit.Framework;
using PowerBall;
using PowerBall.Data;
using PowerBall.Domain;

namespace PowerBallTest
{
    [TestFixture]
    public class PowerBallTest 
    {        
        [TestCase ("buyer", "", false)]
        [TestCase ()]
        public void AddTest(string buyer, string powerBall, bool duplicate)
        {
            
        }

        //test for saving ticket
        //test for not saving duplicates
        //test for saving duplicates
    }
}
