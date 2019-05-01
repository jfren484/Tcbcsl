//#region Column Definitions

var commonStatsColumns = [
    { 'title': 'PA',  'data': 'PlateAppearances' },
    { 'title': 'AB',  'data': 'AtBats' },
    { 'title': 'H',   'data': 'Hits' },
    { 'title': 'TB',  'data': 'TotalBases' },
    { 'title': 'AVG', 'data': 'BattingAverage',     'render': statstable_RenderPct },
    { 'title': 'OBP', 'data': 'OnBasePercentage',   'render': statstable_RenderPct },
    { 'title': 'SLG', 'data': 'SluggingPercentage', 'render': statstable_RenderPct },
    { 'title': 'OPS', 'data': 'OnBasePlusSlugging', 'render': statstable_RenderPct },
    { 'title': 'R',   'data': 'Runs' },
    { 'title': 'RBI', 'data': 'RunsBattedIn' },
    { 'title': '1B',  'data': 'Singles' },
    { 'title': '2B',  'data': 'Doubles' },
    { 'title': '3B',  'data': 'Triples' },
    { 'title': 'HR',  'data': 'HomeRuns' },
    { 'title': 'BB',  'data': 'Walks' },
    { 'title': 'SF',  'data': 'SacrificeFlies' },
    { 'title': 'O',   'data': 'Outs' },
    { 'title': 'FC',  'data': 'FieldersChoices' },
    { 'title': 'E',   'data': 'ReachedByErrors' },
    { 'title': 'SO',  'data': 'Strikeouts' }
];

var gameStatsColumns = [
    { 'title': 'Player', 'data': 'Player', 'render': statstable_RenderPlayerLink }
].concat(commonStatsColumns);

var leagueTeamStatsColumns = [
    { 'title': 'Team', 'data': 'Team', 'render': statstable_RenderTeamLink, 'orderSequence': ['asc', 'desc'] },
    { 'title': 'G', 'data': 'Games' }
].concat(commonStatsColumns);

var leagueIndividualStatsColumns = [
    { 'title': 'Player', 'data': 'Player', 'render': statstable_RenderPlayerLink, 'orderSequence': ['asc', 'desc'] }
].concat(leagueTeamStatsColumns);

var playerCommonStatsColumns = commonStatsColumns.concat([
    { 'title': 'AVG*', 'data': 'ToDateAverage', 'render': statstable_RenderPct }
]);

var playerCareerStatsColumns = [
    { 'title': 'Year', 'data': 'Year', 'render': statstable_RenderPlayerYearLink },
    { 'title': 'Team', 'data': 'Team', 'render': statstable_RenderTeamLink, 'orderSequence': ['asc', 'desc'] },
    { 'title': 'G',    'data': 'Games' }
].concat(playerCommonStatsColumns);

var playerSeasonStatsColumns = [
    { 'title': 'Date',     'data': 'GameDate', 'render': statstable_RenderGameDate },
    { 'title': 'Opponent', 'data': 'Opponent', 'render': statstable_RenderTeamLink }
].concat(playerCommonStatsColumns);

var teamStatsColumns = [
    { 'title': 'Player', 'data': 'Player', 'render': statstable_RenderPlayerLink, 'orderSequence': ['asc', 'desc'] },
    { 'title': 'G',      'data': 'Games' }
].concat(commonStatsColumns);

//#endregion

//#region Field-Rendering Functions

function statstable_RenderGameDate(data, type, row) {
    return type === 'display'
        ? '<a href="/Statistics/Game/' + row.GameId + '">' + moment(data).format('MMMM D') + '</a>'
        : data;
}

function statstable_RenderPct(data, type) {
    return type === 'display'
        ? Number(data).toFixed(3)
        : data;
}

function statstable_RenderPlayerLink(data, type, row) {
    return type === 'display'
        ? '<a href="/Statistics/Player/' + data.PlayerId + yearAsRouteParameter(row.Year) + '">' + data.DisplayName + '</a>'
        : type === 'sort'
            ? data.SortName
            : data.DisplayName;
}

