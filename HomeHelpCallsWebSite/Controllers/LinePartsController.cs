using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeHelpCallsWebSite;
using AutoMapper;
using HomeHelpCallsWebSite.Models;
using HomeHelpCallsWebSite.Infrastructure.Data.Models;

namespace HomeHelpCallsWebSite.Controllers
{
    public class LinePartsController : Controller
    {
        private MaaleDBEntities _db;
        private IMapper _linesMapper;
        private IMapper _partsMapper;
        private CallsModel _CallModel;


        public LinePartsController()
        {
            _db = new MaaleDBEntities();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_CALLS_LINES, LineViewModel>());
            _linesMapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_PARTS, PartModel>());
            _partsMapper = config2.CreateMapper(); 
        }

        // GET: LineParts
        public async Task<ActionResult> Index(long id)
        {
            var res = await _db.VUMM_HH_CALLS_LINES.ToListAsync();
            var dto = res.Where(t => t.DOC_NBR == id);
            var vm = _linesMapper.Map<IEnumerable<VUMM_HH_CALLS_LINES>, IEnumerable<LineViewModel>>(dto);
            return View(vm);
           
        }

        // GET: LineParts/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = await _db.VUMM_HH_CALLS_LINES.FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            LineViewModel vm  = _linesMapper.Map<LineViewModel>(dto);  
            return View(vm);
        }

        // GET: parts
        public IEnumerable<SelectListItem> GetPartsSelectList(string parent_strm_code)  
        {
            var res = _db.VUMM_HH_PARTS.ToList();
            IEnumerable<PartModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, List<PartModel>>(res).Where(m => m.PRMY_STRM_CODE == parent_strm_code);
            var partssl = partsl.Select(x => new SelectListItem { Value = x.PART_CODE, Text = x.PART_CODE_NAME });
            return new SelectList(partssl, "Value", "Text");
          
        }

        // GET: parts
        public IEnumerable<SelectListItem> GetPartsCodesSelectList(string parent_strm_code)  
        {
            var res = _db.VUMM_HH_PARTS.ToList();
            IEnumerable<PartModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, List<PartModel>>(res).Where(m => m.PRMY_STRM_CODE == parent_strm_code);
            var partssl = partsl.Select(x => new SelectListItem { Value = x.PART_CODE, Text = x.PART_CODE });
            return new SelectList(partssl, "Value", "Text");
           
        }


        // GET: LineParts/Create
        public ActionResult Create(long id)
        {
            var call = _db.VUMM_HH_OPEN_CALLS.Find(id);
            var parentStrm = call.PARENT_STRM_CODE;
            LineViewModel vm = new LineViewModel
            {
                Parts = GetPartsSelectList(parentStrm),
                DOC_NBR = id,
            };
            return View(vm);
        }

        // POST: LineParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DOC_NBR,PART_CODE_NAME,RMRK,UNIT_CODE")] LineViewModel iVm)
        {
            VUMM_HH_CALLS_LINES dto = _linesMapper.Map<VUMM_HH_CALLS_LINES>(iVm);
            
            if (ModelState.IsValid)
            {
                _db.VUMM_HH_CALLS_LINES.Add(dto);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(iVm);
        }

        // GET: LineParts/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_CALLS_LINES dto = await _db.VUMM_HH_CALLS_LINES.FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            LineViewModel vm = _linesMapper.Map<LineViewModel>(dto);
            return View(vm);
        }

        // POST: LineParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PART_CODE,QNTY,LINE_NBR,DOC_NBR,PART_NAME,RMRK,ID")] LineViewModel iVm )
        {
            VUMM_HH_CALLS_LINES dto = _linesMapper.Map<VUMM_HH_CALLS_LINES>(iVm);
            if (ModelState.IsValid)
            {
                _db.Entry(dto).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(iVm);
        }


       // GET: LineParts/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_CALLS_LINES dto = await _db.VUMM_HH_CALLS_LINES.FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            LineViewModel vm = _linesMapper.Map<LineViewModel>(dto);
            return View(vm);
        }

        // POST: LineParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            VUMM_HH_CALLS_LINES dto = await _db.VUMM_HH_CALLS_LINES.FindAsync(id);
            _db.VUMM_HH_CALLS_LINES.Remove(dto);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
