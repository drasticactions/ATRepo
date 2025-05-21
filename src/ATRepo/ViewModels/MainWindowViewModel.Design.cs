using FishyFlip.Lexicon;

namespace ATRepo.ViewModels;

public class MainWindowViewModel_Design : MainWindowViewModel
{
    public MainWindowViewModel_Design()
    {
        this.ATObjects.Add(new ATObject { Type = "com.app.test" });
        this.ATObjects.Add(new ATObject { Type = "com.app.test" });
        this.ATObjects.Add(new ATObject { Type = "com.app.test" });
        this.ATObjects.Add(new ATObject { Type = "com.app.test" });
    }
}