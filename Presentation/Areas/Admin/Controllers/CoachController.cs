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
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Coach")]
    public class CoachController : AdminControllerBase
    {
        #region List

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.Coaches
                                .ToList()
                                .Select(c =>
                                        {
                                            var model = Mapper.Map<CoachEditModel>(c);
                                            model.EditUrl = Url.Action("Edit", new {id = model.CoachId});

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new CoachEditModel
                                {
                                    Address = new AddressEditModel
                                              {
                                                  State = new StateEditModel {ItemSelectList = GetStatesSelectList(null)}
                                              },
                                    PhoneNumbers = new PhoneEditModelList
                                                   {
                                                       new PhoneEditModel {PhoneTypes = GetPhoneTypes()}
                                                   }
                                });
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
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
            if (coach == null || !coach.TeamYears
                                       .Where(ty => ty.Year == Consts.CurrentYear)
                                       .Any(ty => User.IsTeamIdValidForUser(ty.TeamId)))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CoachEditModel>(coach);
            model.Address.State.ItemSelectList = GetStatesSelectList(coach.Address.StateId);

            var phoneTypes = GetPhoneTypes();
            model.PhoneNumbers.ForEach(pn => { pn.PhoneTypes = phoneTypes; });

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, CoachEditModel model)
        {
            var coach = DbContext.Coaches.SingleOrDefault(c => c.CoachId == id);
            if (coach == null || !coach.TeamYears
                                       .Where(ty => ty.Year == Consts.CurrentYear)
                                       .Any(ty => User.IsTeamIdValidForUser(ty.TeamId)))
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