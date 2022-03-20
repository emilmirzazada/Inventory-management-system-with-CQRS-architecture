using Sintra.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Sintra.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset localTime => DateTimeOffset.UtcNow.AddHours(4);
        public DateTime NowUtc => localTime.DateTime;
    }
}
