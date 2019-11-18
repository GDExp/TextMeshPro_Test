using UnityEngine;

public class ParentTextPanel :TextPanelBase
{
    
    
    public override void CreatePanel()
    {
        textRect = textTransform.GetComponent<RectTransform>();

        var panel = new GameObject(PanelName);
        panel.AddComponent<CanvasRenderer>();
        panel.transform.SetParent(textTransform.parent);
        panel.transform.localPosition = textMeshPro.transform.localPosition;
        textTransform.SetParent(panel.transform);
        SetupBackgroundImage(panel);
        panelRect = panel.GetComponent<RectTransform>();
    }
    
    public override void ChangeTextPanel()
    {
        base.ChangeTextPanel();

        Vector3 panelOffset = Vector3.zero;

        var deltaResult = GetDeltaOffsetInText();
        
        panelOffset = new Vector3(deltaResult.Item1, deltaResult.Item2);

        textRect.SetPositionAndRotation(panelRect.position - panelOffset, Quaternion.identity);
        
    }
}
