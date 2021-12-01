using Iam_Influencer.Models;
using Iam_Influencer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;

        public HomeController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _context.Sliders.ToListAsync();
            var reviews = await _context.Reviews.ToListAsync();

            ContentViewModel viewModel = new ContentViewModel();
            slider.ForEach(s =>
            {
                viewModel.SliderId = s.Id;
                viewModel.SliderText = s.Text;
                viewModel.SliderTitle = s.Title;
            });
            reviews.ForEach(r =>
            {
                viewModel.Id = r.Id;
                viewModel.Text = r.Text;
                viewModel.Title = r.Title;
                viewModel.Imagepath = r.Imagepath;
                viewModel.Name = r.Name;
                viewModel.Starcount = r.Starcount;
            });

            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
