using System;

namespace StoreFlow.Models
{
    public enum RecordStatus
    {
        Pending,
        Ordered,
        Resolved,
        Canceled
    }

    public class MissingRecord
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public RecordStatus Status { get; set; } = RecordStatus.Pending;
    }
}
