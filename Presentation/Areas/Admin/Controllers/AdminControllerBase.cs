using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    public abstract class AdminControllerBase : Presentation.Controllers.ControllerBase
    {
        #region Helpers

        protected List<PhoneTypeModel> GetPhoneTypes()
        {
            return DbContext.PhoneNumberTypes
                            .Select(Mapper.Map<PhoneTypeModel>)
                            .OrderBy(t => t.PhoneNumberTypeId)
                            .ToList();
        }

        protected List<StateModel> GetStates()
        {
            return DbContext.States
                            .Select(Mapper.Map<StateModel>)
                            .OrderBy(s => s.Name)
                            .ToList();
        }

        protected List<NewsEditTeamListModel> GetTeams(int year)
        {
            return DbContext.TeamYears
                            .Where(ty => ty.Year == year && ty.DivisionYear.IsInLeague)
                            .Select(ty => new NewsEditTeamListModel { TeamId = ty.TeamId, TeamName = ty.FullName })
                            .ToList()
                            .Concat(new[] { new NewsEditTeamListModel { TeamName = Consts.LeagueNameForList } })
                            .FilterTeamsForUser(User, n => n.TeamId)
                            .OrderBy(t => t.TeamName)
                            .ToList();
        }

        #endregion
    }
}
