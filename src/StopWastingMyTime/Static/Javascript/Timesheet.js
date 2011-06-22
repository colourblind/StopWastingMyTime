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
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');
        
        alert('date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val());
        
        $.ajax({
            type: 'POST',
            url: '/Timesheets/EditLine',
            data: 'date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val()
        });
    });
    
    $('#timesheet .delete').click(function() {
        var line = $(this).parents('div:first')
        var date = line.find('.date');
        var workPackage = line.find('.workPackage');
        var hours = line.find('.hours');

        $.ajax({
            type: 'POST',
            url: '/Timesheets/RemoveLine',
            data: 'date=' + date.val() + '&workPackage=' + workPackage.val() + '&hours=' + hours.val()
        });
    });
})
