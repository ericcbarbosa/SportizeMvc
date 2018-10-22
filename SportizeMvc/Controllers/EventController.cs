using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SportizeMvc.DAL;
using SportizeMvc.Models;

namespace SportizeMvc.Controllers
{
    public class EventController : Controller
    {
        private SportizeContext db = new SportizeContext();

        // GET: Event
        public async Task<ActionResult> Index()
        {
            List<Event> events = await SalesforceConnect.GetEventsAsync();

            return View(events.ToList());
        }

        // GET: Event/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Event @event = db.Events.Find(id);
            Event evento = await SalesforceConnect.GetEventByIdAsync(id);

            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Description,EventDate,Address,Neighborhood,City,State")] Event @event)
        {
            if (ModelState.IsValid)
            {
                await SalesforceConnect.AddEventAsync(@event);
                //db.Events.Add(@event);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Event @event = db.Events.Find(id);
            Event @event = await SalesforceConnect.GetEventByIdAsync(id);

            if (@event == null)
            {
                return HttpNotFound();
            }

            return View(@event);
        }

        // POST: Event/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,EventDate,Address,Neighborhood,City,State")] Event @event)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(@event).State = EntityState.Modified;
                //db.SaveChanges();
                await SalesforceConnect.UpdateEventAsync(@event);

                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Event @event = db.Events.Find(id);
            Event @event = await SalesforceConnect.GetEventByIdAsync(id);

            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await SalesforceConnect.DeleteEventAsync(id);
            //Event @event = db.Events.Find(id);
            //db.Events.Remove(@event);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
