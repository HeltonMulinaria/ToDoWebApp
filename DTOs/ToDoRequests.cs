namespace ToDoWebApp.DTOs;

public record CreateToDoRequest(string Title, string? Description);

public record UpdateToDoRequest(string? Title, string? Description, bool? IsCompleted);
