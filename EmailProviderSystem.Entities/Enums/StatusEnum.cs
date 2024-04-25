using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Entities.Enums
{
    public enum StatusEnum
    {
        Ok = 200,
        AlreadyExist = 403,
        Created = 201,
        InternalError = 500
    }
}
