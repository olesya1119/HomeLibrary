using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories;

namespace MVC.Controllers;

public class GenresController : Controller
{
    private readonly IGenreRepository _repository;

    public GenresController(IGenreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
        => View(_repository.GetAll());

    public IActionResult Details(long id)
        => View(_repository.GetById(id));

    public IActionResult Create()
        => View();

    [HttpPost]
    public IActionResult Create(Genre genre)
    {
        _repository.Create(genre);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(long id)
        => View(_repository.GetById(id));

    [HttpPost]
    public IActionResult Edit(Genre genre)
    {
        _repository.Update(genre);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        _repository.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
