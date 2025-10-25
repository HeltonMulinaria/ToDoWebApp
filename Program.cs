using ToDoWebApp.Services;
using ToDoWebApp.DTOs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ToDoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ToDo API Endpoints

app.MapGet("/api/todos", (ToDoService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithName("GetAllTodos")
.WithOpenApi()
.WithTags("ToDo");

app.MapGet("/api/todos/{id}", (int id, ToDoService service) =>
{
    var todo = service.GetById(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("GetToDoById")
.WithOpenApi()
.WithTags("ToDo");

app.MapGet("/api/todos/completed", (ToDoService service) =>
{
    return Results.Ok(service.GetCompleted());
})
.WithName("GetCompletedTodos")
.WithOpenApi()
.WithTags("ToDo");

app.MapGet("/api/todos/pending", (ToDoService service) =>
{
    return Results.Ok(service.GetPending());
})
.WithName("GetPendingTodos")
.WithOpenApi()
.WithTags("ToDo");

app.MapPost("/api/todos", (CreateToDoRequest request, ToDoService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest("Title is required");
    }

    var todo = service.Create(request.Title, request.Description);
    return Results.Created($"/api/todos/{todo.Id}", todo);
})
.WithName("CreateToDo")
.WithOpenApi()
.WithTags("ToDo");

app.MapPut("/api/todos/{id}", (int id, UpdateToDoRequest request, ToDoService service) =>
{
    var todo = service.Update(id, request.Title, request.Description, request.IsCompleted);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("UpdateToDo")
.WithOpenApi()
.WithTags("ToDo");

app.MapDelete("/api/todos/{id}", (int id, ToDoService service) =>
{
  var deleted = service.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteToDo")
.WithOpenApi()
.WithTags("ToDo");

app.Run();
