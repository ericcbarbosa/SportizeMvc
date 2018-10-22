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
    public class PlayerController : Controller
    {
        private SportizeContext db = new SportizeContext();

        // GET: Player
        public async Task<ActionResult> Index()
        {
            List<Player> players = await SalesforceConnect.GetPlayersAsync();

            return View(players.ToList());
        }

        // GET: Player/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Player player = db.Players.Find(id);
            Player player = await SalesforceConnect.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Email,Password,State,City,Neighborhood,Address")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await SalesforceConnect.AddPlayerAsync(player);
                    //db.Players.Add(player);
                    //db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                // Log the error (uncomment dex variable name and add a line here to write a log.
                Console.WriteLine("Error: ", dex);
                ModelState.AddModelError("", "Não foi possivel salvar o novo jogador. Tente novamente.");
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Player player = db.Players.Find(id);
            Player player = await SalesforceConnect.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Email,Password,State,City,Neighborhood,Address")] Player player)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(player).State = EntityState.Modified;
                //db.SaveChanges();
                await SalesforceConnect.UpdatePlayerAsync(player);

                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Player player = db.Players.Find(id);
            Player player = await SalesforceConnect.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await SalesforceConnect.DeletePlayerAsync(id);
            //Player player = db.Players.Find(id);
            //db.Players.Remove(player);
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
