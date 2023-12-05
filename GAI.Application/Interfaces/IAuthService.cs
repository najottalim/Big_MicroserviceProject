﻿using GAI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAI.Application.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> Login(RequestLogin request);
    }
}
