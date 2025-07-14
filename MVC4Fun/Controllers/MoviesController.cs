using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC4Fun.Data;
using MVC4Fun.Models;
using System.Diagnostics;
using System.Reflection;

namespace MVC4Fun.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private readonly ILog logger = LogManager.GetLogger(typeof(Program));

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Movies.ToListAsync());
            }
            catch(Exception ex)
            {
                var a = ex.ToString();
                return View();
            }
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            stopWatch.Stop();
            logger.Info(MethodBase.GetCurrentMethod().ReflectedType.FullName + "，Cost:" + stopWatch.Elapsed.TotalMilliseconds + "，Data:" + movie.ToJsonStr());

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Film,Year,Director")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                _context.Add(movie);
                await _context.SaveChangesAsync();
                stopWatch.Stop();
                logger.Info(MethodBase.GetCurrentMethod().ReflectedType.FullName+"，Cost:"+ stopWatch.Elapsed.TotalMilliseconds+ "，Data:" + movie.ToJsonStr());

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,Film,Year,Director")] Movie movie)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                    stopWatch.Stop();
                    logger.Info(MethodBase.GetCurrentMethod().ReflectedType.FullName + "，Cost:" + stopWatch.Elapsed.TotalMilliseconds + "，Data:" + movie.ToJsonStr());

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    logger.Error(MethodBase.GetCurrentMethod().ReflectedType.FullName + "，Message:" + ex.Message.ToString());
                    if (!MovieExists(movie.MovieID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            stopWatch.Stop();
            logger.Info(MethodBase.GetCurrentMethod().ReflectedType.FullName + "，Cost:" + stopWatch.Elapsed.TotalMilliseconds + "，Data:" + id.ToJsonStr());

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieID == id);
        }
    }
}
