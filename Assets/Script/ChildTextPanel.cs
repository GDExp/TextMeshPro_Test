using UnityEngine;

public class ChildTextPanel : TextPanelBase
{
    public override void CreatePanel()
    {
        var panel = new GameObject(PanelName);
        panel.AddComponent<CanvasRenderer>();
        panel.transform.SetParent(textTransform);
        panel.transform.localPosition = Vector2.zero;
        SetupBackgroundImage(panel);
        panelRect = panel.GetComponent<RectTransform>();
    }

    public override void ChangeTextPanel()
    {
        base.ChangeTextPanel();
        PanelOffestByText();
    }

    private void PanelOffestByText()
    {
        Vector3 panelOffset = Vector3.zero;

        var deltaResult = GetDeltaOffsetInText();

        panelOffset = new Vector3(deltaResult.Item1, deltaResult.Item2);

        panelRect.SetPositionAndRotation(textTransform.position + panelOffset, Quaternion.identity);
    }
}
