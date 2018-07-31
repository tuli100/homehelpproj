$(document).ready(function () {
    //$('#partsList1').attr('data-live-search', true);

    $('.selectPart').selectpicker({
        //style: 'btn-info',
        liveSearch: true,
        size: 10,
        //liveSearchNormalize: true,
        //maxOptions: 10,
        mobile: true,
        title :'בחר פריט'
    });
});