
using System;

namespace StudioModel.Domain
{
    public class UserToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public TimeSpan Validity { get; set; }

        public string Rol { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime ExpiredTime { get; set; }
    }
}
