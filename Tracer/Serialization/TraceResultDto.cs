﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Serialization
{
    [DataContract]
    public class TraceResultDto
    {
        [DataMember]
        public ConcurrentDictionary<int, ThreadTraceResultDto> Threads;

        public TraceResultDto(ConcurrentDictionary<int, ThreadTraceResultDto> threads)
        {
            Threads = threads;
        }
    }
}
