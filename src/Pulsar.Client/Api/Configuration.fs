﻿namespace Pulsar.Client.Api

type PulsarClientConfiguration =
    {
        ServiceUrl: string
    }
    static member Default =
        {
            ServiceUrl = ""
        }

type ConsumerConfiguration =
    {
        Topic: string
        SubscriptionName: string
    }
    static member Default =
        {
            Topic = ""
            SubscriptionName = ""
        }

type ProducerConfiguration =
    {
        Topic: string
        ProducerName: string
    }
    static member Default =
        {
            Topic = ""
            ProducerName = ""
        }
