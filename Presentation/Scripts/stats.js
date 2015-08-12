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
    { 'title': 'Player', 'data': 'PlayerName' }
].concat(commonStatsColumns);
var gameStatsColumnCount = gameStatsColumns.length;

function statstable_RenderPct(data, type) {
    return type === 'display'
        ? Number(data).toFixed(3)
        : data;
}

function statstable_RenderGameStats(tableSelector, data) {
    var headerRow = $(tableSelector + ' thead tr:last');
    for (var i = 0; i < gameStatsColumnCount; ++i) {
        headerRow.append('<th></th>');
    }

    $(tableSelector).dataTable({
        'ajax': {
            'url': '/Statistics/GameData/' + data,
            'type': 'POST',
            'dataSrc': ''
        },
        'language': {
            'emptyTable': 'No statistics available'
        },
        'info': false,
        'ordering': false,
        'paging': false,
        'searching': false,
        'columns': gameStatsColumns
    });
}