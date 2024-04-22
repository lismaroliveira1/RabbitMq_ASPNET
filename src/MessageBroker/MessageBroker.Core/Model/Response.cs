﻿namespace MessageBroker.Core.Model
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
