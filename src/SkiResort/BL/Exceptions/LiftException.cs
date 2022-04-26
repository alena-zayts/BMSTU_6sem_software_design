﻿using BL.Models;

namespace BL.Exceptions
{
    public class LiftException : Exception
    {
        public Lift? Lift { get; }

        public LiftException() : base() { }
        public LiftException(string? message) : base(message) { }
        public LiftException(string? message, Exception? innerException) : base(message, innerException) { }

        public LiftException(string? message, Lift? lift): base(message)
        {
            this.Lift = lift;
        }
    }
    public class LiftDeleteException: LiftException
    {

    }
}

