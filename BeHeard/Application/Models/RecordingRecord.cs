using Microsoft.EntityFrameworkCore;
using System;

namespace BeHeard.Application.Models
{
    public class RecordingRecord
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string Chosen { get; set; }
        public string Result { get; set; }
    }
}
