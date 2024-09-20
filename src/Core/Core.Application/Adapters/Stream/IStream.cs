﻿namespace AutoMais.Core.Application.Adapters.Stream
{
    public interface IStream
    {
        Task SendEventAsync<T>(T notification, string queueName);
        Task SendEventAsync<T>(T notification, string queueName, DateTime? scheduleMessage);
    }
}
