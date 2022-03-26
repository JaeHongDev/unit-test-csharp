using NUnit.Framework;
using System;
namespace NunitTdd
{
    [TestFixture()]
    public class Test
    {
        public bool IsValidLogFileName(string fileName)
        {
            IExtensionManager mgr = new FileExtensionManager();
            return mgr.IsValid(fileName); 
        }
    }


    internal class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            return true; 
        }
    }

    internal interface IExtensionManager
    {
        bool IsValid(string fileName);
    }

    internal class StubExtensionManage : IExtensionManager
    {
        public bool ShouldExtensionBeValid { get; internal set; }

        public bool IsValid(string fileName)
        {
            return true;
        }
    }

    internal class LogAnalzyer
    {
        private IExtensionManager manager;
        public LogAnalzyer()
        {
            manager = new FileExtensionManager();
        }
        public LogAnalzyer(IExtensionManager mgr)
        {
            manager = mgr;
        }
        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsValid(fileName);
        }
    }
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_NameShorterThan6CharsButSupportedExtension_ReturnsFalse()
        {
            StubExtensionManage myFakeManager = new StubExtensionManage();
            myFakeManager.ShouldExtensionBeValid = true;

            LogAnalyzer log = new LogAnalyzer(myFakeManager);
            bool result = log.IsValidLogFileName("short.ext");

            Assert.IsFalse(result, "설사 지원되는 확장자라 하더라도 파일명이 다섯 글자 미만이면 안 됨");
        }
    }
}
