﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeHelpCallsWebSite;
using HomeHelpCallsWebSite.Models;
using AutoMapper;

namespace HomeHelpCallsWebSite.Controllers
{
    public class OpenCallsController : Controller
    {
        private MaaleDBEntities _db;
        private IMapper _mapper;

        public OpenCallsController()
        {
            _db = new MaaleDBEntities();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_OPEN_CALLS, CallsViewModel>());
            _mapper = config.CreateMapper();
        }


        // GET: OpenCalls
        public async Task<ActionResult> Index()
        {
            var dto = await _db.VUMM_HH_OPEN_CALLS.ToListAsync();
            var vm = _mapper.Map<List<VUMM_HH_OPEN_CALLS>, IEnumerable<CallsViewModel>>(dto);
            return View(vm);
        }

        // GET: OpenCalls/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_OPEN_CALLS dto = await _db.VUMM_HH_OPEN_CALLS.FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            CallsViewModel vm = _mapper.Map<CallsViewModel>(dto);
            return View(vm);
        }

        //// GET: OpenCalls/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: OpenCalls/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "DOC_NBR,LINE_NBR,LINE_EVNT_DATE,RQSTD_SHIP_DATE,LINE_EXPR_DATE,STRM_CODE,STRM_NAME,PARENT_STRM_CODE,CALL_DSCR,APT_CODE,APT_NAME,APT_SHRT_NAME,DSTN_CODE,DSTN_NAME,CELL_PHONE,EMAIL,CALL_STAT_CODE,CALL_STAT,CALL_STAT_DATE,CALL_STAT_RMRK,CALL_STAT_FULL,BILL_TO_CUST_CODE,OPEN_DATE,HAS_IMAGES")] VUMM_HH_OPEN_CALLS vUMM_HH_OPEN_CALLS)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.VUMM_HH_OPEN_CALLS.Add(vUMM_HH_OPEN_CALLS);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(vUMM_HH_OPEN_CALLS);
        //}

        //// GET: OpenCalls/Edit/5
        //public async Task<ActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VUMM_HH_OPEN_CALLS vUMM_HH_OPEN_CALLS = await db.VUMM_HH_OPEN_CALLS.FindAsync(id);
        //    if (vUMM_HH_OPEN_CALLS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vUMM_HH_OPEN_CALLS);
        //}

        //// POST: OpenCalls/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "DOC_NBR,LINE_NBR,LINE_EVNT_DATE,RQSTD_SHIP_DATE,LINE_EXPR_DATE,STRM_CODE,STRM_NAME,PARENT_STRM_CODE,CALL_DSCR,APT_CODE,APT_NAME,APT_SHRT_NAME,DSTN_CODE,DSTN_NAME,CELL_PHONE,EMAIL,CALL_STAT_CODE,CALL_STAT,CALL_STAT_DATE,CALL_STAT_RMRK,CALL_STAT_FULL,BILL_TO_CUST_CODE,OPEN_DATE,HAS_IMAGES")] VUMM_HH_OPEN_CALLS vUMM_HH_OPEN_CALLS)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(vUMM_HH_OPEN_CALLS).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(vUMM_HH_OPEN_CALLS);
        //}

        //// GET: OpenCalls/Delete/5
        //public async Task<ActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VUMM_HH_OPEN_CALLS vUMM_HH_OPEN_CALLS = await db.VUMM_HH_OPEN_CALLS.FindAsync(id);
        //    if (vUMM_HH_OPEN_CALLS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vUMM_HH_OPEN_CALLS);
        //}

        //// POST: OpenCalls/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(long id)
        //{
        //    VUMM_HH_OPEN_CALLS vUMM_HH_OPEN_CALLS = await db.VUMM_HH_OPEN_CALLS.FindAsync(id);
        //    db.VUMM_HH_OPEN_CALLS.Remove(vUMM_HH_OPEN_CALLS);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}