function statstable_RenderPlayerYearLink(data, type, row) {
    return type === 'display'
        ? '<a href="/Statistics/Player/' + row.Player.PlayerId + yearAsRouteParameter(data) + '">' + data + '</a>'
        : data;
}

function statstable_RenderTeamLink(data, type, row) {
    return type === 'display'
        ? '<a href="/Statistics/Team/' + data.TeamId + yearAsRouteParameter(row.Year) + '">' + data.TeamName + '</a>'
        : data.TeamName;
}

//#endregion

//#region Table-Rendering Functions

var statsTable;

function statstable_RenderBase(options) {
    addDataTableHeaderCells(options.tableSelector, options.columns);

    statsTable = $(options.tableSelector)
        .DataTable({
            'ajax': {
                'url': options.dataUrl,
                'type': 'POST',
                'dataSrc': ''
            },
            'language': { 'emptyTable': 'No statistics available' },
            'info': options.paging,
            'ordering': options.sorting,
            'paging': options.paging,
            'pagingType': 'full_numbers',
            'pageLength': 25,
            'searching': true,
            'order': options.order,
            'columnDefs': [
                { 'orderSequence': ['desc', 'asc'], 'targets': '_all' }
            ],
            'columns': options.columns,
            'dom': '<"row"<"col-lg-12"tr>><"row"<"col-lg-5"i><"col-lg-2"l><"col-lg-5"p>>'
        });
}

function statstable_RenderGameStats(tableSelector, data) {
    statstable_RenderBase({
        'tableSelector': tableSelector,
        'dataUrl': '/Statistics/GameData/' + data,
        'columns': gameStatsColumns,
        'sorting': false,
        'paging': false
    });
}

function statstable_RenderLeagueIndividualStats(data) {
    var sortColumnIndex = findColumnIndex(leagueIndividualStatsColumns, 'AVG');

    statstable_RenderBase({
        'tableSelector': '#statsTable',
        'dataUrl': '/Statistics/LeagueData/Individual/' + data,
        'columns': leagueIndividualStatsColumns,
        'sorting': true,
        'paging': true,
        'order': [[sortColumnIndex, 'desc']]
    });
}

function statstable_RenderLeagueTeamStats(data) {
    var sortColumnIndex = findColumnIndex(leagueTeamStatsColumns, 'AVG');

    statstable_RenderBase({
        'tableSelector': '#statsTable',
        'dataUrl': '/Statistics/LeagueData/Team/' + data,
        'columns': leagueTeamStatsColumns,
        'sorting': true,
        'paging': false,
        'order': [[sortColumnIndex, 'desc']]
    });
}

function statstable_RenderPlayerCareerStats(data) {
    var sortColumnIndex = findColumnIndex(playerCareerStatsColumns, 'Year');

    statstable_RenderBase({
        'tableSelector': '#statsTable',
        'dataUrl': '/Statistics/PlayerData/' + data,
        'columns': playerCareerStatsColumns,
        'sorting': true,
        'paging': false,
        'order': [[sortColumnIndex, 'asc']]
    });
}

function statstable_RenderPlayerSeasonStats(data) {
    statstable_RenderBase({
        'tableSelector': '#statsTable',
        'dataUrl': '/Statistics/PlayerData/' + data,
        'columns': playerSeasonStatsColumns,
        'sorting': false,
        'paging': false
    });
}

function statstable_RenderTeamStats(data, sortColumn) {
    var sortColumnIndex = findColumnIndex(teamStatsColumns, sortColumn) || findColumnIndex(teamStatsColumns, 'AVG');

    statstable_RenderBase({
        'tableSelector': '#statsTable',
        'dataUrl': '/Statistics/TeamData/' + data,
        'columns': teamStatsColumns,
        'sorting': true,
        'paging': false,
        'order': [[sortColumnIndex, 'desc']]
    });
}

//#endregion
