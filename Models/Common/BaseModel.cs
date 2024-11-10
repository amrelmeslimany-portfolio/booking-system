using System;

namespace api.Models.Common
{
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
