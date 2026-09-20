using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories;

namespace Mvc.Controllers;

public class BooksController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;

    public BooksController(
        IBookRepository bookRepository,
        IPublisherRepository publisherRepository,
        IAuthorRepository authorRepository,
        IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _publisherRepository = publisherRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
    }


    public IActionResult Index()
    {
        return View(_bookRepository.GetAll());
    }


    public IActionResult Details(long id)
    {
        var book = _bookRepository.GetById(id);

        if (book == null)
            return NotFound();

        return View(book);
    }


    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Publishers = _publisherRepository.GetAll();
        ViewBag.Authors = _authorRepository.GetAll();
        ViewBag.Genres = _genreRepository.GetAll();

        return View(new BookDetails());
    }


    [HttpPost]
    public IActionResult Create(BookDetails book, long[] authorIds, long[] genreIds)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Publishers = _publisherRepository.GetAll();
            ViewBag.Authors = _authorRepository.GetAll();
            ViewBag.Genres = _genreRepository.GetAll();

            return View(book);
        }


        book.AuthorsList = authorIds
            .Select(x => new Author { Id = x })
            .ToList();

        book.GenresList = genreIds
            .Select(x => new Genre { Id = x })
            .ToList();


        _bookRepository.Create(book);

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Edit(long id)
    {
        var book = _bookRepository.GetById(id);

        if (book == null)
            return NotFound();


        ViewBag.Publishers = _publisherRepository.GetAll();
        ViewBag.Authors = _authorRepository.GetAll();
        ViewBag.Genres = _genreRepository.GetAll();

        return View(book);
    }


    [HttpPost]
    public IActionResult Edit(BookDetails book, long[] authorIds, long[] genreIds)
    {
        book.AuthorsList = authorIds
            .Select(x => new Author { Id = x })
            .ToList();

        book.GenresList = genreIds
            .Select(x => new Genre { Id = x })
            .ToList();


        _bookRepository.Update(book);

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public IActionResult Delete(long id)
    {
        _bookRepository.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
