﻿using System;
using System.Runtime.Serialization;

namespace Babble.Core.NodeImpl
{
    public class NodeError : BabbleError
    {
        public NodeError(string message = null, Exception innerException = null)
        {
            Message = message;
            InnerException = innerException;
        }
    }
}
