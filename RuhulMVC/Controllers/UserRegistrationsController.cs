using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuhulMVC.Data;
using RuhulMVC.Models;

namespace RuhulMVC.Controllers
{
    public class UserRegistrationsController : Controller
    {
        private readonly CliPolmanDbContext _context;

        public UserRegistrationsController(CliPolmanDbContext context)
        {
            _context = context;
        }

        // GET: UserRegistrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserRegistrations.ToListAsync());
        }

        // GET: UserRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRegistration = await _context.UserRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            return View(userRegistration);
        }

        // GET: UserRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: UserRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598. 
        //  UserRegistrations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password")] UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRegistration);
                await _context.SaveChangesAsync();

                SendMail(userRegistration);

                return RedirectToAction(nameof(Index));
            }
            return View(userRegistration);
        }

        // GET: UserRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRegistration = await _context.UserRegistrations.FindAsync(id);
            if (userRegistration == null)
            {
                return NotFound();
            }
            return View(userRegistration);
        }

        // POST: UserRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password")] UserRegistration userRegistration)
        {
            if (id != userRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRegistrationExists(userRegistration.Id))
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
            return View(userRegistration);
        }


        // GET: UserRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRegistration = await _context.UserRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            return View(userRegistration);
        }

        // POST: UserRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRegistration = await _context.UserRegistrations.FindAsync(id);
            if (userRegistration != null)
            {
                _context.UserRegistrations.Remove(userRegistration);
            }

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool UserRegistrationExists(int id)
        {
            return _context.UserRegistrations.Any(e => e.Id == id);
        }




        public void SendMail(Models.UserRegistration model)
        {






            try
            {
                // Create the SmtpClient object
                using (SmtpClient client = new SmtpClient("ruhul.amin@charteredlifebd.com", 587))
                {
                    // Enable SSL for secure connection
                    client.EnableSsl = true;
                    client.Host = "mail.charteredlifebd.com";
                    // Set credentials (username and password)
                    client.Credentials = new NetworkCredential("ruhul.amin@charteredlifebd.com", "");

                    // Create the email message
                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("ruhul.amin@charteredlifebd.com"),
                        Subject = "Registration Confirmatoin",
                        Body = "Your registration is completed against mail id " + model.Email + "",
                        IsBodyHtml = false  // Set to true if sending HTML email
                    };

                    // Add recipient
                    //  mailMessage.To.Add("m.rasarker77@gmail.com");
                    mailMessage.To.Add(model.Email.ToString());

                    // Send the email
                    client.Send(mailMessage);

                   Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
              Console.WriteLine($"Error sending email: {ex.Message}");
            }






        }

    }
}
