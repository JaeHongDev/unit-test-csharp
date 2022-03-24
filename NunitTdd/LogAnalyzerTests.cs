using System;
using NUnit.Framework;

namespace NunitTdd
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("setup");
            m_analyzer = new LogAnalyzer();
        }
        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("tearDown");
            m_analyzer = null;
        }
        [Test]
        public void IsValidFileName_validFile_ReturnTrue()
        {

            bool result = m_analyzer.IsValidLogFileName("whatever.slf");

            Assert.IsTrue(true, "파일 이름이 적합하지 않음");
        }
        [Test]
        public void IsValidFileName_validFileUpperCased_ReturnTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.SLF");
            Assert.IsTrue(result, "파일 이름이 적합하지 않음");
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => m_analyzer.IsValidLogFileName(string.Empty));
        }
    }

    internal class LogAnalyzer
    {
        private bool wasLastFileNameValid;

        public bool WasLastFileNameValid {
            get => wasLastFileNameValid;
            set => wasLastFileNameValid = value;
        }

        internal bool IsValidLogFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("파일 이름이 없음!");
            }
            if (!fileName.EndsWith(".SLF"))
            {
                return false;
            }

            wasLastFileNameValid = true;
            return true;
        }
    }
}
