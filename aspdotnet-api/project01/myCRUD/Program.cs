var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

var books = new List<Book>{
    new Book {Id = 1, Title = " Title 1", Author = "Author 1"},
    new Book {Id = 2, Title = " Title 2", Author = "Author 2"},
    new Book {Id = 3, Title = " Title 3", Author = "Author 3"}
};



app.MapGet("/book", () => {
    return Results.Json(books);
});

app.MapGet("/book/{id}", (int id) => 
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Sorry, this book doesn't exist.");
        
    return Results.Ok(book);
});

app.MapPost("/book", (Book book) => 
{
    books.Add(book);
    return Results.Json(books);
});

app.MapPut("/book/{id}", (Book updateBook, int id) => 
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Sorry, this book doesn't exist.");

    book.Title = updateBook.Title;
    book.Author = updateBook.Author;
        
    return Results.Ok(books);
});

app.MapDelete("/book/{id}", (int id) => 
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Sorry, this book doesn't exist.");

    books.Remove(book);
        
    return Results.Ok(books);
});

app.MapControllers();

app.Run();

class Book
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public required string Author {get; set;}
}