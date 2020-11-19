namespace App.Interfaces
{
    public interface IActionRequest
    {
        string Run();
        string Action { get; }
        string[] Params { get; }
    }
}