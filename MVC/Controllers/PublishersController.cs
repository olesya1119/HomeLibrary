using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories;

namespace MVC.Controllers;

public class PublishersController : Controller
{
    private readonly IPublisherRepository _repository;

    public PublishersController(IPublisherRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View(_repository.GetAll());
    }

    public IActionResult Details(long id)
    {
        var publisher = _repository.GetById(id);
        if (publisher == null) return NotFound();

        return View(publisher);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Publisher publisher)
    {
        if (!ModelState.IsValid)
            return View(publisher);

        _repository.Create(publisher);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var publisher = _repository.GetById(id);
        if (publisher == null) return NotFound();

        return View(publisher);
    }

    [HttpPost]
    public IActionResult Edit(Publisher publisher)
    {
        if (!ModelState.IsValid)
            return View(publisher);

        _repository.Update(publisher);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(long id)
    {
        _repository.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
