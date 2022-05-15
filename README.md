# EifelMono.TwisetYarn

Stuff for Task and more....

## CancellationTokenNode

Easy source and token link with timeout 

 ```csharp
 // -------------------------------------------------------------------------
// +--+- Parents
// |     +- IsCancellationRequested
// |     +- Tokens
// |        +- IsCancellationRequested
// |     +- Sources
// |        +- IsCancellationRequested
// | 
// |  +--+- Childs
// |  |     +- IsCancellationRequested
// |  |     +- Tokens
// |  |        +- IsCancellationRequested
// |  |     +- Sources
// |  |        +- IsCancellationRequested
// |  |  
// |  |  +--+- Nodes
// |  |  |     +- IsCancellationRequested
// |  |  |     +- CancellationTokenSourceSource
// |  |  |     +- CancellationTokenSourceTimeOut
// |  |  |
// +--+--+--+- Node (Source => linked from Parents, Childs, Nodes)
//             |
//             +- Cancel (CancellationTokenSourceNodeSource)
//             +- IsCancellationRequested (Node (Source => linked from Parents, Childs, Nodes)) 
//             +- IsCancellationRequestedByNode (CancellationTokenSourceNodeSource)
//             +- IsCancellationRequestedByTimeout (CancellationTokenSourceNodeTimeout)
//             +- HasTimeOut
//             +- IsTimeOut => IsCancellationRequestedByTimeout 
// -------------------------------------------------------------------------
 ```
