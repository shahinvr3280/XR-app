using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TherapistConsoleAPI : MonoBehaviour
{
    public string apiUrl = "https://console.shahidi.vr/api/commands";
    public ExposureEngine engine;

    void Start() => StartCoroutine(PollCommands());

    IEnumerator PollCommands()
    {
        while(true)
        {
            using(var web = UnityWebRequest.Get(apiUrl))
            {
                yield return web.SendWebRequest();
                if (web.result == UnityWebRequest.Result.Success)
                {
                    var resp = JsonUtility.FromJson<CommandResponse>(web.downloadHandler.text);
                    engine.SetIntensity(resp.intensity);
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    [System.Serializable]
    class CommandResponse { public float intensity; }
}
