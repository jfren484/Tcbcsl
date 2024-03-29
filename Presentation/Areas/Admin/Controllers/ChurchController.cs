﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using Microsoft.AspNet.Identity;
using MoreLinq;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Church")]
    public class ChurchController : AdminControllerBase
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
            var data = DbContext.Churches
                                .ToList()
                                .Select(c =>
                                        {
                                            var model = Mapper.Map<ChurchEditModel>(c);
                                            model.UrlForEdit = Url.Action("Edit", new {id = model.ChurchId});

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
            return View("Edit", new ChurchEditModel
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
        public ActionResult Create(ChurchEditModel model)
        {
            var church = Mapper.Map<Church>(model);
            church.PhoneNumbers = Mapper.Map<List<ContactPhoneNumber>>(model.PhoneNumbers);

            DbContext.Churches.Add(church);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var church = DbContext.Churches.SingleOrDefault(c => c.ChurchId == id);
            if (church == null || !church.TeamYears
                                         .Where(ty => ty.Year == Consts.CurrentYear)
                                         .Any(ty => User.IsTeamIdValidForUser(ty.TeamId)))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ChurchEditModel>(church);
            model.Address.State.ItemSelectList = GetStatesSelectList(church.Address.StateId);

            var phoneTypes = GetPhoneTypes();
            model.PhoneNumbers.ForEach(pn => { pn.PhoneTypes = phoneTypes; });

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, ChurchEditModel model)
        {
            var church = DbContext.Churches.SingleOrDefault(c => c.ChurchId == id);
            if (church == null || !church.TeamYears
                                         .Where(ty => ty.Year == Consts.CurrentYear)
                                         .Any(ty => User.IsTeamIdValidForUser(ty.TeamId)))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, church);

            var changes = ChangeTracker.GetChangeSets(church.PhoneNumbers,         model.PhoneNumbers.Models,
                                                      e => e.ContactPhoneNumberId, m => m.ContactPhoneNumberId);

            foreach (var pair in changes.CommonItems)
            {
                Mapper.Map(pair.RightItem, pair.LeftItem);
            }

            // TODO: Add
            // TODO: Delete

            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion
    }
}