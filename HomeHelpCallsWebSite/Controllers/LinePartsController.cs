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
        private IMapper _workPartsMapper;
        private CallModel _CallModel;
        private string _parent_strm_code;


        public LinePartsController()
        {
            _conntext = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_CALLS_LINES, LineViewModel>());
            _linesMapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_PARTS, PartViewModel>());
            _partsMapper = config2.CreateMapper();
            var config3 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_WORK_PARTS, PartViewModel>());
            _workPartsMapper = config3.CreateMapper();
        }

        // GET: LineParts
        public async Task<ActionResult> Index(long id)
        {
            var res = _conntext.VUMM_HH_CALLS_LINES.ToList().Where<VUMM_HH_CALLS_LINES>(w => w.DOC_NBR == id);
            var vm = _linesMapper.Map<IEnumerable<LineViewModel>>(res);
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

        ////[NonAction]
        //public IEnumerable<SelectListItem> GetPartsSelectList(string parent_strm_code)  
        //{
        //    var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == parent_strm_code);
        //    IEnumerable<PartViewModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, List<PartViewModel>>(res);
        //    var partssl = partsl.Select(x => new SelectListItem { Value = x.PART_CODE, Text = x.PART_CODE_NAME });
        //    return new SelectList(partssl, "Value", "Text");
        //}

        #region LineParts

        public ActionResult PartsSelect(long id, string strm, string inputcode)
        {
            var parts = FindPart(strm, inputcode);
            parts.First().doc_nbr = id;
            return View(parts);
        }

        public ActionResult workPartsSelect(long id, string grp, string inputcode)
        {
            var parts = FindWorkPart(grp, inputcode);
            parts.First().doc_nbr = id;
            return View(parts);
        }

        public IEnumerable<PartViewModel> FindWorkPart( string grp, string inputcode)
        {
            grp = string.Concat(grp, '0');
            if (string.IsNullOrEmpty(inputcode))
            {
                inputcode = grp;
            }
            var res = _conntext.VUMM_HH_WORK_PARTS.ToList().Where<VUMM_HH_WORK_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PART_GRP_CODE.Equals(grp));
            if( res.Count() == 0 )
            {
                res = _conntext.VUMM_HH_WORK_PARTS.ToList().Where<VUMM_HH_WORK_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PART_GRP_CODE.Equals(grp));
            }
            IEnumerable<PartViewModel> partsl = _workPartsMapper.Map<IEnumerable<VUMM_HH_WORK_PARTS>, IEnumerable<PartViewModel>>(res);
            
            return partsl;
        }

     
        public IEnumerable<PartViewModel> FindPart(string strm, string inputcode)
        {
            if (string.IsNullOrEmpty(inputcode))
            {
                inputcode = strm;
            }
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PRMY_STRM_CODE.Equals(strm));
            if (res.Count() == 0)
            {
                res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PRMY_STRM_CODE.Equals(strm));
            }
            IEnumerable<PartViewModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res);

            return partsl;
        }

        //public Task<T> FindPartAsync<T>(string strm, string inputcode)
        //{

        //    return Task.Run(() => this.FindPart<T>(strm, inputcode));
        //}


        [HttpPost]
        public JsonResult FindPartJson(long doc_nbr, string input)
        {
            var strm = GetParentStrmCode(doc_nbr);
            var part_list = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm);
            var part = part_list.Where(p => p.PART_CODE_NAME.Contains(input));   //TODO CHECK CASE SENSETIVE  // StringComparison.CurrentCultureIgnoreCase));
            return Json(part);
        }

        public JsonResult GetFilterdParts(long doc_nbr, string input="צינור")
        {
            var strm = GetParentStrmCode(doc_nbr);
            var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm);
            IEnumerable<PartViewModel> parts = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res);
            parts = parts.Where(x => x.PART_CODE_NAME.Contains(input));
            return Json(parts, JsonRequestBehavior.AllowGet);
        }

        ////[NonAction]
        //public PartViewModel[] GetPartsByStrm(string strm)
        //{
        //    var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm);
        //    PartViewModel[] partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res).ToArray<PartViewModel>();
        //    return partsl;
        //}

        #endregion LineParts
        //public ActionResult GetParts(string strm, string inputcode)
        //{
        //    //string strm = GetParentStrmCode(doc_nbr);
        //    //string strm = "70" ;
        //    //string inputcode = "70";
        //     IEnumerable < VUMM_HH_PARTS > res;
        //    //var res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PART_CODE.Equals(inputcode) && w.PRMY_STRM_CODE.Equals(strm));
        //    if (!string.IsNullOrEmpty(inputcode))
        //    {
        //         res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PRMY_STRM_CODE.Equals(strm));
        //    }
        //    else
        //    {
        //        inputcode = strm;
        //        res = _conntext.VUMM_HH_PARTS.ToList().Where<VUMM_HH_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PRMY_STRM_CODE.Equals(strm));
        //    }
        //        //PartViewModel parts = _partsMapper.Map<PartViewModel>(res);
        //    IEnumerable<PartViewModel> parts = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res);
        //    return PartialView("SelectPartView",parts);
        //}



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

        //[HttpPost]
        //public ActionResult PartsView(string strm, string partStr)
        //{
        //    //var strm = GetParentStrmCode(doc_nbr);
        //    var p = GetPartsByPartCode(strm, partStr);
        //    if (p == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (p.Count<PartViewModel>() == 1)
        //    {
        //        return PartialView(p);
        //    }
        //    else
        //    {
        //        return PartialView(p);
        //    }
        //}

        //// GET: LineParts/Create
        //public ActionResult Create(long id, PartViewModel part = null)
        //{
        //    var call = _conntext.VUMM_HH_OPEN_CALLS.Find(id);
        //    this._parent_strm_code = call.PARENT_STRM_CODE;
        //    //if(!string.IsNullOrEmpty(part))
        //    //{
        //    //    var pcn = getPartNameById(part);
        //    //}
        //    LineViewModel vm = new LineViewModel
        //    {
        //        //PartsJ = GetPartsByStrm(this._parent_strm_code),
        //        //Parts = GetPartsSelectList(this._parent_strm_code),
        //        doc_nbr = id,
        //        part_code = part.PART_CODE,
        //        part_code_name = part.PART_CODE_NAME
        //    };
        //    return View(vm);
        //}


        // GET: LineParts/Create
        public ActionResult Create(long id, string part = "")
        {
            var call = _conntext.VUMM_HH_OPEN_CALLS.Find(id);
            this._parent_strm_code = call.PARENT_STRM_CODE;
            LineViewModel vm = new LineViewModel
            {
                doc_nbr = id,
                part_code_name = part,
                qnty = 1
            };
            return View(vm);
        }

        // POST: LineParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "doc_nbr, part_code_name, qnty, txt_dscr, line_nbr")] LineViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                var strm = GetParentStrmCode(iVm.doc_nbr);
                var input_part = iVm.part_code;
                var partsList = FindPart(strm, iVm.part_code_name);
                if (string.IsNullOrEmpty(iVm.part_code_name))
                {
                    ModelState.AddModelError("part_code_name", "חובה להזין קלט בשדה חיפוש");
                }
                if (partsList == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (partsList.Count<PartViewModel>() == 1)
                {
                    if (iVm.qnty== 0)
                    {
                        ModelState.AddModelError("qnty", "חובה להזין כמות גדולה מאפס");
                        return View(iVm);
                    }
                    iVm.part_code = partsList.First().PART_CODE;
                    await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_insert_lft", iVm.doc_nbr, iVm.part_code, iVm.qnty, iVm.txt_dscr, iVm.line_nbr);
                    return RedirectToAction("Index", new { id = iVm.doc_nbr });
                }
                if(partsList.Count() >1 )
                { 
                    partsList.First().doc_nbr = iVm.doc_nbr;
                    return PartialView("PartsSelect", partsList);
                }
                else
                {
                    ModelState.AddModelError("part_code_name", "לא נמצא פריט מתאים");
                    return View(iVm);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         }

        public ActionResult AddWork(long id, string part = "")
        {
            var call = _conntext.VUMM_HH_OPEN_CALLS.Find(id);
            this._parent_strm_code = call.PARENT_STRM_CODE;
            LineViewModel vm = new LineViewModel
            {
                doc_nbr = id,
                part_code_name = part,
                qnty =1
            };
            return View(vm);
        }

        // POST: LineParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddWork([Bind(Include = "doc_nbr, part_code_name, qnty, txt_dscr, line_nbr")] LineViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                var strm = GetParentStrmCode(iVm.doc_nbr);
                var input_part = iVm.part_code;
                var partsList = FindWorkPart(strm, iVm.part_code_name);
                if (string.IsNullOrEmpty(iVm.part_code_name))
                {
                    ModelState.AddModelError("part_code_name", "חובה להזין קלט בשדה חיפוש");
                }
                if (partsList == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (partsList.Count<PartViewModel>() == 1)
                {
                    if (iVm.qnty == 0)
                    {
                        ModelState.AddModelError("qnty", "חובה להזין כמות גדולה מאפס");
                        return View(iVm);
                    }
                    iVm.part_code = partsList.First().PART_CODE;
                    await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_insert_lft", iVm.doc_nbr, iVm.part_code, iVm.qnty, iVm.txt_dscr, iVm.line_nbr);
                    return RedirectToAction("Index", new { id = iVm.doc_nbr });
                }
                if (partsList.Count() > 1)
                {
                    //return RedirectToAction("PartsSelect", new { id = iVm.doc_nbr, strm = strm, inputcode = input_part });
                    partsList.First().doc_nbr = iVm.doc_nbr;
                    return PartialView("PartsSelect", partsList);
                }
                else
                {
                    ModelState.AddModelError("part_code_name", "לא נמצא פריט מתאים");
                    return View(iVm);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
        public async Task<ActionResult> Edit([Bind(Include = " doc_nbr, line_nbr, part_code_name, part_code_name, qnty, txt_dscr")] LineViewModel iVm )
        {
            if (ModelState.IsValid)
            {
                //VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.SingleOrDefaultAsync(m => m.PART_CODE_NAME == iVm.part_code_name);
                //dto.QNTY = iVm.QNTY;
                //dto.TXT_DSCR = iVm.rmrk;

                if (iVm.qnty == 0)
                {
                    ModelState.AddModelError("qnty", "חובה להזין כמות גדולה מאפס");
                    return View(iVm);
                }

                await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_update_lft", iVm.doc_nbr, iVm.line_nbr, iVm.qnty, iVm.txt_dscr);
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
            VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.SingleOrDefaultAsync(m => m.LINE_ID == id);
            //FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            else
            {
                await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_delete_lft", dto.DOC_NBR, dto.LINE_NBR);
                return RedirectToAction("Index", new { id = dto.DOC_NBR });
            }
            //LineViewModel vm = new LineViewModel();
            //vm = _linesMapper.Map<LineViewModel>(dto);
            //return View(vm);
        }

        //// POST: LineParts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(decimal? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.SingleOrDefaultAsync(m => m.LINE_ID == id);
        //    if (dto != null)
        //    {
        //         _conntext.ExecuteStoreProcedure("mm_hh.mm_hh_delete_lft", dto.DOC_NBR, dto.LINE_NBR);
        //        return RedirectToAction("Index", new { id = dto.DOC_NBR });
        //    }
        //    else
        //    {
        //        LineViewModel vm = new LineViewModel();
        //        vm = _linesMapper.Map<LineViewModel>(dto);
        //        return View(vm);
        //    }
        //}

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
