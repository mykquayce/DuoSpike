using System.ComponentModel.DataAnnotations;

namespace DuoSpike.BlazorApp1.Data;

public class SessionState
{
    public SessionState()
    {
        State = DuoUniversal.Client.GenerateState();
        System.Diagnostics.Debug.WriteLine(State);
    }

    public string State { get; }

    [Required]
    [MinLength(1)]
    public string? Username { get; set; }

    public string? Groups { get; set; }
}
