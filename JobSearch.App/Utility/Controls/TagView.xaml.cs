using System.Runtime.CompilerServices;

namespace JobSearch.App.Utility.Controls;

public partial class TagView : ContentView
{
    public static readonly BindableProperty TechnologiesProperty = BindableProperty.Create(nameof(Technologies), typeof(string), typeof(TagView));

    public string Technologies
    {
        get => (string)GetValue(TagView.TechnologiesProperty);
        set => SetValue(TagView.TechnologiesProperty, value);
    }

    public static readonly BindableProperty NumberOfWordsProperty = BindableProperty.Create(nameof(NumberOfWords), typeof(int), typeof(TagView));

    public int NumberOfWords
    {
        get => (int)GetValue(TagView.NumberOfWordsProperty);
        set => SetValue(TagView.NumberOfWordsProperty, value);
    }

    public TagView()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == nameof(Technologies))
        {
            Container.Children.Clear();

            if (Technologies != null)
            {
                string[] words = Technologies.Split(',');

                int limit = (words.Count() >= NumberOfWords) ? NumberOfWords : words.Count();

                for (int i = 0; i < limit; i++)
                {
                    var frame = new Frame()
                    {
                        BackgroundColor = Color.FromArgb("#F7F8FA"),
                        Padding = new Thickness(3),
                        Margin = new Thickness(0,0,3,3),
                        HasShadow = false,
                    };
                    var label = new Label()
                    {
                        Text = words[i],
                        FontFamily = "MontserratLight",
                        FontSize = 13,
                        TextColor = Color.FromArgb("8D9EAA"),
                    };

                    frame.Content = label;

                    Container.Children.Add(frame);
                }
            }
        }

        base.OnPropertyChanged(propertyName);
    }
}