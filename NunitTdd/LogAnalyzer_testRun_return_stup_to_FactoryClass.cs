using System;
using System.IO;

namespace NunitTdd
{
    public class LogAnalyzer_testRun_return_stup_to_FactoryClass
    {
        public LogAnalyzer_testRun_return_stup_to_FactoryClass()
        {
        }

        public class LogAnalyzer
        {
            private IExtensionManager manager;

            public IExtensionManager Manager { get => manager; set => manager = value; }

            public bool IsValidLogFileName(string fileName)
            {
                return manager.IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
            }

        }

        public interface IExtensionManager
        {
            bool IsValid(string fileName);
        }

        public class FileExtensionManager:IExtensionManager
        {
            public FileExtensionManager()
            {
            }

            public bool IsValid(string fileName)
            {
                return true;
            }
        }

        class ExtensionManagerFactory
        {
            private IExtensionManager customManager = null;
            public IExtensionManager Create()
            {
                if (customManager != null) return customManager;
                return new FileExtensionManager();
            }
        }

        public class LogAnalyzerUsingFactoryMethod
        {
            public bool IsValidLogFileName(string fileName)
            {
                return GetManager().IsValid(fileName);
            }
            public  virtual IExtensionManager GetManager()
            {
                return new FileExtensionManager();
            }
        }

        class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
        {
            public IExtensionManager Manager;

            //protected override IExtensionManager GetManager()
            //{
            //    return Manager;
            //}
            
        }
        internal class StubExtensionManager : IExtensionManager
        {
            public bool IsValid(string fileName)
            {
                return true;
            }
        }
    }


}
