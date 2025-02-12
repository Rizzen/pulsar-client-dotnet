﻿namespace Pulsar.Client.Internal

open Pulsar.Client.Api
open FSharp.Control.Tasks.V2.ContextInsensitive
open Pipelines.Sockets.Unofficial
open System.Buffers
open System.Text
open Pulsar.Client.Common
open System
open Utf8Json
open System.Threading.Tasks


type BinaryLookupService (config: PulsarClientConfiguration) =
    let serviceNameResolver = ServiceNameResolver(config)

    member __.GetPartitionedTopicMetadata topicName = 
        task {
            let endpoint = serviceNameResolver.ResolveHost()
            use! conn = SocketManager.getConnection { PhysicalAddress = endpoint; LogicalAddress = endpoint }
            let requestId = Generators.getNextRequestId()
            let request = 
                Commands.newPartitionMetadataRequest topicName requestId
            let! result = SocketManager.sendAndWaitForReply requestId (conn, request)
            return result :?> PartitionedTopicMetadata
        }

    member __.GetServiceUrl() = serviceNameResolver.GetServiceUrl()
 
    member __.UpdateServiceUrl(serviceUrl) = serviceNameResolver.UpdateServiceUrl(serviceUrl)

    member __.GetBroker(topicName: TopicName): Task<Broker> = 
        raise (System.NotImplementedException())