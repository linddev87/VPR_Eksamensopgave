namespace App.Interfaces
{
    public interface IActionRequest
    {
        string Run();

        string[] Params { get; }
    }
}