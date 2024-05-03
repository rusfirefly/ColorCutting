using UnityEngine;
using UnityEngine.UI;

public class BackgroundGradient : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    private Image _image;

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();
        _image.color = SetGradient();
    }

    private Color SetGradient()
    {
        var gradient = new Gradient();

        // Blend color from red at 0% to blue at 100%
        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(_startColor, 0.0f);
        colors[1] = new GradientColorKey(_endColor, 1.0f);

        // Blend alpha from opaque at 0% to transparent at 100%
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);

        gradient.SetKeys(colors, alphas);

        // What's the color at the relative time 0.25 (25%) ?
        return gradient.Evaluate(0.25f);
    }
}
