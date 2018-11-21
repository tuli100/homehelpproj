using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeHelpCallsWebSite.ViewModels;
using HomeHelpCallsWebSite.Models;
using AutoMapper;
using HomeHelpCallsWebSite.Infrastructure.Data;
using System.Security.Claims;
using System.IO;
using System.Drawing;
using System.Text;

namespace HomeHelpCallsWebSite.Controllers
{
    [Authorize]
    public class OpenCallsController : Controller
    {
        private ApplicationDbContext _conntext;
        private IMapper _callsMapper;
        private IMapper _hndlCallsMapper;
        private IMapper _strmsMapper;
        private IMapper _imageMapper;
        private readonly int firm = 25;
        private readonly int ptc = 322;

        public OpenCallsController()
        {
            _conntext = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_OPEN_CALLS, CallsViewModel>());
            _callsMapper = config.CreateMapper();
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_STRMS_USERS, StrmViewModel>());
            _strmsMapper = config2.CreateMapper();
            var config3 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_HNDL_CALLS, CallsViewModel>(MemberList.Source));
            _hndlCallsMapper = config3.CreateMapper();
            var config4 = new MapperConfiguration(cfg => cfg.CreateMap<VUMM_HH_HNDL_CALLS, CallsViewModel>(MemberList.Destination));
            _imageMapper = config4.CreateMapper();
        }

        //    // GET: OpenCalls
        //    public async Task<ActionResult> Index(String searchString)
        //    {
        //        var user = User.Identity.Name;

        //        var ctx = Request.GetOwinContext();
        //        var authenticationManager = ctx.Authentication;
        //        var tt = authenticationManager.User.Claims.SingleOrDefault(w => w.Type == ClaimTypes.Role);
        //        IEnumerable<VUMM_HH_OPEN_CALLS> dto;
        //        if (tt.Value.Contains("*"))
        //            {
        //                dto =  _conntext.VUMM_HH_OPEN_CALLS;
        //            }
        //            else
        //            {
        //                dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => tt.Value.Contains(w.STRM_CODE));
        //            }
        //            var vm = _callsMapper.Map<IEnumerable<VUMM_HH_OPEN_CALLS>, IEnumerable<CallsViewModel>>(dto);
        //            if (vm.Count() > 0)
        //            {
        //                foreach (var item in vm)
        //                {
        //                    if (item.RQSTD_SHIP_DATE.Value.ToShortTimeString() == "00:00")
        //                    {
        //                        item.RQSTD_SHIP_TIME = "";
        //                    }
        //                    else
        //                    {
        //                        item.RQSTD_SHIP_TIME = item.RQSTD_SHIP_DATE.Value.ToShortTimeString();
        //                    }
        //                }
        //            }

        //        return View(vm);
        //}


        public async Task<ActionResult> CallsTable(string searchString)
        {
            var user = User.Identity.Name;

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            var tt = authenticationManager.User.Claims.SingleOrDefault(w => w.Type == ClaimTypes.Role);
            string userStrms = tt.Value;
            IEnumerable<VUMM_HH_OPEN_CALLS> dto;
            if (searchString != null)
            {
                long n;
                if (long.TryParse(searchString, out n))
                {
                    if (userStrms.Contains("*"))
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => w.DOC_NBR.Equals(n));
                    }
                    else
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => userStrms.Contains(w.STRM_CODE) && w.DOC_NBR.Equals(n));
                    }
                }
                else
                {
                    if (userStrms.Contains("*"))
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => w.CALL_DSCR.Contains(searchString) || w.APT_NAME.Contains(searchString) || w.CALL_STAT_FULL.Contains(searchString));
                    }
                    else
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => userStrms.Contains(w.STRM_CODE) && (w.CALL_DSCR.Contains(searchString) || w.APT_NAME.Contains(searchString) || w.CALL_STAT_FULL.Contains(searchString)));
                    }
                }
            }
            else
            {
                if (userStrms.Contains("*"))
                {
                    dto = _conntext.VUMM_HH_OPEN_CALLS;
                }
                else
                {
                    dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => userStrms.Contains(w.STRM_CODE));
                }
            }
            var vm = _callsMapper.Map<IEnumerable<VUMM_HH_OPEN_CALLS>, IEnumerable<CallsViewModel>>(dto);
            if (vm.Count() > 0)
            {
                foreach (var item in vm)
                {
                    item.isOpen = true;
                    if (item.RQSTD_SHIP_DATE.Value.ToShortTimeString() == "00:00")
                    {
                        item.RQSTD_SHIP_TIME = "";
                    }
                    else
                    {
                        item.RQSTD_SHIP_TIME = item.RQSTD_SHIP_DATE.Value.ToShortTimeString();
                    }
                }
            }

            return PartialView(vm);
        }

        //public async Task<ActionResult> Index()
        //{
        //    IEnumerable<VUMM_HH_OPEN_CALLS> dto = _conntext.VUMM_HH_OPEN_CALLS;
        //    var vm = _callsMapper.Map<IEnumerable<VUMM_HH_OPEN_CALLS>, IEnumerable<CallsViewModel>>(dto);
        //    return PartialView(vm);

        //}

        //GET: OpenCalls
        public async Task<ActionResult> Index(string searchString) //string searchString)
        {
            var user = User.Identity.Name;
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            var tt = authenticationManager.User.Claims.SingleOrDefault(w => w.Type == ClaimTypes.Role);
            string userStrms = tt.Value;
            //GetOpenCalls(searchString, userStrms);
            IEnumerable<VUMM_HH_OPEN_CALLS> dto;
            if (searchString != null)
            {
                long n;
                if (long.TryParse(searchString, out n))
                {
                    if (userStrms.Contains("*"))
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => w.DOC_NBR.Equals(n));
                    }
                    else
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => userStrms.Contains(w.STRM_CODE) && w.DOC_NBR.Equals(n));
                    }
                }
                else
                {
                    if (userStrms.Contains("*"))
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => w.CALL_DSCR.Contains(searchString) || w.APT_NAME.Contains(searchString) || w.CALL_STAT_FULL.Contains(searchString));
                    }
                    else
                    {
                        dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => userStrms.Contains(w.STRM_CODE) && (w.CALL_DSCR.Contains(searchString) || w.APT_NAME.Contains(searchString) || w.CALL_STAT_FULL.Contains(searchString)));
                    }
                }
            }
            else
            {
                if (userStrms.Contains("*"))
                {
                    dto = _conntext.VUMM_HH_OPEN_CALLS;
                }
                else
                {
                    dto = _conntext.VUMM_HH_OPEN_CALLS.Where(w => userStrms.Contains(w.STRM_CODE));
                }
            }
            var vm = _callsMapper.Map<IEnumerable<VUMM_HH_OPEN_CALLS>, IEnumerable<CallsViewModel>>(dto);
            if (vm.Count() > 0)
            {
                foreach (var item in vm)
                {
                    item.isOpen = true;
                    if (item.RQSTD_SHIP_DATE.Value.ToShortTimeString() == "00:00")
                    {
                        item.RQSTD_SHIP_TIME = "";
                    }
                    else
                    {
                        item.RQSTD_SHIP_TIME = item.RQSTD_SHIP_DATE.Value.ToShortTimeString();
                    }
                }
            }
            return PartialView(vm);
        }

        // GET: OpenCalls/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUMM_HH_OPEN_CALLS dto = await _conntext.VUMM_HH_OPEN_CALLS.FindAsync(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            CallsViewModel vm = _callsMapper.Map<CallsViewModel>(dto);
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

        // GET: OpenCalls/Edit/5

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VUMM_HH_OPEN_CALLS callO = _conntext.VUMM_HH_OPEN_CALLS.Find(id);
            CallsViewModel vm;
            if (callO == null)
            {
               VUMM_HH_HNDL_CALLS callH = _conntext.VUMM_HH_HNDL_CALLS.Find(id);
                if(callH == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                  
                vm = _hndlCallsMapper.Map<CallsViewModel>(callH);
            }
            else
            {
                vm = _callsMapper.Map<CallsViewModel>(callO);
            }
           
            vm.StrmList = getStrmsList(vm.STRM_CODE);
              
            return View(vm);
        }

        [NonAction]
        public SelectList getStrmsList(string strm_code)
        {
            var res = _conntext.VUMM_HH_STRMS_USERS.Where(w => w.USER_NAME == User.Identity.Name && w.STRM_CODE != strm_code);
            var sl = new SelectList(res, "strm_code", "strm_name", strm_code);
            if (sl.Count() == 1)
            {
                sl.First().Selected = true;
            }
            return sl;
        }

        //POST: OpenCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "doc_nbr,line_nbr,STRM_CODE")] CallsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var strm = _conntext.ExecuteFunction<string>("mm_hh.mm_hh_get_strm", model.doc_nbr, model.line_nbr);
                if(strm == model.STRM_CODE)
                {
                    return RedirectToAction("Index");
                }
                await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_update_strm_lft", model.doc_nbr, model.line_nbr, model.STRM_CODE);
                await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_strmchng_wo_user" ,model.doc_nbr, model.line_nbr,strm ,model.STRM_CODE, User.Identity.Name);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: OpenCalls/Edit/5
        //public async Task<ActionResult> Close(long? id, int stat, string? rk)
        public async Task<ActionResult> Close([Bind(Include = "doc_nbr,stat_rmrk, status, line_nbr")] CallsViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                //VUMM_HH_OPEN_CALLS call = _conntext.VUMM_HH_OPEN_CALLS.Find(iVm.doc_nbr);
                //if (call == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                //}
                //else
                //{
                    await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_close", iVm.doc_nbr, iVm.line_nbr, 300, User.Identity.Name, iVm.stat_rmrk);
                    return RedirectToAction("Index");
                //}
            }
            return View(iVm);
        }

        [NonAction]
        public SelectList createStatusList(int stat)
        {
            var res = _conntext.VUMM_HH_STATUS_LIST.Where(w => w.STEP_CODE != stat);

            var sl = new SelectList(res, "step_code", "step_dscr");
            return sl;
        }

        // GET: OpenCalls/ChangeStatus/5
        public async Task<ActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallsViewModel vm;
            VUMM_HH_OPEN_CALLS call = _conntext.VUMM_HH_OPEN_CALLS.Find(id);
            if (call == null)
            {

                    VUMM_HH_HNDL_CALLS call2 = _conntext.VUMM_HH_HNDL_CALLS.Find(id);
                    if (call2 == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        vm = _hndlCallsMapper.Map<CallsViewModel>(call2);
                    }
            }
            else
            {
                vm = _callsMapper.Map<CallsViewModel>(call);
            }
            vm.StatusList = createStatusList(vm.CALL_STAT_CODE);

            return View(vm);
        }

        // GET: OpenCalls/ChangeStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeStatus([Bind(Include = "doc_nbr,stat_rmrk, CALL_STAT_CODE, line_nbr")] CallsViewModel iVm)
        {
            if (ModelState.IsValid)
            {
                if (iVm.CALL_STAT_CODE < 301 || iVm.CALL_STAT_CODE >340)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    await _conntext.ExecuteStoreProcedureAsync("mm_hh.mm_hh_close", iVm.doc_nbr, iVm.line_nbr, iVm.CALL_STAT_CODE, User.Identity.Name, iVm.stat_rmrk);
                    return RedirectToAction("Index");
                }
            }
            return View(iVm);
        }

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

        public ActionResult Images(long? id)
        {
            if (id != null)
            {
                IEnumerable<MM_DOCS_FILES> dto = _conntext.MM_DOCS_FILE.Where(w => w.DOC_NBR == id && w.FIRM_CODE == firm && w.PRMY_TRNS_CLSS == ptc);
                IEnumerable<ImagesViewModelcs> vm = _imageMapper.Map<IEnumerable<MM_DOCS_FILES>, IEnumerable<ImagesViewModelcs>>(dto);
                foreach(var item in vm)
                {
                    string base64 = Convert.ToBase64String(item.BIN_FILE_DATA);
                    item.MIME_TYPE = string.Format("data:{0};base64,{1}", item.MIME_TYPE, base64);
                }
                return View(vm);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //public MvcHtmlString Image(this HtmlHelper html, byte[] image, string mime_type)
        //{
        //    var img = String.Format("data:{0};base64,{1}",mime_type ,Convert.ToBase64String(image));
        //    return new MvcHtmlString("<img src='" + img + "' />");
        //}

        //public FileContentResult Image(int electedOfficialId)
        //{
        //    byte[] picture = GetPicture(electedOfficialId);
        //    return new FileContentResult(picture, "image/jpeg");
        //}

        //public FileContentResult getImg(byte[] data , string mime_type)
        //{
        //    byte[] byteArray = data;
        //    return byteArray != null ? new FileContentResult(byteArray, mime_type) : null;
        //}

        public ActionResult GetImage(byte[] data, string mime_type)
        {

            //string imageBase64Data = Convert.ToBase64String(data);
            //string imageDataURL = string.Format("data:{0};base64,{1}",mime_type, imageBase64Data);
            //ViewBag.ImageData = imageDataURL;
            //return View();

            //string base64 = Convert.ToBase64String(data);
            ////base64 = "data:" + mime_type + ";base64," + base64;
            //var ht = "<html><body><img src=data:" + mime_type + ";base64," + base64 + "/></body></html>";
            ////var data64 = Convert.ToByte[](base64);
            //////MemoryStream ms = new MemoryStream(data, 0, data.Length);
            //////ms.Write(data, 0, data.Length);
            //////returnImage = Image.FromStream(ms, true);    //Exception occurs here
            ////base64 = base64.Replace('-', '+').Replace('_', '/').PadRight(4 * ((base64.Length + 3) / 4), '=');
            ////var d = ASCIIEncoding.ASCII.GetBytes(base64);
            ////return Convert.FromBase64String(data);
            using (var ms = new MemoryStream(data))
            {
                return File(data, mime_type); // mime_type);
            }

            //return Content(ht, "text/html");
        }

        //public static System.Drawing.Image ByteArrayToImage(byte[] bArray)
        //{
        //    if (bArray == null)
        //        return null;

        //    System.Drawing.Image newImage;

        //    try
        //    {
        //        using (MemoryStream ms = new MemoryStream(bArray, 0, bArray.Length))
        //        {
        //            ms.Write(bArray, 0, bArray.Length);
        //            newImage = System.Drawing.Image.FromStream(ms, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        newImage = null;

        //        //Log an error here
        //    }

        //    return newImage;
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
