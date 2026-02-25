using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Data;
using StoreFlow.Models;

namespace StoreFlow.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string NewItemText { get; set; } = "";

        public List<MissingRecord> Records { get; set; } = new();

        public async Task OnGetAsync()
        {
            Records = await _context.MissingRecords
                .Where(x => x.Status == RecordStatus.Pending)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewItemText))
            {
                var record = new MissingRecord
                {
                    Text = NewItemText.Trim(),
                    Status = RecordStatus.Pending
                };

                _context.MissingRecords.Add(record);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
