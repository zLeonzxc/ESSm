namespace ESSmPrototype.Custom;

public class ApprovalTypeTemplateSelector : DataTemplateSelector
{
    public DataTemplate? LeaveTemplate { get; set; }
    public DataTemplate? OvertimeTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (item is LeaveRequest leaveRequest)
        {
            return LeaveTemplate;
        }
        else if (item is OTRequest overtimeRequest)
        {
            return OvertimeTemplate;
        }
        return null;
    }
}
