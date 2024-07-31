using Homework3.Data;
using Homework3.Entities;
using Homework3.Models;
using Homework3.Repositories.Abstracts;
using Homework3.Repositories.Concrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework3.Controllers
{
    public class ProductController : Controller
    {


        private readonly ProductDbContext _context;
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(ProductDbContext context, IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._repository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var vm = new ProductsViewModel
            {
                Products = _context.Products.ToList()
            };
            return View(vm);
        }



        [HttpGet]
        public IActionResult Add()
        {

            var vm = new AddProductViewModel
            {
                Product = new()
            };
            return View(vm);

        }
        [HttpPost]
        public IActionResult Add(AddProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.Image != null)
                {

                    string upload = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    fileName = Guid.NewGuid().ToString() + "-" + vm.Image.FileName;
                    string filePath = Path.Combine(upload, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.Image.CopyTo(fileStream);
                    }


                }

                vm.Product.ImageUrl = fileName;
                _repository.Add(vm.Product);

                return RedirectToAction("Index");
            }

            return View(vm);
        }


        public IActionResult Edit(int id) { 
        
            var prod = _context.Products.FirstOrDefault(x => x.Id == id);
            if (prod == null) {
                return Ok("error");
            }
            var viewModel = new AddProductViewModel { Product = prod };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.Image != null)
                {

                    string upload = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    fileName = Guid.NewGuid().ToString() + "-" + vm.Image.FileName;
                    string filePath = Path.Combine(upload, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.Image.CopyTo(fileStream);
                    }


                }

                vm.Product.ImageUrl = fileName;
                _repository.Update(vm.Product);

                return RedirectToAction("Index");
            }

            return View(vm);
        }
        public IActionResult Delete(int id) { 
            _repository.Delete(id);
                return RedirectToAction("Index");
        
        }

    }
}
