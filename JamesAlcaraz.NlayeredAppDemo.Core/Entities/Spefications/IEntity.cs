﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamesAlcaraz.NlayeredAppDemo.Core.Entities.Spefications
{
    /// <summary>
    /// Entity with int as primary key type
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}
