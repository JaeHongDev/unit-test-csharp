using System;
using NUnit.Framework;

namespace NunitTdd
{
    [TestFixture]
    public class WebServiceTest
    {
        public WebServiceTest()
        {
        }

        [Test]
        public void Analyze_TooShortFIleName_CallsWebService()
        {
            MockService mockService = new MockService();
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            Assert.AreEqual("FileName too short:abc.ext", mockService.LastError); // 목 객체에 대한 assert
        }
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            StubService stubService = new StubService();
            stubService.ToThrow = new Exception("fake exceptino");

            MockEmailService mockEmail = new MockEmailService();
            LogAnalyzer log = new LogAnalyzer()
            {
                Email = mockEmail,
                Service = stubService
            };

            string tookShortFileName = "abc.ext";
            log.Analyze(tookShortFileName);

            Assert.AreEqual("a", mockEmail.To);
            Assert.AreEqual("fake exception", mockEmail.Body);
            Assert.AreEqual("subject", mockEmail.Subject);

           



        }
        public class LogAnalyzer
        {
            private IWebService service;
            private IEmailService email;

            public LogAnalyzer()
            {
            }

            public LogAnalyzer(IWebService service)
            {
                this.Service = service;
            }

            public IWebService Service { get => service; set => service = value; }
            public IEmailService Email { get => email; set => email = value; }

            public void Analyze(string fileName)
            {
                if (fileName.Length < 8)
                {
                    try
                    {
                        Service.LogError("FileName too short:" + fileName); // 제품 코드에 에러 기록을 남긴다. 
                    }
                    catch (Exception e)
                    {
                        email.SendMail("a", "subject", e.Message);
                    }
                }
            }

        }

        public interface IWebService // 웹 사이트와 소통하는 인터페이스
        {
            void LogError(string message);
        }

        public class MockService : IWebService // 스텁과 구분 지을 수 있도록 하자.
        {
            public string LastError;

            public void LogError(string message)
            {
                LastError = message;
            }
        }

        public interface IEmailService
        {
            void SendMail(string to, string subject, string body);
        }
        public class StubService : IWebService
        {
            public Exception ToThrow;
            public void LogError(string message)
            {
                if (ToThrow != null) throw ToThrow;
            }
        }

        public class MockEmailService : IEmailService
        {
            public string To;
            public string Subject;
            public string Body;

            public MockEmailService()
            {
            }

            public MockEmailService(string to, string subject, string body)
            {
                To = to;
                Subject = subject;
                Body = body;
            }

            public void SendMail(string to, string subject, string body)
            {

            }
        }
    }

}