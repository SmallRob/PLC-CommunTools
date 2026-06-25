using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Commun.Client.WPF.Controls;

public class MetroWindow : Window
{
    static MetroWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindow),
            new FrameworkPropertyMetadata(typeof(MetroWindow)));
    }

    public MetroWindow()
    {
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
        Foreground = Brushes.White;
    }
}

public class MetroButton : Button
{
    static MetroButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroButton),
            new FrameworkPropertyMetadata(typeof(MetroButton)));
    }
}

public class MetroTextBox : TextBox
{
    static MetroTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTextBox),
            new FrameworkPropertyMetadata(typeof(MetroTextBox)));
    }
}

public class MetroLabel : Label
{
    static MetroLabel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroLabel),
            new FrameworkPropertyMetadata(typeof(MetroLabel)));
    }
}

public class MetroGroupBox : GroupBox
{
    static MetroGroupBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroGroupBox),
            new FrameworkPropertyMetadata(typeof(MetroGroupBox)));
    }
}

public class MetroTabControl : TabControl
{
    static MetroTabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTabControl),
            new FrameworkPropertyMetadata(typeof(MetroTabControl)));
    }
}

public class MetroTabItem : TabItem
{
    static MetroTabItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTabItem),
            new FrameworkPropertyMetadata(typeof(MetroTabItem)));
    }
}

public class MetroSwitch : CheckBox
{
    static MetroSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroSwitch),
            new FrameworkPropertyMetadata(typeof(MetroSwitch)));
    }
}

public class MetroProgressBar : ProgressBar
{
    static MetroProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroProgressBar),
            new FrameworkPropertyMetadata(typeof(MetroProgressBar)));
    }
}
