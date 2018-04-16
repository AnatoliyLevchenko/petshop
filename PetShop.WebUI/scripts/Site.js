

    function qw() {
        var arr = document.querySelectorAll("#quantity");
        var arrSpan = document.querySelectorAll("#price");
        var total = 0;
        for (var i = 0, len = arr.length; i < len; i++) {
            total += parseInt(arr[i].value) * parseFloat(arrSpan[i].innerHTML);
        }
        document.getElementById('total').innerHTML = total;
    }