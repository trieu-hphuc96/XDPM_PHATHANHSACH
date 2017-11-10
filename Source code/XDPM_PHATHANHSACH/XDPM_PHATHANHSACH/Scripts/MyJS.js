
//dynamicId for text box's id

//event handler for Add button
//$('#btnAdddd').click(function () {
//    dynamicId += 1
//    $('#chitiet').append(
//        "<tr>" +
//        "<td>" + dynamicId + "</td>" +
//        "<td>" + $('#item').val() + "</td> " +
//        "<td>" + $("#item option:selected").text() + "</td>" +
//        "<td>" + $('#amount').val() + "</td>" +
//        '<td><button class="btn btn-danger btnDelete">Delete</button></td>' +
//        '<input type="text" name="ctpn.[' + (dynamicId - 1) + '].MAPN" value="48" />'+
//        '<input type="text" name="ctpn.[' + (dynamicId - 1) + '].MASACH" value="' + $('#item').val() + '" />' +
//        '<input type="text" name="ctpn.[' + (dynamicId - 1) + '].SOLUONG" value="' + $('#amount').val() + '" />' +
//        "</tr>"
//    )
//});


function abc() {
    var x = document.getElementById('chitiet').insertRow(1);
    var c1 = x.insertCell(0);
    var c2 = x.insertCell(1);
    var c3 = x.insertCell(2);
    var c4 = x.insertCell(3);
    var c5 = x.insertCell(4);
    var c6 = x.insertCell(5);
    c1.innerHTML = dynamicID;
    c2.innerHTML = $('#item').val();
    c3.innerHTML = '<button class="btn btn-danger btnDelete">Delete</button>';

    var container = document.getElementById("concac");
    // Create an <input> element, set its type and name attributes
    var input = document.createElement("input");
    input.type = "text";
    input.name = '"[' + (dynamicID - 1) + '].MAPN"';
    input.value = "48";
    container.appendChild(input);

    var input1 = document.createElement("input");
    input1.type = "text";
    input1.name = '"[' + (dynamicID - 1) + '].MASACH"';
    input1.value = $('#item').val() + " " + (dynamicID - 1) + " " + '"[' + (dynamicID - 1) + '].MASACH"';
    container.appendChild(input1);

    var input2 = document.createElement("input");
    input2.type = "text";
    input2.name = '"[' + (dynamicID - 1) + '].SOLUONG"';
    input2.value = $('#amount').val();
    container.appendChild(input2);
}

//$("#chitiet").on('click', '.btn.btn-danger.btnDelete', function () {
//    $(this).closest('tr').find("input[type='hidden']").remove();
//    $(this).closest('tr').remove();
//});

$(function () { // will trigger when the document is ready
    $('.datepicker').datepicker({ dateFormat: 'dd-mm-yy' }); //Initialise any date pickers
});

//$('#throw').keyup(function () {
//    $('#catch').val($('#throw').val());
//});
//jquery(window).load(function () {
//    $('#catch').val($('#throw').val());
//});


