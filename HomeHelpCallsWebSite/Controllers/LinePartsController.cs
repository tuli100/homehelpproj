using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using HomeHelpCallsWebSite.Models;
using HomeHelpCallsWebSite.Infrastructure.Data;
using System.Linq.Dynamic.Core;
using System;
using System.Web;
using System.Security.Claims;

namespace HomeHelpCallsWebSite.Controllers
{
    [Authorize]
    public class LinePartsController : Controller
    {
        private ApplicationDbContext _conntext;
        private IMapper _linesMapper;
        private IMapper _partsMapper;
        private IMapper _workPartsMapper;
        private IMapper _callMapper;
        private string _parent_strm_code;

        public LinePartsController()
        {
            _conntext = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_CALLS_LINES, LineViewModel>());
            _linesMapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VUMM_HH_PARTS, PartViewModel>(MemberList.Source);
            });
            _partsMapper = config2.CreateMapper();
            var config3 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_WORK_PARTS, PartViewModel>());
            _workPartsMapper = config3.CreateMapper();
        }

         // GET: LineParts
        public async Task<ActionResult> Index(long id, bool isOpen = true )
        {
            var res = _conntext.VUMM_HH_CALLS_LINES.Where<VUMM_HH_CALLS_LINES>(w => w.DOC_NBR == id);
            var vm = _linesMapper.Map<IEnumerable<LineViewModel>>(res);
            string strm;
            try
            {
                strm = _conntext.VUMM_HH_OPEN_CALLS.Find(id).STRM_CODE;
            }
            catch
            {
                strm = _conntext.VUMM_HH_HNDL_CALLS.Find(id).STRM_CODE;
            }
            SelectList partsList = FindWorkPart(strm);
            vm.First().WParts = partsList;
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

        #region LineParts

        public ActionResult PartsSelect(long id, string strm, string inputcode)
        {
            var parts = FindPart(strm, inputcode);
            parts.First().doc_nbr = id;
            return View(parts);
        }

        public SelectList FindWorkPart( string strm) //, string inputcode)
        {
           
            PartViewModel dflt_part;
            SelectList sl;
            IEnumerable<PartViewModel> partsl = null;
            var res = _conntext.VUMM_HH_WORK_PARTS.Where(w => w.RSRV_STRM_CODE.Equals(strm));
            partsl = _workPartsMapper.Map<IEnumerable<VUMM_HH_WORK_PARTS>, IEnumerable<PartViewModel>>(res);
            if (partsl.Count() == 1)
            {
                dflt_part = partsl.First();
            }
            else
            {
                try
                {
                    dflt_part = partsl.Where(w => w.dflt_part == 1).First();
                }
                catch
                {
                    sl = new SelectList(partsl, "part_code", "part_long_name");
                    return sl;
                }
            }
            sl = new SelectList(partsl, "part_code", "part_long_name", dflt_part.PART_CODE);
            return sl;
        }

        public IEnumerable<PartViewModel> FindPartSrch([Bind] IEnumerable<PartViewModel> iVm, string searchString)
        {
            if (1 == 1)
            {
                var t = 9;
            }
            return iVm.Where(w => w.PART_CODE_NAME.Contains(searchString));
        }

        public IEnumerable<PartViewModel> FindPart(string strm, string inputcode)
        {
            if (string.IsNullOrEmpty(inputcode))
            {
                inputcode = strm;
            }

            var res = _conntext.VUMM_HH_PARTS.Where<VUMM_HH_PARTS>(w => w.PART_CODE_NAME.Contains(inputcode) && w.PRMY_STRM_CODE.Equals(strm));
            IEnumerable<PartViewModel> partsl = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res);

            return partsl;
        }

        //public JsonResult FindPartJson(long id)
        //{
        //    var strm = GetParentStrmCode(id);
        //    var part_list = _conntext.VUMM_HH_PARTS.Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm && w.PART_CODE_NAME.Contains("צינור")).Select(w => w.PART_CODE_NAME).ToList();
        //    return Json(part_list, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult FindPartJson(long id, string term)
        {
            var strm = GetParentStrmCode(id);
            //var strm = "70";
            var part_list = _conntext.VUMM_HH_PARTS.Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm && w.PART_CODE_NAME.Contains(term)).Take(20).Select(w => w.PART_CODE_NAME).ToList();
            return Json(part_list, JsonRequestBehavior.AllowGet);
        }

        public List<string> GetFilterdParts(long id, string input="צינור")
        {
            var strm = GetParentStrmCode(id);
            var res = _conntext.VUMM_HH_PARTS.Where<VUMM_HH_PARTS>(w => w.PRMY_STRM_CODE == strm && w.PART_CODE_NAME.Contains(input)).Select(e =>e.PART_CODE_NAME).ToList();
            //IEnumerable<PartViewModel> parts = _partsMapper.Map<IEnumerable<VUMM_HH_PARTS>, IEnumerable<PartViewModel>>(res);
            //parts = parts.Where(x => x.PART_CODE_NAME.Contains(input)).ToList();
            return res;
        }

        #endregion LineParts
        private string GetParentStrmCode( long doc_nbr)
        {
            //return "70";
            string strm;
            try
            {
                strm = _conntext.VUMM_HH_OPEN_CALLS.Find(doc_nbr).PARENT_STRM_CODE;
            }
            catch
            {
                try
                {
                    strm = _conntext.VUMM_HH_HNDL_CALLS.Find(doc_nbr).PARENT_STRM_CODE;
                }
                catch
                {
                    return "70";
                }
            }
            if (strm == " ." || strm == "613")  return "70";
            return strm;
        }

        private string GetParentStrmCodeForWork(long doc_nbr)
        {
            string strm;
            try
            {
                strm = _conntext.VUMM_HH_OPEN_CALLS.Find(doc_nbr).PARENT_STRM_CODE;
            }
            catch
            {
                try
                {
                    strm = _conntext.VUMM_HH_HNDL_CALLS.Find(doc_nbr).PARENT_STRM_CODE;
                }
                catch
                {
                    return "70";
                }
            }
            if (strm == " ." ) return "70";
            return strm;
        }
        // GET: LineParts/Create
        public ActionResult Create(long id, string part = "")
        {
            //string strm;
            //try
            //{
            //    strm = _conntext.VUMM_HH_OPEN_CALLS.Find(id).STRM_CODE;
            //}
            //catch
            //{
            //    strm = _conntext.VUMM_HH_HNDL_CALLS.Find(id).STRM_CODE;
            //}
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
        public async Task<ActionResult> Create([Bind(Include = "doc_nbr, part_code_name, qnty, txt_dscr, line_nbr, call")] LineViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                var strm = GetParentStrmCode(iVm.doc_nbr);
                //var input_part = iVm.part_code;
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
                    //return PartialView("testView", iVm.doc_nbr);
                    PartialView("PartsTableView", partsList);
                }
                else
                {
                    ModelState.AddModelError("part_code_name", "לא נמצא פריט מתאים");
                }
            }
            return View(iVm);
            
         }

        public ActionResult AddWork(long id, string part = "")
        {
            string strm;
            try
            {
                strm = _conntext.VUMM_HH_OPEN_CALLS.Find(id).STRM_CODE;
            }
            catch
            {
                strm = _conntext.VUMM_HH_HNDL_CALLS.Find(id).STRM_CODE;
            }
            SelectList partsList = FindWorkPart(strm);
               
            LineViewModel vm = new LineViewModel
                {
                    doc_nbr = id,
                    part_code_name = part,
                    qnty = 1,
                    WParts = partsList,
                    //part_code = part_code
                };
                return View(vm);
        }

        // POST: LineParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddWork([Bind(Include = "doc_nbr, part_code, qnty, txt_dscr, line_nbr")] LineViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                //var strm = GetParentStrmCodeForWork(iVm.doc_nbr);
                //if(strm is null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                var input_part = iVm.part_code;
                if (string.IsNullOrEmpty(iVm.part_code_name))
                {
                    ModelState.AddModelError("part_code_name", "חובה להזין קלט בשדה חיפוש");
                }
              
                if (iVm.qnty == 0)
                {
                    ModelState.AddModelError("qnty", "חובה להזין כמות גדולה מאפס");
                    return View(iVm);
                }
                await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_insert_lft", iVm.doc_nbr, iVm.part_code, iVm.qnty, iVm.txt_dscr, iVm.line_nbr);
                return RedirectToAction("Index", new { id = iVm.doc_nbr });
                
            }
            else
            {
                string strm;
                try
                {
                    strm = _conntext.VUMM_HH_OPEN_CALLS.Find(iVm.doc_nbr).STRM_CODE;
                }
                catch
                {
                    strm = _conntext.VUMM_HH_HNDL_CALLS.Find(iVm.doc_nbr).STRM_CODE;
                }
                iVm.WParts = FindWorkPart(strm);
                return View(iVm);
            }
        }

        // GET: LineParts/Edit/5
        public async Task<ActionResult> Edit(decimal? id)
        {
            //LINE_ID is doc_nbr with line_nbr
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_CALLS_LINES dto =  await _conntext.VUMM_HH_CALLS_LINES.FindAsync(id);
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
                if (iVm.qnty == 0)
                {
                    ModelState.AddModelError("qnty", "חובה להזין כמות גדולה מאפס");
                    return View(iVm);
                }

                await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_update_qnty_lft", iVm.doc_nbr, iVm.line_nbr, iVm.qnty, iVm.txt_dscr);
                return RedirectToAction("Index", new { id = iVm.doc_nbr });
            }
            return View(iVm);
        }

       // GET: LineParts/Delete/5
        public async Task<ActionResult> Delete(decimal? id)
        {
            //LINE_ID is doc_nbr with line_nbr
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_CALLS_LINES dto = await _conntext.VUMM_HH_CALLS_LINES.FindAsync(id);
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

        public ActionResult addPartEmptyEditor(long id, string part = "")
        {
            LineViewModel model = new LineViewModel
            {
                doc_nbr = id,
                part_code_name = part,
                qnty = 1
            };
            return PartialView("addLine", model);
        }

        public ActionResult addWorkPartEmptyEditor(long id, string part = "")
        {
            string strm;
            try
            {
                strm = _conntext.VUMM_HH_OPEN_CALLS.Find(id).STRM_CODE;
            }
            catch
            {
                strm = _conntext.VUMM_HH_HNDL_CALLS.Find(id).STRM_CODE;
            }
            SelectList partsList = FindWorkPart(strm);

            LineViewModel vm = new LineViewModel
            {
                doc_nbr = id,
                part_code_name = part,
                qnty = 1,
                WParts = partsList,
                //part_code = part_code
            };
            return PartialView("addWorkLine", vm);
        }

        public LineViewModel addWorkPartEmptyModel(long id, string part = "")
        {
            string strm;
            try
            {
                strm = _conntext.VUMM_HH_OPEN_CALLS.Find(id).STRM_CODE;
            }
            catch
            {
                strm = _conntext.VUMM_HH_HNDL_CALLS.Find(id).STRM_CODE;
            }
            SelectList partsList = FindWorkPart(strm);

            LineViewModel vm = new LineViewModel
            {
                doc_nbr = id,
                part_code_name = part,
                qnty = 1,
                WParts = partsList,
                //part_code = part_code
            };
            return vm;
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
