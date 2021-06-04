using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.Api.Seedwork
{
    interface IMediatorValidation
    {
        ValidationResult Validate();
    }
}
