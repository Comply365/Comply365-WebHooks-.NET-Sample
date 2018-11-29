using System;
using System.ComponentModel.DataAnnotations;

namespace Comply365.WebHooks.Sample.Models.Core
{
    public class BaseWebHook<T>
    {
        [Required]
        public Guid Uid { get; set; }
        [Required]
        public Guid SubscriptionUid { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string Token { get; set; }
        public T Data { get; set; }
    }
}
