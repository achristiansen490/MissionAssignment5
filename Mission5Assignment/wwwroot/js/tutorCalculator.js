$(function () {
    const rate = parseFloat($("#rate").val());

    $("#calcBtn").on("click", function () {
        const hoursRaw = $("#hours").val();
        const hours = parseFloat(hoursRaw);

        if (!hoursRaw || isNaN(hours) || hours <= 0) {
            $("#hoursError").show();
            $("#total").val("");
            return;
        }

        $("#hoursError").hide();

        const total = hours * rate;
        $("#total").val(`$${total.toFixed(2)}`);
    });
});
