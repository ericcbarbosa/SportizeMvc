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
    public class GroupController : Controller
    {
        private SportizeContext db = new SportizeContext();

        // GET: Group
        public async Task<ActionResult> Index()
        {
            List<Group> groups = await SalesforceConnect.GetGroupsAsync();

            return View(groups.ToList());
        }

        // GET: Group/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = await SalesforceConnect.GetGroupByIdAsync(id);
            //Group group = db.Groups.Find(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = ",Name,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                await SalesforceConnect.AddGroupAsync(group);
                //db.Groups.Add(group);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Group/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = await SalesforceConnect.GetGroupByIdAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // POST: Group/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(group).State = EntityState.Modified;
                //db.SaveChanges();
                await SalesforceConnect.UpdateGroupAsync(group);

                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Group/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Group group = db.Groups.Find(id);
            Group group = await SalesforceConnect.GetGroupByIdAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {

            await SalesforceConnect.DeleteGroupAsync(id);
            //Group group = db.Groups.Find(id);
            //db.Groups.Remove(group);
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
