﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Entities;

namespace DPA_Musicsheets.IO
{
    public interface INoteEvent
    {
        INote Note { get; }
    }
}
