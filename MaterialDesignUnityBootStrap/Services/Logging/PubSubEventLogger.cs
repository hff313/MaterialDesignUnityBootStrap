﻿using System;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Services.Logging;
using Microsoft.Extensions.Logging;
using Prism.Events;

public class PubSubEventLogger : ILogger
{
    private readonly IEventAggregator _logEventAgreggator;
    private readonly string _name;
    private readonly PubSubEventLoggerOption _option;

    public PubSubEventLogger(
        string name,
        PubSubEventLoggerOption option, IEventAggregator eventAggregator)
    {
        (_name, _option, _logEventAgreggator) = (name, option, eventAggregator);
    }

    public IDisposable BeginScope<TState>(TState state) => default;

    public bool IsEnabled(LogLevel logLevel) =>
        true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        _logEventAgreggator.GetEvent<LogPubSubEvent>().Publish(new LogEventMessage(logLevel,eventId, formatter(state,exception)));
    }
}