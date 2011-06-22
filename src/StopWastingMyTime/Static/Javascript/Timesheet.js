$(document).ready(function() {

    $('#timesheet .add').click(function() {
        var line = $(this).parents('div:first');
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');
        
        $.ajax({
            type: 'POST',
            url: '/Timesheets/AddLine',
            data: 'date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val()
        });
    });

    $('#timesheet .edit').click(function() {
        var line = $(this).parents('div:first');
        line.find('input').removeAttr('readonly');
        line.find('.cancel').show();
        line.find('.save').show();
        line.find('.delete').hide();
        $(this).hide();
    });
    
    $('#timesheet .cancel').click(function() {
        var line = $(this).parents('div:first');
        line.find('input').attr('readonly', 'readonly');
        line.find('.edit').show();
        line.find('.delete').show();
        line.find('.save').hide();
        $(this).hide();
    });
    
    $('#timesheet .save').click(function() {
        var line = $(this).parents('div:first');
        var timeBlockId = line.find('.timeBlockId');
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');
        
        $.ajax({
            type: 'POST',
            url: '/Timesheets/EditLine',
            data: 'timeBlockId=' + timeBlockId.val() + '&date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val()
        });
    });
    
    $('#timesheet .delete').click(function() {
        var line = $(this).parents('div:first')
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var timeBlockId = line.find('.timeBlockId');

        if (!confirm('Delete ' + workPackage.val() + ' from ' + date.val() + '?'))
            return;

        $.ajax({
            type: 'POST',
            url: '/Timesheets/RemoveLine',
            data: 'timeBlockId=' + timeBlockId.val()
        });
    });
})
