namespace DotNetCoreConsoleAppSample.Services
{
    public interface IHelloService
    {
        string Greeting();
    }

    public class HelloService : IHelloService
    {
        public string Greeting()
        {
            return "Hello World!";
        }
    }
}
