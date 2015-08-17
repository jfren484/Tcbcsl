//#region Column Definitions

var commonStatsColumns = [
    { 'title': 'PA',  'data': 'PlateAppearances' },
    { 'title': 'AB',  'data': 'AtBats' },
    { 'title': 'H',   'data': 'Hits' },
    { 'title': 'TB',  'data': 'TotalBases' },
    { 'title': 'AVG', 'data': 'BattingAverage',     'render': statstable_RenderPct },
    { 'title': 'OBP', 'data': 'OnBasePercentage',   'render': statstable_RenderPct },
    { 'title': 'SLG', 'data': 'SluggingPercentage', 'render': statstable_RenderPct },
    { 'title': 'OBP', 'data': 'OnBasePlusSlugging', 'render': statstable_RenderPct },
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
    { 'title': 'Player', 'data': 'PlayerName', 'render': statstable_RenderPlayerLink }
].concat(commonStatsColumns);

var playerCareerStatsColumns = [
    { 'title': 'Year', 'data': 'Year', 'render': statstable_RenderPlayerLink },
    { 'title': 'G', 'data': 'Games' }
].concat(commonStatsColumns);

var playerSeasonStatsColumns = [
    { 'title': 'Date',     'data': 'GameDate',     'render': statstable_RenderGameDate },
    { 'title': 'Opponent', 'data': 'OpponentName', 'render': statstable_RenderTeamLink }
].concat(commonStatsColumns);

var teamStatsColumns = [
    { 'title': 'Player', 'data': 'PlayerName', 'render': statstable_RenderPlayerLink, 'orderSequence': ['asc', 'desc'] },
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
        ? '<a href="/Statistics/Player/' + row.PlayerId + (row.Year === consts.currentYear ? '' : '/' + (row.Year === 0 ? 'All' : row.Year)) + '">' + data + '</a>'
        : data;
}

function statstable_RenderTeamLink(data, type, row) {
    return type === 'display'
        ? '<a href="/Team/' + row.OpponentTeamId + (row.OpponentTeamYear === consts.currentYear ? '' : '/' + row.OpponentTeamYear) + '">' + data + '</a>'
        : data;
}

//#endregion

//#region TableRendering Functions

function statstable_RenderBase(options) {
    var headerRow = $(options.tableSelector + ' thead tr:last');
    for (var i = 0; i < options.columns.length; ++i) {
        headerRow.append('<th></th>');
    }

    $(options.tableSelector).dataTable({
        'ajax': {
            'url': options.dataUrl,
            'type': 'POST',
            'dataSrc': ''
        },
        'language': { 'emptyTable': 'No statistics available' },
        'info': options.paging,
        'ordering': options.sorting,
        'paging': options.paging,
        'searching': false,
        'order': options.order,
        'columnDefs': [
            { 'orderSequence': ['desc', 'asc'], 'targets': '_all' }
        ],
        'columns': options.columns
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

function statstable_RenderPlayerCareerStats(data) {
    var matches = playerCareerStatsColumns.filter(function (col) { return col.title === 'Year' });
    var sortColumnIndex = playerCareerStatsColumns.indexOf(matches[0]);

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
    var matches = teamStatsColumns.filter(function (col) { return col.title === sortColumn });
    if (matches.length === 0) {
        matches = teamStatsColumns.filter(function (col) { return col.title === 'AVG' });
    }
    var sortColumnIndex = teamStatsColumns.indexOf(matches[0]);

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
