using System.Diagnostics.CodeAnalysis;

namespace Library.Book.Service
{
    [ExcludeFromCodeCoverage]
    public class MassTransitConfigConstants
    {
        public string HostAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}