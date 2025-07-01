using System;
using BookCatalogApi.Models;

namespace BookCatalogApi.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public IEnumerable<Book> GetAll() => _books;

    public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);


    public void Add(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
    }

    public void Update(Book book)
    {
        var existing = GetById(book.Id);
        if (existing is null) return;

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Genre = book.Genre;
        existing.PublishedYear = book.PublishedYear;
    }

    public void Delete(int id)
    {
        var book = GetById(id);
        if (book is not null) _books.Remove(book);
    }

    public bool Exists(int id) => _books.Any(b => b.Id == id);

}
