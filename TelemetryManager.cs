using UnityEngine;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;

public class TelemetryManager : MonoBehaviour
{
    private ClientWebSocket ws;
    private readonly string serverUri = "wss://console.shahidi.vr/telemetry";

    async void Start()
    {
        ws = new ClientWebSocket();
        await ws.ConnectAsync(new System.Uri(serverUri), CancellationToken.None);
        InvokeRepeating(nameof(SendFrameData), 0f, 1f/90f);
    }

    async void SendFrameData()
    {
        if (ws.State != WebSocketState.Open) return;
        var pos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.Head);
        string json = JsonUtility.ToJson(new { t=Time.time, x=pos.x, y=pos.y, z=pos.z });
        var buf = System.Text.Encoding.UTF8.GetBytes(json);
        await ws.SendAsync(new System.ArraySegment<byte>(buf), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    async void OnDestroy()
    {
        if (ws != null)
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
    }
}
