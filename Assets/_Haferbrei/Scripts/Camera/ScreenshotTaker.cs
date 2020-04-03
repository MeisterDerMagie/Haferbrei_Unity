using UnityEngine;

namespace Haferbrei{
public class ScreenshotTaker
{
    public static Texture2D TakeScreenshot(Camera _camera, int _width, int _height)
    {
        Rect rect = new Rect(0, 0, _width, _height);
        RenderTexture renderTexture = new RenderTexture(_width, _height, 24);
        Texture2D screenShot = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
 
        _camera.targetTexture = renderTexture;
        _camera.Render();
 
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);
 
        _camera.targetTexture = null;
        RenderTexture.active = null;
 
        Object.Destroy(renderTexture);
        renderTexture = null;
        return screenShot;
    }
}
}