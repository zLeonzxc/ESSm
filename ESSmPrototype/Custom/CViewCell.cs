namespace ESSmPrototype.Custom;

public class CViewCell : Microsoft.Maui.Controls.ViewCell
{

    public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(
        nameof(SelectedBackgroundColor), typeof(Color), typeof(CViewCell), Colors.White
    );

    public Color SelectedBackgroundColor
    {
        get { return (Color)GetValue(SelectedBackgroundColorProperty); }
        set { SetValue(SelectedBackgroundColorProperty, value); }
    }

    public CViewCell()
    {

    }
}
