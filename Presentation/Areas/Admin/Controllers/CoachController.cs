using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using MoreLinq;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "League Commissioner, Team Coach")]
    [RouteArea("Admin")]
    [RoutePrefix("Coach")]
    public class CoachController : AdminControllerBase
    {
        #region List

        [Authorize(Roles = "League Commissioner")]
        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [Authorize(Roles = "League Commissioner")]
        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.Coaches
                                .OrderByDescending(c => c.Created)
                                .ToList()
                                .Select(n =>
                                        {
                                            var model = Mapper.Map<CoachEditModel>(n);
                                            model.EditUrl = Url.Action("Edit", new {id = model.CoachId});

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Authorize(Roles = "League Commissioner")]
        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new CoachEditModel
                                {
                                    Address = new AddressEditModel
                                              {
                                                  State = new StateEditModel {States = GetStates()}
                                              },
                                    PhoneNumbers = new PhoneEditModelList
                                                   {
                                                       new PhoneEditModel {PhoneTypes = GetPhoneTypes()}
                                                   }
                                });
        }

        [Authorize(Roles = "League Commissioner")]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(CoachEditModel model)
        {
            var coach = Mapper.Map<Coach>(model);
            coach.PhoneNumbers = Mapper.Map<List<ContactPhoneNumber>>(model.PhoneNumbers);

            DbContext.Coaches.Add(coach);
            DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var coach = DbContext.Coaches.SingleOrDefault(c => c.CoachId == id);
            if (coach == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CoachEditModel>(coach);
            model.Address.State.States = GetStates();

            var phoneTypes = GetPhoneTypes();
            model.PhoneNumbers.ForEach(pn => { pn.PhoneTypes = phoneTypes; });

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, CoachEditModel model)
        {
            var coach = DbContext.Coaches.SingleOrDefault(c => c.CoachId == id);
            if (coach == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, coach);

            var changes = ChangeTracker.GetChangeSets(coach.PhoneNumbers, model.PhoneNumbers.Models, e => e.ContactPhoneNumberId, m => m.ContactPhoneNumberId);

            foreach (var pair in changes.CommonItems)
            {
                Mapper.Map(pair.RightItem, pair.LeftItem);
            }

            // TODO: Add
            // TODO: Delete

            DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        #endregion
    }
}