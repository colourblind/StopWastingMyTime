$(document).ready(function() {

    $('#timesheet .add').live('click', function() {
        var line = $(this).parents('div:first');
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');
        
        $.ajax({
            type: 'POST',
            url: '/Timesheets/AddLine',
            data: 'date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val(),
            success: function(data) { $('#timesheet').html(data); }
        });
        
        return false;
    });

    $('#timesheet .edit').live('click', function() {
        var line = $(this).parents('div:first');
        line.find('input').removeAttr('readonly');
        line.find('.cancel').show();
        line.find('.save').show();
        line.find('.delete').hide();
        $(this).hide();
        return false;
    });
    
    $('#timesheet .cancel').live('click', function() {
        var line = $(this).parents('div:first');
        line.find('input').attr('readonly', 'readonly');
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
            url: '/Timesheets/EditLine/' + timeBlockId.val(),
            data: 'date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val(),
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
            return;

        $.ajax({
            type: 'POST',
            url: '/Timesheets/RemoveLine/' + timeBlockId.val(),
            success: function(data) { $('#timesheet').html(data); }
        });

        return false;
    });
})
