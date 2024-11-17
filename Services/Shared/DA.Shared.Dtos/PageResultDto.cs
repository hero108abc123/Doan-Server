﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Shared.Dtos
{
    public class PageResultDto<T>
    {
        public T Items { get; set; }
        public int TotalItem { get; set; }
    }
}