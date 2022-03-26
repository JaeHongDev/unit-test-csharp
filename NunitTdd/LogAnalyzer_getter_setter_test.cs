using System;
using NUnit.Framework;

namespace NunitTdd
{
    [TestFixture]
    public class LogAnalyzer_getter_setter_test
    {
        public LogAnalyzer_getter_setter_test()
        {

        }

        public void IsValidFileName_NameShorterThan6CharsButSpportedExtension_ReturnFalse()
        {
            LogAnalyzer log = new LogAnalyzer();
        }

        public class LogAnalyzer
        {
            private IExtensionManager manager;
            public LogAnalyzer()
            {
            }

            public IExtensionManager Manager { get => manager; set => manager = value; }
            public bool IsValidLogFileName(string fileName)
            {
                return manager.IsValid(fileName);
            }
        }

        public interface IExtensionManager
        {
            bool IsValid(string fileName);
        }
    }

}
