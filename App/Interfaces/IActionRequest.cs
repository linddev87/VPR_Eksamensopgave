namespace App.Interfaces
{
    /// <summary>
    /// Super simple interface which allows us to use the Factory pattern to return different requests to the same UserInterface
    /// </summary>
    public interface IActionRequest
    {
        string Run();
    }
}