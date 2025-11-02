namespace DucktritionAPP.Views;

public partial class ForumPage : ContentPage
{
    public ForumPage()
    {
        InitializeComponent();
        SortPicker.SelectedIndex = 0;
        BuildForumPosts();
    }

    void BuildForumPosts()
    {
        // Clear any previous posts
        PostsArea.Children.Clear();

        var header = new Label
        {
            Text = "Forum Posts",
            FontAttributes = FontAttributes.Bold
        };
        PostsArea.Children.Add(header);

        // Example mock posts
        var posts = new List<(string Author, string Role, string Text)>
        {
            ("Duck Trition", "User", "Does anyone know some vegan friendly options in the Hoboken or NYC area?"),
            ("Ductrition Inc.", "Manufacturer", "We’ve just released a new nut-free granola option!"),
            ("Ductrition", "Owner", "This is the forum! You can browse, create, or reply to others in here and get some ideas!")
        };

        foreach (var post in posts)
        {
            var frame = new Frame
            {
                BorderColor = Colors.Gray,
                CornerRadius = 10,
                Padding = 10,
                Content = new VerticalStackLayout
                {
                    Spacing = 5,
                    Children =
                    {
                        new Label { Text = $"{post.Author} ({post.Role})", FontAttributes = FontAttributes.Bold },
                        new Label { Text = post.Text, FontSize = 12, LineBreakMode = LineBreakMode.TailTruncation },
                        new HorizontalStackLayout
                        {
                            HorizontalOptions = LayoutOptions.End,
                            Spacing = 10,
                            Children =
                            {
                                new Button { Text = "Like", FontSize = 10, WidthRequest = 60, HeightRequest = 30 },
                                new Button { Text = "Share", FontSize = 10, WidthRequest = 60, HeightRequest = 30 }
                            }
                        }
                    }
                }
            };

            PostsArea.Children.Add(frame);
        }
    }

    private async void OnCreatePostClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Create Post", "New post!", "OK");
    }
}
