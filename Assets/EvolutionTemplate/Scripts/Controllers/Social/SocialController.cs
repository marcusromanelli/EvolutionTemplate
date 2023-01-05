using UnityEngine;

/// <summary>
/// Originally, this would be used for Facebook sharing, but the current SDK doesn't not support FeedShare (photos + text) anymore, so it was removed.
/// So I used a default-OS sharing instead.
/// </summary>
public class SocialController : Singleton<SocialController> {
    const string SCREENSHOT_NAME = "screenshot.png";

    public static void ShareScreen()
    {
        Instance._shareScreen();
    }

    private void _shareScreen()
    {
        ScreenCapture.CaptureScreenshot(SCREENSHOT_NAME);

        Invoke("Save", 2);
    }

    protected void Save()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        NativeShare aux = new NativeShare();
        aux.AddFile(Path.Combine(Application.persistentDataPath, SCREENSHOT_NAME));

        aux.Share();
#else
        Debug.Log("Sharing is not supported on this platform.");
#endif

    }
}
