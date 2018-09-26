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
using HomeHelpCallsWebSite.Infrastructure.Data;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;

namespace HomeHelpCallsWebSite.Controllers
{
    public class LinePartsController : Controller
    {
        private ApplicationDbContext _conntext;
        private IMapper _linesMapper;
        private IMapper _partsMapper;
        private CallModel _CallModel;
        private string _parent_strm_code;


        public LinePartsController()
        {
            _conntext = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_CALLS_LINES, LineViewModel>());
            _linesMapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_PARTS, PartViewModel>());
            _partsMapper = config2.CreateMapper();
        }

        // GET: LineParts
        public async Task<ActionResult> Index(long id)
        {
            var res = _conntext.VUMM_HH_CALLS_LINES.ToList().Where<VUMM_HH_CALLS_LINES>(w => w.DOC_NBR == id);
            var vm = _linesMapper.Map<IEnumerable<LineViewModel>>(res);
            foreach (var line in vm)
            {
                var p = _conntext.VUMM_HH_PARTS.Find(line.part_code);
                //line.PartV = Mapper.Map<PartViewModel>(p);
            }
            return View(vm);
        }

        // GET: LineParts/Details/5
        public async Task<ActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = await _conntext.VUMM_HH_CALLS_LINES.FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            LineViewModel vm  = _linesMapper.Map<LineViewModel>(dto);  
            return View(vm);
        }

        //[NonAction]
        public IEnumerable<SelectListItem> GetPartsSelectList(string parent_strm_code)  
        {
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == parent_strm_code);
            IEnumerable<PartViewModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, List<PartViewModel>>(res);
            var partssl = partsl.Select(x => new SelectListItem { Value = x.PART_CODE, Text = x.PART_CODE_NAME });
            return new SelectList(partssl, "Value", "Text");
        }

        #region LineParts
        public string GetPartsByStr(LineViewModel lvm)
        {
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE.Contains(lvm.part_code));
            PartViewModel[] partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res).ToArray<PartViewModel>();
            return JsonConvert.SerializeObject(partsl);
        }


        //[NonAction]
        public PartViewModel[] GetPartsByStrm(string strm)
        {
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm);
            PartViewModel[] partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res).ToArray<PartViewModel>();
            return partsl;
        }


        #endregion LineParts
        public ActionResult GetParts ()
        {
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == this._parent_strm_code);
            IEnumerable<PartViewModel> parts = _partsMapper.Map < IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res); 
             return PartialView("Parts/SelectPartView",parts);
        }

        public JsonResult GetFilterdParts(string term, long doc)
        {
            var strm = GetParentStrmCode(doc);
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm);
 
            IEnumerable<PartViewModel> parts = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res);
            parts = parts.Where(x => x.PART_CODE_NAME.Contains(term));
            return Json(parts, JsonRequestBehavior.AllowGet);
        }


        public string GetParentStrmCode( long doc_nbr)
        {
            var call = _conntext.VUMM_HH_OPEN_CALLS.Find(doc_nbr);
            return call.PARENT_STRM_CODE;
        }


        //// GET: parts
        //public IEnumerable<SelectListItem> GetPartsCodesSelectList(string parent_strm_code)  
        //{
        //    var res = _conntext.VUMM_HH_PARTS.ToList();
        //    IEnumerable<PartModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, List<PartModel>>(res).Where(m => m.strm_code == parent_strm_code);
        //    var partssl = partsl.Select(x => new SelectListItem { Value = x.part_code, Text = x.part_code });
        //    return new SelectList(partssl, "Value", "Text");

        //}


        // GET: LineParts/Create
        public ActionResult Create(long id)
        {
            var call = _conntext.VUMM_HH_OPEN_CALLS.Find(id);
            this._parent_strm_code = call.PARENT_STRM_CODE;
            LineViewModel vm = new LineViewModel
            {
                //PartsJ = GetPartsByStrm(this._parent_strm_code),
                //Parts = GetPartsSelectList(this._parent_strm_code),
                doc_nbr = id,
            };
            return View(vm);
        }

        // POST: LineParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "doc_nbr, part_code, qnty, txt_dscr, line_nbr")] LineViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                _conntext.ExecuteStoreProcedure("mm_hh.mm_hh_insert_lft", iVm.doc_nbr, iVm.part_code, iVm.qnty, iVm.txt_dscr, iVm.line_nbr);
                return RedirectToAction("Index", new { id = iVm.doc_nbr });
            }
            else
            {
                return View(iVm);
            }
            //var part = iVm.PartsJ.Where<PartViewModel>(m => m.PART_CODE_NAME.Contains(iVm.part_str));
            //iVm.PartV = part.First<PartViewModel>();
            // VUMM_HH_CALLS_LINES dto = _linesMapper.Map<VUMM_HH_CALLS_LINES>(iVm);

            //if (ModelState.IsValid)
            //{
            //    _conntext.VUMM_HH_CALLS_LINES.Add(dto);
            //    await _conntext.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //return View(iVm);
        }

        // GET: LineParts/Edit/5
        public async Task<ActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.SingleOrDefaultAsync(m => m.LINE_ID == id);
            //FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }

            LineViewModel vm = new LineViewModel();
            vm = _linesMapper.Map<LineViewModel>(dto);  
            return View(vm);
        }

        // POST: LineParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = " doc_nbr, line_nbr, part_code, part_code_name, qnty, txt_dscr")] LineViewModel iVm )
        {
            if (ModelState.IsValid)
            {
                //VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.SingleOrDefaultAsync(m => m.PART_CODE_NAME == iVm.part_code_name);
                //dto.QNTY = iVm.QNTY;
                //dto.TXT_DSCR = iVm.rmrk;
                _conntext.ExecuteStoreProcedure("mm_hh.mm_hh_update_lft", iVm.doc_nbr, iVm.line_nbr, iVm.qnty, iVm.txt_dscr);
                // _conntext.Entry(dto).State = EntityState.Modified;
                //await _conntext.SaveChangesAsync();
                return RedirectToAction("Index", new { id = iVm.doc_nbr });
            }
            else
            {
                return View(iVm);
            }
        }


       // GET: LineParts/Delete/5
        public async Task<ActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.FindAsync(id);
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
            VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.FindAsync(id);
            _conntext.VUMM_HH_CALLS_LINES.Remove(dto);
            await _conntext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _conntext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
