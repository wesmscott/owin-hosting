﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Owin.Types
{
    using SendFileAsyncDelegate = Func<string, long, long?, CancellationToken, Task>;

    public partial struct OwinResponse
    {
        public bool CanSendFile
        {
            get { return SendFileAsyncDelegate != null; }
        }

        public SendFileAsyncDelegate SendFileAsyncDelegate
        {
            get { return Get<SendFileAsyncDelegate>(OwinConstants.SendFiles.SendAsync); }
            set { Set(OwinConstants.SendFiles.SendAsync, value); }
        }

        public Task SendFileAsync(string filePath, long offset, long? count, CancellationToken cancel)
        {
            var sendFile = SendFileAsyncDelegate;
            if (sendFile == null)
            {
                throw new NotSupportedException(OwinConstants.SendFiles.SendAsync);
            }
            return sendFile.Invoke(filePath, offset, count, cancel);
        }

        public Task SendFileAsync(string filePath, long offset, long? count)
        {
            return SendFileAsync(filePath, offset, count, CancellationToken.None);
        }

        public Task SendFileAsync(string filePath, CancellationToken cancel)
        {
            return SendFileAsync(filePath, 0, null, cancel);
        }

        public Task SendFileAsync(string filePath)
        {
            return SendFileAsync(filePath, 0, null, CancellationToken.None);
        }
    }
}