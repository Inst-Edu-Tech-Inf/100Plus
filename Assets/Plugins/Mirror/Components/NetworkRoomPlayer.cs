using System;
using System.ComponentModel;
using UnityEngine;

namespace Mirror
{
    /// <summary>
    /// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
    /// <para>The RoomPrefab object of the NetworkRoomManager must have this component on it. This component holds basic room player data required for the room to function. Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.</para>
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkRoomPlayer")]
    [HelpURL("https://mirror-networking.com/docs/Components/NetworkRoomPlayer.html")]
    public class NetworkRoomPlayer : NetworkBehaviour
    {
        static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomPlayer));

        /// <summary>
        /// This flag controls whether the default UI is shown for the room player.
        /// <para>As this UI is rendered using the old GUI system, it is only recommended for testing purposes.</para>
        /// </summary>
        [Tooltip("This flag controls whether the default UI is shown for the room player")]
        public bool showRoomGUI = true;

        [Header("Diagnostics")]

        /// <summary>
        /// Diagnostic flag indicating whether this player is ready for the game to begin.
        /// <para>Invoke CmdChangeReadyState method on the client to set this flag.</para>
        /// <para>When all players are ready to begin, the game will start. This should not be set directly, CmdChangeReadyState should be called on the client to set it on the server.</para>
        /// </summary>
        [Tooltip("Diagnostic flag indicating whether this player is ready for the game to begin")]
        [SyncVar(hook = nameof(ReadyStateChanged))]
        public bool readyToBegin;

        /// <summary>
        /// Diagnostic index of the player, e.g. Player1, Player2, etc.
        /// </summary>
        [Tooltip("Diagnostic index of the player, e.g. Player1, Player2, etc.")]
        [SyncVar(hook = nameof(IndexChanged))]
        public int index;

        #region Unity Callbacks

        /// <summary>
        /// Do not use Start - Override OnStartrHost / OnStartClient instead!
        /// </summary>
        public void Start()
        {
            if (NetworkManager.singleton is NetworkRoomManager room)
            {
                // NetworkRoomPlayer object must be set to DontDestroyOnLoad along with NetworkRoomManager
                // in server and all clients, otherwise it will be respawned in the game scene which would
                // have undesireable effects.
                if (room.dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);

                room.roomSlots.Add(this);
                room.RecalculateRoomPlayerIndices();

                if (NetworkClient.active)
                    OnClientEnterRoom();
            }
            else
                logger.LogError("RoomPlayer could not find a NetworkRoomManager. The RoomPlayer requires a NetworkRoomManager object to function. Make sure that there is one in the scene.");
        }

        #endregion

        #region Commands

        [Command]
        public void CmdChangeReadyState(bool readyState)
        {
            readyToBegin = readyState;
            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room != null)
            {
                room.ReadyStatusChanged();
            }
        }

        #endregion

        #region SyncVar Hooks

        /// <summary>
        /// This is a hook that is invoked on clients when the index changes.
        /// </summary>
        /// <param name="oldIndex">The old index value</param>
        /// <param name="newIndex">The new index value</param>
        public virtual void IndexChanged(int oldIndex, int newIndex) { }

        /// <summary>
        /// This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
        /// <para>This function is called when the a client player calls CmdChangeReadyState.</para>
        /// </summary>
        /// <param name="newReadyState">New Ready State</param>
        public virtual void ReadyStateChanged(bool _, bool newReadyState)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            OnClientReady(newReadyState);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        #endregion

        #region Room Client Virtuals

        /// <summary>
        /// This is a hook that is invoked on clients for all room player objects when entering the room.
        /// <para>Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.</para>
        /// </summary>
        public virtual void OnClientEnterRoom() { }

        /// <summary>
        /// This is a hook that is invoked on clients for all room player objects when exiting the room.
        /// </summary>
        public virtual void OnClientExitRoom() { }

        // Deprecated 05/18/2020
        /// <summary>
        /// Obsolete: Override <see cref="ReadyStateChanged(bool, bool)">ReadyStateChanged(bool, bool)</see> instead.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Override ReadyStateChanged(bool, bool) instead")]
        public virtual void OnClientReady(bool readyState) { }

        #endregion

        #region Optional UI

        /// <summary>
        /// Render a UI for the room.   Override to provide your on UI
        /// </summary>
        public virtual void OnGUI()
        {
            if (!showRoomGUI)
                return;

            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room)
            {
                if (!room.showRoomGUI)
                    return;

                if (!NetworkManager.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        void DrawPlayerReadyState()
        {
            GUIStyle customButton = new GUIStyle("button");
            customButton.fontSize = 40;

            GUILayout.BeginArea(new Rect(120f + (index * 100), 200f, 90f, 130f));

            GUILayout.Label($"Player [{index + 1}]");

            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("X", customButton))//"REMOVE"
            {
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }

            GUILayout.EndArea();
        }

        void DrawPlayerReadyButton()
        {
            GUIStyle customButton = new GUIStyle("button");
            customButton.fontSize = 40;
            customButton.normal.textColor = Color.red;
 

            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(120f, 300f, 200f, 400f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("X",customButton))
                        CmdChangeReadyState(false);
                }
                else
                {
                    Texture tex= Resources.Load<Texture>("ChoiceOKsmall");
                    if (GUILayout.Button(tex))
                    //if (GUILayout.Button("Ready",customButton))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }

        #endregion
    }
}
