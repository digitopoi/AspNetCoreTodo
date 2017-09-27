using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers 
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task<IActionResult> Index()
        {
            //  Get to-do items from database / or mock
            var todoItems = await _todoItemService.GetIncompleteItemsAsync();
            //  Put items into a model
            var model = new TodoViewModel() 
            {
                Items = todoItems
            };
            //  Pass the view to a model and render
            return View(model);
        }
        public async Task<IActionResult> AddItem(NewTodoItem newItem) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successful = await _todoItemService.AddItem(newItem);
            if (!successful)
            {
                return BadRequest(new { error = "Could not add item." });
            }

            return Ok();
        }
    }
}