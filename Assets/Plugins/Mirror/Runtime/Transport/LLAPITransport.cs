// Coburn: LLAPI is not available on UWP. There are a lot of compile directives here that we're checking against.
// Checking all of them may be overkill, but it's better to cover all the possible UWP directives. Sourced from
// https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
// TODO: Check if LLAPI is supported on Xbox One?

// LLAPITransport wraps UNET's LLAPI for use as a HLAPI TransportLayer, only if you're not on a UWP platform.
#if !(UNITY_WSA || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_10_0 || NETFX_CORE)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using Mirror;

namespace Mirror
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("LLAPI is obsolete and will be removed from future versions of Unity")]
    public class LLAPITransport : Transport
    {
        public const string Scheme = "unet";

        public ushort port = 7777;

        [Tooltip("Enable for WebGL games. Can only do either WebSockets or regular Sockets, not both (yet).")]
        public bool useWebsockets;

        // always use first channel
        readonly int channelId;
        byte error;

        int clientId = -1;
        int clientConnectionId = -1;
        readonly byte[] clientReceiveBuffer = new byte[4096];
        byte[] clientSendBuffer;

        int serverHostId = -1;
        readonly byte[] serverReceiveBuffer = new byte[4096];
        byte[] serverSendBuffer;

        void Awake()
        {
            Debug.Log("LLAPITransport initialized!");

            // initialize send buffers
            clientSendBuffer = new byte[4096];
            serverSendBuffer = new byte[4096];
        }

        public override bool Available()
        {
            // LLAPI runs on all platforms, including webgl
            return true;
        }

        #region client
        public override bool ClientConnected()
        {
            return clientConnectionId != -1;
        }

        void ClientConnect(string address, int port)
        {
            // LLAPI can't handle 'localhost'
            if (address.ToLower() == "localhost") address = "127.0.0.1";

            // Replace with Mirror's transport connection logic
            Transport.activeTransport.ClientConnect(address);

            // Handle connection result
            clientConnectionId = 1; // Mirror does not provide a direct way to get connection ID
        }

        public override void ClientConnect(string address)
        {
            ClientConnect(address, port);
            //Transport.activeTransport.OnClientDataReceived = OnClientDataReceivedHandler();
        }

        public override void ClientConnect(Uri uri)
        {
            if (uri.Scheme != Scheme)
                throw new ArgumentException($"Invalid url {uri}, use {Scheme}://host:port instead", nameof(uri));

            int serverPort = uri.IsDefaultPort ? port : uri.Port;

            ClientConnect(uri.Host, serverPort);
        }

        public override bool ClientSend(int channelId, ArraySegment<byte> segment)
        {
            // Send buffer is copied internally, so we can get rid of segment
            // immediately after returning and it still works.
            // -> BUT segment has an offset, Send doesn't. we need to manually
            //    copy it into a 0-offset array
            if (segment.Count <= clientSendBuffer.Length)
            {
                Array.Copy(segment.Array, segment.Offset, clientSendBuffer, 0, segment.Count);
                Transport.activeTransport.ClientSend(channelId, new ArraySegment<byte>(clientSendBuffer, 0, segment.Count));
                return true;
            }
            Debug.LogError("LLAPI.ClientSend: buffer( " + clientSendBuffer.Length + ") too small for: " + segment.Count);
            return false;
        }

        public bool ProcessClientMessage()
        {
            if (clientConnectionId == -1)
                return false;

            // Replace with Mirror's transport receive logic
            Transport.activeTransport.OnClientDataReceived = new ClientDataReceivedEvent();

            return true;
        }

        private void OnClientDataReceivedHandler(ArraySegment<byte> data, int channel)
        {
            OnClientDataReceived.Invoke(data, channel);
        }


        public override void ClientDisconnect()
        {
            if (clientConnectionId != -1)
            {
                Transport.activeTransport.ClientDisconnect();
                clientConnectionId = -1;
            }
        }
        #endregion

        #region server

        public override bool ServerSend(List<int> connectionIds, int channelId, ArraySegment<byte> segment)
        {
            // Send buffer is copied internally, so we can get rid of segment
            // immediately after returning and it still works.
            // -> BUT segment has an offset, Send doesn't. we need to manually
            //    copy it into a 0-offset array
            if (segment.Count <= serverSendBuffer.Length)
            {
                // copy to 0-offset
                Array.Copy(segment.Array, segment.Offset, serverSendBuffer, 0, segment.Count);

                // send to all
                bool result = true;
                List<int> mojePolaczenia = new List<int>(connectionIds);
                int pozycja = 0;
                foreach (int connectionId in connectionIds)
                {
                    mojePolaczenia[pozycja] = connectionId;
                    pozycja++;
                    result &= Transport.activeTransport.ServerSend(mojePolaczenia, channelId, new ArraySegment<byte>(serverSendBuffer, 0, segment.Count));
                }
                return result;
            }
            Debug.LogError("LLAPI.ServerSend: buffer( " + serverSendBuffer.Length + ") too small for: " + segment.Count);
            return false;
        }


        // right now this just returns the first available uri,
        // should we return the list of all available uri?
        public override Uri ServerUri()
        {
            UriBuilder builder = new UriBuilder();
            builder.Scheme = Scheme;
            builder.Host = Dns.GetHostName();
            builder.Port = port;
            return builder.Uri;
        }

        public override bool ServerActive()
        {
            return serverHostId != -1;
        }

        public override void ServerStart()
        {
            // Replace with Mirror's transport server start logic
            Transport.activeTransport.ServerStart();

            serverHostId = 1; // Mirror does not provide a direct way to get server host ID
        }

        public bool ProcessServerMessage()
        {
            if (clientConnectionId == -1)
                return false;

            // Replace with Mirror's transport receive logic
            Transport.activeTransport.OnServerDataReceived = new ServerDataReceivedEvent();

            return true;
        }

      

      /*  public bool ProcessServerMessage()
        {
            if (serverHostId == -1)
                return false;

            // Replace with Mirror's transport receive logic
            Transport.activeTransport.OnServerDataReceived += (int connectionId, ArraySegment<byte> data, int channel) =>
            {
                OnServerDataReceived.Invoke(connectionId, data, channel);
            };

            return true;
        }*/

        public override bool ServerDisconnect(int connectionId)
        {
            return Transport.activeTransport.ServerDisconnect(connectionId);
        }

        public override string ServerGetClientAddress(int connectionId)
        {
            // Replace with Mirror's transport get address logic
            return Transport.activeTransport.ServerGetClientAddress(connectionId);
        }

        public override void ServerStop()
        {
            Transport.activeTransport.ServerStop();
            serverHostId = -1;
            Debug.Log("LLAPITransport.ServerStop");
        }
        #endregion

        #region common
        // IMPORTANT: set script execution order to >1000 to call Transport's
        //            LateUpdate after all others. Fixes race condition where
        //            e.g. in uSurvival Transport would apply Cmds before
        //            ShoulderRotation.LateUpdate, resulting in projectile
        //            spawns at the point before shoulder rotation.
        public void LateUpdate()
        {
            // process all messages
            while (ProcessClientMessage()) { }
            while (ProcessServerMessage()) { }
        }

        public override void Shutdown()
        {
            Transport.activeTransport.Shutdown();
            serverHostId = -1;
            clientConnectionId = -1;
            Debug.Log("LLAPITransport.Shutdown");
        }

        public override int GetMaxPacketSize(int channelId)
        {
            return 4096;
        }

        public override string ToString()
        {
            if (ServerActive())
            {
                return "LLAPI Server port: " + port;
            }
            else if (ClientConnected())
            {
                return "LLAPI Client connected";
            }
            return "LLAPI (inactive/disconnected)";
        }
        #endregion
    }
}
#endif

