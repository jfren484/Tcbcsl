﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Data.Entities;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("News")]
    public class NewsController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.NewsItems
                                .OrderByDescending(n => n.Created)
                                .ToList()
                                .Select(n =>
                                        {
                                            var model = Mapper.Map<NewsEditModel>(n);
                                            model.EditUrl = Url.Action("Edit", new {id = model.NewsItemId});

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new NewsEditModel
                                {
                                    IsActive = true,
                                    StartDate = DateTime.Now,
                                    EndDate = DateTime.Today.AddDays(14),
                                    TeamModel = new NewsEditTeamModel {Teams = GetTeams(Consts.CurrentYear)}
                                });
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(NewsEditModel model)
        {
            var newsItem = Mapper.Map<NewsItem>(model);
            UpdateCreatedFields(newsItem);
            DbContext.NewsItems.Add(newsItem);
            DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var newsItem = DbContext.NewsItems.SingleOrDefault(n => n.NewsItemId == id);
            if (newsItem == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<NewsEditModel>(newsItem);
            model.TeamModel.Teams = GetTeams(newsItem.Created.Year);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, NewsEditModel model)
        {
            var newsItem = DbContext.NewsItems.SingleOrDefault(n => n.NewsItemId == id);
            if (newsItem == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, newsItem);
            UpdateModifiedFields(newsItem);
            DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        #endregion

        #region Helpers

        private List<NewsEditTeamListModel> GetTeams(int year)
        {
            return DbContext.TeamYears
                            .Where(ty => ty.Year == year && ty.DivisionYear.IsInLeague)
                            .Select(ty => new NewsEditTeamListModel {TeamId = ty.TeamId, TeamName = ty.FullName})
                            .ToList()
                            .Concat(new[] {new NewsEditTeamListModel {TeamName = Consts.LeagueNameForList}})
                            .OrderBy(t => t.TeamName)
                            .ToList();
        }

        #endregion
    }
}