using System;
namespace Bondo.Application.DTOs.Email
{
    public class EmailRequestDto
    {
        public List<Recepient> to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        // public Recepients[] from { get; set; }

        public class Recepient{
            public string email { get; set; }
            public string name { get; set; }

        }
    }
}

