using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories;

namespace MVC.Controllers;

public class AuthorsController : Controller
{
    private readonly IAuthorRepository _repository;

    public AuthorsController(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()=> View(_repository.GetAll());
    public IActionResult Details(long id) => View(_repository.GetById(id));
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Author author)
    {
        _repository.Create(author);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(long id) => View(_repository.GetById(id));

    [HttpPost]
    public IActionResult Edit(Author author)
    {
        _repository.Update(author);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        _repository.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}