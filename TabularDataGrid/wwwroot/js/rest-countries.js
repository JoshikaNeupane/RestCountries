$(document).ready(function () {

    loadTabulatorGrid();
});

function loadTabulatorGrid() {

    // Show loading spinner 
    showLoading("divStudentList");

    $.ajax({
        type: "get",
        url: app.baseUrl + "/Home/GetCountriesJson",
        data: {},
        success: function (result) {

            var flagImg = function (cell) { //plain text value
                console.log(cell, cell.getRow().getData().flag);
                return "<img src='" + cell.getRow().getData().flag + "' width='100' width='100' />";
            };

            var table = new Tabulator("#divStudentList", {
                data: result,           //load row data from array
                layout: "fitColumns",      //fit columns to width of table
                responsiveLayout: "hide",  //hide columns that dont fit on the table
                tooltips: true,            //show tool tips on cells
                addRowPos: "top",          //when adding a new row, add it to the top of the table
                history: true,             //allow undo and redo actions on the table
                pagination: "local",       //paginate the data
                paginationSize: 100,         //allow 100 rows per page of data
                movableColumns: true,      //allow column order to be changed
                resizableRows: true,       //allow row order to be changed
                initialSort: [             //set the initial sort order of the data
                    { column: "population", dir: "desc" },
                ],
                columns: [
                    {
                        title: "Countries List (https://restcountries.com/v3.1/all)",
                        headerMenu: getColumnChooserList(table),
                        columns: [
                            //define table columns
                            { title: "Flag", formatter: flagImg, width: 110, hozAlign: "center" },
                            { title: "Name", field: "name", width: 150, headerFilter: "input" },
                            { title: "Official Name", field: "officialName", width: 200, headerFilter: "input" },
                            { title: "Code", field: "countryCode", width: 75 },
                            { title: "Capital", field: "capital", width: 150, headerFilter: "input" },
                            { title: "Population", field: "population", width: 130 },
                            { title: "UN Member", field: "isUnMember", width: 150, hozAlign: "center", formatter: "tickCross", sorter: "boolean" },
                        ]
                    }
                ],
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#divStudentList').html("Error fetching data..");
            if (xhr.status == 404) {
                alert(thrownError);
            }
        }
    });
}

function getColumnChooserList() {

    //define column header menu as column visibility toggle
    var headerMenu = function () {
        var menu = [];
        var columns = this.getColumns();
        for (let column of columns) {

            //create checkbox element using font awesome icons
            let icon = document.createElement("i");
            icon.classList.add("fa");
            icon.classList.add(column.isVisible() ? "fa-check-square" : "fa-square");

            //build label
            let label = document.createElement("span");
            let title = document.createElement("span");

            title.textContent = " " + column.getDefinition().title;

            label.appendChild(icon);
            label.appendChild(title);

            //create menu item
            menu.push({
                label: label,
                action: function (e) {
                    //prevent menu closing
                    e.stopPropagation();

                    //toggle current column visibility
                    column.toggle();

                    //change menu item icon
                    if (column.isVisible()) {
                        icon.classList.remove("fa-square");
                        icon.classList.add("fa-check-square");
                    } else {
                        icon.classList.remove("fa-check-square");
                        icon.classList.add("fa-square");
                    }
                }
            });
        }

        return menu;
    };

    return headerMenu;
}