function Page_Reload() { window.location.reload(); }

function Page_Redirect(url) {
    window.top.location.replace(url);
}

function CopyToClipboard(elementId, sender) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(elementId).html()).select();
    document.execCommand("copy");
    $temp.remove();

    if (sender) {
        sender.textContent = 'Copied';
    }
    else {
        alert('Copied to Clipboard');
    }
}