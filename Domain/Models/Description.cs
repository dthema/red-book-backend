namespace Domain.Models;

public class Description
{
    // public Description(string name, string title, string descriptionText, string audioFilePath)
    // {
    //     Name = name;
    //     Title = title;
    //     DescriptionText = descriptionText;
    //     AudioFilePath = audioFilePath;
    // }
    //
    // protected Description() { }

    public string Name { get; set; }
    public string Title { get; set; }
    public string DescriptionText { get; set; }
    public string AudioFilePath { get; set; }
}