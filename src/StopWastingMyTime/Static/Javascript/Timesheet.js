$(document).ready(function() {

    var jobData = [];
    $.get('/Jobs/List?term=', function(data) { jobData = $.parseJSON(data); });

    var url = window.location.toString();
    var qs = url.indexOf('?') > 0 ? url.substring(url.indexOf('?')) : '';
    
    $('#timesheet .add').live('click', function() {
        var line = $(this).parents('div:first');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');
        
        $.ajax({
            type: 'POST',
            url: '/Timesheets/AddLine' + qs,
            data: 'workPackage=' + escape(workPackage.val()) + '&hours=' + hours.val(),
            success: function(data) { $('#timesheet').html(data); $('a.add').siblings('.workPackage').autocomplete({ source: jobData, delay: 0 }).focus(); }
        });
        
        return false;
    });

    $('#timesheet .edit').live('click', function() {
        var line = $(this).parents('div:first');
        line.find('input').removeAttr('disabled');
        line.find('.cancel').show();
        line.find('.save').show();
        line.find('.delete').hide();
        line.find('input.hours').focus();
        $(this).hide();
        return false;
    });
    
    $('#timesheet .cancel').live('click', function() {
        var line = $(this).parents('div:first');
        line.find('input').attr('disabled', 'disabled');
        line.find('.edit').show();
        line.find('.delete').show();
        line.find('.save').hide();
        $(this).hide();
        return false;
    });
    
    $('#timesheet .save').live('click', function() {
        var line = $(this).parents('div:first');
        var timeBlockId = line.find('.timeBlockId');
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');
        
        $.ajax({
            type: 'POST',
            url: '/Timesheets/EditLine/' + timeBlockId.val() + qs,
            data: 'workPackage=' + escape(workPackage.val()) + '&hours=' + hours.val(),
            success: function(data) { $('#timesheet').html(data); }
        });

        return false;
    });
    
    $('#timesheet .delete').live('click', function() {
        var line = $(this).parents('div:first')
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var timeBlockId = line.find('.timeBlockId');

        if (!confirm('Delete ' + workPackage.val() + ' from ' + date.val() + '?'))
            return false;

        $.ajax({
            type: 'POST',
            url: '/Timesheets/RemoveLine/' + timeBlockId.val() + qs,
            success: function(data) { $('#timesheet').html(data); }
        });

        return false;
    });
    
    $('#timesheet .workPackage').live('click', function() {
        $(this).autocomplete({ source: jobData, delay: 0 });
    });
    
    $('#timesheet input[type=text]').live('keypress', function(e) {
        if ((e.keyCode || e.which) == 13)
        {
            $(this).siblings('.save').click();
            $(this).siblings('.add').click();
            return false;
        }
        if ((e.keyCode || e.which) == 27)
            $(this).siblings('.cancel').click();
    });
})
