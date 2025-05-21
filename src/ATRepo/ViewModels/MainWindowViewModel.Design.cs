using FishyFlip.Lexicon;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATRepo.ViewModels;

public class MainWindowViewModel_Design : MainWindowViewModel
{
    public MainWindowViewModel_Design()
    {
        this.ATObjects.Add(Post.FromJson("{\"$type\":\"app.bsky.feed.post\",\"text\":\"I can\\u2019t wait for next weeks Guys\\u002B episode so I can watch them in reverse order.\",\"langs\":[\"en\"],\"createdAt\":\"2025-05-16T16:24:58.344Z\"}"));
        
        this.SelectedATObject = this.ATObjects[0];
    }
}