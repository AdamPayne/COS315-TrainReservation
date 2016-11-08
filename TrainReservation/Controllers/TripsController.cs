using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainReservation.Models;
using TrainReservationDBContext;

namespace TrainReservation.Controllers
{
    public class TripsController : Controller
    {
        private TrainReservationDbContext db = new TrainReservationDbContext();

        // GET: Trips
        public ActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TripID,Departure,Departure_Time,Destination,Arrival_Time,Seats,Price")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }



        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TripID,Departure,Departure_Time,Destination,Arrival_Time,Seats,Price")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        Bookings get_data_from(int id, string sender)
        {
            Bookings booking = new Bookings();
            booking.TripID = id;
            booking.UserId = "";
            int j = 0;
            System.Diagnostics.Debug.WriteLine(sender);
            System.Diagnostics.Debug.WriteLine("^^^^^^^^^^^^^^^^^^^^");

            for (; (j < sender.Length) && (sender[j] != '#') ; j++)
            {
           //     System.Diagnostics.Debug.WriteLine(j);
                booking.UserId += sender[j];
            }
            string tmp = "";
            for (; j < sender.Length; j++)
                tmp += sender[j];
            if (tmp == "") tmp = "-1";
            booking.SeatId = Int32.Parse(tmp);
            return booking;
        }

        // Trips/Book/3
        public ActionResult Book(int id,  string sender)
        {
            System.Diagnostics.Debug.WriteLine("Book Called");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }

            Bookings booking = get_data_from(id, sender);
            return View(booking);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Book")]
        [ValidateAntiForgeryToken]
        public ActionResult BookingConfirmed(int id, string sender, int SeatId = -1)
        {
            System.Diagnostics.Debug.WriteLine("BookingConfirmed Called");
            System.Diagnostics.Debug.WriteLine(SeatId);

            Bookings booking = get_data_from(id, sender);
            db.Bookings.Add(booking);
            db.SaveChanges();
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
