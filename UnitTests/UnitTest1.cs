using Boggle.Controllers;
using System.Net.Sockets;

namespace UnitTests;


[TestClass]
public class DieTests
{
    [TestMethod]
    public void TestDie()
    {
        Die Test = new Die("ABCDEF");
        for (int i = 0; i <= 5; i++)
            Assert.IsNotNull(Test.Letters[i]);
    }

    [TestMethod]
    public void TestDie2()
    {
        Die Test = new Die("123456");
        for (int i = 0; i <= 5; i++)
            Assert.IsNotNull(Test.Letters[i]);
    }

    [TestMethod]
    public void TestLetterIndex()
    {
        Die Test = new Die("123456");
        Test.SetRandomLetterIndex();
        for (int i = 0; i <= 5; i++)
            Assert.IsNotNull(Test.LetterIndex);
    }


    [TestMethod]
    public void TestLetterIndex2()
    {
        Die Test = new Die("ABCDEFG");
        Test.SetRandomLetterIndex();
        for (int i = 0; i <= 5; i++)
            Assert.IsNotNull(Test.LetterIndex);
    }

    [TestMethod]
    public void getRolledLetterTest()
    {
        Die Test = new Die("AAAAAA");
        Test.SetRandomLetterIndex();
        char x = Test.GetRolledLetter();

        Assert.IsTrue(x == 'A');
    }

    [TestMethod]
    public void getRolledLetterTest2()
    {
        Die Test = new Die("ABCDEF");
        Test.SetRandomLetterIndex();
        char x = Test.GetRolledLetter();

        Assert.IsTrue(x == 'A' || x == 'B' || x == 'C' || x == 'D' || x == 'E' || x == 'F');
    }





}

    [TestClass]
    public class HostTests
    {
        [TestMethod]
        public void TestHostAddress()
        {
            Host host = new Host();
            host.makeHost();
            Console.WriteLine(host.Address);
            Assert.IsNotNull(host.Address);


        }



    


}





