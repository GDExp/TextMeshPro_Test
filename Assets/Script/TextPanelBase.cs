using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPanelBase : MonoBehaviour, IPanel
{
    protected const string PanelName = "TextPanel";

    [Header("VisualSetup")]
    public Sprite bgSprite;
    [Range(0, 1f)]
    public float hideColor;
    
    protected RectTransform textRect;
    protected RectTransform panelRect;
    protected Transform textTransform;
    protected TextMeshProUGUI textMeshPro;

    private int _stringLenght;
    private bool _isWork;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textTransform = transform;
        StartCoroutine(PanelIsChange());
    }

    private void OnDestroy()
    {
        _isWork = false;
    }

    private IEnumerator PanelIsChange()
    {
        _isWork = true;
        CreatePanel();

        while (_isWork && panelRect != null)
        {
            ChangeTextPanel();
            yield return new WaitForFixedUpdate();
        }
    }

    public virtual void CreatePanel()
    {
        
    }

    public virtual void ChangeTextPanel()
    {
        PanelWrapByText();
    }

    private void PanelWrapByText()
    {
        float nHeight = 0f;
        float nWidth = 0f;

        var rect = new Rect(textMeshPro.rectTransform.rect);
        if (textMeshPro.text.Length != 0)
        {
            var resultDelta = GetDeltaWrapInText();

            nWidth = rect.width + resultDelta.Item1;
            nHeight = rect.height + resultDelta.Item2;
        }
        panelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, nHeight);
        panelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nWidth);
    }

    protected (float, float) GetDeltaWrapInText()
    {
        float delaHorizontal = Mathf.Abs(textMeshPro.margin.x) - textMeshPro.margin.z;
        float deltaVertical = Mathf.Abs(textMeshPro.margin.w) - textMeshPro.margin.y;

        return (delaHorizontal, deltaVertical);
    }

    protected (float,float) GetDeltaOffsetInText()
    {
        float deltaHorizontal = textMeshPro.margin.x / 2 - textMeshPro.margin.z / 2;
        float deltaVertical = textMeshPro.margin.w / 2 - textMeshPro.margin.y / 2;

        return (deltaHorizontal, deltaVertical);
    }

    protected void SetupBackgroundImage(GameObject panel)
    {
        var image = panel.AddComponent<Image>();
        image.sprite = bgSprite;
        image.type = Image.Type.Sliced;
        image.color = new Color(1f, 1f, 1f, hideColor);
    }
}