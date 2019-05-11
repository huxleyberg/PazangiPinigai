using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankAggregator.Domain.Models;
using BankAggregator.Web.Models;

namespace BankAggregator.Web.Controllers
{
    public class accountModelsController : Controller
    {
        private readonly AggregatorContext _context;

        public accountModelsController(AggregatorContext context)
        {
            _context = context;
        }

        // GET: accountModels
        public async Task<IActionResult> Index()
        {
            var aggregatorContext = _context.AccountModels.Include(a => a.User);
            return View(await aggregatorContext.ToListAsync());
        }

        // GET: accountModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _context.AccountModels
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountModel == null)
            {
                return NotFound();
            }

            return View(accountModel);
        }

        // GET: accountModels/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.appUsers, "Id", "Id");
            return View();
        }

        // POST: accountModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserID,BankName,BankAccountNumber,Balance,CreatedAt,TransactionLimit")] accountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.appUsers, "Id", "Id", accountModel.UserID);
            return View(accountModel);
        }

        // GET: accountModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _context.AccountModels.FindAsync(id);
            if (accountModel == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.appUsers, "Id", "Id", accountModel.UserID);
            return View(accountModel);
        }

        // POST: accountModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserID,BankName,BankAccountNumber,Balance,CreatedAt,TransactionLimit")] accountModel accountModel)
        {
            if (id != accountModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!accountModelExists(accountModel.Id))
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
            ViewData["UserID"] = new SelectList(_context.appUsers, "Id", "Id", accountModel.UserID);
            return View(accountModel);
        }

        // GET: accountModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _context.AccountModels
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountModel == null)
            {
                return NotFound();
            }

            return View(accountModel);
        }

        // POST: accountModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountModel = await _context.AccountModels.FindAsync(id);
            _context.AccountModels.Remove(accountModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool accountModelExists(int id)
        {
            return _context.AccountModels.Any(e => e.Id == id);
        }
    }
}
