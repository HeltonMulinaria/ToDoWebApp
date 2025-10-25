using ToDoWebApp.Models;

namespace ToDoWebApp.Services;

public class ToDoService
{
    private readonly List<ToDoItem> _todos = new();
    private int _nextId = 1;

    public IEnumerable<ToDoItem> GetAll()
    {
        return _todos;
    }

    public ToDoItem? GetById(int id)
    {
        return _todos.FirstOrDefault(t => t.Id == id);
    }

    public ToDoItem Create(string title, string? description)
    {
        var todo = new ToDoItem
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _todos.Add(todo);
        return todo;
    }

    public ToDoItem? Update(int id, string? title, string? description, bool? isCompleted)
    {
        var todo = GetById(id);
        if (todo == null)
            return null;

        if (title != null)
            todo.Title = title;

        if (description != null)
            todo.Description = description;

        if (isCompleted.HasValue)
        {
            todo.IsCompleted = isCompleted.Value;
            todo.CompletedAt = isCompleted.Value ? DateTime.UtcNow : null;
        }

        return todo;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo == null)
            return false;

        _todos.Remove(todo);
        return true;
    }

    public IEnumerable<ToDoItem> GetCompleted()
    {
        return _todos.Where(t => t.IsCompleted);
    }

    public IEnumerable<ToDoItem> GetPending()
    {
        return _todos.Where(t => !t.IsCompleted);
    }
}
