﻿namespace Pulsar.Client.Api

open ExceptionHelper

type ConsumerBuilder private (client: PulsarClient, config: ConsumerConfiguration) =

    let consumerExceptionIfBlankString message arg =
        arg
        |> throwIfBlankString (fun() -> ConsumerException(message))

    let verify(config : ConsumerConfiguration) =
        let checkValue check config =
            check config |> ignore
            config

        config
        |> checkValue
            (fun c ->
                c.Topic
                |> consumerExceptionIfBlankString "Topic name must be set on the producer builder.")
        |> checkValue
            (fun c ->
                c.SubscriptionName
                |> consumerExceptionIfBlankString "Subscription name name must be set on the producer builder.")

    new(client: PulsarClient) = ConsumerBuilder(client, ConsumerConfiguration.Default)

    member __.Topic topic = 
        ConsumerBuilder(
            client,
            { config with
                Topic = topic |> invalidArgIfBlankString "Topic must not be blank." })
        
    member __.SubscriptionName subscriptionName =
        ConsumerBuilder(
            client,
            { config with
                SubscriptionName = subscriptionName |> invalidArgIfBlankString "Subscription name must not be blank." })

    member __.SubscribeAsync() =
        config
        |> verify
        |> client.SubscribeAsync