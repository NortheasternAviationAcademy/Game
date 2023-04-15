﻿using System;

namespace Proyecto26
{
    public class RequestException : Exception
    {
        public RequestException()
        {
        }

        public RequestException(string message) : base(message)
        {
        }

        public RequestException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public RequestException(string message, bool isHttpError, bool isNetworkError, long statusCode,
            string response) : base(message)
        {
            IsHttpError = isHttpError;
            IsNetworkError = isNetworkError;
            StatusCode = statusCode;
            Response = response;
        }

        public bool IsHttpError { get; private set; }

        public bool IsNetworkError { get; private set; }

        public long StatusCode { get; private set; }

        public string ServerMessage { get; set; }

        public string Response { get; set; }
    }
}