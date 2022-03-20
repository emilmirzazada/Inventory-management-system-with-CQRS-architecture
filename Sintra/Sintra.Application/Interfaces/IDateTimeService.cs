using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTimeOffset localTime { get; }
    }
}
