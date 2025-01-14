﻿using MassTransit;
using MessagingComparisons.Domain;
using MessagingComparisons.Domain.Interfaces;

namespace MessagingComparisons.MassTransit;

public class MassTransitMessageSender(IBus bus, ISendEndpointProvider sendEndpointProvider)
    : IMessageSender, ICustomMessageSender
{
    public async Task SendMessageAsync(Message message) => await bus.Publish(message);
    
    public async Task SendMessageAsync(Message message, string queueUri)
    {
        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(queueUri));
        
        await sendEndpoint.Send(message);
    }
}
