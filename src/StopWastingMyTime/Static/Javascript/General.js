function UpdateTextSize(i)
{
    var bodyTag = $('body');
    var currentSize = parseInt(bodyTag.css('font-size').replace('px', ''));
    bodyTag.css('font-size', currentSize + i + 'px');
    SetCookie('TextSize', currentSize + i);
}

function SetTextSize(i)
{
    $('body').css('font-size', i + 'px');
    SetCookie('TextSize', i);
}

function ResetTextSize()
{
    $('body').css('font-size', '');
    RemoveCookie('TextSize')
}

function GetCookie(name)
{
    var result = null;
    var start = -1, end = -1;
    if (document.cookie.indexOf(name + '=') > -1)
        start = document.cookie.indexOf(name + '=') + name.length + 1;
    var end = document.cookie.indexOf(';', start) > -1 ? document.cookie.indexOf(';', start) : document.cookie.length;
    if (start > -1 && end > -1)
        result = unescape(document.cookie.substring(start, end));
    return result;
}

function SetCookie(name, value)
{
    var expiryDate = new Date();
    expiryDate.setDate(expiryDate.getDate() + 90);
    document.cookie = name + '=' + escape(value) + ';expires=' + expiryDate.toGMTString() + ';path=/';
}

function RemoveCookie(name)
{
    document.cookie = name + '=0;expires=Thu, 01-Jan-1970 00:00:01 GMT;path=/';
}

$(document).ready(function() {
    var textSize = GetCookie('TextSize');
    if (textSize != null)
        SetTextSize(textSize);
});
