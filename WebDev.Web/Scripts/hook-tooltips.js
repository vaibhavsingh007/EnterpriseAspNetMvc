$(function () {
    // Hook tooltipster
    $('.tooltip').tooltipster({
        contentAsHTML: true,
        content: 'Loading...',
        functionBefore: function (origin, continueTooltip) {
            var addressUrl = $('#AddressAction').val();
            addressUrl += '/' + origin[0].id;

            // we'll make this function asynchronous and allow the tooltip to go ahead 
            //..and show the loading notification while fetching our data
            continueTooltip();

            // next, we want to check if our data has already been cached
            if (origin.data('ajax') !== 'cached') {
                $.ajax({
                    type: 'POST',
                    url: addressUrl,
                    success: function (data) {
                        // update our tooltip content with our returned data and cache it
                        origin.tooltipster('content', CreateAddressTooltip(data)).data('ajax', 'cached');
                    }
                });
            }
        }
    });

    // Create the tooltip HTML
    function CreateAddressTooltip(data) {
        var tooltipHtml = '<ul>';
        var addresses = data;

        for (var i = 0; i < addresses.length; i++) {
            tooltipHtml += '<li><strong>Address ' + (i+1) + '</strong></li>';
            tooltipHtml += '<li>Street: ' + addresses[i].Street + '</li>';
            tooltipHtml += '<li>City: ' + addresses[i].City + '</li>';
            tooltipHtml += '<li>State: ' + addresses[i].State + '</li>';
            tooltipHtml += '<li>...</li>';
        }
        tooltipHtml += '</ul>';
        return tooltipHtml;
    }

});