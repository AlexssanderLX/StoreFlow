using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Data;
using StoreFlow.Models;

namespace StoreFlow.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<MissingRecord> Pending { get; set; } = new();
        public List<MissingRecord> Ordered { get; set; } = new();

        public async Task OnGetAsync()
        {
            var all = await _context.MissingRecords
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            Pending = all.Where(x => x.Status == RecordStatus.Pending).ToList();
            Ordered = all.Where(x => x.Status == RecordStatus.Ordered).ToList();
        }

        public async Task<IActionResult> OnPostChangeStatusAsync(int id, RecordStatus status)
        {
            var record = await _context.MissingRecords.FindAsync(id);

            if (record != null)
            {
                record.Status = status;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var record = await _context.MissingRecords.FindAsync(id);

            if (record != null)
            {
                _context.MissingRecords.Remove(record);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearAllAsync()
        {
            _context.MissingRecords.RemoveRange(_context.MissingRecords);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
