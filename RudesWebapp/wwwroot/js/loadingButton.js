$(document).ready(function () {
    $('#loadingButton').on('click', function () {
        var $this = $(this);
        var loadingText = '<i class="fa fa-circle-o-notch fa-spin"></i> obrada plaćanja...';
        if ($(this).html() !== loadingText) {
            $this.data('original-text', $(this).html());
            $this.html(loadingText);
        }
        setTimeout(function () {
            $this.html($this.data('original-text'));
        }, 5000);
    });
})